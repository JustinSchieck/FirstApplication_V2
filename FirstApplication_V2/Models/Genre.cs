namespace FirstApplication_V2.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre()
        {
            GameGenres = new HashSet<GameGenre>();
        }

        public string GenreId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Genre Name")]
        public string Name { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime EditDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameGenre> GameGenres { get; set; }

        public String GetGameNames()
        {
            ArrayList temp = new ArrayList();
            foreach (GameGenre g in GameGenres)
            {
                temp.Add(g.Game.Name);
            }
            return string.Join(", ", temp.ToArray());
        }
    }
}
