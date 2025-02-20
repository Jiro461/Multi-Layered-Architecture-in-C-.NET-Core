namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Review
    {
        public int Id { get; set; }

        // Liên kết với User
        public int UserId { get; set; }
        public User User { get; set; }

        // Liên kết với Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
