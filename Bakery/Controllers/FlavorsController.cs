using Microsoft.AspNetCore.Mvc;
using Bakery.Models;

namespace Bakery.Controllers;

public class FlavorsController : Controller
{
  private readonly BakeryContext _db;

  public FlavorsController(BakeryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Flavor> model = _db.Flavors.ToList();
    return View(model);
  }
  public ActionResult Create()
  {
    return View();
  }
}