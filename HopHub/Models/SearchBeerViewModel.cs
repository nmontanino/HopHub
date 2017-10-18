using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopHub.Models
{
    public class SearchBeerViewModel
    {
        [Required]
        [Display(Name = "Enter search term:")]
        public string Query { get; set; }

        // TODO: Include list of Beer objects to be returned during search
    }
}
