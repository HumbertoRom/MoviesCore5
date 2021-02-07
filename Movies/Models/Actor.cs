using Microsoft.AspNetCore.Http;
using Movies.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Actor
    {
        [Key]
        public int ActorID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
    }

    public class ActorDto
    {
        [Key]
        public int ActorID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
    }

    public class CreateActorDto
    {
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede contener mas de 40 carácteres")]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        [FileWeightValidation(MaxSizeMegaBytes: 4)]
        [TypeFileValidation(typeFile: TypeFile.Image)]
        public IFormFile Photo { get; set; }
    }
}
