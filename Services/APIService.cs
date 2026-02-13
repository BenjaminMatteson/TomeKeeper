using TomeKeeper.Models;
using System.Text.Json;

namespace TomeKeeper.Services
{
    internal class APIService : IAPIService
    {
        // Reuse a single HttpClient for the app lifetime
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://www.dnd5eapi.co/")
        };

        private readonly HttpClient _localHttpClient;

        public APIService(HttpClient httpClient)
        {
            _localHttpClient = httpClient;
        }

        /// <summary>
        /// Retrieves the spells list from the local 2024 D&D 5e spell data.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>List of spells (index, name, url).</returns>
        public async Task<IList<SpellListItem>> GetSpellsList(CancellationToken cancellationToken = default)
        {
            // Load from local JSON file (merged 2014 + 2024 spells)
            using var response = await _localHttpClient.GetAsync("data/dnd_all_spells.json", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var payload = await JsonSerializer.DeserializeAsync<Local2024SpellsResponse>(stream, options, cancellationToken).ConfigureAwait(false);

            if (payload == null)
            {
                throw new InvalidOperationException("Failed to deserialize spells list response.");
            }

            var spells = new List<SpellListItem>();
            foreach (var spell in payload.Results)
            {
                var school = spell.School?.Name ?? "Unknown";
                var descPreview = spell.Desc.Any() ? spell.Desc[0] : "";
                // Limit preview to 80 characters
                if (descPreview.Length > 80)
                {
                    descPreview = descPreview.Substring(0, 77) + "...";
                }

                spells.Add(new SpellListItem(spell.Index, spell.Name, (short)spell.Level, spell.Url)
                {
                    School = school,
                    DescPreview = descPreview
                });
            }

            return spells;
        }

        public async Task<SpellDetails> GetSpellDetails(string spellIndex, CancellationToken cancellationToken = default)
        {
            // Load from local JSON file (merged 2014 + 2024 spells)
            using var response = await _localHttpClient.GetAsync("data/dnd_all_spells.json", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var payload = await JsonSerializer.DeserializeAsync<Local2024SpellsResponse>(stream, options, cancellationToken).ConfigureAwait(false);
            if (payload == null)
            {
                throw new InvalidOperationException("Failed to deserialize spell list.");
            }

            var spellDetailsResult = payload.Results.FirstOrDefault(s => s.Index == spellIndex);
            if (spellDetailsResult == null)
            {
                throw new InvalidOperationException($"Spell with index '{spellIndex}' not found.");
            }

            return spellDetailsResult;
        }

        // Models for the local 2024 spells JSON file
        private sealed record Local2024SpellsResponse(int Count, List<SpellDetails> Results);
    }
}
