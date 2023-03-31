using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Experience { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Municipality { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
