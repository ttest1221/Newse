using System;
using System.Collections.Generic;

namespace Newse2.Models
{
    public partial class Commentary
    {
        public int Id { get; set; }
        public string? CommentaryContent { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime? DateOfWritten { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
