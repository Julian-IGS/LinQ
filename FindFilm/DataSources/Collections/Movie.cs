using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindFilm.DataSources.Collections
{
    public class Movie
    {
        public int Year { get; set; }
        public string Title { get; set; }

        public List<string> Cast { get; set; }

        public List<string> Genres { get; set; }
    }
}
