using Link2Web.Models;
using System.Linq;

namespace Link2Web.DAL
{
    public class DbAddOrUpdate
    {
        public void AddOrUpdateUserSetting(Link2WebDbContext context, UserSetting userSetting, string userId)
        {
            var userSettingInDb = context.UserSettings.Where(u => u.UserId.Equals(userId)).Count(u => u.Setting == userSetting.Setting) > 0;

            if (userSettingInDb)
            {
               // context.Entry(userSetting).State = EntityState.Modified;
            }
            else
            {
                context.UserSettings.Add(userSetting);
            }

            context.SaveChanges();
        }
    }
}