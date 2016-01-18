using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class ImageUpload
    {
        string image_data;
        string city;
        string country;
        public string ImageData
        {
            get { return image_data; }
            set { image_data = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
    }
}
