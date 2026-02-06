using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwissSystem.WebApp.Models;

public class Round
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int Number { get; set; }
    
    public int TournamentId { get; set; }
    
    [ForeignKey("TournamentId")]
    public virtual Tournament Tournament { get; set; }
    
    public virtual List<Match> Matches { get; set; }
}