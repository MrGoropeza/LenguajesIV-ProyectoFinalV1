﻿using ProyectoFinalV1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        //private string region = "AR";
        //private string url = "api.themoviedb.org";
        private string language = "es-ES";

        TMDbClient client;

        public async Task<List<PeliculaModel>> getMovieFromSearch(string search, int pagina, int sizePoster, int sizeBackground)
        {
            List<PeliculaModel> resultados = new List<PeliculaModel>();
            SearchContainer<SearchMovie> request = await client.SearchMovieAsync(search,page: pagina,language:language);
            if (request.Results.Count() > 0 && pagina<=request.TotalPages)
            {
                foreach (SearchMovie r in request.Results)
                {
                    resultados.Add(new PeliculaModel()
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
                        imageUrl = getUrlFromPath(r.PosterPath, sizePoster),
                        backgroundUrl = getUrlFromPath(r.BackdropPath, sizeBackground),
                    });
                }
            }
            return resultados;
        }


        public async Task<List<PeliculaModel>> getEnCines(int sizePoster, int sizeBackground)
        {
            List<PeliculaModel> enCines = new List<PeliculaModel>();
            SearchContainerWithDates<SearchMovie> request = await client.GetMovieNowPlayingListAsync(language:language);
            foreach(SearchMovie r in request.Results)
            {
                enCines.Add(new PeliculaModel()
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
                    imageUrl = getUrlFromPath(r.PosterPath,sizePoster),
                    backgroundUrl = getUrlFromPath(r.BackdropPath,sizeBackground),
                });
            }
            return enCines;
        }
        public string getUrlFromPath(string path,int size)
        {
            return "https://image.tmdb.org/t/p/w"+size.ToString()+path;
        }

        public async Task<List<PeliculaModel>> getPopulares(int pagina, int sizePoster, int sizeBackground)
        {
            List<PeliculaModel> populares = new List<PeliculaModel>();
            SearchContainer<SearchMovie> request = await client.GetMoviePopularListAsync(language,pagina);
            foreach (SearchMovie r in request.Results)
            {
                populares.Add(new PeliculaModel()
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
                    imageUrl = getUrlFromPath(r.PosterPath, sizePoster),
                    backgroundUrl = getUrlFromPath(r.BackdropPath, sizeBackground),
                });
            }
            return populares;
        }

        public async Task<List<PeliculaModel>> getTopRated(int pagina, int sizePoster, int sizeBackground)
        {
            List<PeliculaModel> topRated = new List<PeliculaModel>();
            SearchContainer<SearchMovie> request = await client.GetMovieTopRatedListAsync(language, pagina);
            foreach (SearchMovie r in request.Results)
            {
                topRated.Add(new PeliculaModel()
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
                    imageUrl = getUrlFromPath(r.PosterPath, sizePoster),
                    backgroundUrl = getUrlFromPath(r.BackdropPath, sizeBackground),
                });
            }
            return topRated;
        }

        public PeliculasProvider()
        {
            client = new TMDbClient(this.apikey);
        }
    }
}
