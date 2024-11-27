using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class MergeRequestDTO
    {
        public int Id { get; set; }
        public int MergeRequestIid { get; set; }
        public string MergeCommitMessage { get; set; } = "Merged successfully";
        public bool ShouldRemoveSourceBranch { get; set; } = true;
    }

}
