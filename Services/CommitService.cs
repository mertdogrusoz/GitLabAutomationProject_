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
    public class CommitService
    {
        private readonly string _accessToken = "glpat-nMJKFjgP4BQfpJyWz4xH"; // GitLab erişim token'ı
        private readonly string _apiUrl = "http://localhost/api/v4";


        public async Task<List<Commites>> GetCommites(int id)
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/commits";
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Commites>>(json);



                }
                else
                {
                    Console.WriteLine($"Api isteği başarısız oldu {response.StatusCode}");
                    return null;

                }




            }


        }


        //       public async Task<CommitResponse> CreateCommit(int projectId, string branchName, string commitMessage, List<ActionItem> actions)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        //        var apiUrl = $"{_apiUrl}/projects/{projectId}/repository/commits";


        //        var payload = new
        //        {
        //            branch = branchName,
        //            commit_message = commitMessage,
        //            actions = actions
        //        };

        //        var jsonPayload = JsonConvert.SerializeObject(payload);
        //        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        //        var response = await client.PostAsync(apiUrl, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<CommitResponse>(json);
        //        }
        //        else
        //        {
        //            Console.WriteLine($"API isteği başarısız oldu: {response.StatusCode}");
        //            return null;
        //        }
        //    }
        //}

        public async Task<Commites> CreateCommitAsync(int projectId, string branchName, string commitMessage, List<ActionItem> actions)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{projectId}/repository/commits";

              
                var payload = new
                {
                    branch = branchName,
                    commit_message = commitMessage,
                    actions = actions 
                };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var requestContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Commites>(json);
                }
                else
                {
                    Console.WriteLine($"API isteği başarısız oldu: {response.StatusCode}");
                    return null;
                }
            }
        }





    }
}
