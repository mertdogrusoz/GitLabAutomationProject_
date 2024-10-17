using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class NuGetPackageUpdateRequest
    {
        public string ProjectName { get; set; }
        public string PackageId { get; set; }
        public string Version { get; set; }
    }

}
