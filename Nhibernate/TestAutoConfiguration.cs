using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAuto.Nhibernate
{
    /// <summary>
    /// 
    /// 
    /// </summary>
   /* public class TestAutoConfiguration  : DefaultAutomappingConfiguration
    {
      /// <summary>
      ///
      /// </summary>
      /// <param name="type"></param>
      /// <returns></returns>
      public override bool ShouldMap(Type type)
      {
          return type.Namespace == "TestAuto.Models";
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="member"></param>
      /// <returns></returns>
      public override bool IsId(FluentNHibernate.Member member)
      {
          return member.Name == member.DeclaringType.Name + "Id";
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="member"></param>
      /// <returns></returns>
      public override string GetComponentColumnPrefix(FluentNHibernate.Member member)
      {
          return member.PropertyType.Name + "_";
      }
    }
    */

    
 
}