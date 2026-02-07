using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwissSystem.WebApp.Models;

public class Tournament
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TournamentId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public virtual List<Player> Players { get; set; }
    
    public virtual List<Round> Rounds { get; set; }
    
    public virtual FinalsBracket? FinalsBracket { get; set; }
}