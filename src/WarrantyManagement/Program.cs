using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Models.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WarrantyManagement.Services.Auth;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(opts => {
    opts.Conventions.AuthorizePage("/admin");
    
});

builder.Services.AddHttpsRedirection(opts =>{
    opts.HttpsPort = 44350;
});
builder.Services.AddDbContext<warrantyContext>(opts =>{
    opts.UseNpgsql(builder.Configuration["db-key"]);
});
builder.Services.AddAuthentication(opts => {
    // options goes here
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //opts.DefaultForbidScheme = "forbidScheme";
    //opts.AddScheme<CustomAuthenticationHandler>("forbidScheme", "Handle forbidden");
}
).AddJwtBearer(o => {
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters(){
        // JWT Payload goes here
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSession();
builder.Services.AddScoped<IAuthHandler, AuthHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// authorization using tokens
app.Use(async (context, next) => {
    var token = context.Session.GetString("jwt");
    if (!string.IsNullOrEmpty(token)){
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});

app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();

app.Run();
