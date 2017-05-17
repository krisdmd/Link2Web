using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ILinkStatusRepository: IDisposable
    {
        IEnumerable<LinkStatus> GetLinkStatuses();
        LinkStatus GetLinkStatusById(int id);
        void InsertLinkStatus(Project linkStatus);
        void DeleteLinkStatus(int id);
        void UpdateLinkStatus(LinkStatus linkStatus);
        void Save();
    }
}
