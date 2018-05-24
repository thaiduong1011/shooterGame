namespace Server2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            scores = new HashSet<score>();
        }

        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(200)]
        public string user_name { get; set; }

        [Required]
        [StringLength(200)]
        public string password { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime updated_at { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<score> scores { get; set; }
    }
}
