using GeoCodingService.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ////----------------GeoCoding Application for Lat Long----------------//
                //string addressType = ConfigurationManager.AppSettings["AddressType"];

                //if (addressType.ToUpper() == "SCHOOL")
                //{
                //    GeoCallHelper.executeSchool();
                //}
                //else if (addressType.ToUpper() == "STUDENT")
                //{
                //    GeoCallHelper.executeStudent();
                //}

                ////----------------Code for converting Country Image to Image Array----------------//
                //ContryImageHelper.InsertCountryImage();

                //-------------Distance Calculation from Lat and Long-------------//
                //DistanceCalculatorHelper.CalculateDistanceFromSchool();

                //------------------ Calculation for Latitude and Longitude-----------------//
                NearbyPlacesHelper.executePlaces();

                //-----------------Find nearby Places-----------------//
                //NearbyPlacesHelper.FindNearbyPlaces();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
