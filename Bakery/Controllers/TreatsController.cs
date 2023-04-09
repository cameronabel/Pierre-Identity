using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
  public ActionResult AddFlavor(int id)
  {
    Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
    ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
    return View(thisTreat);
  }

  [HttpPost]
  public ActionResult AddTreat(Treat treat, int flavorId)
  {
#nullable enable
    FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.FlavorId == flavorId && join.TreatId == treat.TreatId));
#nullable disable
    if (joinEntity == null && flavorId != 0)
    {
      _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treat.TreatId, FlavorId = flavorId });
      _db.SaveChanges();
    }
    return RedirectToAction("Details", new { id = treat.TreatId });
  }
}