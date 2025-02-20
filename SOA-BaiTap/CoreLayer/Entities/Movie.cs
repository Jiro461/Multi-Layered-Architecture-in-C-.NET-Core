namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public ICollection<MovieSeriesTag> MovieSeriesTags { get; set; } = new HashSet<MovieSeriesTag>();
    }
}
