using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.Models
{
    public class OpinionModel
    {
        public string ID { get; set; }
        public string movieID { get; set; }
        public double stars { get; set; }
        public string opinionTXT { get; set; }
    }
}
