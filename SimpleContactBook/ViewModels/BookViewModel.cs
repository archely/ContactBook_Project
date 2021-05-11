using SimpleContactBook.Services;
using SimpleContactBook.Utilty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleContactBook.ViewModels
{
    /// <summary>
    /// View model for the Book View Model.
    /// </summary>
    
    public class BookViewModel : ObservableObject 
    {

        /// <summary>
        /// Every contacts save to database. Database provide to separate favourite and normal contacts
        /// </summary>

        private IContactDataService _dataService;
        private IDialogService _dialogService;

        private ContactsViewModel _contactsVM; // VM is View Model
        public ContactsViewModel ContactsVM 
        {
            get { return _contactsVM; }
            set { OnPropertyChanged(ref _contactsVM, value); } 
        }

        public ICommand LoadContactsCommand { get; set; } 
        public ICommand LoadFavoritesCommand { get; set; }

        public BookViewModel(IContactDataService dataService, IDialogService dialogService) // Load contact and favorite side from dataService
        {
            ContactsVM = new ContactsViewModel(dataService, dialogService);

            _dataService = dataService;

            LoadContactsCommand = new RelayCommand(LoadContacts); 
            LoadFavoritesCommand = new RelayCommand(LoadFavorites);
        }

        private void LoadContacts()
        {
            ContactsVM.LoadContacts(_dataService.GetContacts());
        }

        private void LoadFavorites()
        {
            var favorites = _dataService.GetContacts().Where(c => c.IsFavorite);
            ContactsVM.LoadContacts(favorites);
        }
    }
}
