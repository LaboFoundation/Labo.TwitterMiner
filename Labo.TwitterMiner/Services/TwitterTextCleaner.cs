namespace Labo.TwitterMiner.Services
{
    using System.Text.RegularExpressions;

    internal class TwitterTextCleaner : ITwitterTextCleaner
    {
        private static readonly Regex s_UrlRegex = new Regex(@"(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?", RegexOptions.Compiled);
        private static readonly Regex s_HashTagRegex = new Regex(@"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)", RegexOptions.Compiled);
        private static readonly Regex s_UsernameRegex = new Regex(@"(?:(?<=\s)|^)@(\w*[A-Za-z_]+\w*)", RegexOptions.Compiled);

        public string CleanText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            text = s_UrlRegex.Replace(text, string.Empty);
            text = s_HashTagRegex.Replace(text, string.Empty);
            text = s_UsernameRegex.Replace(text, string.Empty);
            return text.Trim();
        }
    }
}
