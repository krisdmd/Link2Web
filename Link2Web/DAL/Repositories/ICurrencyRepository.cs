using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ICurrencyRepository: IDisposable
    {
        List<Currency> GetCurrencies();
        Currency GetCurrencyById(int id);
        void InsertCurrency(Currency currency);
        void DeleteCurrency(int id);
        void UpdateCurrency(Currency currency);
        void Save();
    }
}
