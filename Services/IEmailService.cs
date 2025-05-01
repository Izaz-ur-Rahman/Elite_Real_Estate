using EliteRealEstate.Models;

namespace EliteRealEstate.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(ContactUs contact);
        Task QuotationEmailAsync(QuotationModel contact);
        Task<int> SendThankYouEmailAsync(string recipientName, string recipientEmail);
        Task<int> QuotationEmailResponse(string recipientName, string recipientEmail);
    }
}
