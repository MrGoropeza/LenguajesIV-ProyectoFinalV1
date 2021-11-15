using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.Models
{
    public class UserModel
    {
        public string email { get; set; }
        public string username { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string edad { get; set; }
        public Dictionary<string, string> friends { get; set; }
        public Dictionary<string, string> likes { get; set; }
        public Dictionary<string, string> Opiniones { get; set; }
        public Dictionary<string, string> clases { get; set; }
    }
}
