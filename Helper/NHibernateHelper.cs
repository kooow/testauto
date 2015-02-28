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
using TestAuto.Nhibernate;

namespace TestAuto.Helper
{
    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    public static class NHibernateHelper
    {

        /// <summary>
        /// 
        ///  isessionfactory singleton
        /// 
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory GetNHibernateSessionFactory()
        {


            Configuration conf = new NHibernate.Cfg.Configuration(); 
           
            conf.DataBaseIntegration(delegate(NHibernate.Cfg.Loquacious.IDbIntegrationConfigurationProperties dbi)
            {
                dbi.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                dbi.ConnectionStringName = "testAutoHibernate";
                dbi.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                dbi.Driver<NHibernate.Driver.SqlClientDriver>();

            });
    

            // Assembly asm =Assembly.GetExecutingAssembly();

          //  TestAutoConfiguration testconf = new TestAutoConfiguration();

            FluentConfiguration fluent_conf = Fluently.Configure(conf);

         //   new SchemaUpdate(conf).Execute(true, true);

            fluent_conf.CurrentSessionContext<WebSessionContext>();

            ISessionFactory sessionFactory = fluent_conf              
                  .Mappings(m => m.FluentMappings.Add<CarMapping>())
                  .Mappings(m => m.FluentMappings.Add<SiteMapping>())                      
                  //  m.AutoMappings.Add(
                  // your automapping setup here
                  //    AutoMap.AssemblyOf<Car>(testconf)))
                  .BuildSessionFactory();

      
            return sessionFactory;
        }

    }
}