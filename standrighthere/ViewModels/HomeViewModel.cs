using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parse;

namespace standrighthere.ViewModels
{
    public partial class HomeViewModel
    {
        private IEnumerable<ParseObject> Challenges { get; set; }

        public bool IsDataLoaded
        {
            get; private set;
        }

        public async void LoadData()
        {
            if (ParseUser.CurrentUser != null)
            {
                var query = ParseObject.GetQuery("Challenges");
                query.WhereNear("location", ParseUser.CurrentUser.Get<ParseGeoPoint>("location"));
                query.Limit(20);
                Challenges = await query.FindAsync();

                IsDataLoaded = true;
            }
        }
    }
}
