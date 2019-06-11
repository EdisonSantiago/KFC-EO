using System;
using System.Text.RegularExpressions;

namespace Oulanka.Domain.Common
{
    public static class Utilities
    {
        public static DateTime GetDateTime()
        {

            var myLocalDate = DateTime.UtcNow.AddHours(-5);

            return myLocalDate;
        }

        public static DateTime MinDate()
        {
            return System.Convert.ToDateTime("01/01/1753");
        }

        public static string StripHtmlXmlTags(string value)
        {
            return Regex.Replace(value, "<[^>]+>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public static string StripScriptTags(string value)
        {
            // Perform RegEx
            value = Regex.Replace(value, "<script((.|\n)*?)</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var cleanText = Regex.Replace(value, "\"javascript:", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return cleanText;
        }

        public static string RemoveSpacesAndSpecialsChars(string value)
        {
            value = value.Replace(" ", "-");
            value = value.Replace(".", "-");
            value = value.Replace(",", string.Empty);
            value = value.Replace(";", string.Empty);
            value = value.Replace(":", string.Empty);
            value = value.Replace("?", string.Empty);
            value = value.Replace("¿", string.Empty);
            value = value.Replace("@", string.Empty);
            value = value.Replace("#", string.Empty);
            value = value.Replace("%", string.Empty);
            value = value.Replace("&", string.Empty);
            value = value.Replace("*", string.Empty);
            value = value.Replace("(", string.Empty);
            value = value.Replace(")", string.Empty);
            value = value.Replace("!", string.Empty);
            value = value.Replace("/", string.Empty);
            value = value.Replace("\\", string.Empty);
            value = value.Replace("|", string.Empty);
            value = value.Replace("+", string.Empty);
            value = value.Replace("{", string.Empty);
            value = value.Replace("}", string.Empty);
            value = value.Replace("[", string.Empty);
            value = value.Replace("]", string.Empty);

            value = value.Replace("á", "a");
            value = value.Replace("Á", "A");
            value = value.Replace("é", "e");
            value = value.Replace("É", "E");
            value = value.Replace("í", "i");
            value = value.Replace("Í", "I");
            value = value.Replace("ó", "o");
            value = value.Replace("Ó", "O");
            value = value.Replace("ú", "u");
            value = value.Replace("Ú", "u");

            value = value.Replace("ñ", "n");
            value = value.Replace("Ñ", "N");

            return value.TrimEnd();
        } 
    }
}