using eBilet.Data.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBilet.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage ="Cinema logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="Cinema name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
		[Required(ErrorMessage = "Cinema description is required")]
		public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }

        



    }
}
