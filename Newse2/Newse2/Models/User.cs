using System;
using System.Collections.Generic;

namespace Newse2.Models
{
    public partial class User
    {
        public User()
        {
            Commentaries = new HashSet<Commentary>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? Nickname { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime? DateOfRegistration { get; set; }

        public virtual ICollection<Commentary> Commentaries { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
