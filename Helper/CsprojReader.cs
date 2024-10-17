using System.Xml.Linq;
using System.Collections.Generic;
using EntityLayer;

public class CsprojReader
{
    public List<NuGetPackage> GetNuGetPackagesFromCsproj(string csprojPath)
    {
        var packages = new List<NuGetPackage>();

        
        var doc = XDocument.Load(csprojPath);
        var packageReferences = doc.Descendants("PackageReference");

        foreach (var package in packageReferences)
        {
            var id = package.Attribute("Include")?.Value;
            var version = package.Attribute("Version")?.Value;

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(version))
            {
                packages.Add(new NuGetPackage
                {
                    PackageId = id,
                    Version = version
                });
            }
        }

        return packages;
    }

}
