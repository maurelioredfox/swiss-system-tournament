using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SwissSystem.WebApp.DAL;
using SwissSystem.WebApp.DAL.Repositories;
using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Services;
using SwissSystem.WebApp.Services.Interfaces;
using SwissSystem.WebApp;
using SwissSystem.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var dbConfig = builder.Configuration.GetSection(("Database")).Get<DbConfig>();

if (dbConfig == null)
{
    throw new Exception("Database configuration is invalid.");
}

builder.Services.AddMudServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConfig.ConnectionString)
);

builder.Services.AddTransient<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentService, TournamentMockService>();
builder.Services.AddScoped<IRoundService, RoundMockService>();
builder.Services.AddScoped<IScoresService, ScoresMockService>();
builder.Services.AddScoped<IFinalsService, FinalsMockService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();