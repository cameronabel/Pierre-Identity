using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


using Bakery.Models;

namespace Bakery.Controllers;

[Authorize]
public class FlavorsController : Controller
{
  private readonly BakeryContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public FlavorsController(BakeryContext db, UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
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

  [HttpPost]
  public async Task<ActionResult> Create(Flavor flavor)
  {
    if (!ModelState.IsValid)
    {
      return View(flavor);
    }
    else
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
  public ActionResult Details(int id)
  {
    Flavor thisFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
    return View(thisFlavor);
  }
  public ActionResult AddTreat(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
    ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
    return View(thisFlavor);
  }

  [HttpPost]
  public ActionResult AddTreat(Flavor flavor, int treatId)
  {
#nullable enable
    FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.TreatId == treatId && join.FlavorId == flavor.FlavorId));
#nullable disable
    if (joinEntity == null && treatId != 0)
    {
      _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
      _db.SaveChanges();
    }
    return RedirectToAction("Details", new { id = flavor.FlavorId });
  }
  [HttpPost]
  public ActionResult DeleteJoin(int joinId)
  {
    FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
    _db.FlavorTreats.Remove(joinEntry);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
  public ActionResult Edit(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
    return View(thisFlavor);
  }

  [HttpPost]
  public ActionResult Edit(Flavor flavor)
  {
    _db.Flavors.Update(flavor);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
  public ActionResult Delete(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
    return View(thisFlavor);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
    _db.Flavors.Remove(thisFlavor);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}