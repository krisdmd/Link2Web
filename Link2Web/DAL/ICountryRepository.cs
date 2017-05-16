using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL
{
    public interface ICountryRepository: IDisposable
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryById(int id);
        void InsertCountry(Country country);
        void DeleteCountry(int id);
        void UpdateCountry(Country country);
        void Save();
    }
}
