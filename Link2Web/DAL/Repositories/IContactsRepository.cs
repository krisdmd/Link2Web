using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface IContactsRepository: IDisposable
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(int id);
        void InsertContact(Contact contact);
        void DeleteContact(int id);
        void UpdateContact(Contact contact);
        void Save();
    }
}
