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
            GameGenres = new HashSet<GameGenre>();
        }

        public string GameId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Multiplayer")]
        public bool IsMultiplayer { get; set; }

 
        public DateTime CreateDate { get; set; }

 
        public DateTime EditDate { get; set; }

 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameGenre> GameGenres { get; set; }

  
    }
}
