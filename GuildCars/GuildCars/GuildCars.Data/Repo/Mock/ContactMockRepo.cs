using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.EF;
using GuildCars.Data.Interfaces;

namespace GuildCars.Data.Repo.Mock
{
    public class ContactMockRepo : IContactRepo
    {
        public static List<Contact> _contacts;

        static ContactMockRepo()
        {
            _contacts = new List<Contact>
           {
               new Contact
               {
                   ContactId =1,ContactEmail="stuff@things.com",ContactName="Stuff Smith",ContactPhone="123-456-7890",ContactMessage="I had a great time buying a car from you!"
               }
            };
        }

        public void Insert(Contact contact)
        {
            _contacts.Add(contact);
        }

        public int GetCount()
        {
            return _contacts.Count();
        }
    }
}
