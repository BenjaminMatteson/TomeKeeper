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

        public APIService() { }

        /// <summary>
        /// Retrieves the spells list from the DnD 5e API.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>List of spells (index, name, url).</returns>
        public async Task<IList<SpellListItem>> GetSpellsList(CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.GetAsync("api/2014/spells", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var payload = await JsonSerializer.DeserializeAsync<SpellsListResponse>(stream, options, cancellationToken).ConfigureAwait(false);

            if (payload == null)
            {
                throw new InvalidOperationException("Failed to deserialize spells list response.");
            }

            var spells = new List<SpellListItem>();
            foreach (var spell in payload.Results)
            {
                spells.Add(new SpellListItem(spell.Index, spell.Name, spell.Level, spell.Url));
            }

            return spells;
        }

        public async Task<SpellDetails> GetSpellDetails(string spellIndex, CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.GetAsync($"api/2014/spells/{spellIndex}", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var spellDetailsResult = await JsonSerializer.DeserializeAsync<SpellDetails>(stream, options, cancellationToken).ConfigureAwait(false);
            if (spellDetailsResult == null)
            {
                throw new InvalidOperationException($"Failed to deserialize spell details for index: {spellIndex}.");
            }

            return spellDetailsResult;
        }

        // Models for the API response
        private sealed record SpellsListResponse(int Count, List<SpellListItem> Results);
    }
}
