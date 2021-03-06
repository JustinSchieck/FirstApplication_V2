namespace FirstApplication_V2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameGenre
    {
        [Key]
        public string GameGenreId { get; set; }


        [Required]
        [StringLength(128)]
        [Display(Name = "Game")]
        public string GameId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Genre")]
        public string GenreId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
