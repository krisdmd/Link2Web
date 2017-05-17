using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ICurrencyRepository: IDisposable
    {
        IEnumerable<Currency> GetCurrencies();
        Currency GetCurrencyById(int id);
        void InsertCurrency(Currency currency);
        void DeleteCurrency(int id);
        void UpdateCurrency(Currency currency);
        void Save();
    }
}
