using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using EliteRealEstate.Models;
using EliteRealEstate.Services;
using System.Net.Http;
using Newtonsoft.Json;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public EmailService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory =  httpClientFactory;
    }

    public async Task SendEmailAsync(ContactUs contact)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        var smtpClient = new SmtpClient(smtpSettings["Host"])
        {
            Port = int.Parse(smtpSettings["Port"]),
            Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
            EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings["FromEmail"]),
            Subject = "New Contact Us Submission",
            Body = $"Name: {contact.FirstName}\n" +
                   $"Phone: {contact.PhoneNumber}\n" +
                   $"Email: {contact.Email}\n" +
                   $"Message: {contact.Message}",
            IsBodyHtml = false
        };

        mailMessage.To.Add(smtpSettings["RecieverEmail"]);

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task<int> SendThankYouEmailAsync(string recipientName, string recipientEmail)
    {
        try
        {
            // Get SMTP settings
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string fromEmail = smtpSettings["FromEmail"];

            // Fetch template from API
            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            string apiUrl = $"{baseUrl}EmailTemplate/EmailTemplateList?id={2}";

            var emailTemplate = await GetEmailTemplateAsync(apiUrl);
            if (emailTemplate == null || string.IsNullOrEmpty(emailTemplate.Details))
                return 0;

            // Compose the email
            string subject = emailTemplate.Subject;
            string body = $"Dear {recipientName},<br><br>{emailTemplate.Details}";

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(recipientEmail);

            using var smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
            };

            await smtpClient.SendMailAsync(message);
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending Thank You email: {ex.Message}");
            return 0;
        }
    }

    private async Task<Cms_EmailTemplete> GetEmailTemplateAsync(string apiUrl)
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var client = new HttpClient(handler);
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var templates = JsonConvert.DeserializeObject<List<Cms_EmailTemplete>>(json);
                return templates?.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching email template: {ex.Message}");
        }
        return null;
    }




    public async Task QuotationEmailAsync(QuotationModel contact)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        var smtpClient = new SmtpClient(smtpSettings["Host"])
        {
            Port = int.Parse(smtpSettings["Port"]),
            Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
            EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings["FromEmail"]),
            Subject = "New Contact Us Submission",
            Body = $"Name: {contact.FullName}\n" +
                   $"Phone: {contact.PhoneNumber}\n" +
                   $"Email: {contact.Email}\n" +
                   $"Category: {contact.Category}\n" +
                   $"Budget Range: {contact.Budget}\n" +
                   $"Message: {contact.Message}",
            IsBodyHtml = false
        };

        mailMessage.To.Add(smtpSettings["RecieverEmail"]);

        await smtpClient.SendMailAsync(mailMessage);
    }


    public async Task<int> QuotationEmailResponse(string recipientName, string recipientEmail)
    {
        try
        {
            // Get SMTP settings
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string fromEmail = smtpSettings["FromEmail"];

            // Fetch template from API
            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            string apiUrl = $"{baseUrl}EmailTemplate/EmailTemplateList?id={1}";

            var emailTemplate = await GetEmailTemplateAsync(apiUrl);
            if (emailTemplate == null || string.IsNullOrEmpty(emailTemplate.Details))
                return 0;

            // Compose the email
            string subject = emailTemplate.Subject;
            string body = $"Dear {recipientName},<br><br>{emailTemplate.Details}";

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(recipientEmail);

            using var smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"])
            };

            await smtpClient.SendMailAsync(message);
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending Thank You email: {ex.Message}");
            return 0;
        }
    }




}


public class Cms_EmailTemplete
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? EntryDate { get; set; }
    public string Createdby { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string Details { get; set; }
}
