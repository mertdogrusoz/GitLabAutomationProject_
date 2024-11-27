using EntityLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace Services
{
    public class MyService
    {
        private readonly string _accessToken;

       
        public MyService(IOptions<GitLabSettings> gitLabSettings)
        {
            _accessToken = gitLabSettings.Value.AccessToken;
        }

        public string GetAccessToken()
        {
            return _accessToken;
        }
    }

    public class BranchService
    {
       
        private readonly string _apiUrl = "http://localhost:8080/api/v4";
        private readonly MyService _myService;


        public BranchService(MyService myService)
        {
            _myService = myService;
        }


        public async Task<List<Branches>> getBranch(int id)
        {
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/branches";
                var response = await client.GetAsync(apiUrl);
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Branches>>(json);
  
                }
                else
                {
                    Console.WriteLine($"Api isteği başarısız oldu:  {response.StatusCode}");
                    return null;
                }


            }
           
        }
        public async Task<Branches> GetBranchByBranchName(int id, string branchName)
        {
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/branches/{branchName}";
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Branches>(json);

                }
                else
                {
                    Console.WriteLine($"Api isteği başarısız oldu :{response.StatusCode}");
                    return null;
                }


            }

        }
        public async Task<Branches> CreateBranchesAsync(int id, string branchName, string refName)
        {
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/{id}/repository/branches";

                var requestContent = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("branch", branchName),
                     new KeyValuePair<string, string>("ref", refName),
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
        public async Task<Branches> CreateBranch(int id,string branchName,string refName)
        {
            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
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
        public async Task<Branches> DeleteBranch(int id,string branchName)
        {

            using (var client = new HttpClient())
            {
                var _accessToken = _myService.GetAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/projects/:id/repository/merged_branches";
                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("branch",branchName)

                });

                var response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                  
                    if(json == null)
                    {
                        Console.WriteLine("json verisi boş");
                    }

                    return JsonConvert.DeserializeObject<Branches>(json);


                }
                else
                {
                    Console.WriteLine($"Api isteği başarısız oldu " + response.StatusCode);
                    return null;
                }


            }

        }
    }
}
