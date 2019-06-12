namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            //Comment1 = new HashSet<Comment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long commentId { get; set; }

        public long? parentId { get; set; }

        public string tableName { get; set; }


        public long? traveler { get; set; }

        //[StringLength(256)]
        //public string poi { get; set; }

        //public long? category { get; set; }

        [StringLength(50)]
        public string subject { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [StringLength(500)]
        public string text { get; set; }

        public int grade { get; set; }

        //public virtual Category Category1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Comment> Comment1 { get; set; }

        //public virtual Comment Comment2 { get; set; }

        //public virtual POI POI1 { get; set; }

        public virtual Traveler Traveler1 { get; set; }
    }
}
