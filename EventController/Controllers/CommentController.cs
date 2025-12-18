using EventController.Models.DAO.Implements;
using EventController.Models.Entity;
using EventController.Models.ViewModels;
using EventController.Util;
using EventController.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EventController.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentDAO _commentDAO;
        private readonly EventDAO _eventDAO;
        private readonly UserDAO _userDAO;
        private readonly RegistrationDAO _registrationDAO;
        private readonly IHubContext<CommentHub> _commentHub;

        public CommentController(CommentDAO commentDAO, EventDAO eventDAO, UserDAO userDAO, RegistrationDAO registrationDAO, IHubContext<CommentHub> commentHub)
        {
            _commentDAO = commentDAO;
            _eventDAO = eventDAO;
            _userDAO = userDAO;
            _registrationDAO = registrationDAO;
            _commentHub = commentHub;
        }

        [HttpGet]
        public IActionResult GetComments(int eventId)
        {
            try
            {
                var comments = _commentDAO.GetCommentsByEventId(eventId);
                var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
                int? currentUserId = null;
                if (currentUser != null)
                {
                    var user = _userDAO.GetUserByEmail(currentUser.Email);
                    currentUserId = user?.UserID;
                }

                var commentViewModels = comments.Select(c => MapToViewModel(c, currentUserId)).ToList();
                
                return Json(new { success = true, comments = commentViewModels });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error loading comments: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int eventId, string commentText, int? parentCommentId = null)
        {
            try
            {
                var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "You must be logged in to comment." });
                }

                var user = _userDAO.GetUserByEmail(currentUser.Email);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                if (string.IsNullOrWhiteSpace(commentText))
                {
                    return Json(new { success = false, message = "Comment text cannot be empty." });
                }

                if (commentText.Length > 2000)
                {
                    return Json(new { success = false, message = "Comment text cannot exceed 2000 characters." });
                }

                var evt = _eventDAO.GetEventById(eventId);
                if (evt == null)
                {
                    return Json(new { success = false, message = "Event not found." });
                }

                // Check if user is registered and has paid for the event
                if (!IsUserParticipant(user.UserID, eventId))
                {
                    return Json(new { success = false, message = "Only registered participants who have paid can comment on this event." });
                }

                var comment = new Comment
                {
                    EventID = eventId,
                    UserID = user.UserID,
                    CommentText = commentText,
                    ParentCommentID = parentCommentId,
                    CreatedAt = DateTime.Now
                };

                var success = _commentDAO.AddComment(comment);
                if (success)
                {
                    var addedComment = _commentDAO.GetCommentById(comment.CommentID);
                    var viewModel = MapToViewModel(addedComment, user.UserID);
                    
                    // Broadcast new comment to all users viewing this event
                    await _commentHub.Clients.Group($"event_{eventId}").SendAsync("ReceiveComment", viewModel);
                    
                    return Json(new { success = true, comment = viewModel });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to add comment." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error adding comment: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int commentId, string commentText)
        {
            try
            {
                var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "You must be logged in." });
                }

                var user = _userDAO.GetUserByEmail(currentUser.Email);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                if (string.IsNullOrWhiteSpace(commentText))
                {
                    return Json(new { success = false, message = "Comment text cannot be empty." });
                }

                if (commentText.Length > 2000)
                {
                    return Json(new { success = false, message = "Comment text cannot exceed 2000 characters." });
                }

                var comment = _commentDAO.GetCommentById(commentId);
                if (comment == null)
                {
                    return Json(new { success = false, message = "Comment not found." });
                }

                if (comment.UserID != user.UserID)
                {
                    return Json(new { success = false, message = "You can only edit your own comments." });
                }

                var success = _commentDAO.UpdateComment(commentId, commentText);
                if (success)
                {
                    // Broadcast comment update to all users viewing this event
                    await _commentHub.Clients.Group($"event_{comment.EventID}").SendAsync("UpdateComment", new
                    {
                        commentId = commentId,
                        commentText = commentText,
                        updatedAt = DateTime.Now
                    });
                    
                    return Json(new { success = true, message = "Comment updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update comment." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating comment: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var currentUser = HttpContext.Session.GetObject<UserViewModel>("currentUser");
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "You must be logged in." });
                }

                var user = _userDAO.GetUserByEmail(currentUser.Email);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                var comment = _commentDAO.GetCommentById(commentId);
                if (comment == null)
                {
                    return Json(new { success = false, message = "Comment not found." });
                }

                // Allow deletion if user is the comment owner or admin (RoleID = 1)
                if (comment.UserID != user.UserID && user.RoleID != 1)
                {
                    return Json(new { success = false, message = "You can only delete your own comments." });
                }

                var success = _commentDAO.DeleteComment(commentId);
                if (success)
                {
                    // Broadcast comment deletion to all users viewing this event
                    await _commentHub.Clients.Group($"event_{comment.EventID}").SendAsync("DeleteComment", commentId);
                    
                    return Json(new { success = true, message = "Comment deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete comment." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting comment: " + ex.Message });
            }
        }

        private bool IsUserParticipant(int userId, int eventId)
        {
            // Check if user is registered for the event with "Success" status (paid)
            return _registrationDAO.IsUserPaidParticipant(userId, eventId);
        }

        private CommentViewModel MapToViewModel(Comment comment, int? currentUserId)
        {
            return new CommentViewModel
            {
                CommentID = comment.CommentID,
                UserID = comment.UserID,
                UserName = comment.User?.FullName ?? "Unknown User",
                ProfileImage = comment.User?.ProfileImage ?? "/img/avartars/default-avatar.png",
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                ParentCommentID = comment.ParentCommentID,
                CanEdit = currentUserId.HasValue && comment.UserID == currentUserId.Value,
                CanDelete = currentUserId.HasValue && comment.UserID == currentUserId.Value,
                Replies = comment.Replies?.Select(r => MapToViewModel(r, currentUserId)).ToList() ?? new List<CommentViewModel>()
            };
        }
    }
}
