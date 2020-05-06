using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wallhaven.API.Models
{

    public partial class WallhavenImage
    {
        [JsonProperty("data")]
        public WallhavenImageData Data { get; set; }
    }

    public partial class WallhavenImageData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("short_url")]
        public Uri ShortUrl { get; set; }

        [JsonProperty("uploader")]
        public WallhavenImageUploader Uploader { get; set; }

        [JsonProperty("views")]
        public int Views { get; set; }

        [JsonProperty("favorites")]
        public int Favorites { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("purity")]
        public WallhavenPurity Purity { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("dimension_x")]
        public int DimensionX { get; set; }

        [JsonProperty("dimension_y")]
        public int DimensionY { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("ratio")]
        public string Ratio { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }

        [JsonProperty("file_type")]
        public string FileType { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("colors")]
        public string[] Colors { get; set; }

        [JsonProperty("path")]
        public Uri Path { get; set; }

        [JsonProperty("thumbs")]
        public WallhavenImageThumbs Thumbs { get; set; }

        [JsonProperty("tags")]
        public WallhavenImageTag[] Tags { get; set; }
    }

    public partial class WallhavenImageTag
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("purity")]
        public WallhavenPurity Purity { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }

    public partial class WallhavenImageThumbs
    {
        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }

        [JsonProperty("small")]
        public Uri Small { get; set; }
    }

    public partial class WallhavenImageUploader
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("avatar")]
        public Dictionary<string, Uri> Avatar { get; set; }
    }


    public partial class WallhavenImage
    {
        public static WallhavenImage FromJson(string json) => JsonConvert.DeserializeObject<WallhavenImage>(json, WallhavenPaginatedDataConverter.Settings);
    }
}
