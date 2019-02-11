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
using System.Diagnostics;
using ExchangeApplication.ViewModels;

namespace ExchangeApplication.Controllers
{
    public class HomeController : Controller
    {
        ISaveData _saveData;
        IGetJeson _getJeson;
        IGetUserId _getUserId;
        IBuyMoney _buyMoney;
        ISellMoney _sellMoney;
        IJsonIdUser _jsonIdUser;

        public HomeController(ISaveData saveData, IGetJeson getJeson, IGetUserId getUserId, IBuyMoney buyMoney, ISellMoney sellMoney, IJsonIdUser jsonIdUser)
        {
            _saveData = saveData;
            _getJeson = getJeson;
            _getUserId = getUserId;
            _buyMoney = buyMoney;
            _sellMoney = sellMoney;
            _jsonIdUser = jsonIdUser;
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
            var jsonIdUser = _jsonIdUser;
            var exchangeRate = _getJeson.GetJson();
            var getUser = _getUserId.GetUserID(db);

            jsonIdUser.user = getUser;
            jsonIdUser.ar = exchangeRate;

            ViewBag.USDM = exchangeRate[0].rates[0].bid * getUser.info.USD;
            ViewBag.EURM = exchangeRate[0].rates[3].bid * getUser.info.EUR;
            ViewBag.CHFM = exchangeRate[0].rates[5].bid * getUser.info.CHF;
            ViewBag.RUBM = (exchangeRate[0].rates[10].bid * getUser.info.RUB);
            ViewBag.CZKM = (exchangeRate[0].rates[8].bid * getUser.info.CZK);
            ViewBag.GBPM = exchangeRate[0].rates[6].bid * getUser.info.GBP;

            return View(jsonIdUser);
        }

        [HttpGet]
        public ActionResult Sell(string id, string currency)
        {
            var getUser = db.Users.Find(id);
            ViewBag.Message1 = currency;
            return View(getUser);
        }

        [HttpGet]
        public ActionResult Buy(string id, string currency)
        {
            var getUser = db.Users.Find(id);
            ViewBag.Message1 = currency;
            return View(getUser);
        }

        [HttpPost]
        public ActionResult BuyEUR(int? numberIdUser, string currencyNameString, int amount)
        {
            return (_buyMoney.BuyEUR(numberIdUser,currencyNameString, amount, db));
        }
 

        [HttpPost]
        public ActionResult SellEUR(int? numberIdUser, string currencyNameString, int amount)
        {
            return (_sellMoney.SellEUR(numberIdUser, currencyNameString, amount, db));
        }
              
    }
}