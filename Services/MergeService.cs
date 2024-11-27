using EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;



namespace Services
{

    public class MergeService
    {

        private readonly MyService _myService;
        private readonly string _apiUrl = "http://localhost:8080/api/v4";

        public MergeService(MyService myService)
        {
            _myService = myService;
        }

        public async Task<List<MergeRequest>> GetMergeRequest(int id)
        {
           
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests?state=opened";
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
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests";


                var mergeRequestData = new
                {
                    source_branch = sourceBranch,
                    target_branch = targetBranch,
                    title = title
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(mergeRequestData), Encoding.UTF8, "application/json");


                var response = await client.PostAsync(apiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MergeRequest>(json);
                }

                return null;
            }
        }

        public async Task<MergeRequest> MergeAMergeRequest(int id, int mergeRequestIid, string mergeCommitMessage = "Merged successfully", bool shouldRemoveSourceBranch = true)
        {
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var apiUrl = $"{_apiUrl}/projects/{id}/merge_requests/{mergeRequestIid}/merge";

                var requestData = new
                {
                    merge_commit_message = mergeCommitMessage,
                    should_remove_source_branch = shouldRemoveSourceBranch,
                    squash = false 
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Merge işlemi başarısız: {response.StatusCode} - {errorMessage}");
                }

                var result = JsonConvert.DeserializeObject<MergeRequest>(await response.Content.ReadAsStringAsync());

                return result;
            }
        }


    } 
}
