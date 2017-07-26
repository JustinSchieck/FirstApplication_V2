namespace FirstApplication_V2.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Genres = new HashSet<GameGenre>();
        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string GameId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Multiplayer")]
        public bool IsMultiplayer { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        //[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime EditDate { get; set; }

  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [InverseProperty("Game")]
        public virtual ICollection<GameGenre> Genres { get; set; }

        [Display(Name = "Ratings")]
        [InverseProperty("Game")]
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

        //public decimal Rating
        //{
        //    get
        //    {
        //        return (Ratings.Average(x => x.Rank));
        //    }
        //}

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }


    }
}
