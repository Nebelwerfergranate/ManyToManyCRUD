using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Motorists.Models;

namespace Motorists.Controllers
{
    public class CarsController : Controller
    {
        private readonly IUnitOfWork _db;

        public CarsController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: Cars
        public ActionResult Index()
        {
            return View(_db.Cars.GetItems());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _db.Cars.GetItem((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,Brand,Type,Cost,Year,RegistrationNumber")] Car car)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Create(car);
                _db.Save();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _db.Cars.GetItem((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,Brand,Type,Cost,Year,RegistrationNumber")] Car car)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Update(car);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _db.Cars.GetItem((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Cars.Delete(id);
            _db.Save();
            return RedirectToAction("Index");
        }

        public ActionResult AddDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _db.Cars.GetItem((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDriver(int id, string vatin)
        {
            Car car = _db.Cars.GetItem(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            Driver driver = _db.Drivers.GetItems().Where(d => d.Vatin == vatin).FirstOrDefault();
            if (driver == null)
            {
                ViewData["id"] = id;
                ViewData["vatin"] = vatin;
                return View("DriverNotFound");
            }
            
            car.Owners.Add(driver);
            _db.Cars.Update(car);
            _db.Save();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _db.Cars.GetItem((int)id);
            if (car == null)
            {
                return HttpNotFound();
            }

            ICollection<SelectListItem> drivers = new List<SelectListItem>();
            foreach (Driver driver in car.Owners)
            {
                drivers.Add(new SelectListItem {Value = driver.Id.ToString(), Text = driver.ToString()});
            }
            ViewData["drivers"] = drivers;
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDriver(int id, int driverId)
        {
            Car car = _db.Cars.GetItem(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            Driver driver = _db.Drivers.GetItem(driverId);
            if (driver == null)
            {
                return HttpNotFound();
            }

            car.Owners.Remove(driver);
            _db.Cars.Update(car);
            _db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}