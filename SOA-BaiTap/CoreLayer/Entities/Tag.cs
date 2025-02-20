namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieSeriesTag> MovieSeriesTag { get; set; }
    }
}
