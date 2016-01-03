using DataBaseLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows.Media.Media3D;

namespace QuickErrandsWebApi.Models
{
    public class Coordinate : IEntity
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }        

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static implicit operator double[](Coordinate instance)
        {
            return new double[] { instance.Latitude, instance.Longitude };
        }

        public static implicit operator Coordinate(double[] instance)
        {
            Contract.Requires(instance != null && instance.Length == 3);

            return new Coordinate(instance[0], instance[1]);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Latitude.ToString(CultureInfo.GetCultureInfo("en-US")), Longitude.ToString(CultureInfo.GetCultureInfo("en-US"))).ToString(new CultureInfo("ru-RU", false));
        }
    }
}