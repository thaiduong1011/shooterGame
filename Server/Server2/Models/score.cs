namespace Server2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("score")]
    public partial class score
    {
        [Key]
        [Column(Order = 0)]
        public int user_id { get; set; }

        [Key]
        [Column("score", Order = 1)]
        [StringLength(200)]
        public string score1 { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime updated_at { get; set; }

        public virtual user user { get; set; }
    }
}
