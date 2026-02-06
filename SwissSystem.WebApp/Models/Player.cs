using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwissSystem.WebApp.Models;

public class Player
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public int TournamentId { get; set; }
    
    [ForeignKey("TournamentId")]
    public virtual Tournament Tournament { get; set; }
}