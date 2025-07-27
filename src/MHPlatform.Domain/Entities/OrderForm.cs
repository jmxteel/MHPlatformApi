using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities
{
    public class OrderForm
    {
        [Key]
        public int ID { get; set; }
        public string? Ordrno { get; set; } = string.Empty;

        public string? Ctitle { get; set; } = string.Empty;

        public string? CName { get; set; } = string.Empty;

        public string? CSurname { get; set; } = string.Empty;

        public string? CAdd { get; set; } = string.Empty;

        public string? InsAdd { get; set; } = string.Empty;

        public string? CCon { get; set; } = string.Empty;

        public string? Cmobile { get; set; } = string.Empty;

        public string? CFax { get; set; } = string.Empty;

        public string? ConPrsn { get; set; } = string.Empty;
    }
}
