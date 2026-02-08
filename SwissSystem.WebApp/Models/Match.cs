using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwissSystem.WebApp.Models;

public class Match
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int RoundId { get; set; }
    
    public int Player1Id { get; set; }
    
    public bool Bye { get; set; }
    
    public int? Player2Id { get; set; }
    
    public int? WinnerId { get; set; }
    
    [ForeignKey("Player1Id")]
    public virtual Player Player1 { get; set; }
    
    [ForeignKey("Player2Id")]
    public virtual Player? Player2 { get; set; }
    
    [ForeignKey("WinnerId")]
    public virtual Player? Winner { get; set; }
    
    [ForeignKey("RoundId")]
    public virtual Round Round { get; set; }
}
