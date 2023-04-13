using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerApp.Helpers
{
    public class Constants
    {
        public const string LOCATION_SEARCH = "https://{0}/search/{1}/reverseGeocode/{2}.json?key={3}";
        public const string BASE_URL = "api.tomtom.com";
        public const string VERSION_NUM = "2";
    }
}
