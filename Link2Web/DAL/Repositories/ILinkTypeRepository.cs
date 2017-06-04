using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ILinkTypeRepository: IDisposable
    {
        List<LinkType> GetLinkTypes();
        LinkType GetLinkTypeById(int id);
        void InsertLinkType(LinkType linkType);
        void DeleteLinkType(int id);
        void UpdateLinkType(LinkType linkType);
        void Save();
    }
}
