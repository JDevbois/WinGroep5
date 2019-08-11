using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Commands;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private List<Company> _searchResults;
        private List<String> _allowedTypes;
        private String _nameFilter;
        private String _typeFilter;
        private bool _hasPromotionsFilter;
        public DelegateCommand SearchCommand { get; set; }

        public List<Company> SearchResults { get => _searchResults; set
            {
                _searchResults = value;
                NotifyPropertyChanged(nameof(SearchResults));
            }
        }

        public string NameFilter { get => _nameFilter; set
            {
                _nameFilter = value;
                NotifyPropertyChanged(nameof(NameFilter));
            }
        }

        public string TypeFilter { get => _typeFilter; set
            {
                _typeFilter = value;
                NotifyPropertyChanged(nameof(TypeFilter));
            }
        }

        public bool HasPromotionsFilter { get => _hasPromotionsFilter; set
            {
                _hasPromotionsFilter = value;
                NotifyPropertyChanged(nameof(HasPromotionsFilter));
            }
        }
        public List<String> AllowedTypes
        { get => _allowedTypes; set
            {
                _allowedTypes = value;
                NotifyPropertyChanged(nameof(AllowedTypes));
            }

        }

        public SearchViewModel()
        {
            HasPromotionsFilter = false;
            AllowedTypes = new Company().AllowedTypes.Prepend("Don't filter on type").ToList();
            NotifyPropertyChanged(nameof(AllowedTypes));
            SearchCommand = new DelegateCommand(Search);
        }

        private void Search(object parameter)
        {
            try
            {
                SearchResults = new CompanyManager().Search(_nameFilter, _typeFilter, _hasPromotionsFilter);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
