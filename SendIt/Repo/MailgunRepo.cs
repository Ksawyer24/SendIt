using SendIt.Dto;
using System.Text;

namespace SendIt.Repo
{
    public class MailgunRepo
    {
        private readonly string _apiKey;
        private readonly string _domain;
        private readonly HttpClient _httpClient;

        public MailgunRepo(string apiKey, string domain)
        {
            _apiKey = apiKey;
            _domain = domain;
            _httpClient = new HttpClient();
        }

        public async Task SendEmailAsync(MailgunRequest mailgunRequest)
        {
            var requestUri = $"https://api.mailgun.net/v3/{_domain}/messages";

            var requestData = new MultipartFormDataContent
        {
            { new StringContent(mailgunRequest.From), "from" },
            { new StringContent(mailgunRequest.To), "to" },
            { new StringContent(mailgunRequest.Subject), "subject" },
            { new StringContent(mailgunRequest.Text), "text" },
            { new StringContent(mailgunRequest.Html), "html" }
        };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Headers =
            {
                Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_apiKey}")))
            },
                Content = requestData
            };


            try
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email sent successfully");
                }
                else
                {
                    Console.WriteLine($"Failed to send email. Response: {responseContent}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }
    }
}
