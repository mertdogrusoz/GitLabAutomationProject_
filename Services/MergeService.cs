using EntityLayer;
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

        public async Task<List<Branches>> MergeRequest()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_accessToken);
                var apiUrl = $"{_apiUrl}/mergeRequest";
                
                
            }


            return null;
        }
        public async Task<Branches> getBranch()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var apiUrl = $"{_apiUrl}/mergeRequest";


            }



            return null;
        }
       

       
    }

   
}
