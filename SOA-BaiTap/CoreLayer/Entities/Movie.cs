namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        // Quan hệ nhiều-nhiều với Tag thông qua MovieSeriesTag
        public ICollection<MovieSeriesTag> MovieSeriesTags { get; set; } = new HashSet<MovieSeriesTag>();

        // Quan hệ 1-nhiều với Review
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        // Quan hệ 1-nhiều với Rating
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
