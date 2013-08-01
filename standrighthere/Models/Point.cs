using Microsoft.WindowsAzure.MobileServices;
using System.Runtime.Serialization;

namespace standrighthere.Models
{
    [DataTable(Name = "points")]
    public class Point
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Badge { get; set; }

        [DataMember(Name = "points")]
        public int Points { get; set; }
    }
}