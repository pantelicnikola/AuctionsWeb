using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AuctionsWeb.Models;
using Microsoft.AspNet.Identity;


namespace AuctionsWeb.Controllers
{
    [Authorize]
    public class UserFunctionalitiesController : Controller
    {
        // GET: UserFunctionalities
        public ActionResult CreateAuction()
        {
            ViewBag.Message = "Create Auction page.";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuction(CreateAuctionModel model)
        {
            var auction = new Auction {
                IdUser = User.Identity.GetUserId(),
                Name = model.AuctionName,
                Duration = model.Duration,
                Photo = model.PhotoURL,
                PriceStart = model.PriceStart,
                PriceNow = model.PriceStart,
                State = "READY",
                TimeCreate = System.DateTime.Now
            };

            var entity = new auctiondbEntities();
            entity.Auctions.Add(auction);
            await entity.SaveChangesAsync();

            
            return Redirect("/");

        }
    }
}