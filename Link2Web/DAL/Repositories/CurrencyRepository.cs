using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class CurrencyRepository: ICurrencyRepository
    {
        private Link2WebDbContext _context;
        private bool _disposed;
        public CurrencyRepository(Link2WebDbContext context)
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

        public IEnumerable<Currency> GetCurrencies()
        {
            return _context.Currencies.ToList();
        }

        public Currency GetCurrencyById(int id)
        {
            return _context.Currencies.Find(id);
        }

        public void InsertCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
        }

        public void DeleteCurrency(int id)
        {
            var currency = _context.Currencies.Find(id);
            _context.Currencies.Remove(currency);
        }

        public void UpdateCurrency(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}