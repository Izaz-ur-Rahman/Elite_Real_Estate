using EliteRealEstate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        // Add other JsonSerializerOptions as needed
    });

// Register dependencies
builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Route for blog details using slug
    endpoints.MapControllerRoute(
        name: "blogDetails",
        pattern: "blog/{blogname}",
        defaults: new { controller = "Blog", action = "BlogDetails" });

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
