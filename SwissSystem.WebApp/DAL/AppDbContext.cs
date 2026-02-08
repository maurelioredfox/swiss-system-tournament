using Microsoft.EntityFrameworkCore;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tournament> Tournaments { get; init; }
    public DbSet<Round> Rounds { get; init; }
    public DbSet<Match> Matches { get; init; }
    public DbSet<FinalsBracket> FinalsBrackets { get; init; }
    public DbSet<Player> Players { get; init; }
    
}