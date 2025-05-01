using EliteRealEstate.Models;
using EliteRealEstate.Services;
using Microsoft.AspNetCore.Mvc;

namespace EliteRealEstate.Controllers
{
    public class BlogController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IConfiguration _configuration;

        public BlogController(IApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }


        [Route("blogs")]
        public async Task<IActionResult> BlogList(int? id = 0)
        {
            try
            {
                var externalAppUrl = _configuration["ApiSettings:ExternalAppUrl"];
                ViewBag.ExternalAppUrl = externalAppUrl;

                var endpoint = id.HasValue ? $"Blog/BlogList?id={id}" : "Blog/BlogList";
                var blogList = await _apiService.GetBlogListAsync(endpoint);

                if (blogList != null)
                {
                    foreach (var blog in blogList)
                    {
                        var empNo = blog.Createdby;
                        var user = await _apiService.GetUserByEmpNoAsync();
                        var users = user.FirstOrDefault(x => x.EmpNo == empNo); // Assuming EmpNo is a string
                        if (users != null)
                        {
                            blog.UserName = users.Name;
                            blog.ProfileImage = users.Profileimage;
                        }
                    }
                    return View(blogList);
                }

                return View("Failed to retrieve blog list" );
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        public async Task<IActionResult> BlogDetails(string blogname)
        {
            var externalAppUrl = _configuration["ApiSettings:ExternalAppUrl"];
            ViewBag.ExternalAppUrl = externalAppUrl;

            var data = await _apiService.GetBlogBySlug(blogname); // Await the Task to get BlogPostModel

            var empNo = data.Createdby;

            var userList = await _apiService.GetUserByEmpNoAsync(); // Await the Task to get List<UserModal>

            var userdata = userList.FirstOrDefault(x => x.EmpNo == empNo);
            ViewBag.userdata = userdata;

            var userid = userdata?.id; // Safe access
            var profileData = await _apiService.GetProfileAsync(userid);
            ViewBag.profiledata = profileData;

            return View(data);
        }


       



    }
}
