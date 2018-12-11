using System.Collections.ObjectModel;

namespace SharedLib
{
    public class Merchant : BasePerson
    {
        public ObservableCollection<Company> Companies { get; set; }
    }
}
