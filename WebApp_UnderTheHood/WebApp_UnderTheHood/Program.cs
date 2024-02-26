using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;
using WebApp_UnderTheHood.Entities.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add for cookie authentication and encryption
builder.Services
    .AddAuthentication(Constants.TokenScheme)
    .AddCookie(Constants.TokenScheme, options =>
    {
        options.Cookie.Name = Constants.TokenScheme;
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
    });

builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy(Constants.HRDeptPolicy, policy =>
        {
            policy.RequireClaim(Constants.DepartMentClaimKey, Constants.HRDepartMentClaimValue);
        });
        options.AddPolicy(Constants.CoOrdinatorsPolicy, policy =>
        {
            policy.Requirements.Add(new CoOrdinatorRequirement(6));
        });
    });

builder.Services.AddScoped<IAuthorizationHandler, CoOrdinatorRequirementHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Add for deserialization & decryption cookie from request
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
