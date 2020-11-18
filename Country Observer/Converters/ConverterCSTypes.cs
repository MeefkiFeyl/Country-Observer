using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Country_Observer.Converters
{
    public static class ConverterCSTypes
    {
        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration) => new ObservableCollection<T>(enumeration);
    }
}
