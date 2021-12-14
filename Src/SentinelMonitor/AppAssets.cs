namespace SentinelMonitor
{
    public static class AppAssets
    {
        public static readonly string conStr = "Server=localhost; Port=3306; uid=jerhynh; pwd=Developer2002!; database=auth";

        public static string ToUpperFirstCharacter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}
