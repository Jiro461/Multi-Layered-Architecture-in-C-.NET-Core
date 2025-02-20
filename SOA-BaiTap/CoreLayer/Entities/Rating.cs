namespace SOA_BaiTap.CoreLayer.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        // Liên kết với User
        public int UserId { get; set; }
        public User User { get; set; }

        // Liên kết với Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public decimal Value { get; set; }
    }
}
