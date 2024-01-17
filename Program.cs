using Blazored.SessionStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using InformationManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);
string ConnectionString = "Data Source=InfoManagementSystemDb.db";


// Add services to the container.
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    // Replace "your_connection_string" and "your_database_name" with your actual MongoDB connection details
    return new MongoDbContext("mongodb://host.docker.internal:27018", "InfoManagementSystemDb");
});

builder.Services.AddDbContext<SqlDbContext>((serviceProvider, options) =>
{
    options.UseSqlite(ConnectionString);
});

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
