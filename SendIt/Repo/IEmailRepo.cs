namespace SendIt.Repo
{
    public interface IEmailRepo
    {
        Task SendEmail(string receptor,string subject,string body);
    }
}
