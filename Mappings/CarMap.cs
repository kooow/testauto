using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAuto.Models;


namespace TestAuto.Mappings
{
    /// <summary>
    /// 
    /// Car entitás leképezési modelje
    /// 
    /// </summary>
    public class CarMapping : ClassMap<Car>
    {
        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        public CarMapping()
        {

            Table("Car");
            LazyLoad();
            Id(x => x.CarId).GeneratedBy.Identity().Column("CarId");
            References(x => x.Site).Column("SiteId");
            Map(x => x.Manufacturer).Column("Manufacturer");
            Map(x => x.Type).Column("Type").Not.Nullable();
            Map(x => x.Year).Column("Year").Not.Nullable();
            Map(x => x.Productiondate).Column("Productiondate").Not.Nullable();
            Map(x => x.Condition).Column("Condition");
            Map(x => x.Owners).Column("Owners");

        }

    }
}