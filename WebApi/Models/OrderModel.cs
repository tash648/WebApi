using DataBaseLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickErrandsWebApi.Models
{ 
    public class OrderModel : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string AddressFrom { get; set; }

        public string AddressTo { get; set; }

        public double Cost { get; set; }

        public string Commentary { get; set; }

        public virtual List<Coordinate> Coordinates { get; set; }
    }
}