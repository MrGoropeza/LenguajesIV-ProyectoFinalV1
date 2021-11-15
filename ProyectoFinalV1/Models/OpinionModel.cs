using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.Models
{
    public class OpinionModel
    {
        public int movieID { get; set; }
        public string opinionTXT { get; set; }
        public string username { get; set; }
        public DateTime fechaOpinion { get; set; }
        public string id{ get; set; }
        public string FechaOpinionTxt { get; set; }
        public string NombreUsuarioTxt { get; set; }
    }
}
