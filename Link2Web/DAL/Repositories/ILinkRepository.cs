using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ILinkRepository: IDisposable
    {
        IEnumerable<Link> GetLinks(int projectId);
        Link GetLinkById(int id, string userId);
        void InsertLink(Link link);
        void DeleteLink(int id);
        void UpdateLink(Link link);
        void Save();
    }
}
