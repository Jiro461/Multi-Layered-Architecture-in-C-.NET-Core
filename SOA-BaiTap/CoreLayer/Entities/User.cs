namespace SOA_BaiTap.CoreLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        // Quan hệ 1-nhiều với Review
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        // Quan hệ 1-nhiều với Rating
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
