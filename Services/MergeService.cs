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

        private readonly string _accessToken = "glpat-VzAHu_nzzdbQoCsrBNMV"; // GitLab erişim token'ı
        private readonly string _apiUrl = "http://localhost:8080/api/v4";

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


        public async Task<MergeRequest> CreateMerge(int id, string sourceBranch, string targetBranch, string title)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests";

             
                var mergeRequestData = new
                {
                    source_branch = sourceBranch,
                    target_branch = targetBranch,
                    title = title
                };

                // JSON içeriğini serileştirin
                var jsonContent = new StringContent(JsonConvert.SerializeObject(mergeRequestData), Encoding.UTF8, "application/json");

                // POST isteğini gönderin
                var response = await client.PostAsync(apiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MergeRequest>(json);
                }

                return null;
            }
        }
       


    }
}
