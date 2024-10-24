using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {
        // BranchService nesnesini oluştur
        BranchService branchService = new BranchService();

        // Test değişkenleri
        string projectId = "123456";  
        string refBranch = "main";    
        string gitLabToken = "glpat-nMJKFjgP4BQfpJyWz4xH";  
        string csprojPath = @"C:\path\to\your\project.csproj";  

     
        var packageUpdates = new Dictionary<string, string>
        {
            { "Newtonsoft.Json", "13.0.2" }, 
            { "Microsoft.EntityFrameworkCore", "6.0.0" }
        };

      
        await branchService.AutomateGitLabProcess(projectId, refBranch, gitLabToken, csprojPath, packageUpdates);

        Console.WriteLine("Branch oluşturma ve commit işlemi tamamlandı.");
    }
}
