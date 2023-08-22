using GeoCodingService.Entity;
using System.Configuration;
using System.IO;
using System.Web;


namespace GeoCodingService.Helper
{
    public class ContryImageHelper
    {
        static string fileName = string.Empty;
        
        public static void InsertCountryImage()
        {
            string imagePath = ConfigurationManager.AppSettings["ImagePath"];
            using (EFDBContext db = new EFDBContext())
            {
                if (Directory.Exists(imagePath))
                {
                    foreach (var file in new DirectoryInfo(imagePath).GetFiles())
                    {
                        fileName = file.Name;
                        string extension = Path.GetExtension(fileName);

                        if (extension == ".png" || extension == "jpg")
                        {
                            CountryEntity countryEntity = new CountryEntity();

                            string country = Path.GetFileNameWithoutExtension(fileName);

                            //Convert Image to byte Array
                            byte[] img = converterImage(file.FullName);

                            countryEntity.Country = country;
                            countryEntity.Image = img;

                            db.countryEntities.Add(countryEntity);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public static byte[] converterImage(string path)
        {
            byte[] imgdata = File.ReadAllBytes(path);

            return imgdata;
        }
    }
}
