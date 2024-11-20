using SendIt.Dto;

namespace SendIt.Repo
{
    public interface IEmailRepo
    {
        Task SendEmailAsync(EmailDto request);
    }
}
