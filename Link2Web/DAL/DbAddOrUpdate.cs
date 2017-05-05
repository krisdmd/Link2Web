using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Link2Web.Models;

namespace Link2Web.DAL
{
    public class DbAddOrUpdate
    {
        public void AddOrUpdateUserSetting(Link2WebDbContext context, UserSetting userSetting)
        {

            var userSettingInDb = context.UserSettings.Count(u => u.Setting == userSetting.Setting) > 0;
            context.Entry(userSetting).State = (userSettingInDb ? EntityState.Modified : EntityState.Added);
            context.SaveChanges();
        }
    }
}