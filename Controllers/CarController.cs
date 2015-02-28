using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAuto.Models;
using TestAuto.Models.Repositories;
using PagedList;
using System.ComponentModel.DataAnnotations;

namespace TestAuto.Controllers
{
    /// <summary>
    /// 
    /// Autók crud controller osztály
    /// 
    /// </summary>
    public class CarController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        private CarRepository carRepository;

        /// <summary>
        /// 
        /// </summary>
        private SiteRepository siteRepository;

        /// <summary>
        /// 
        /// </summary>
        public CarController()
        {

            this.carRepository = new CarRepository(MvcApplication.NHibernateSessionFactory.GetCurrentSession());
            this.siteRepository = new SiteRepository(MvcApplication.NHibernateSessionFactory.GetCurrentSession());

        }

        /// <summary>
        /// 
        /// listanézet
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(string sort, int? page)
        {


            ViewBag.SiteSort = sort == "site" ? "site_desc" : "site";
            ViewBag.TypeSort = sort == "type" ? "type_desc" : "type";
            ViewBag.ManufacturerSort = sort == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewBag.ProductionDateSort = sort == "productiondate" ? "productiondate_desc" : "productiondate";
            ViewBag.YearSort = sort == "year" ? "year_desc" : "year";
            ViewBag.ConditionSort = sort == "condition" ? "condition_desc" : "condition";
            ViewBag.OwnersSort = sort == "owners" ? "owners_desc" : "owners";

            IList<Car> cars = this.carRepository.List();

            if (sort == "site")
            {
                cars = cars.OrderBy(c => (c.Site.City + " " + c.Site.Address + c.Site.Postcode)).ToList();
            }
            else if (sort == "site_desc")
            {
                cars = cars.OrderByDescending(c => (c.Site.City + " " + c.Site.Address + c.Site.Postcode)).ToList();
            }
            else if (sort == "type")
            {
                cars = cars.OrderBy(c => c.Type).ToList();
            }
            else if (sort == "type_desc")
            {
                cars = cars.OrderByDescending(c => c.Type).ToList();
            }
            else if (sort == "year")
            {
                cars = cars.OrderBy(c => c.Year).ToList();
            }
            else if (sort == "year_desc")
            {
                cars = cars.OrderByDescending(c => c.Year).ToList();
            }
            else if (sort == "manufacturer")
            {
                cars = cars.OrderBy(c => c.Manufacturer).ToList();
            }
            else if (sort == "manufacturer_desc")
            {
                cars = cars.OrderByDescending(c => c.Manufacturer).ToList();
            }
            else if (sort == "productiondate")
            {
                cars = cars.OrderBy(c => c.Productiondate).ToList();
            }
            else if (sort == "productiondate_desc")
            {
                cars = cars.OrderByDescending(c => c.Productiondate).ToList();
            }
            else if (sort == "condition")
            {
                cars = cars.OrderBy(c => c.Condition).ToList();
            }
            else if (sort == "condition_desc")
            {
                cars = cars.OrderByDescending(c => c.Condition).ToList();
            }
            else if (sort == "owners")
            {
                cars = cars.OrderBy(c => c.Owners).ToList();
            }
            else if (sort == "owners_desc")
            {
                cars = cars.OrderByDescending(c => c.Owners).ToList();
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentSort = sort;

            return View(cars.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// létrehozás
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Site site = this.siteRepository.Get(car.SiteId);
                    car.Site = site;

                    this.carRepository.Create(car);
                }

                return RedirectToAction("List");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Car car = this.carRepository.Get(id);

            return View(car);
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Car car)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Site site = this.siteRepository.Get(car.SiteId);
                    car.Site = site;

                    this.carRepository.Update(car);
                  
                }
                return RedirectToAction("List");
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Car car = this.carRepository.Get(id);
            return View(car);
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>     
        public ActionResult Delete(int id)
        {
            Car car = this.carRepository.Get(id);
            return View(car);
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Car car)
        {
            try
            {

                this.carRepository.Delete(car);

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


    }
}