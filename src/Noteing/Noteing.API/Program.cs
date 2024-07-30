using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Noteing.API.Data;
using Noteing.API.Helpers;
using Noteing.API.Hubs;
using Noteing.API.Models;
using Noteing.API.Services;

var builder = WebApplication.CreateBuilder(args);

// This shouldn't be seen by anyone.
// Good that you guys are joining tonight! The secrets for today is *********************

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer(opt =>
{
    opt.UserInteraction.LoginUrl = "/login";
})
    .AddSigningCredential(CertificateHelper.LoadSigningCertificate(), SecurityAlgorithms.RsaSha256)
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.Configure<JwtBearerOptions>(
    IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
    options =>
    {
        options.Authority = "noteing";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireAudience = false,
            RequireExpirationTime = false,
        };
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(builder => builder.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<MailService>();
builder.Services.AddTransient<SummarizeService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();
app.UseStaticFiles();

app.UseDeveloperExceptionPage();
app.UseSwagger();

app.MapControllers();
app.MapHub<LiveUpdateHub>("/live-updates");

app.Run();
