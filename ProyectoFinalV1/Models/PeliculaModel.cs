using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalV1.Models
{
    public class PeliculaModel
    {
        #region principales
            public int movieID { get; set; }
            public string title { get; set; }
            public DateTime releaseDate { get; set; }
            public string overview { get; set; }
        #endregion

        #region imagenes
            public string backgroundPath { get; set; }
            public string posterPath { get; set; }
            public string imageUrl { get; set; }
            public string backgroundUrl { get; set; }
        #endregion

        #region stats
        public double popularity { get; set; }
            public double voteAverage { get; set; }
            public double voteCount { get; set; }
        #endregion

        #region booleans
            public bool isForAdults { get; set; }
            public bool video { get; set; }
        #endregion

        public List<int> genreIDs { get; set; }
        public string originalLanguage { get; set; }
        public string originalTitle { get; set; }
    }
}
