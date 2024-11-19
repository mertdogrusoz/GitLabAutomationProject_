using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ActionItem
    {
        public string action { get; set; }
        public string file_path { get; set; }
        public string previous_path { get; set; }
        public string content { get; set; }
        public string encoding { get; set; }
        public string last_commit_id { get; set; }
        public bool execute_filemode { get; set; }
    }

}

