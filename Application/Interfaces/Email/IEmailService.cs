using Application.Dtos.Account;
using Application.Dtos.Email;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);

    }
}