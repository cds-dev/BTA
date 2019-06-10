namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Traveler")]
    public partial class Traveler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Traveler()
        {
            Comments = new HashSet<Comment>();
        }

        public long travelerId { get; set; }

        [StringLength(128)]
        public string identityId { get; set; }

        public bool activity { get; set; }

        [StringLength(150)]
        [Display(Name ="Profile picture")]
        public string imgUrl { get; set; }

        [StringLength(50)]
        [Display(Name = "Full name")]
        public string fullName { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
