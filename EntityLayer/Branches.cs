using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Branches
    {
        public string name { get; set; }
        public Commit commit { get; set; }
        public bool merged { get; set; }
        public bool @protected { get; set; }
        public bool developers_can_push { get; set; }
        public bool developers_can_merge { get; set; }
        public bool can_push { get; set; }
        public bool @default { get; set; }
        public string web_url { get; set; }
    }

    public class Commit
    {
        public string id { get; set; }
        public string short_id { get; set; }
        public DateTime created_at { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string author_name { get; set; }
        public string author_email { get; set; }
        public DateTime authored_date { get; set; }
        public string committer_name { get; set; }
        public string committer_email { get; set; }
        public DateTime committed_date { get; set; }
        public string web_url { get; set; }
    }

}
