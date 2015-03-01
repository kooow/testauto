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
using TestAuto.Helper;

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
        public ActionResult List(int? page)
        {

            string sort = "site";

            IList<Car> cars = this.carRepository.List();

            if (sort == "site")
            {
                cars = cars.OrderBy(c => (c.Site.City + " " + c.Site.Address + c.Site.Postcode)).ToList();
            }
            else if (sort == "site_desc")
            {
                cars = cars.OrderByDescending(c => (c.Site.City + " " + c.Site.Address + c.Site.Postcode)).ToList();
            }
      
            int pageNumber = (page ?? 1);

            ViewBag.CurrentSort = sort;

            return View(cars.ToPagedList(pageNumber, CshtmlHelper.PAGESIZE));
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
        /// autó létrehozása
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
        /// autó szerkesztése
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
        /// autó szerkesztésnek mentése
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
        /// Autó tulajdonságai ablak
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
        /// autó törlése ablak
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
        /// autó törlése ablak felküldése, a tényleges törlés
        /// 
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Car car)
        {
            try
            {
                Car deleteCar = this.carRepository.Get(car.CarId);

                this.carRepository.Delete(deleteCar);

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


    }
}