using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravellerApp.Model;

namespace TravellerApp.Logic
{
    public class LocationLogic
    {
        public async static Task<List<Address>> GetLocation(double latitude, double longitude)
        {
            List<Address> addresses = new List<Address>();
            var url = Location.GenerateUrl(latitude, longitude);
            Console.WriteLine(url);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<Location>(json);
                addresses = address.addresses;
            }
            return addresses;
        }
    }
}
