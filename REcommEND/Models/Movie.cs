using System;

namespace REcommEND.Models
{
    public class Movie
    {

        public string Title { get; set; }
        public int Year { get; set; }
        public DateTime Released { get; set; }
        public string Plot { get; set; }
        public decimal IMDBRating { get; set; }
        public int IMDBVotesNumber { get; set; }
        public Uri Poster { get; set; }
    }
}
