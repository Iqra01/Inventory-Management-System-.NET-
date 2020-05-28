using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eadproj.Models;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;

namespace eadproj.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Toys()
        {
            var selection = dc.inventitems.Where(x => x.Category == "TOYS");
            return View(selection.ToList());
        }

        public ActionResult cosmetics()
        {
            var selection = dc.inventitems.Where(x => x.Category == "COSMETICS");
            return View(selection.ToList());
        }
        public ActionResult sportgood()
        {
            var selection = dc.inventitems.Where(x => x.Category == "SPORTGOOD");
            return View(selection.ToList());
        }

        public ActionResult groceryitem()
        {
            var selection = dc.inventitems.Where(x => x.Category == "GROCERYITEM");
            return View(selection.ToList());
        }
        public ActionResult Added()
        {
            string name = Request["name"];
            string brand = Request["brand"];
            string category = Request["category"];
            string pr = Request["price"];
            string description = Request["description"];
            int price = Convert.ToInt32(pr);
            var selection = dc.inventitems.Where(x => x.Name == name && x.Brand==brand);
            int id;
            if (selection != null) 
            {
                foreach (var s in selection) // if added item already exist
                {
                    id = s.Id;
                    return RedirectToAction("EditITEM", new { id = id });
                }
            }
            inventitem t = new inventitem();

            int count = dc.inventitems.Select(h => h.Id > 0).Count();
            if (count == 0) // if no item exist in table
            {
                t.Id = 1;
            }
            else
            {
                int max = dc.inventitems.Max(r => r.Id);
                 t.Id = max + 1;
            }

            t.Name = name;
            t.Brand = brand;
            t.Price = price;
            t.Category = category;
            t.Description = description;
            dc.inventitems.InsertOnSubmit(t);
            dc.SubmitChanges();

            if (category == "TOYS")
                return RedirectToAction("Toys");
            else if (category == "COSMETICS")
                return RedirectToAction("cosmetics");
            else if (category == "GROCERYITEM")
                return RedirectToAction("groceryitem");
            else if (category == "SPORTGOOD")
                return RedirectToAction("SPORTGOOD");
            else
            return RedirectToAction("ADDITEM");
        }
        public ActionResult ADDITEM()
        {
            return View();
        }

        public ActionResult EditITEM(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult Changebox(int id)
        {
            var s = dc.inventitems.First(x => x.Id == id);
             s.Name= Request["name"];
             s.Brand = Request["brand"];
             s.Category = Request["category"];
             string pr = Request["price"];
             s.Description = Request["description"];
             s.Price = Convert.ToInt16(pr);
             dc.SubmitChanges();

            if (s.Category == "TOYS")
                return RedirectToAction("Toys");
            else if (s.Category == "COSMETICS")
                return RedirectToAction("cosmetics");
            else if (s.Category == "GROCERYITEM")
                return RedirectToAction("groceryitem");
            else if (s.Category == "SPORTGOOD")
                return RedirectToAction("SPORTGOOD");
            else
                return RedirectToAction("Index");
        }


    }
}