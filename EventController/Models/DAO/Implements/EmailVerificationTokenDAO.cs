using EventController.Models.DAO.Interfaces;
using EventController.Models.Data.DBcontext;
using EventController.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventController.Models.DAO.Implements
{
    public class EmailVerificationTokenDAO : IEmailVerificationTokenDAO
    {
        private readonly DBContext _context;

        public EmailVerificationTokenDAO(DBContext context)
        {
            _context = context;
        }

        public async Task<EmailVerificationToken> GenerateTokenAsync(int userId)
        {
            var token = new EmailVerificationToken
            {
                UserID = userId,
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddHours(24),
                IsUsed = false
            };

            _context.EmailVerificationTokens.Add(token);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<EmailVerificationToken> GetValidTokenAsync(string token, int userId)
        {
            return await _context.EmailVerificationTokens
                .FirstOrDefaultAsync(t =>
                    t.Token == token &&
                    t.UserID == userId &&
                    !t.IsUsed &&
                    (!t.ExpiresAt.HasValue || t.ExpiresAt > DateTime.Now));
        }

        public async Task MarkTokenAsUsedAsync(EmailVerificationToken token)
        {
            token.IsUsed = true;
            _context.EmailVerificationTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
