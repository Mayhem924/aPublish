using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace aPublish
{
    public partial class Page
    {
        [JsonProperty("page")]
        public long CurrentPage { get; set; }

        [JsonProperty("posts")]
        public List<Post> Posts { get; set; }

        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; }
    }

    public class Post
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("createdDate")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

    public partial class Page
    {
        public static Page FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Page>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this Page self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new JsonDateConverter { }
            },
        };
    }
}