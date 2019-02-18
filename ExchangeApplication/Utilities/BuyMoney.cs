using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExchangeApplication.Utilities
{
    public class BuyMoney : Controller, IBuyMoney
    {
        IGetJeson _getJeson;
        ISaveData _saveData;

        private GenericUnitOfWork uow = null;

        public BuyMoney(IGetJeson getJeson, ISaveData saveData)
        {
            _getJeson = getJeson;
            _saveData = saveData;
            uow = new GenericUnitOfWork();
        }

        public BuyMoney(IGetJeson getJeson, ISaveData saveData, GenericUnitOfWork _uow)
        {
            _getJeson = getJeson;
            _saveData = saveData;
            this.uow = _uow;
        }
        [HttpPost]
        public ActionResult BuyEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db)
        {
            var exchangeRate = _getJeson.GetJson();
            int price = 0;
            if (numberIdUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(numberIdUser);

            if (info == null)
            {
                return HttpNotFound();
            }

            switch (currencyNameString)
            {
                case ("dolar amerykański"):
                    price = (int)exchangeRate[0].rates[0].ask;
                    int currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.USD = info.USD + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("euro"):
                    price = (int)exchangeRate[0].rates[3].ask;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.EUR = info.EUR + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("frank szwajcarski"):
                    price = (int)exchangeRate[0].rates[5].ask;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.CHF = info.CHF + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("korona norweska"):
                    double priceDouble = exchangeRate[0].rates[10].ask;
                    double currencyValueDouble = amount * priceDouble;
                    if (currencyValueDouble <= info.PLN)
                    {
                        currencyValue = (int)currencyValueDouble;
                        info.RUB = info.RUB + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("korona czeska"):
                    priceDouble = exchangeRate[0].rates[8].ask;
                    currencyValueDouble = amount * priceDouble;
                    if (currencyValueDouble <= info.PLN)
                    {
                        currencyValue = (int)currencyValueDouble;
                        info.CZK = info.CZK + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("funt szterling"):
                    price = (int)exchangeRate[0].rates[6].ask;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.GBP = info.GBP + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                default:
                    return View("Error");
            }
        }
    }
}