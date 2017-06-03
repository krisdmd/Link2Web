using Link2Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface ISettingRepository: IDisposable
    {
        IEnumerable<Setting> GetSettings();
        Setting GetSettingById(int id);
        void InsertSetting(Setting setting);
        void DeleteSetting(int id);
        void UpdateSetting(Setting setting);
        void Save();
    }
}
