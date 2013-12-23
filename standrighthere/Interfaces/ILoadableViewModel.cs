using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standrighthere.Interfaces
{
    public abstract class ILoadableViewModel : INotifyPropertyChanged
    {
        public bool IsDataLoading { get; set; }
        public bool IsDataLoaded { get; set; }

        public async Task LoadData(bool forceReload = false)
        {
            if (forceReload || (!IsDataLoading && !IsDataLoaded))
            {
                IsDataLoading = true;
                NotifyPropertyChanged("IsDataLoading");

                await LoadDataImpl(forceReload);

                IsDataLoading = false;
                IsDataLoaded = true;
                NotifyPropertyChanged("IsDataLoading");
                NotifyPropertyChanged("IsDataLoaded");
            }
        }

        protected abstract Task LoadDataImpl(bool forceReload = false);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
