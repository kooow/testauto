using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAuto.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class NHibernateContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);
            else
                return base.CreateContract(objectType);
        }
    }


}