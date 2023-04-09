using System.ComponentModel.DataAnnotations;

namespace Bakery.Models;

public class Flavor
{
  public int FlavorId { get; set; }
  [Required(ErrorMessage = "The flavor's name can't be empty")]
  public string Name { get; set; }
  public List<FlavorTreat> JoinEntities { get; }
}