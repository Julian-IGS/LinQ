using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using FindFilm.DataSources;
using FindFilm.DataSources.Collections;
using Newtonsoft.Json.Linq;

namespace FindFilm
{
    class Program
    {

        static void Main(string[] args)
        {
            // retrieve movie data
            var allData = ListMoviesData.ListMovies;

            // ask user
            Console.WriteLine("\r\n   __ __    ____                                            __          __    _             ___                                 _       ___ \r\n  / // /__ / / /__    ___ ________   __ ____ _____  __ __  / /__  ___  / /__ (_)__  ___ _  / _/__  ____  ___ _  __ _  ___ _  __(_)__   /__ \\\r\n / _  / -_) / / _ \\  / _ `/ __/ -_) / // / // / _ \\/ // / / / _ \\/ _ \\/  '_// / _ \\/ _ `/ / _/ _ \\/ __/ / _ `/ /  ' \\/ _ \\ |/ / / -_)   /__/\r\n/_//_/\\__/_/_/\\___/  \\_,_/_/  \\__/  \\_,_/\\_, /\\___/\\_,_/ /_/\\___/\\___/_/\\_\\/_/_//_/\\_, / /_/ \\___/_/    \\_,_/ /_/_/_/\\___/___/_/\\__/   (_)  \r\n                                        /___/                                     /___/                                                     \r\n");
            Console.WriteLine("What do you prefer:");
            Console.WriteLine("1- Search for a particular movie");
            Console.WriteLine("2- Download the entire list in an XML file");
            Console.Write("Your choice: ");
            // read user response
            string response = Console.ReadLine();

            switch (response)
            {
                // if user chooses to search for a movie
                case "1":
                    Search(allData);
                    break;
                // if user chooses to download the entire list
                case "2":
                    DownloadAll(allData);
                    break;
                // if user chooses an invalid option
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void Search(List<DataSources.Collections.Movie> allData)
        {
            // ask user for search criteria
            Console.Clear();
            Console.WriteLine("\r\n  _      ____        __                                ___ \r\n | | /| / / /  ___ _/ /_  ___ ____ ___  _______ ___   /__ \\\r\n | |/ |/ / _ \\/ _ `/ __/ / _ `/ -_) _ \\/ __/ -_|_-<    /__/\r\n |__/|__/_//_/\\_,_/\\__/  \\_, /\\__/_//_/_/  \\__/___/   (_)  \r\n                        /___/                              \r\n");
            Console.WriteLine("- Adventure");
            Console.WriteLine("- Musical");
            Console.WriteLine("- War");
            Console.WriteLine("- Drama");
            Console.WriteLine("- Science Fiction");
            Console.WriteLine("- Crime");
            Console.WriteLine("- Noir");
            Console.WriteLine("- Comedy");
            Console.WriteLine("- Romance");
            Console.WriteLine("- Western");
            Console.WriteLine("- Animated");
            Console.WriteLine("- Biography");
            Console.WriteLine("- Documentary");
            Console.WriteLine("- Fantasy");
            Console.WriteLine("- Horror");
            Console.WriteLine("- All");
            Console.Write("Your choice: ");
            string genres = Console.ReadLine();

            // ask user if they want to add another genre only if he didn't choose All
            string genres2 = string.Empty;
            if (genres != "All")
            {
                Console.WriteLine("\r\n   ___             __  __                                     ___ \r\n  / _ | ___  ___  / /_/ /  ___ ____  ___ ____ ___  _______   /__ \\\r\n / __ |/ _ \\/ _ \\/ __/ _ \\/ -_) __/ / _ `/ -_) _ \\/ __/ -_)   /__/\r\n/_/ |_/_//_/\\___/\\__/_//_/\\__/_/    \\_, /\\__/_//_/_/  \\__/   (_)  \r\n                                   /___/                          \r\n");
                Console.WriteLine("Otherwise, say no");
                Console.Write("Your choice: ");
                genres2 = Console.ReadLine();
                if (genres2.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                {
                    genres2 = string.Empty;
                }
            }

            // ask user for cast criteria
            Console.Clear();
            Console.WriteLine("\r\n   ____             _ ____                    __    ___ \r\n  / __/__  ___ ____(_) _(_)___  _______ ____ / /_  /__ \\\r\n _\\ \\/ _ \\/ -_) __/ / _/ / __/ / __/ _ `(_-</ __/   /__/\r\n/___/ .__/\\__/\\__/_/_//_/\\__/  \\__/\\_,_/___/\\__/   (_)  \r\n   /_/                                                  \r\n");
            Console.WriteLine("or say no?");
            Console.WriteLine("ex : Clark Gable");
            Console.Write("Your choice: ");
            string cast = Console.ReadLine();
            if (cast.Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                cast = string.Empty;
            }

            // ask user for minimum release year
            Console.Clear();
            Console.WriteLine("\r\n   __  ____      _                                             ___          __                \r\n  /  |/  (_)__  (_)_ _  __ ____ _    __ _____ ___ _____  ___  / _/ _______ / /__ ___ ____ ___ \r\n / /|_/ / / _ \\/ /  ' \\/ // /  ' \\  / // / -_) _ `/ __/ / _ \\/ _/ / __/ -_) / -_) _ `(_-</ -_)\r\n/_/  /_/_/_//_/_/_/_/_/\\_,_/_/_/_/  \\_, /\\__/\\_,_/_/    \\___/_/  /_/  \\__/_/\\__/\\_,_/___/\\__/ \r\n                                   /___/                                                      \r\n");
            Console.WriteLine("or none?");
            Console.WriteLine("ex : 1972 or write 0 if you don't care");
            Console.Write("Your choice: ");
            string minDate = Console.ReadLine();
            if (minDate == "0")
            {
                minDate = string.Empty;
            }

            // ask user for maximum release year
            Console.Clear();
            Console.WriteLine("\r\n   __  ___           _                                             ___          __                \r\n  /  |/  /__ ___ __ (_)_ _  __ ____ _    __ _____ ___ _____  ___  / _/ _______ / /__ ___ ____ ___ \r\n / /|_/ / _ `/\\ \\ // /  ' \\/ // /  ' \\  / // / -_) _ `/ __/ / _ \\/ _/ / __/ -_) / -_) _ `(_-</ -_)\r\n/_/  /_/\\_,_//_\\_\\/_/_/_/_/\\_,_/_/_/_/  \\_, /\\__/\\_,_/_/    \\___/_/  /_/  \\__/_/\\__/\\_,_/___/\\__/ \r\n                                       /___/                                                      \r\n");
            Console.WriteLine("or none?");
            Console.WriteLine("ex : 1985 or write 0 if you don't care");
            Console.Write("Your choice: ");
            string maxDate = Console.ReadLine();
            if (maxDate == "0")
            {
                maxDate = string.Empty;
            }

            var searchResult = from movie in allData
                               where ((genres == "All" || string.Join(", ", movie.Genres).Contains(genres, StringComparison.InvariantCultureIgnoreCase)) &&
                                   (string.IsNullOrWhiteSpace(genres2) || genres == "All" || string.Join(", ", movie.Genres).Contains(genres2, StringComparison.InvariantCultureIgnoreCase)) &&
                                   (string.IsNullOrWhiteSpace(cast) || string.Join(", ", movie.Cast).Contains(cast, StringComparison.InvariantCultureIgnoreCase)) &&
                                   (string.IsNullOrWhiteSpace(minDate) || (!string.IsNullOrWhiteSpace(minDate) && movie.Year >= int.Parse(minDate))) &&
                                   (string.IsNullOrWhiteSpace(maxDate) || (!string.IsNullOrWhiteSpace(maxDate) && movie.Year <= int.Parse(maxDate))))
                               select movie;

            if (!searchResult.Any())
            {
                Console.WriteLine("No movies found matching your search criteria.");
                return;
            }

            // Sort by date or by title
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Choose the sorting method:");
            Console.WriteLine("1- By date");
            Console.WriteLine("2- By title");
            Console.Write("Your choice: ");
            string sortChoice = Console.ReadLine();

            // Sorting order
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Choose the sorting order:");
            Console.WriteLine("1- Ascending");
            Console.WriteLine("2- Descending");
            Console.Write("Your choice: ");
            string orderChoice = Console.ReadLine();



            bool sortByDate = sortChoice == "1";
            bool isAscending = orderChoice == "1";

            IEnumerable<Movie> sortedMovies;

            if (sortByDate)
            {
                if (isAscending)
                {
                    sortedMovies = searchResult.OrderBy(m => m.Year);
                }
                else
                {
                    sortedMovies = searchResult.OrderByDescending(m => m.Year);
                }
            }
            else
            {
                if (isAscending)
                {
                    sortedMovies = searchResult.OrderBy(m => m.Title);
                }
                else
                {
                    sortedMovies = searchResult.OrderByDescending(m => m.Title);
                }
            }

            // Display the sorted movies
            Console.WriteLine("The following movies match your criteria:");
            foreach (var movie in sortedMovies)
            {
                Console.WriteLine($"Title: {movie.Title}, Year: {movie.Year}, Cast: {string.Join(", ", movie.Cast)}");
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Do you want to download the list ? Answer yes or no");
            Console.Write("Your choice: ");
            string Answer = Console.ReadLine();
            if (Answer == "yes")
            {
                DownloadByCriteria(searchResult);
            }
        }

        static void DownloadAll(List<DataSources.Collections.Movie> allData)
        {
            var newXMLFile = new XElement("Root",
                from movie in allData
                select new XElement("Movie",
                    new XElement("Title", movie.Title),
                    new XElement("Year", movie.Year),
                    new XElement("Cast", string.Join(", ", movie.Cast)),
                    new XElement("Genres", string.Join(", ", movie.Genres))
                )
            );

            newXMLFile.Save("AllMovies.xml");

            Console.WriteLine("XML file has been saved as AllMovies.xml.");
        }
        static void DownloadByCriteria(IEnumerable<DataSources.Collections.Movie> searchResult)
        {
            var newXMLFile = new XElement("Root",
                from movie in searchResult
                select new XElement("Movie",
                    new XElement("Title", movie.Title),
                    new XElement("Year", movie.Year),
                    new XElement("Cast", string.Join(", ", movie.Cast))
                )
            );

            newXMLFile.Save("Movies.xml");

            Console.WriteLine("XML file has been saved as Movies.xml.");
        }

    }
}