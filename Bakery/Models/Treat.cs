using System.ComponentModel.DataAnnotations;

namespace Bakery.Models;

public class Treat
{
  public int TreatId { get; set; }
  [Required(ErrorMessage = "The treat's name can't be empty")]
  public string Name { get; set; }
  public List<FlavorTreat> JoinEntities { get; }
}