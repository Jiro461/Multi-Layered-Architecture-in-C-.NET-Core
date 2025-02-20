namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
        public User User { get; set; }
        public Movie MoviesSeries { get; set; }
    }
}
