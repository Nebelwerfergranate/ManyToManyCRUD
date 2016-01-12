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
    public class DriversController : Controller
    {
        private readonly IUnitOfWork _db;

        public DriversController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: Drivers
        public ActionResult Index()
        {
            return View(_db.Drivers.GetItems());
        }

        // GET: Owners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver owner = _db.Drivers.GetItem((int)id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,Vatin,LastName,BirthYear,DrivingExperience")] Driver owner)
        {
            if (ModelState.IsValid)
            {
                _db.Drivers.Create(owner);
                _db.Save();
                return RedirectToAction("Index");
            }

            return View(owner);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver owner = _db.Drivers.GetItem((int)id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Vatin,BirthYear,DrivingExperience")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                _db.Drivers.Update(driver);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver owner = _db.Drivers.GetItem((int)id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Drivers.Delete(id);
            _db.Save();
            return RedirectToAction("Index");
        }

        public ActionResult AddCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = _db.Drivers.GetItem((int)id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(int id, string registrationNumber)
        {
            Driver driver = _db.Drivers.GetItem(id);
            if (driver == null)
            {
                return HttpNotFound();
            }

            Car car = _db.Cars.GetItems().Where(d => d.RegistrationNumber == registrationNumber).FirstOrDefault();
            if (car == null)
            {
                ViewData["id"] = id;
                ViewData["registrationNumber"] = registrationNumber;
                return View("CarNotFound");
            }

            driver.Cars.Add(car);
            _db.Drivers.Update(driver);
            _db.Save();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = _db.Drivers.GetItem((int)id);
            if (driver == null)
            {
                return HttpNotFound();
            }

            ICollection<SelectListItem> cars = new List<SelectListItem>();
            foreach (Car car in driver.Cars)
            {
                cars.Add(new SelectListItem { Value = car.Id.ToString(), Text = car.ToString() });
            }
            ViewData["cars"] = cars;
            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCar(int id, int carId)
        {
            Car car = _db.Cars.GetItem(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            Driver driver = _db.Drivers.GetItem(carId);
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
