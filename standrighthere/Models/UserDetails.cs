using System;
using Windows.Devices.Geolocation;
using System.Windows.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System.Runtime.Serialization;
using standrighthere.Utilities;

namespace standrighthere.Models
{
    [DataTable(Name = "user_details")]
    public class UserDetails
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "user_id")]
        public string UserId { get; set; }
    }
}