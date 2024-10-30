using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Commites
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
