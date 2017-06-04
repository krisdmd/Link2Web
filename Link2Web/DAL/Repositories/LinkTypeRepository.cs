using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL.Repositories
{
    public class LinkTypeRepository : ILinkTypeRepository
    {
        private Link2WebDbContext _context;
        private bool _disposed;
        public LinkTypeRepository(Link2WebDbContext context)
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

        public List<LinkType> GetLinkTypes()
        {
            return _context.LinkTypes.ToList();
        }

        public LinkType GetLinkTypeById(int id)
        {
            return _context.LinkTypes.Find(id);
        }

        public void InsertLinkType(LinkType linkType)
        {
            _context.LinkTypes.Add(linkType);
        }

        public void DeleteLinkType(int id)
        {
            var linkType = _context.LinkTypes.Find(id);
            _context.LinkTypes.Remove(linkType);
        }

        public void UpdateLinkType(LinkType linkType)
        {
            _context.Entry(linkType).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}