using System.Collections.ObjectModel;
using System.ComponentModel;

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel
    {
#if DESIGN
        private void _WireDesignerData()
        {
            if (!DesignerProperties.IsInDesignTool) return;

            var challenge = new Challenge()
            {
                Id = 1,
                Title = "another trial",
                Description = "A new challenge to fight over!",
                Created = System.DateTime.Now,
                Modified = System.DateTime.Now,
                Location = new System.Device.Location.GeoPosition<System.Device.Location.GeoCoordinate>(
                    new System.DateTimeOffset(),
                    new System.Device.Location.GeoCoordinate(32, 32)),
                Image = "SampleData/Images/img1.jpg",
                SolvedCount = 3,
                MissedCount = 2
            };
            
            var challenge2 = new Challenge()
            {
                Id = 1,
                Title = "On the edge",
                Description = "Of the world",
                Created = System.DateTime.Now,
                Modified = System.DateTime.Now,
                Location = new System.Device.Location.GeoPosition<System.Device.Location.GeoCoordinate>(
                    new System.DateTimeOffset(),
                    new System.Device.Location.GeoCoordinate(32, 32)),
                Image = "SampleData/Images/img2.jpg",
                SolvedCount = 2,
                MissedCount = 8
            };
            Challenges.Add(challenge);
            Challenges.Add(challenge2);
        }
#endif
    }
}
