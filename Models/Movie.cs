using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace OutseraApiTest.Models
{
    public class Movie
    {
        public int Year { get; set; }
        public string? Title { get; set; }
        public string? Studios { get; set; }
        public string? Producers { get; set; }
        public string? Winner { get; set; }
        [NotMapped]
        public bool IsWinner { get {  return Winner != null && Winner.ToLower() == "yes"; } }
    }

    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Map(m => m.Year).Name("year"); 
            Map(m => m.Title).Name("title");
            Map(m => m.Studios).Name("studios");
            Map(m => m.Producers).Name("producers");
            Map(m => m.Winner).Name("winner");
        }
    }
}
