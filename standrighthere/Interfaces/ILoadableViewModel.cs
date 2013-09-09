using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standrighthere.Interfaces
{
    public abstract class ILoadableViewModel
    {
        public bool IsDataLoading { get; set; }
        public bool IsDataLoaded { get; set; }

        public async Task LoadData(bool forceReload = false)
        {
            if (forceReload || (!IsDataLoading && !IsDataLoaded))
            {
                IsDataLoading = true;

                await LoadDataImpl(forceReload);

                IsDataLoading = false;
                IsDataLoaded = true;
            }
        }

        protected abstract Task LoadDataImpl(bool forceReload = false);
    }
}
