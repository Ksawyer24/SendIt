namespace SendIt.Repo
{
    public interface IEmailRepo
    {
        Task SendEmail(string provider,string receptor,string subject,string body);
    }
}
