
using EventController.Models.Entity;

namespace EventController.Models.DAO.Interfaces
{
    public interface IEmailVerificationTokenDAO
    {
        public Task<EmailVerificationToken> GenerateTokenAsync(int userId);
        public Task<EmailVerificationToken> GetValidTokenAsync(string token, int userId);
        public Task MarkTokenAsUsedAsync(EmailVerificationToken token);

    }
}
