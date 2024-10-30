using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ActionItem
    {
        public string action { get; set; } // create, delete, move, update, chmod
        public string file_path { get; set; } // Dosya yolu
        public string previous_path { get; set; } // Taşınan dosyanın eski yolu (sadece move için)
        public string content { get; set; } // Dosya içeriği (create ve update için)
        public string encoding { get; set; } // text veya base64 (isteğe bağlı)
        public string last_commit_id { get; set; } // Son bilinen dosya commit ID'si (isteğe bağlı)
        public bool? execute_filemode { get; set; } // chmod için
    }

}
