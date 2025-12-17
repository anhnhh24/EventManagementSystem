using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class CommentDAO
    {
        private readonly DBContext _context;

        public CommentDAO(DBContext context)
        {
            _context = context;
        }

        public List<Comment> GetCommentsByEventId(int eventId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Replies)
                    .ThenInclude(r => r.User)
                .Where(c => c.EventID == eventId 
                    && c.ParentCommentID == null 
                    && !c.IsDeleted 
                    && c.User.Status == "Active")
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public List<Comment> GetRepliesByCommentId(int commentId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.ParentCommentID == commentId 
                    && !c.IsDeleted 
                    && c.User.Status == "Active")
                .OrderBy(c => c.CreatedAt)
                .ToList();
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Event)
                .FirstOrDefault(c => c.CommentID == commentId && !c.IsDeleted);
        }

        public bool AddComment(Comment comment)
        {
            try
            {
                comment.CreatedAt = DateTime.Now;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateComment(int commentId, string newText)
        {
            try
            {
                var comment = _context.Comments.FirstOrDefault(c => c.CommentID == commentId && !c.IsDeleted);
                if (comment == null)
                    return false;

                comment.CommentText = newText;
                comment.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteComment(int commentId)
        {
            try
            {
                var comment = _context.Comments.FirstOrDefault(c => c.CommentID == commentId);
                if (comment == null)
                    return false;

                comment.IsDeleted = true;
                comment.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCommentCountByEventId(int eventId)
        {
            return _context.Comments
                .Count(c => c.EventID == eventId && !c.IsDeleted);
        }

        public bool IsCommentOwner(int commentId, int userId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.CommentID == commentId);
            return comment != null && comment.UserID == userId;
        }
    }
}
