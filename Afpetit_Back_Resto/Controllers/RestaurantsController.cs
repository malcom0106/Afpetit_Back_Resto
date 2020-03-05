using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Afpetit_Back_Resto.Models;

namespace Afpetit_Back_Resto.Controllers
{
    public class RestaurantsController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();
        
        // GET: Restaurants/Details/5
        public ActionResult Details()
        {
            if (Session["restaurant"] != null)
            {
                Restaurant restaurant = (Restaurant)Session["restaurant"];
                return View(restaurant);
            }
            else
            {
                return RedirectToAction("Connexion", "Restaurants");
            }            
        }       

        // GET: Restaurants/Edit/5
        public ActionResult Edit()
        {
            if (Session["restaurant"] != null)
            {
                Restaurant restaurant = (Restaurant)Session["restaurant"];
                ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
                return View(restaurant);
            }
            else
            {
                return RedirectToAction("Connexion", "Restaurants");
            }
        }

        // POST: Restaurants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRestaurant,Nom,Responsable,IdTypeCuisine,Adresse,CodePostal,Ville,Mobile,Telephone,Tag,Budget,Email,Description")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Connexion()
        {           
            return View();
        }

        // POST: Restaurants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "Login,Password")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                Restaurant monRestaurant = db.Restaurants.Where(r => r.Login == restaurant.Login && r.Password == restaurant.Password).First();
                if(monRestaurant != null)
                {
                    Session["restaurant"] = monRestaurant;
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    return View();
                }
                
            }
            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
            return View(restaurant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
