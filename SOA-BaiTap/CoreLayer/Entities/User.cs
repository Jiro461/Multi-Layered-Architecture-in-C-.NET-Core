namespace SOA_BaiTap.CoreLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
