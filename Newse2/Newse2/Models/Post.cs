using System;
using System.Collections.Generic;

namespace Newse2.Models
{
    public partial class Post
    {
        public Post()
        {
            Commentaries = new HashSet<Commentary>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PostContent { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }

        public virtual User? Author { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Commentary> Commentaries { get; set; }
    }
}
