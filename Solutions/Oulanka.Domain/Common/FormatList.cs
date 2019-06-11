using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace Oulanka.Domain.Common
{
    public class FormatList
    {
        private readonly NameValueCollection _items;

        public FormatList()
        {
            _items = new NameValueCollection();
        }

        public static string Format(string input, string name, string text)
        {
            return SafeRegexReplace(input, FormatName(name), text);
        }

        public void Add(string name, string value)
        {
            _items.Add(FormatName(name), value);
        }

        public string Format(string text)
        {
            if (_items.Count == 0)
                return text;

            foreach (var key in _items.AllKeys)
            {
                var value = _items[key];
                if (value != null)
                {
                    text = SafeRegexReplace(text, key, value);
                }
            }

            return text;
        }

        private static string FormatName(string name)
        {
            return $@"\[{name}\]";
        }

        private static string SafeRegexReplace(string input, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var output = new StringBuilder();
            var lastEndIndex = 0;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var match = regex.Match(input);

            while (!string.IsNullOrEmpty(match.Value))
            {
                if (lastEndIndex != match.Index)
                {
                    // add whatever text was between the tags
                    output.Append(input.Substring(lastEndIndex, match.Index - lastEndIndex));
                }

                // perform replacement
                output.Append(replacement);

                // get the next match
                lastEndIndex = match.Index + match.Length;
                match = regex.Match(input, lastEndIndex);
            }

            // add whatever text exists after the last tag
            if (lastEndIndex < input.Length)
            {
                output.Append(input.Substring(lastEndIndex));
            }

            return output.ToString();
        }

    }
}