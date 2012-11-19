using System;
using System.Device.Location;
using System.Windows.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System.Runtime.Serialization;
using standrighthere.Utilities;

namespace standrighthere
{
    [DataTable(Name = "challenges")]
    public class Challenge
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }
        
        public string CreatedRelative
        { 
            get
            {
                return TimeAgo.GetTimeAgo(Created);
            }
        }
        
        [DataMember(Name = "modified")]
        public DateTime Modified { get; set; }
        
        [DataMember(Name = "location")]
        public GeoPosition<GeoCoordinate> Location { get; set; }
        
        [DataMember(Name = "image")]
        public string Image { get; set; }
        
        [DataMember(Name = "user_id")]
        public MobileServiceUser UserId { get; set; }

        [DataMember(Name = "solved_count")]
        public int SolvedCount { get; set; }

        [DataMember(Name = "missed_count")]
        public int MissedCount { get; set; }
    }
}