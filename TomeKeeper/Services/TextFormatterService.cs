using System.Text.RegularExpressions;

namespace TomeKeeper.Services
{
    internal sealed class TextFormatterService : ITextFormatterService
    {
        public string FormatBoldWords(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            // Regex to find text enclosed in exactly three asterisks and replace with strong tags
            // (.*?) captures the content between the delimiters (non-greedy)
            string pattern = @"\*\*\*(.*?)\*\*\*";
            string replacement = @"<strong>$1</strong>";

            // Replace all occurrences
            string result = Regex.Replace(input, pattern, replacement);

            return result;
        }
    }
}
