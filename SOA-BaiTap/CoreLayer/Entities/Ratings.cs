namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Ratings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieSeriesId {  get; set; }
        public Movie MoviesSeries { get; set; }
    }
}
