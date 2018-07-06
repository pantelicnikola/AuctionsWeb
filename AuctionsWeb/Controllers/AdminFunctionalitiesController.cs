

using AuctionsWeb.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AuctionsWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminFunctionalitiesController : Controller
    {
        //List<Auction> list;


        // GET: UserFunctionalities
        [HttpGet]
        public ActionResult OpenAuctions(OpenAuctionsModel model)
        {
            model = new OpenAuctionsModel();
            auctiondbEntities entities = new auctiondbEntities();
            model.auctions = entities.Auctions.Where(x => x.State.Equals("READY")).ToList();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult OpenAuctions(OpenAuctionsModel model)
        //{
        //    auctiondbEntities entities = new auctiondbEntities();
        //    entities.Auctions.Remove(model.toAdd);
        //    entities.SaveChanges();
        //    return null;
        //}

        public ActionResult Add(int id, int duration)
        {

            auctiondbEntities context = new auctiondbEntities();
            System.DateTime timeOpen = System.DateTime.Now;
            System.DateTime timeEnd = timeOpen.AddSeconds(duration);
            Auction auction = new Auction { Id = id, TimeOpen = timeOpen, TimeEnd = timeEnd};
            auction.State = "OPEN";

            context.Auctions.Attach(auction);
             
            context.Entry<Auction>(auction).Property("State").IsModified = true;
            context.Entry<Auction>(auction).Property("TimeOpen").IsModified = true;
            context.Entry<Auction>(auction).Property("TimeEnd").IsModified = true;
            context.SaveChanges();
            return RedirectToAction("OpenAuctions");
        }
    }
}