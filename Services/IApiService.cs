using EliteRealEstate.Models;

namespace EliteRealEstate.Services
{
    public interface IApiService
    {
        Task AddContactUs(ContactUs contact);
        Task AddQuotation(QuotationModel contact);
        Task<List<BlogDetail>> GetBlogListAsync(string apiUrl);
        Task<BlogPostModel> GetBlogBySlug(string Name);
        Task<List<UserModal>> GetUserByEmpNoAsync();
        Task<UserProfile> GetProfileAsync(int? id);
        Task<PropertyListing> GetPropertyById(int Id);
    }
}
