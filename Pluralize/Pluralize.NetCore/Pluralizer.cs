using System.Text.RegularExpressions;
using Pluralize.Core.Exceptions;
using Pluralize.Core.Helpers;
using Pluralize.Core.Rules;

namespace Pluralize.Core
{
    public class Pluralizer
    {
        private readonly Dictionary<string, string> _antonymsRules = AntonymsRules.GetAntonymsRules();
        private readonly Dictionary<string, string> _irregularPlurals = IrregularRules.GetIrregularPlurals();
        private readonly Dictionary<string, string> _irregularSingles = IrregularRules.GetIrregularSingulars();
        private readonly Dictionary<Regex, string> _pluralRules = PluralRules.GetRules();
        private readonly Dictionary<Regex, string> _singularRules = SingularRules.GetRules();
        private readonly List<string> _uncountable = UncountableRules.GetUncountable();
        private readonly Regex replacementRegex = new Regex("\\$(\\d{1,2})");

        public string Antonym(string word)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                var wordException = new NullOrEmptyWordException("Word cannot be null or empty");
                throw wordException;
            }
            if (_antonymsRules.ContainsKey(word.ToTitle()))
                return _antonymsRules[word.ToTitle()].ToLowerInvariant();

            return word;
        }

        public string Pluralize(string word)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                var wordException = new NullOrEmptyWordException("Word cannot be null or empty");
                throw wordException;
            }
            return Transform(word, _irregularSingles, _irregularPlurals, _pluralRules);
        }

        public string Singular(string word)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            {
                var wordException = new NullOrEmptyWordException("Word cannot be null or empty");
                throw wordException;
            }
            return Transform(word, _irregularPlurals, _irregularSingles, _singularRules);
        }

        private string ApplyRules(string token, string originalWord, Dictionary<Regex, string> rules)
        {
            // Empty string or doesn't need fixing.
            if (string.IsNullOrEmpty(token) || _uncountable.Contains(token))
                return RestoreCase(originalWord, token);

            var length = rules.Count;

            // Iterate over the sanitization rules and use the first one to match.
            while (length-- > 0)
            {
                var rule = rules.ElementAt(length);

                // If the rule passes, return the replacement.
                if (rule.Key.IsMatch(originalWord))
                {
                    var match = rule.Key.Match(originalWord);
                    var matchString = match.Groups[0].Value;
                    if (string.IsNullOrWhiteSpace(matchString))
                        return rule.Key.Replace(originalWord, GetReplaceMethod(originalWord[match.Index - 1].ToString(), rule.Value), 1);
                    return rule.Key.Replace(originalWord, GetReplaceMethod(matchString, rule.Value), 1);
                }
            }

            return originalWord;
        }

        private MatchEvaluator GetReplaceMethod(string originalWord, string replacement)
        {
            return match =>
            {
                return RestoreCase(originalWord, replacementRegex.Replace(replacement, m => match.Groups[Convert.ToInt32(m.Groups[1].Value)].Value));
            };
        }

        private string RestoreCase(string originalWord, string newWord)
        {
            // Tokens are an exact match.
            if (originalWord == newWord)
                return newWord;

            // Upper cased words. E.g. "HELLO".
            if (originalWord == originalWord.ToUpper())
                return newWord.ToUpper();

            // Title cased words. E.g. "Title".
            if (originalWord[0] == char.ToUpper(originalWord[0]))
                return char.ToUpper(newWord[0]) + newWord.Substring(1);

            // Lower cased words. E.g. "test".
            return newWord.ToLower();
        }

        private string Transform(string word, Dictionary<string, string> replacables, Dictionary<string, string> keepables, Dictionary<Regex, string> rules)
        {
            var token = word.ToLower();
            if (keepables.ContainsKey(token))
                return RestoreCase(word, token);
            if (replacables.ContainsKey(token))
                return RestoreCase(word, replacables[token]);
            return ApplyRules(token, word, rules);
        }
    }
}
