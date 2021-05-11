using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleContactBook.Models;

namespace SimpleContactBook.Services
{

    /// <summary>
    /// Services for contact data services. Add Newtonsoft_Json
    /// </summary>
    
    public class JsonContactDataService : IContactDataService
    {
        /// <summary>
        /// Convert for json format
        /// </summary>
        private readonly string _dataPath = "Resources/contactdata.json"; // From resources folder

        public IEnumerable<Contact> GetContacts() // Enumerator
        {
            if(!File.Exists(_dataPath))  // Check file
            {
                File.Create(_dataPath).Close();
            }

            var serializedContacts = File.ReadAllText(_dataPath);
            var contacts = JsonConvert.DeserializeObject<IEnumerable<Contact>>(serializedContacts);

            if (contacts == null)
                return new List<Contact>();

            return contacts;
        }

        public void Save(IEnumerable<Contact> contacts)
        {
            var serializedContacts = JsonConvert.SerializeObject(contacts);
            File.WriteAllText(_dataPath, serializedContacts);
        }
    }
}
