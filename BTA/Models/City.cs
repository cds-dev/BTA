namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            POIs = new HashSet<POI>();
        }

        [StringLength(256)]
        public string cityId { get; set; }

        [Column("city")]
        [Required]
        [Display(Name = "City")]
        [StringLength(20)]
        public string city1 { get; set; }

        //[Required]
        [StringLength(20)]
        public string country { get; set; }

        public double? lon { get; set; }

        public double? lat { get; set; }

        [StringLength(2083)]
        public string image { get; set; }

        public int? population { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POI> POIs { get; set; }
    }
}
