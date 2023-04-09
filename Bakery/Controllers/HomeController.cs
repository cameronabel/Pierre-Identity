using Microsoft.AspNetCore.Mvc;

using Bakery.Models;

namespace Bakery.Controllers;
public class HomeController : Controller
{
  private readonly BakeryContext _db;

  public HomeController(BakeryContext db)
  {
    _db = db;
  }

  [HttpGet("/")]
  public async Task<ActionResult> Index()
  {
    Dictionary<string, object[]> model = new Dictionary<string, object[]>();
    Flavor[] flavors = _db.Flavors.ToArray();
    model.Add("flavors", flavors);
    Treat[] treats = _db.Treats.ToArray();
    model.Add("treats", treats);

    return View(model);
  }
}