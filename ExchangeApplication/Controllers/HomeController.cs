using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Threading.Tasks;
using ExchangeApplication.Utilities;

namespace ExchangeApplication.Controllers
{
    public class HomeController : Controller
    {
        ISaveData _saveData;
        IGetJeson _getJeson;
        IGetUserId _getUserId;
        IBuyMoney _buyMoney;
        ISellMoney _sellMoney;

        public HomeController(ISaveData saveData, IGetJeson getJeson, IGetUserId getUserId, IBuyMoney buyMoney, ISellMoney sellMoney)
        {
            _saveData = saveData;
            _getJeson = getJeson;
            _getUserId = getUserId;
            _buyMoney = buyMoney;
            _sellMoney = sellMoney;
        }

        ApplicationDbContext db = new ApplicationDbContext();
        

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ApplicationUser GetUserId(ApplicationDbContext db)
        {
            var id = User.Identity.GetUserId();
            var us = db.Users.Find(id);
            return us;
        }

        [Authorize]
        public ActionResult About()
        {
            
            var arr = _getJeson.GetJson();
            var us = _getUserId.GetUserID(db);

            ViewBag.USDN = arr.items[0].name;
            ViewBag.EURN = arr.items[1].name;
            ViewBag.CHFN = arr.items[2].name;
            ViewBag.RUBN = arr.items[3].name;
            ViewBag.CZKN = arr.items[4].name;
            ViewBag.GBPN = arr.items[5].name;

            ViewBag.USDP = arr.items[0].purchasePrice;
            ViewBag.EURP = arr.items[1].purchasePrice;
            ViewBag.CHFP = arr.items[2].purchasePrice;
            ViewBag.RUBP = arr.items[3].purchasePrice;
            ViewBag.CZKP = arr.items[4].purchasePrice;
            ViewBag.GBPP = arr.items[5].purchasePrice;

            ViewBag.USDS = arr.items[0].sellPrice;
            ViewBag.EURS = arr.items[1].sellPrice;
            ViewBag.CHFS = arr.items[2].sellPrice;
            ViewBag.RUBS = arr.items[3].sellPrice;
            ViewBag.CZKS = arr.items[4].sellPrice;
            ViewBag.GBPS = arr.items[5].sellPrice;

            ViewBag.USDA = arr.items[0].averagePrice;
            ViewBag.EURA = arr.items[1].averagePrice;
            ViewBag.CHFA = arr.items[2].averagePrice;
            ViewBag.RUBA = arr.items[3].averagePrice;
            ViewBag.CZKA = arr.items[4].averagePrice;
            ViewBag.GBPA = arr.items[5].averagePrice;

            ViewBag.USDU = arr.items[0].unit;
            ViewBag.EURU = arr.items[1].unit;
            ViewBag.CHFU = arr.items[2].unit;
            ViewBag.RUBU = arr.items[3].unit;
            ViewBag.CZKU = arr.items[4].unit;
            ViewBag.GBPU = arr.items[5].unit;

            ViewBag.USDM = arr.items[0].averagePrice * us.info.USD;
            ViewBag.EURM = arr.items[1].averagePrice * us.info.EUR;
            ViewBag.CHFM = arr.items[2].averagePrice * us.info.CHF;
            ViewBag.RUBM = (arr.items[3].averagePrice * us.info.RUB) / 100;
            ViewBag.CZKM = (arr.items[4].averagePrice * us.info.CZK) / 100;
            ViewBag.GBPM = arr.items[5].averagePrice * us.info.GBP;

            return View(us);
        }

        [HttpGet]
        public ActionResult Sell(string id, string currency)
        {
            var us = db.Users.Find(id);
            ViewBag.Message1 = currency;
            return View(us);
        }

        [HttpGet]
        public ActionResult Buy(string id, string currency)
        {
            var us = db.Users.Find(id);
            ViewBag.Message1 = currency;
            return View(us);
        }

        [HttpPost]
        public ActionResult BuyEUR(int? numberIdUser, int amount, String currencyName)
        {
            return (_buyMoney.BuyEUR(numberIdUser, amount, currencyName, db));
        }
 

        [HttpPost]
        public ActionResult SellEUR(int? numberIdUser, int amount, String currencyName)
        {
            return (_sellMoney.SellEUR(numberIdUser, amount, currencyName, db));
        }
              
    }
}