using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventController.Models.Entity
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public virtual Event Event { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(2000)]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int? ParentCommentID { get; set; }

        [ForeignKey("ParentCommentID")]
        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Replies { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
