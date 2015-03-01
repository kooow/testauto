using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using TestAuto.Mappings;
using TestAuto.Models;


namespace TestAuto.Helper
{
    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    public class NHibernateHelper
    {

        /// <summary>
        /// 
        /// </summary>
        private ISessionFactory sessionFactory;
        
        /// <summary>
        /// 
        /// </summary>
        public ISessionFactory SessionFactory 
        { 
            get
            {
                return sessionFactory;
            }        
        }

        /// <summary>
        /// 
        /// </summary>
        public NHibernateHelper()
        {

            Configuration conf = new NHibernate.Cfg.Configuration();

            conf.DataBaseIntegration(delegate(NHibernate.Cfg.Loquacious.IDbIntegrationConfigurationProperties dbi)
            {
                dbi.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                dbi.ConnectionStringName = "testAutoHibernate";
                dbi.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                dbi.Driver<NHibernate.Driver.SqlClientDriver>();
            });

            // nhibernate konfiguráció
            FluentConfiguration fluent_conf = Fluently.Configure(conf);

            fluent_conf.CurrentSessionContext<WebSessionContext>();

            sessionFactory = fluent_conf
              .Mappings(m => m.FluentMappings.Add<CarMapping>())
              .Mappings(m => m.FluentMappings.Add<SiteMapping>())
              .BuildSessionFactory();

        }
 

    }
}