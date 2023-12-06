using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using Newtonsoft.Json;
using Frency.Helpers;
using System.Dynamic;
using AngleSharp.Common;

namespace Frency.DataAccess.Extensions
{
    public static class ObjectExtension
    {
        public static async Task<IHtmlDocument> ParseHtml(this HttpResponseMessage response)
        {
            return new HtmlParser().ParseDocument(await response.Content.ReadAsStringAsync());
        }
        public static List<KeyValuePair<string, string>> AddKey(this List<KeyValuePair<string, string>> data, string key, string value)
        {
            data.Add(new KeyValuePair<string, string>(key, value));
            return data;
        }
        public static FormUrlEncodedContent ToFormData(this object data)
        {
            return new FormUrlEncodedContent(data.ToDictionary());
        }
        // public static List<KeyValuePair<string, string>> ToDictionary(this object data)
        // {
        //     string json = JsonConvert.SerializeObject(data);
        //     return JsonConvert.DeserializeObject<Dictionary<string, string>>(json).ToList();
        //     // return data.GetType().GetProperties()
        //     //     .Select(x => new KeyValuePair<string, string>(x .Name, x.GetValue(data)?.ToString())).ToList();
        // }
        // public static dynamic ToExpando(this object data)
        // {
        //     string json = JsonConvert.SerializeObject(data);
        //     return JsonConvert.DeserializeObject<ExpandoObject>(json);
        //     // var expando = new ExpandoObject() as IDictionary<string, object>;
        //     // foreach (var property in data.GetType().GetProperties())
        //     //     expando.Add(property.Name, property.GetValue(data));
        // }
    }
}
