using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestAuto.Models
{
    /// <summary>
    /// 
    /// Autó entitás
    /// 
    /// </summary>
    public class Car
    {
       
        /// <summary>
        /// 
        /// </summary>
        public virtual int CarId { get; set; }

        /// <summary>
        /// gyártó
        /// </summary>
        [Display(Name = "Gyártó")]
        public virtual string Manufacturer { get; set; }

        /// <summary>
        /// típus
        /// </summary>
        [Display(Name = "Típus")]
        [Required(ErrorMessage = "A típust meg kell adni!")]
        public virtual string Type { get; set; }

        /// <summary>
        /// évjárat
        /// </summary>
        [Display(Name = "Évjárat")]
        public virtual int Year { get; set; }

        /// <summary>
        /// Gyártási idő
        /// </summary>
        [Display(Name = "Gyártási idő")]
        [Required(ErrorMessage = "A gyártási időt meg kell adni!")]
        public virtual DateTime Productiondate { get; set; }

        /// <summary>
        /// állapot
        /// </summary>
        [Display(Name = "Állapot")]
        public virtual string Condition { get; set; }

        /// <summary>
        /// tulajdonosok száma
        /// </summary>
        [Display(Name = "Tulajdonosok száma")]
        public virtual int? Owners { get; set; }

        /// <summary>
        /// Telephely
        /// </summary>
        [Display(Name = "Telephely")]
        [Required(ErrorMessage = "A telephelyet ki kell választani!")]
        public virtual int SiteId { get; set;  }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Telephely")]
        public virtual Site Site { get; set; }


    }
}