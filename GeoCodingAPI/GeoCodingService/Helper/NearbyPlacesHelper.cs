using GeoCodingService;
using GeoCodingService.Entity;
using GeoCodingService.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeoCodingService.Helper
{
    public class NearbyPlacesHelper
    {
        static List<PlacesEntity> placesEntities = new List<PlacesEntity>();
        static List<NearestPlacesEntity> nearestPlacesEntities = new List<NearestPlacesEntity>();

        public static void executePlaces()
        {
            CommonUtility utility = new CommonUtility();
            using (EFDBContext db = new EFDBContext())
            {
                placesEntities = db.placesEntities.Where(p => p.Address != null && p.LATITUDE == null && p.LONGITUDE == null).ToList();

                Console.WriteLine("Total record : " + placesEntities.Count);

                if (placesEntities.Count == 0)
                {
                    Console.WriteLine("No Data");
                }
                try
                {
                    if (placesEntities.Count > 0)
                    {
                        for (int i = 0; i < placesEntities.Count; i++)
                        {
                            string address = placesEntities[i].Address; //.Split(',')[0]

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

                                    placesEntities[i].LATITUDE = latitude;
                                    placesEntities[i].LONGITUDE = longitude;
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

        public static void FindNearbyPlaces()
        {
            CommonUtility utility = new CommonUtility();
            using (EFDBContext db = new EFDBContext())
            {
                placesEntities = db.placesEntities.Where(p => p.Address != null && p.LATITUDE != null && p.LONGITUDE != null).Take(100).ToList();

                Console.WriteLine("Total record : " + placesEntities.Count);

                if (placesEntities.Count == 0)
                {
                    Console.WriteLine("No Data");
                }
                try
                {
                    if (placesEntities.Count > 0)
                    {
                        DataTable dtPlaces = new DataTable();
                        dtPlaces.Clear();
                        dtPlaces.Columns.Add("Name");
                        dtPlaces.Columns.Add("nLatitude");
                        dtPlaces.Columns.Add("nLongitude");
                        dtPlaces.Columns.Add("nName");
                        dtPlaces.Columns.Add("nRating");
                        dtPlaces.Columns.Add("nAddress");

                        for (int i = 0; i < placesEntities.Count; i++)
                        {
                            string address = placesEntities[i].Address.Split(',')[0];
                            address = address != null ? address.Replace(" ", "+") : address;

                            string lat = placesEntities[i].LATITUDE;
                            string lon = placesEntities[i].LONGITUDE;

                            string param = lat + "," + lon;

                            //Call Google Place search API
                            string result = utility.NearbyPlacesAPICall(address, param);

                            if (!string.IsNullOrEmpty(result))
                            {
                                Root response = JsonConvert.DeserializeObject<Root>(result);

                                if (response.results.Count() > 0)
                                {
                                    for (int r = 0; r < response.results.Count(); r++)
                                    {
                                        //string latitude = response.results[r].geometry.location.lat.ToString();
                                        //string longitude = response.results[r].geometry.location.lng.ToString();
                                        //string name = response.results[r].name.ToString();
                                        //string rating = response.results[r].rating.ToString();
                                        //string addr = response.results[r].vicinity.ToString();

                                        //DataRow row = dtPlaces.NewRow();
                                        //row["Name"] = placesEntities[i].Name;
                                        //row["nLatitude"] = latitude;
                                        //row["nLongitude"] = longitude;
                                        //row["nName"] = name;
                                        //row["nRating"] = rating;
                                        //row["nAddress"] = addr;
                                        //dtPlaces.Rows.Add(row);
                                    }
                                }
                            }
                            Console.WriteLine("Processed Record : " + i);
                        }

                        //--------------------Save Data-------------------//
                        foreach (DataRow dr in dtPlaces.Rows)
                        {
                            nearestPlacesEntities.Add(new NearestPlacesEntity
                            {
                                Name = dr["Name"].ToString(),
                                nLatitude = dr["nLatitude"].ToString(),
                                nLongitude = dr["nLongitude"].ToString(),
                                nName = dr["nName"].ToString(),
                                nRating = dr["nRating"].ToString(),
                                nAddress = dr["nAddress"].ToString()
                            });
                        }
                        db.nearestPlacesEntities.AddRange(nearestPlacesEntities);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in thread : " + ex);
                }

            }
        }
    }
}
