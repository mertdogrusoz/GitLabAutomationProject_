using EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BranchService
    {
        private readonly string _accessToken = "glpat-nMJKFjgP4BQfpJyWz4xH";
        private readonly string _apiUrl = "http://localhost/api/v4";
        
        public async Task<List<Branches>> getBranch(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/branches";
                var response = await client.GetAsync(apiUrl);
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject <List<Branches>>(json);

                    
                }
                else
                {
                    Console.WriteLine($"Api isteği başarısız oldu:  {response.StatusCode}");
                    return null;
                }


            }
          
           
        }
        public async Task<Branches> CreateBranch(int id,string branchName,string refName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/branches";

                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("branch",branchName),
                    new KeyValuePair<string, string>("ref",refName)

                });
              
                var response = await client.PostAsync(apiUrl, requestContent);

               
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Branches>(json);
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
