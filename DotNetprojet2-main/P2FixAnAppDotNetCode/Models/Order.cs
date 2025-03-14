using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P2FixAnAppDotNetCode.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Il manque le nom")]
        public string Name { get; set; }

        [Required(ErrorMessage = "l'adresse est manquante")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ville est manquante")]
        public string City { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Il manque le pays")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
