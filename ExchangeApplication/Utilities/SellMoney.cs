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

        public SellMoney(IGetJeson getJeson, ISaveData saveData)
        {
            _getJeson = getJeson;
            _saveData = saveData;
        }

        [HttpPost]
        public ActionResult SellEUR(int? numberIdUser, int amount, String currencyName, ApplicationDbContext db)
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
            var arr = _getJeson.GetJson();
            int price = 0;

            switch (currencyName)
            {
                case ("US"):
                    price = (int)arr.items[0].purchasePrice;
                    if (amount <= info.USD)
                    {
                        info.USD = info.USD - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Euro"):
                    price = (int)arr.items[1].purchasePrice;
                    if (amount <= info.EUR)
                    {

                        info.EUR = info.EUR - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Swiss"):
                    price = (int)arr.items[1].purchasePrice;
                    if (amount <= info.CHF)
                    {
                        info.CHF = info.CHF - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Russian"):
                    price = (int)arr.items[1].purchasePrice;
                    if (amount <= info.RUB)
                    {
                        info.RUB = info.RUB - amount * 100;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Czech"):
                    price = (int)arr.items[1].purchasePrice;
                    if (amount <= info.CZK)
                    {
                        info.CZK = info.CZK - amount * 100;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Pound"):
                    price = (int)arr.items[1].purchasePrice;
                    if (amount <= info.GBP)
                    {
                        info.GBP = info.GBP - amount;
                        int newZlotyValue = amount * price;
                        info.PLN = info.PLN + newZlotyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
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