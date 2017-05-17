using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class CountryRepository: ICountryRepository, IDisposable
    {
        private Link2WebDbContext _context;
        private bool _disposed;
        public CountryRepository(Link2WebDbContext context)
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

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.Find(id);
        }

        public void InsertCountry(Country country)
        {
            _context.Countries.Add(country);
        }

        public void DeleteCountry(int id)
        {
            var country = _context.Countries.Find(id);
            _context.Countries.Remove(country);
        }

        public void UpdateCountry(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}