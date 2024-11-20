namespace SendIt.Dto
{
    public class MailgunRequest
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
    }
}
