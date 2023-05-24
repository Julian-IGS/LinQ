using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindFilm.DataSources.Collections
{
    public static class ListMoviesData
    {
        public static List<Movie> ListMovies { get; private set; }


        static ListMoviesData()
        {
            try
            {
                string json = File.ReadAllText("./DataSources/Json/Movies.json");
                JArray jsonArray = JArray.Parse(json);
                ListMovies = jsonArray.SelectMany(jObject => jObject["AllMovies"].ToObject<List<Movie>>()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading movies: " + ex.Message);
                throw;
            }
        }
    }
}
