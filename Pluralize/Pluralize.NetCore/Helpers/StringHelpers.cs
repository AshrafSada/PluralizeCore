using System.Globalization;

namespace Pluralize.Core.Helpers
{
    public static class StringHelpers
    {
        public static string ToTitle(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word);
        }
    }
}