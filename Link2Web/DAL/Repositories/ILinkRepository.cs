using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ILinkRepository: IDisposable
    {
        IEnumerable<Link> GetLinks();
        Link GetLinkById(int id);
        void InsertLink(Link link);
        void DeleteLink(int id);
        void UpdateLink(Link link);
        void Save();
    }
}
