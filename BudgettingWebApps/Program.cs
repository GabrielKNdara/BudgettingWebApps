using BudgettingWebApps.Components;
using BudgettingWebApps.Configuration;
using BudgettingWebApps.Reposiotories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddFastEndpoints(); //added fastendpoint

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(5);//move to 30 if need be
        options.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var pgConnectionString = builder.Configuration.GetConnectionString("DefaultPgConnection");
var connectionFactory = new PsSqlDbConnectionFactory(pgConnectionString);
builder.Services.AddSingleton<IPsSqlDbConnectionFactory>(provider => connectionFactory);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IincomeRepository, IncomeRepository>();
builder.Services.AddSingleton<IincomeStatuRepository,IncomeStatuRepository>();
builder.Services.AddSingleton<IExpenseRepository, ExpenseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();//
app.UseAuthorization();//

app.UseStaticFiles();
app.UseAntiforgery();

app.UseFastEndpoints(); //Added fastendpoint 

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
