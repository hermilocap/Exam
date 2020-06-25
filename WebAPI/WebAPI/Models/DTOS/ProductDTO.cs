using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DTOS
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public int CantidadInventario { get; set; }
    }
}
