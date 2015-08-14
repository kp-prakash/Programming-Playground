using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    using MongoDB.Driver;
    using RealEstate.App_Start;
    using RealEstate.Rentals;

    public class RentalsController : Controller
    {
        public readonly RealEstateContext Context = new RealEstateContext();

        public ActionResult Index()
        {
            var rentals = Context.Rentals.Find(rental => true).ToListAsync();
            return View(rentals.Result);
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            var rental = new Rental(postRental);
            Context.Rentals.InsertOneAsync(rental);
            return RedirectToAction("Index");
        }
    }
}