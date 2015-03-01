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
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;

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
        /// 
        /// 
        /// </summary>
        /// <param name="helper">injektált helper</param>
        public CarController(NHibernateHelper helper)
        {

            ISession nhSession = helper.SessionFactory.GetCurrentSession();

            this.carRepository = new CarRepository(nhSession);
            this.siteRepository = new SiteRepository(nhSession);
        }

        /// <summary>
        /// 
        /// Telephelyek lekérdezése
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSitesWithEmpty()
        {
            List<SelectListItem> sites = new List<SelectListItem>();

            sites.Add(new SelectListItem());
            
            foreach (Site site in siteRepository.List())
            {
                SelectListItem site_item = new SelectListItem { Value = site.SiteId.ToString(), Text = site.ToString() };
                sites.Add(site_item);
            }

            return sites;
        }
        
        /// <summary>
        /// 
        /// listanézet
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            
            int pageNumber = 1;

            IPagedList<Car> cars = this.carRepository.PagedAndOrderedList(pageNumber,  CshtmlHelper.PAGESIZE);

            ViewBag.SiteList = this.siteRepository.List();
            ViewBag.SitesWithEmpty = GetSitesWithEmpty();

            return View(cars);
        }

        /// <summary>
        /// létrehozás
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.SiteList = this.siteRepository.List();

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

                ViewBag.SiteList = this.siteRepository.List();
                ViewBag.SitesWithEmpty = GetSitesWithEmpty();
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
            ViewBag.SiteList = this.siteRepository.List();
     

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

                ViewBag.SitesWithEmpty = GetSitesWithEmpty();
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

                ViewBag.SitesWithEmpty = GetSitesWithEmpty();

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


    }
}