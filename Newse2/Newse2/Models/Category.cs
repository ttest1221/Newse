using System;
using System.Collections.Generic;

namespace Newse2.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
