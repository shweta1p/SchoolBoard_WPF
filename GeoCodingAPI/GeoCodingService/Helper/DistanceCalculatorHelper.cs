using GeoCodingService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingService.Helper
{
    public class DistanceCalculatorHelper
    {
        const double PIx = 3.141592653589793;
        const double RADIUS = 6371;

        static List<SchoolEntity> schoolEntities = new List<SchoolEntity>();
        static List<StudentEntity> studentEntities = new List<StudentEntity>();

        public static void CalculateDistanceFromSchool()
        {
            using (EFDBContext db = new EFDBContext())
            {
                schoolEntities = db.schoolEntities.Where(p => p.ADDRESS != null && p.LATITUDE != null && p.LONGITUDE != null).ToList();

                studentEntities = db.studentEntities.Where(p => p.ADDRESS != null && p.LATITUDE != null && p.LONGITUDE != null).ToList();

                Console.WriteLine("Total Students : " + studentEntities.Count);

                if (studentEntities.Count == 0)
                {
                    Console.WriteLine("No Data");
                }

                try
                {
                    if (studentEntities.Count > 0)
                    {
                        for (int i = 0; i < studentEntities.Count; i++)
                        {
                            double latitude = Convert.ToDouble(studentEntities[i].LATITUDE);
                            double longitude = Convert.ToDouble(studentEntities[i].LONGITUDE);

                            int schoolId = studentEntities[i].SCHOOL_ID;

                            double sLatitude = Convert.ToDouble(schoolEntities.Where(s => s.ID == schoolId).Select(s => s.LATITUDE).FirstOrDefault());
                            double sLongitude = Convert.ToDouble(schoolEntities.Where(s => s.ID == schoolId).Select(s => s.LONGITUDE).FirstOrDefault());

                            double distance = DistanceBetweenPlaces(sLongitude, sLatitude, longitude, latitude);

                            Console.WriteLine("Distance " + i + " : " + distance);

                            studentEntities[i].Distance1 = distance.ToString();

                            Console.WriteLine("Processed Record : " + i);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in thread : " + ex);
                }
                db.SaveChanges();
            }
        }


        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double dlon = Radians(lon2 - lon1);
            double dlat = Radians(lat2 - lat1);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * RADIUS;
        }
    }
}
