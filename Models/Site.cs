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
    /// telephely entitás
    /// 
    /// </summary>
    public class Site
    {

        /// <summary>
        /// 
        /// </summary>
        public Site()
        {
            this.Cars = new List<Car>();
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        public virtual int SiteId { get; set;  }
    
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Város")]
        [Required(ErrorMessage = "A várost meg kell adni!")]
        public virtual string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Cím")]
        [Required(ErrorMessage = "A címet meg kell adni!")]
        public virtual string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Irányítószám")]
        [Range(1000, 9999, ErrorMessage = "1000 és 9999 közötti értékek kellenek!")]
        [Required(ErrorMessage = "Az irányítószámot meg kell adni!")]
        public virtual int Postcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Parkolóhelyek száma")]
        [Range(1, 400000, ErrorMessage="1 és 400 000 közötti értékek kellenek!")]
        [Required(ErrorMessage = "A parkolóhelyek számot meg kell adni!")]
        public virtual int Parkingspace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual IList<Car> Cars { get; set;  }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FullName
        {
            get
            {
                return this.City + " " + this.Address + " " + this.Postcode.ToString();
            }
        }

        /// <summary>
        /// név helyett
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.City + " " + this.Address + " " + this.Postcode.ToString();
        }

    }
}