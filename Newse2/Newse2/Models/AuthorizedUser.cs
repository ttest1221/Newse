namespace Newse2.Models
{
    public class AuthorizedUser
    {
        public int Id { get; set; } = 0;
        public string Nickname { get; set; }
        public string Role { get; set; }
        public int CurrentPost { get; set; }
        public int CurrentCategory { get; set; }
        public IndexViewModel IndexVM { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Commentary> Comments { get; set; }
    }
}
