namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("POI")]
    //[ValidateInput(false)]
    public partial class POI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POI()
        {
            //Comments = new HashSet<Comment>();
        }

        [StringLength(256)]
        public string poiId { get; set; }

        [StringLength(256)]
        [Display(Name ="City")]
        public string city { get; set; }

        //[Required]
        [StringLength(250)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [StringLength(150)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [StringLength(2083)]
        [Display(Name = "Website")]
        public string website { get; set; }

        [StringLength(2083)]
        [Display(Name = "Image")]
        public string poiImg { get; set; }

        [Display(Name = "Rating")]
        public double rating { get; set; }

        public double? lon { get; set; }

        public double? lat { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Category")]
        public long? category { get; set; }

        public virtual Category Category1 { get; set; }

        public virtual City City1 { get; set; }

        [Display(Name = "Description")]
        public string pOIDescription { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Comment> Comments { get; set; }
    }
}
