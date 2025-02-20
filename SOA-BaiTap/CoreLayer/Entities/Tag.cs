namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Quan hệ nhiều-nhiều với Movie
        public ICollection<MovieSeriesTag> MovieSeriesTags { get; set; } = new HashSet<MovieSeriesTag>();
    }
}
