using System.Text.RegularExpressions;

namespace PucMinas.TCC.Utility.Extensions
{
    public static class RegexExtension
    {
        const string MaskNumebers = @"[^\d]";

        public static string RemoveCharactersExceptNumbers(this string value)
        {
            return Regex.Replace(value, MaskNumebers, string.Empty);
        }
    }
}
