using System.Globalization;
using System.Web;

namespace Frency.Helpers
{
    public static class StringHelper
    {
        public static string RandomString(int length = 7)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";//0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string ToTitleCase(this string title)
        {
            return title != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower()) : null;
        }
        public static string ToQueryString(this object data, string url)
        {
            var properties = data.GetType().GetProperties()
                            .Where(property => property.GetValue(data) != null)
                            .Select(property => $"{property.Name}={HttpUtility.UrlEncode(property.GetValue(data).ToString())}");
            url += "?" + string.Join("&", properties.ToArray());
            return url;
        }
    }
}
