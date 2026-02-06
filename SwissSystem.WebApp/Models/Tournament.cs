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
    
    // public int? SemifinalsAPlayer1Id  { get; set; }
    // public int? SemifinalsAPlayer2Id  { get; set; }
    // public int? SemifinalsBPlayer1Id  { get; set; }
    // public int? SemifinalsBPlayer2Id  { get; set; }
    //
    // public int? FinalsPlayer1Id  { get; set; }
    // public int? FinalsPlayer2Id  { get; set; }
    //
    // public int? WinnerId  { get; set; }
    
    public virtual List<Player> Players { get; set; }
    
    public virtual List<Round> Rounds { get; set; }
    
    // [ForeignKey("SemifinalsAPlayer1Id")]
    // public virtual Player? SemifinalsAPlayer1 { get; set; }
    //
    // [ForeignKey("SemifinalsAPlayer2Id")]
    // public virtual Player? SemifinalsAPlayer2 { get; set; }
    //
    // [ForeignKey("SemifinalsBPlayer1Id")]
    // public virtual Player? SemifinalsBPlayer1 { get; set; }
    //
    // [ForeignKey("SemifinalsBPlayer2Id")]
    // public virtual Player? SemifinalsBPlayer2 { get; set; }
    //
    // [ForeignKey("FinalsPlayer1Id")]
    // public virtual Player? FinalsPlayer1 { get; set; }
    //
    // [ForeignKey("FinalsPlayer2Id")]
    // public virtual Player? FinalsPlayer2 { get; set; }
    //
    // [ForeignKey("WinnerId")]
    // public virtual Player? Winner { get; set; }
}