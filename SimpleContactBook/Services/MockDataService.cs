﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleContactBook.Models;

namespace SimpleContactBook.Services
{
    public class MockDataService : IContactDataService
    {
        private IEnumerable<Contact> _contacts;

        public MockDataService()
        {
            _contacts = new List<Contact>()
            {
                new Contact
                {
                    Name = "Arda Burak Atila",
                    PhoneNumbers = new string[]
                    {
                        "334-434-343-43",
                        "334-434-343-43"
                    },
                    Emails = new string[]
                    {
                        "arda_atila@hotmail.com",
                        "arda_atila@hotmail.com"
                    },
                    Locations = new string[]
                    {
                        "344 Example",
                        "7644 er"
                    }
                },
                new Contact
                {
                    Name = "Arda Burak Atila",
                    PhoneNumbers = new string[]
                    {
                        "555-333-3333",
                        "555-444-4444"
                    },
                    Emails = new string[]
                    {
                        "arda_atila@hotmail.com",
                        "arda_atila@hotmail.com"
                    },
                    Locations = new string[]
                    {
                        "111 Example street",
                        "43 example Ave"
                    }
                },
            };
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _contacts;
        }

        public void Save(IEnumerable<Contact> contacts)
        {
            _contacts = contacts;
        }
    }
}
