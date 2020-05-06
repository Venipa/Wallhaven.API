using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wallhaven.API.Models
{
    public partial class WallhavenPaginatedData
    {
        public WallhavenPaginatedData()
        {
        }
        [JsonProperty("data")]
        public List<WallhavenData> Data { get; set; }

        [JsonProperty("meta")]
        public WallhavenPaginatedMeta Meta { get; set; }
    }

    public partial class WallhavenData
    {
        public WallhavenData()
        {
        }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("short_url")]
        public Uri ShortUrl { get; set; }

        [JsonProperty("views")]
        public int Views { get; set; }

        [JsonProperty("favorites")]
        public int Favorites { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("purity")]
        public WallhavenPurity Purity { get; set; }

        [JsonProperty("category")]
        public WallhavenCategory Category { get; set; }

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
        public FileType FileType { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("colors")]
        public string[] Colors { get; set; }

        [JsonProperty("path")]
        public Uri Path { get; set; }

        [JsonProperty("thumbs")]
        public WallhavenThumbs Thumbs { get; set; }
    }

    public partial class WallhavenThumbs
    {
        public WallhavenThumbs()
        {
        }
        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }

        [JsonProperty("small")]
        public Uri Small { get; set; }
    }

    public partial class WallhavenPaginatedMeta
    {
        public WallhavenPaginatedMeta()
        {
        }
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("query")]
        public object Query { get; set; }

        [JsonProperty("seed")]
        public object Seed { get; set; }
    }


    public enum FileType { ImageJpeg, ImagePng, Image };

    public partial class WallhavenPaginatedData
    {
        public static WallhavenPaginatedData FromJson(string json) => JsonConvert.DeserializeObject<WallhavenPaginatedData>(json, WallhavenPaginatedDataConverter.Settings);
    }

    internal static class WallhavenPaginatedDataConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            FloatParseHandling = FloatParseHandling.Decimal,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                CategoryConverter.Singleton,
                FileTypeConverter.Singleton,
                PurityConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CategoryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(WallhavenCategory) || t == typeof(WallhavenCategory?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "anime":
                    return WallhavenCategory.Anime;
                case "general":
                    return WallhavenCategory.General;
                case "people":
                    return WallhavenCategory.People;
            }
            throw new Exception("Cannot unmarshal type Category");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WallhavenCategory)untypedValue;
            switch (value)
            {
                case WallhavenCategory.Anime:
                    serializer.Serialize(writer, "anime");
                    return;
                case WallhavenCategory.General:
                    serializer.Serialize(writer, "general");
                    return;
                case WallhavenCategory.People:
                    serializer.Serialize(writer, "people");
                    return;
            }
            throw new Exception("Cannot marshal type Category");
        }

        public static readonly CategoryConverter Singleton = new CategoryConverter();
    }

    internal class FileTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FileType) || t == typeof(FileType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "image/jpeg":
                    return FileType.ImageJpeg;
                case "image/png":
                    return FileType.ImagePng;
                default:
                    return FileType.Image;
            }
            throw new Exception("Cannot unmarshal type FileType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FileType)untypedValue;
            switch (value)
            {
                case FileType.ImageJpeg:
                    serializer.Serialize(writer, "image/jpeg");
                    return;
                case FileType.ImagePng:
                    serializer.Serialize(writer, "image/png");
                    return;
                default:
                    serializer.Serialize(writer, null);
                    return;
            }
            throw new Exception("Cannot marshal type FileType");
        }

        public static readonly FileTypeConverter Singleton = new FileTypeConverter();
    }

    internal class PurityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(WallhavenPurity) || t == typeof(WallhavenPurity?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "sfw":
                    return WallhavenPurity.SFW;
                case "sketchy":
                    return WallhavenPurity.SKETCHY;
                case "nsfw":
                    return WallhavenPurity.NSFW;
            }
            throw new Exception("Cannot unmarshal type Purity");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WallhavenPurity)untypedValue;
            switch (value)
            {
                case WallhavenPurity.SFW:
                    serializer.Serialize(writer, "sfw");
                    return;
                case WallhavenPurity.SKETCHY:
                    serializer.Serialize(writer, "sketchy");
                    return;
                case WallhavenPurity.NSFW:
                    serializer.Serialize(writer, "nsfw");
                    return;
            }
            throw new Exception("Cannot marshal type Purity");
        }

        public static readonly PurityConverter Singleton = new PurityConverter();
    }
}

