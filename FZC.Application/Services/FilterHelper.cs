namespace FZC.Application.Services
{
    public static class FilterHelper
    {
        public static string? GetStringFromFilter(Dictionary<string, object> filterObj, string key)
        {
            if (filterObj.TryGetValue(key, out var value) && value != null)
                return value.ToString();
            return null;
        }

        public static DateTime? GetDateFromFilter(Dictionary<string, object> filterObj, string key)
        {
            if (filterObj.TryGetValue(key, out var value) && DateTime.TryParse(value?.ToString(), out var date))
                return date;
            return null;
        }
    }
}