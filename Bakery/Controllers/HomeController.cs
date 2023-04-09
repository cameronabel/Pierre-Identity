using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
  public ActionResult Index()
  {
    return View();
  }
}