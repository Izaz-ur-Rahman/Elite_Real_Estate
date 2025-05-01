using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EliteRealEstate.Models;
using EliteRealEstate.Services;
using Microsoft.Extensions.Configuration;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = new HttpClient(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            // Match the same options configured in AddJsonOptions
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task AddContactUs(ContactUs contact)
    {
        try
        {            
            var apiUrl = _configuration["ApiSettings:ContactUrl"];
            var jsonContent = JsonSerializer.Serialize(contact);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(apiUrl, content);
        }
        catch (Exception ex) 
        {
            return ;
        }        
    }

    public async Task AddQuotation(QuotationModel contact)
    {
        try
        {
            var apiUrl = _configuration["ApiSettings:QuotationUrl"];
            var jsonContent = JsonSerializer.Serialize(contact);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(apiUrl, content);
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public async Task<List<BlogDetail>> GetBlogListAsync(string endpoint)
    {
        var apiUrl = $"{_configuration["ApiSettings:BaseUrl"]}{endpoint}";
        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BlogDetail>>(jsonData);
        }

        return null;
    }

    public async Task<BlogPostModel> GetBlogBySlug(string Name)
    {
        
        var response = await _httpClient.GetAsync($"Blog/BlogDetailsbySlug?name={Name}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BlogPostModel>(jsonData);
        }

        return null;
    }

    public async Task<List<UserModal>> GetUserByEmpNoAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"General/GetUser");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserModal>>(responseData, _jsonOptions);
        }
        catch (HttpRequestException ex)
        {
            // Log error
            return null;
        }
    }

    public async Task<UserProfile> GetProfileAsync(int? id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"General/GetProfile?id={id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<UserProfile>(responseData, options);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error retrieving profile: {ex.Message}");
            return null;
        }
    }

    public async Task<PropertyListing> GetPropertyById(int Id)
    {

        var response = await _httpClient.GetAsync($"GetPropertybyId?id={Id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PropertyListing>(jsonData);
        }

        return null;
    }




}
