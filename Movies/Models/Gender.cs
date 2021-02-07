using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Gender
    {
        [Key]
        public int GenderID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage ="El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
    }

    public class GenderDto
    {
        public int GenderID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
    }

    public class CreateGenderDto
    {
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
    }
}
