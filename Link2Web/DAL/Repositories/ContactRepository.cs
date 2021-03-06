﻿using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class ContactRepository: IContactRepository
    {
        private Link2WebDbContext _context;
        private bool _disposed;
        public ContactRepository(Link2WebDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetContactById(int id, string userId)
        {
            return _context.Contacts.FirstOrDefault(c => c.ContactId == id && c.UserId.Equals(userId));

        }

        public void InsertContact(Contact contact)
        {
            _context.Contacts.Add(contact);
        }

        public void DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            _context.Contacts.Remove(contact);
        }

        public void UpdateContact(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}