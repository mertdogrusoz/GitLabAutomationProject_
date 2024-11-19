
using EntityLayer;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;

public class GitLabService
{
    private readonly string _accessToken = "glpat-VzAHu_nzzdbQoCsrBNMV"; // GitLab erişim token'ı
    private readonly string _apiUrl = "http://localhost:8080/api/v4/groups";

    public async Task<List<GitLabGroup>> GetGroups()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await client.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GitLabGroup>>(json);
            }

            return null;
        }
    }

    public async Task<List<GitLabProject>> GetProjectByGroup(int id)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var apiUrl = $"{_apiUrl}/{id}/projects";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var projects = JsonConvert.DeserializeObject<List<GitLabProject>>(json);


                foreach (var project in projects)
                {
                    Console.WriteLine($"Proje Adı: {project.name}, Proje ID: {project.id}");
                }

                return projects;
            }
            else
            {

                Console.WriteLine($"API isteği başarısız oldu. StatusCode: {response.StatusCode}");
                return null;
            }
        }
    }
    public bool UpdatePackageVersion(string projectFilePath, Dictionary<string, string> packageUpdates)
    {
        try
        {
            if (!File.Exists(projectFilePath))
            {
                Console.WriteLine($"Proje dosyası bulunamadı: {projectFilePath}");
                return false;
            }

            var xmlDoc = XDocument.Load(projectFilePath);
            bool isUpdated = false;

            foreach (var packageReference in xmlDoc.Descendants("PackageReference"))
            {
                var packageId = packageReference.Attribute("Include")?.Value;

                if (packageId != null && packageUpdates.ContainsKey(packageId))
                {
                    var versionAttribute = packageReference.Attribute("Version");
                    if (versionAttribute != null)
                    {
                        versionAttribute.Value = packageUpdates[packageId];
                        isUpdated = true;
                    }
                }
            }

            if (isUpdated)
            {
                xmlDoc.Save(projectFilePath);
                return true;
            }
            else
            {
                Console.WriteLine("Hiçbir paket güncellenmedi.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu: {ex.Message}");
            return false;
        }
    }
  

}
