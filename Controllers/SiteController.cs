using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAuto.Helper;
using TestAuto.Models;
using TestAuto.Models.Repositories;

namespace TestAuto.Controllers
{
    /// <summary>
    /// 
    ///  
    /// </summary>
    public class SiteController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        private SiteRepository siteRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper">injektált helper</param>
        public SiteController(NHibernateHelper helper)
        {
            ISession nhSession = helper.SessionFactory.GetCurrentSession();

            this.siteRepository = new SiteRepository(nhSession);
        }

        /// <summary>
        /// 
        /// listanézet
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            IList<Site> sites = this.siteRepository.List();
            return View(sites);
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
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Site site)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.siteRepository.Create(site);
                }

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Site site = this.siteRepository.Get(id);
            return View(site);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Site site)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.siteRepository.Update(site);
                }

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
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
            Site site = this.siteRepository.Get(id);
            return View(site);
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>     
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Site site = this.siteRepository.Get(id);
            return View(site);
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int SiteId)
        {
            try
            {
                Site site = this.siteRepository.Get(SiteId);
                this.siteRepository.Delete(site);

                return RedirectToAction("List");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }
}