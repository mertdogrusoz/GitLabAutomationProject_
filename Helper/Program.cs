using System;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

public class Project
{
    public string ProjectName { get; set; }
    public string PackageId { get; set; }
    public string Version { get; set; }
}

class Program
{
    static void Main()
    {
        string json = @"
        [
          {
            'projectName': 'class-library-1',
            'packageId': 'Microsoft.EntityFrameworkCore.Design',
            'version': '6.0.35'
          },
          {
            'projectName': 'module-1',
            'packageId': 'Microsoft.EntityFrameworkCore.Design',
            'version': '6.0.35'
          }
        ]";

        // JSON'u listeye deserialize et
        List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(json);

        // Version'ı değiştirmek için kullanıcı girdisi al
        Console.Write("Yeni versiyonu girin: ");
        string newVersion = Console.ReadLine();

        // Tüm projelerin versiyonunu güncelle
        foreach (var project in projects)
        {
            project.Version = newVersion;
        }

        // Güncellenmiş JSON'u serialize et
        string updatedJson = JsonConvert.SerializeObject(projects, Newtonsoft.Json.Formatting.Indented);

        // Güncellenmiş JSON'u ekrana yazdır
        Console.WriteLine("Güncellenmiş JSON:");
        Console.WriteLine(updatedJson);
    }
}
