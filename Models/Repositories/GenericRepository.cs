using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace TestAuto.Models.Repositories
{

    /// <summary>
    /// Közös generikus adatbázisműveletek kezelését végző osztály
    /// a megadott entitásoz kapcsolódóan
    /// </summary>
    public class GenericRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly ISession hibernateSession;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public GenericRepository(ISession session)
        {
            this.hibernateSession = session;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> List()
        {

            ICriteria criteria = hibernateSession.CreateCriteria(typeof(TEntity));
            return criteria.List<TEntity>();
        
       
        }

        /// <summary>
        /// 
        /// entitáslista lapozható formátumban visszaadva
        /// 
        /// </summary>
        /// <param name="page">aktuális oldal</param>
        /// <param name="pagesize">oldalméret</param>
        /// <returns></returns>
        public IPagedList<TEntity> List(int page, int pagesize)
        {
            ICriteria criteria = hibernateSession.CreateCriteria(typeof(TEntity));
            return criteria.List<TEntity>().ToPagedList(page, pagesize);
        }

        /// <summary>
        ///  adatbázis rekord létrehozása az entitáson keresztül
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Create(TEntity entity)
        {
            using (ITransaction tran = hibernateSession.BeginTransaction())
            {
                hibernateSession.SaveOrUpdate(entity);
                tran.Commit();
                return entity;
            }
        }

        /// <summary>
        /// 
        /// adatbázis rekord frissítése az entitáson keresztül
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            using (ITransaction tran = hibernateSession.BeginTransaction())
            {
                hibernateSession.Update(entity);
                tran.Commit();
                return entity;
            }
        }

        /// <summary>
        /// 
        /// adatbázis rekord frissítése az entitáson keresztül
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            using (ITransaction tran = hibernateSession.BeginTransaction())
            {
                hibernateSession.Delete(entity);
                tran.Commit();
            }
        }

        /// <summary>
        /// Visszaadjuk az etitást azonosítón keresztül
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
           return hibernateSession.Get<TEntity>(id);
        }

    }

}