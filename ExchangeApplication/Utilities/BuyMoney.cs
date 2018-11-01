using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
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

        public BuyMoney(IGetJeson getJeson, ISaveData saveData)
        {
            _getJeson = getJeson;
            _saveData = saveData;
        }
        [HttpPost]
        public ActionResult BuyEUR(int? numberIdUser, int amount, String currencyName, ApplicationDbContext db)
        {
            var jeson = _getJeson.GetJson();
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

            switch (currencyName)
            {
                case ("US"):
                    price = (int)jeson.items[0].purchasePrice;
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
                case ("Euro"):
                    price = (int)jeson.items[1].purchasePrice;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.EUR = info.EUR + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db);//SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Swiss"):
                    price = (int)jeson.items[2].purchasePrice;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.CHF = info.CHF + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Russian"):
                    price = (int)jeson.items[3].purchasePrice;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.RUB = info.RUB + amount * 100;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Czech"):
                    price = (int)jeson.items[4].purchasePrice;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.CZK = info.CZK + amount * 100;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                case ("Pound"):
                    price = (int)jeson.items[5].purchasePrice;
                    currencyValue = amount * price;
                    if (currencyValue <= info.PLN)
                    {
                        info.GBP = info.GBP + amount;
                        info.PLN = info.PLN - currencyValue;
                        _saveData.Save(numberIdUser, db); //SaveData(param1);
                        return RedirectToAction("About");
                    }
                    else
                    {
                        return View("ErrorLackOfMoney");
                    }
                default:
                  //  ViewBag.Message11 = currencyName;
                    return View("Error");
            }
        }
    }
}