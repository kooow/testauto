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
    ///  Telephely entitás leképezési modelje
    /// 
    /// </summary>
    public class SiteMapping : ClassMap<Site>
    {

        /// <summary>
        /// 
        /// </summary>
        public SiteMapping()
        {
            Table("Site");
            LazyLoad();
            Id(x => x.SiteId).GeneratedBy.Identity().Column("SiteId");
            Map(x => x.City).Column("City").Not.Nullable();
            Map(x => x.Address).Column("Address").Not.Nullable();
            Map(x => x.Postcode).Column("PostCode").Not.Nullable();
            Map(x => x.Parkingspace).Column("ParkingSpace").Not.Nullable();
            HasMany(x => x.Cars).KeyColumn("SiteId");
        }


    }
}