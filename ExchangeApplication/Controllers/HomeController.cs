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
            uow = new GenericUnitOfWork();
        }

        public HomeController(ISaveData saveData, IGetJeson getJeson, IGetUserId getUserId, IBuyMoney buyMoney, ISellMoney sellMoney, IJsonIdUser jsonIdUser, GenericUnitOfWork _uow)
        {
            _saveData = saveData;
            _getJeson = getJeson;
            _getUserId = getUserId;
            _buyMoney = buyMoney;
            _sellMoney = sellMoney;
            _jsonIdUser = jsonIdUser;
            this.uow = _uow;
        }
        private GenericUnitOfWork uow = null;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            var jsonIdUser = _jsonIdUser;
            var exchangeRate = _getJeson.GetJson();
            var getUser = _getUserId.GetUserID();

            jsonIdUser.user = getUser;
            jsonIdUser.ar = exchangeRate;

            ViewBag.USDM = exchangeRate[0].rates[0].bid * getUser.info.USD; // add this also to view model
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
            var idLoggedUser = User.Identity.GetUserId();
            var loggedUser = uow.Repository<ApplicationUser>().GetDetail(p => p.Id == idLoggedUser);
            ViewBag.Message1 = currency;
            return View(loggedUser);
        }

        [HttpGet]
        public ActionResult Buy(string id, string currency)
        {
            var idLoggedUser = User.Identity.GetUserId();
            var loggedUser = uow.Repository<ApplicationUser>().GetDetail(p => p.Id == idLoggedUser);
            ViewBag.Message1 = currency;
            return View(loggedUser);
        }

        [HttpPost]
        public ActionResult BuyEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db)
        {
            return (_buyMoney.BuyEUR(numberIdUser,currencyNameString, amount, db));
        }
 

        [HttpPost]
        public ActionResult SellEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db)
        {
            return (_sellMoney.SellEUR(numberIdUser, currencyNameString, amount, db));
        }
              
    }
}