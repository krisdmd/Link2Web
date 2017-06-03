using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ILanguageRepository: IDisposable
    {
        IEnumerable<Language> GetLanguages();
        Language GetLanguageById(int id);
        void InsertLanguage(Language language);
        void DeleteLanguage(int id);
        void UpdateLanguage(Language language);
        void Save();
    }
}
