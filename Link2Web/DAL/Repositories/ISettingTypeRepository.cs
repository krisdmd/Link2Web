using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ISettingTypeRepository: IDisposable
    {
        IEnumerable<SettingType> GetSettingTypes();
        SettingType GetSettingTypeById(int id);
        void InsertSettingType(SettingType settingType);
        void DeleteSettingType(int id);
        void UpdateSettingType(SettingType settingType);
        void Save();
    }
}
