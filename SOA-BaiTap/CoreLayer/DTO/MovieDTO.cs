namespace SOA_BaiTap.CoreLayer.DTO
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
