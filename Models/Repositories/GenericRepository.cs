using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAuto.Models.Repositories
{

    /// <summary>
    /// /
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
        
           // IList<TEntity> entities = hibernateSession.CreateQuery("from " + typeof(TEntity)).List<TEntity>();
          //  return entities;
        }
       
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
           return hibernateSession.Get<TEntity>(id);
        }

    }

}