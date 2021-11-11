using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.Models
{
    public class ClassModel
    {
        public string nombre { get; set; }
        // userID - nomProf
        public Dictionary<string, string> professors { get; set; }
        // userID - nomAlumno
        public Dictionary<string, string> students { get; set; }
    }
}
