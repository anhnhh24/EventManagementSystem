namespace EventController.Models.ViewModels
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public int? ParentCommentID { get; set; }
        public List<CommentViewModel> Replies { get; set; } = new List<CommentViewModel>();
    }
}
