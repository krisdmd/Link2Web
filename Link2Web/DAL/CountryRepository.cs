using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL
{
    public class CountryRepository: ICountryRepository, IDisposable
    {
        private Link2WebDbContext _db;
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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
            return _db.Countries.ToList();
        }

        public Country GetCountryById(int id)
        {
            return _db.Countries.Find(id);
        }

        public void InsertCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public void DeleteCountry(int id)
        {
            var country = _db.Countries.Find(id);
            _db.Countries.Remove(country);
        }

        public void UpdateCountry(Country country)
        {
            _db.Entry(country).State = EntityState.Modified;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}