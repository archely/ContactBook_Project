using SimpleContactBook.Models;
using SimpleContactBook.Services;
using SimpleContactBook.Utilty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleContactBook.ViewModels
{
    /// <summary>
    /// View model for the contact View.
    /// </summary>
    
    public class ContactsViewModel : ObservableObject
    {
        private Contact _selectedContact; 
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set { OnPropertyChanged(ref _selectedContact, value); }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                OnPropertyChanged(ref _isEditMode, value);
                OnPropertyChanged("IsDisplayMode");
            }
        }

        public bool IsDisplayMode
        {
            get { return !_isEditMode; }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public ICommand EditCommand { get;  set; } //Get edit command
        public ICommand SaveCommand { get;  set; } //Get save command
        public ICommand UpdateCommand { get;  set; } //Get Update command
        public ICommand BrowseImageCommand { get;  set; } // Get browse image command
        public ICommand AddCommand { get;  set; } // Get add command
        public ICommand DeleteCommand { get;  set; } // Get delete command

        private IContactDataService _dataService;
        private IDialogService _dialogService;

        public ContactsViewModel(IContactDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;

            EditCommand = new RelayCommand(Edit, CanEdit); // Add edit
            SaveCommand = new RelayCommand(Save, IsEdit); // Add save command
            UpdateCommand = new RelayCommand(Update); // Add update command
            BrowseImageCommand = new RelayCommand(BrowseImage, IsEdit); // Add browser command ,you can choose any picture from your computer
            AddCommand = new RelayCommand(Add); // Add add command
            DeleteCommand = new RelayCommand(Delete, CanDelete); // Delete command, you can delete your contacts
        }

        private void Delete()
        {
            Contacts.Remove(SelectedContact);
            Save();
        }

        private bool CanDelete()
        {
            return SelectedContact == null ? false : true;
        }

        private void Add()
        {
            var newContact = new Contact  // Created new contact, empty bars (phone numbers, emails,location)
            {
                Name = "N/A",
                PhoneNumbers = new string[2],
                Emails = new string[2],
                Locations = new string[2]
            };

            Contacts.Add(newContact);
            SelectedContact = newContact;
        }

        private void BrowseImage()
        {
            var filePath = _dialogService.OpenFile("Image files|*.bmp;*.jpg;*.jpeg;*.png|All files");
            SelectedContact.ImagePath = filePath;
        }

        private void Update() // Saves to dataservice file 
        {
            _dataService.Save(Contacts);
        }

        private void Save() //Save button
        {
            _dataService.Save(Contacts);
            IsEditMode = false;
            OnPropertyChanged("SelectedContact");
        }

        private bool IsEdit() //Call the edit 
        {
            return IsEditMode;
        }

        private bool CanEdit()  
        {
            if (SelectedContact == null)
                return false;

            return !IsEditMode;
        }

        private void Edit()
        {
            IsEditMode = true;
        }

        public void LoadContacts(IEnumerable<Contact> contacts)
        {
            Contacts = new ObservableCollection<Contact>(contacts);
            OnPropertyChanged("Contacts");
        }
    }
}
