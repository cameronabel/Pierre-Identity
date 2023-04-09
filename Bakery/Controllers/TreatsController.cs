using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Bakery.Models;

namespace Bakery.Controllers;

public class TreatsController : Controller
{
  private readonly BakeryContext _db;

  public TreatsController(BakeryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Treat> model = _db.Treats.ToList();
    return View(model);
  }
  public ActionResult Create()
  {
    return View();
  }
  [HttpPost]
  public ActionResult Create(Treat treat)
  {
    if (!ModelState.IsValid)
    {
      return View(treat);
    }
    else
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
  public ActionResult Details(int id)
  {
    Treat thisTreat = _db.Treats
      .Include(treat => treat.JoinEntities)
      .ThenInclude(join => join.Flavor)
      .FirstOrDefault(treat => treat.TreatId == id);
    return View(thisTreat);
  }
}