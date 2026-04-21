// public class Comment {
//     public int Id { get; set; }
//     public string Content { get; set; }
//     public int PostId { get; set; }
//     public int? ParentCommentId { get; set; } // Null if it's a top-level comment
//     public virtual Comment ParentComment { get; set; }
//     public virtual ICollection<Comment> Replies { get; set; }
// }
namespace TodoApi.BlogModel
{
    public class Comment
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }

        public virtual Comment? ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}