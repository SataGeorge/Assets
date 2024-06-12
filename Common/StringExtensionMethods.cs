namespace Common
{
    public static class StringExtensionMethods
    {
        public static string TrimSafe(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Trim();
        }
    }
}
