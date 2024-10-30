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
        public Commites commit { get; set; }
        public bool merged { get; set; }
        public bool @protected { get; set; }
        public bool developers_can_push { get; set; }
        public bool developers_can_merge { get; set; }
        public bool can_push { get; set; }
        public bool @default { get; set; }
        public string web_url { get; set; }
    }



}
