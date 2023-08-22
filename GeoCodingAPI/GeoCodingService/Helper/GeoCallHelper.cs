using GeoCodingService.Entity;
using GeoCodingService.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingService.Helper
{
    public class GeoCallHelper
    {
        static List<SchoolEntity> schoolEntities = new List<SchoolEntity>();
        static List<StudentEntity> studentEntities = new List<StudentEntity>();

        public static void executeSchool()
        {
            CommonUtility utility = new CommonUtility();
            using (EFDBContext db = new EFDBContext())
            {
                schoolEntities = db.schoolEntities.Where(p => p.ADDRESS != null && p.LATITUDE == null && p.LONGITUDE == null).ToList();

                Console.WriteLine("Total record : " + schoolEntities.Count);

                if (schoolEntities.Count == 0)
                {
                    Console.WriteLine("No Data");
                }
                try
                {
                    if (schoolEntities.Count > 0)
                    {
                        for (int i = 0; i < schoolEntities.Count; i++)
                        {
                            string address = schoolEntities[i].ADDRESS;

                            address = address != null ? address.Replace(" ", "+") : address;

                            //Call Google GeoCode API
                            string result = utility.GeoAPICall(address);

                            if (!string.IsNullOrEmpty(result))
                            {
                                Root response = JsonConvert.DeserializeObject<Root>(result);

                                if (response.results.Count() > 0) {
                                    string latitude = response.results[0].geometry.location.lat.ToString();
                                    string longitude = response.results[0].geometry.location.lng.ToString();

                                    schoolEntities[i].LATITUDE = latitude;
                                    schoolEntities[i].LONGITUDE = longitude;
                                }
                               
                            }

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

        public static void executeStudent()
        {
            CommonUtility utility = new CommonUtility();
            using (EFDBContext db = new EFDBContext())
            {
                studentEntities = db.studentEntities.Where(p => p.ADDRESS != null && p.LATITUDE == null && p.LONGITUDE == null).Take(80).ToList();

                Console.WriteLine("Total record : " + studentEntities.Count);

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
                            string address = studentEntities[i].ADDRESS;

                            address = address != null ? address.Replace(" ", "+") : address;

                            //Call Google GeoCode API
                            string result = utility.GeoAPICall(address);

                            if (!string.IsNullOrEmpty(result))
                            {
                                Root response = JsonConvert.DeserializeObject<Root>(result);

                                if (response.results.Count() > 0)
                                {
                                    string latitude = response.results[0].geometry.location.lat.ToString();
                                    string longitude = response.results[0].geometry.location.lng.ToString();

                                    studentEntities[i].LATITUDE = latitude;
                                    studentEntities[i].LONGITUDE = longitude;
                                }
                            }

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

    }
}
