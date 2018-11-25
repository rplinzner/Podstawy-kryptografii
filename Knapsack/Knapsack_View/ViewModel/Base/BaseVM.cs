using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Knapsack_View.Annotations;

namespace Knapsack_View.ViewModel.Base
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}