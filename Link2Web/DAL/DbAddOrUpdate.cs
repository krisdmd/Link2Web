using Link2Web.Models;
using System.Data.Entity;
using System.Linq;

namespace Link2Web.DAL
{
    public class DbAddOrUpdate
    {
        public void AddOrUpdateUserSetting(Link2WebDbContext context, UserSetting userSetting)
        {
            var userSettingInDb = context.UserSettings.Count(u => u.Setting == userSetting.Setting) > 0;

            if (userSettingInDb)
            {
                context.UserSettings.Add(userSetting);
            }
            else
            {
                context.Entry(userSetting).State = EntityState.Modified;
            }


           // context.Entry(userSetting).State = (userSettingInDb ? EntityState.Modified : EntityState.Added);
            context.SaveChanges();
        }
    }
}