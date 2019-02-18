using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExchangeApplication.Utilities
{
    public class SellMoney : Controller, ISellMoney
    {
        IGetJeson _getJeson;
        ISaveData _saveData;
        private GenericUnitOfWork uow = null;

        public SellMoney(IGetJeson getJeson, ISaveData saveData)
        {
            _getJeson = getJeson;
            _saveData = saveData;
            uow = new GenericUnitOfWork();

        }

        public SellMoney(IGetJeson getJeson, ISaveData saveData, GenericUnitOfWork _uow)
        {
            _getJeson = getJeson;
            _saveData = saveData;
            this.uow = _uow;

        }

        [HttpPost]
        public ActionResult SellEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db)
        {
            if (numberIdUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = db.Infoes.Find(numberIdUser);
            if (info == null)
            {
                return HttpNotFound();
            }
            var exchangeRate = _getJeson.GetJson();
            int price = 0;

            switch (currencyNameString)
            {
                case ("dolar amerykański"):
                    price = (int)exchangeRate[0].rates[0].bid;
                    if (amount <= info.USD)
                    {
                        info.USD = info.USD - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("euro"):
                    price = (int)exchangeRate[0].rates[3].bid;
                    if (amount <= info.EUR)
                    {
                        info.EUR = info.EUR - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("frank szwajcarski"):
                    price = (int)exchangeRate[0].rates[5].bid;
                    if (amount <= info.CHF)
                    {
                        info.CHF = info.CHF - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("korona norweska"):
                    double priceDouble = exchangeRate[0].rates[10].bid;
                    if (amount <= info.RUB)
                    {
                        info.RUB = info.RUB - amount;
                        double newZlotyValueDouble = amount * priceDouble;
                        info.PLN = info.PLN + ((int)newZlotyValueDouble);
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("korona czeska"):
                    priceDouble = exchangeRate[0].rates[8].bid;
                    if (amount <= info.CZK)
                    {
                        info.CZK = info.CZK - amount;
                        double newZlotyValueDouble = amount * priceDouble;
                        info.PLN = info.PLN + ((int)newZlotyValueDouble);
                        _saveData.Save(numberIdUser, db); 
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("funt szterling"):
                    price = (int)exchangeRate[0].rates[6].bid;
                    if (amount <= info.GBP)
                    {
                        info.GBP = info.GBP - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
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