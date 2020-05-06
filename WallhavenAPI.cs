using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wallhaven.API.Models;

namespace Wallhaven.API
{
    public class WallhavenAPI
    {
        public string Token { get; private set; }
        public bool AllowNSFW { get => Token != null; }
        private RestClient client;
        public WallhavenAPI(string token = null)
        {
            this.Token = token;
            this.client = this.getClient();
            if (token != null)
            {
                this.client.AddDefaultHeader("X-API-Key", token);
            }
        }

        private RestClient getClient()
        {
            return new RestClient("https://wallhaven.cc/api/v1");
        }

        private string getCategoryFilterByFlags(WallhavenCategory categories)
        {
            var s = string.Join("", new bool[] {
                categories.HasFlag(WallhavenCategory.General),
                categories.HasFlag(WallhavenCategory.Anime),
                categories.HasFlag(WallhavenCategory.People) }
            .Select(x => x ? 1 : 0).SelectMany(x => "".Concat(x.ToString())));
            return s;
        }
        private string getPurityFilterByFlags(WallhavenPurity purity)
        {
            var s = string.Join("", new bool[] {
                purity.HasFlag(WallhavenPurity.SFW),
                purity.HasFlag(WallhavenPurity.SKETCHY),
                purity.HasFlag(WallhavenPurity.NSFW) }
            .Select(x => x ? 1 : 0).SelectMany(x => "".Concat(x.ToString())));
            return s;
        }

        public async Task<WallhavenPaginatedData> search(string q, WallhavenCategory category = WallhavenCategory.General | WallhavenCategory.Anime, WallhavenPurity purity = WallhavenPurity.SFW | WallhavenPurity.SKETCHY, WallhavenSort sort = WallhavenSort.Relevance, WallhavenSortOrder order = WallhavenSortOrder.Descending, string[] tags = null)
        {
            var catFilter = this.getCategoryFilterByFlags(category);
            var purFilter = this.getPurityFilterByFlags(purity);
            return await this.client.ExecuteAsync(
                new RestRequest("search", Method.GET)
                .AddQueryParameter("q", q)
                .AddQueryParameter("purity", purFilter)
                .AddQueryParameter("categories", catFilter)
                .AddQueryParameter("sorting", sort.Value())
                .AddQueryParameter("order", order.Value())
                ).ContinueWith(x =>
                {
                    return WallhavenPaginatedData.FromJson(x.Result.Content);
                });
        }
        public async Task<WallhavenImageData> getById(string id)
        {
            return await this.client.ExecuteAsync(
                new RestRequest("w/{id}", Method.GET)
                .AddUrlSegment("id", id)
                ).ContinueWith(x =>
                {
                    return WallhavenImage.FromJson(x.Result.Content)?.Data;
                });
        }
    }
}
