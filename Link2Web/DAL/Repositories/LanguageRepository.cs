using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class LanguageRepository: ILanguageRepository
    {
        private Link2WebDbContext _context;
        private bool _disposed;
        public LanguageRepository(Link2WebDbContext context)
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

        public IEnumerable<Language> GetLanguages()
        {
            return _context.Languages.ToList();
        }

        public Language GetLanguageById(int id)
        {
            return _context.Languages.Find(id);
        }

        public void InsertLanguage(Language language)
        {
            _context.Languages.Add(language);
        }

        public void DeleteLanguage(int id)
        {
            var language = _context.Languages.Find(id);
            _context.Languages.Remove(language);
        }

        public void UpdateLanguage(Language language)
        {
            _context.Entry(language).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}