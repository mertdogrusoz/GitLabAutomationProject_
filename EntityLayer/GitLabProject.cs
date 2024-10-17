using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class GitLabProject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string description { get; set; }
        public string web_url { get; set; }
        public string default_branch { get; set; }
        public bool archived { get; set; }
        public string visibility { get; set; }
    }

}
