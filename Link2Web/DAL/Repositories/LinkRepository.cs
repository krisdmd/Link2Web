using Link2Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class LinkRepository: ILinkRepository
    {
        private Link2WebDbContext _context;

        private bool _disposed;
        public LinkRepository(Link2WebDbContext context)
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

        public IEnumerable<Link> GetLinks()
        {
            return _context.Links.ToList();
        }

        public Link GetLinkById(int id, string userId)
        {
            return _context.Links.FirstOrDefault(l => l.LinkId == id && l.UserId.Equals(userId));
        }

        public void InsertLink(Link link)
        {
            _context.Links.Add(link);
        }

        public void DeleteLink(int id)
        {
            var link = _context.Links.Find(id);
            _context.Links.Remove(link);
        }

        public void UpdateLink(Link link)
        {
            _context.Entry(link).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}