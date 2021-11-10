using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace ProyectoFinalV1.Services
{
    public class PeliculasProvider
    {
        private string apikey = "ebdc806192995e9346956e65b6c70927";
        private string url = "api.themoviedb.org";
        private string language = "es-ES";

        TMDbClient client;
        async Task<List<Pelicula>> getEnCines()
        {
            List<Pelicula> enCines = new List<Pelicula>();
            SearchContainerWithDates<SearchMovie> request = await client.GetMovieNowPlayingListAsync(language);
            foreach(SearchMovie r in request.Results)
            {
                enCines.Add(new Pelicula()
                {
                    movieID = r.Id,
                    title = r.Title,
                    overview = r.Overview,
                    isForAdults = r.Adult,
                    video = r.Video,
                    backgroundPath = r.BackdropPath,
                    posterPath = r.PosterPath,
                    genreIDs = r.GenreIds,
                    originalLanguage = r.OriginalLanguage,
                    originalTitle = r.OriginalTitle,
                    popularity = r.Popularity,
                    voteAverage = r.VoteAverage,
                    voteCount = r.VoteCount,
                    releaseDate = r.ReleaseDate.Value,
                });
            }
            return enCines;
        }

        async Task<List<Pelicula>> getPopulares()
        {
            List<Pelicula> populares = new List<Pelicula>();
            SearchContainer<SearchMovie> request = await client.GetMoviePopularListAsync(language);
            foreach (SearchMovie r in request.Results)
            {
                populares.Add(new Pelicula()
                {
                    movieID = r.Id,
                    title = r.Title,
                    overview = r.Overview,
                    isForAdults = r.Adult,
                    video = r.Video,
                    backgroundPath = r.BackdropPath,
                    posterPath = r.PosterPath,
                    genreIDs = r.GenreIds,
                    originalLanguage = r.OriginalLanguage,
                    originalTitle = r.OriginalTitle,
                    popularity = r.Popularity,
                    voteAverage = r.VoteAverage,
                    voteCount = r.VoteCount,
                    releaseDate = r.ReleaseDate.Value,
                });
            }
            return populares;
        }

        public PeliculasProvider()
        {
            client = new TMDbClient(this.apikey);
        }
    }
}
