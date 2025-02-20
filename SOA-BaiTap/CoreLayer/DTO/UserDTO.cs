namespace SOA_BaiTap.CoreLayer.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }

    public class UserGetDTO : UserDTO
    {
        public int Id { get; set; }

    }
}
