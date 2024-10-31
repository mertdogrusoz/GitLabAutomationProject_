using EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MergeService
    {

        private readonly string _accessToken = "glpat-nMJKFjgP4BQfpJyWz4xH";
        private readonly string _apiUrl = "http://localhost/api/v4";

        public async Task<List<MergeRequest>> GetMergeRequest(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<MergeRequest>>(json) ?? new List<MergeRequest>();
                }

                return new List<MergeRequest>();
            }
        }

        public async Task<List<MergeRequest>> CreateMerge(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<MergeRequest>>(json);




                }
                return null;
            }
        }
    }
}
