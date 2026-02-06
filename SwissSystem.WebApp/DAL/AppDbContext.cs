using Microsoft.EntityFrameworkCore;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tournament> Tournaments { get; init; }
}