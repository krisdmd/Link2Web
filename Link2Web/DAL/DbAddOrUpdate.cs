using Link2Web.Models;
using System.Linq;

namespace Link2Web.DAL
{
    public class DbAddOrUpdate
    {
        public void AddOrUpdateUserSetting(UserSetting userSetting, string userId)
        {
            var context = new Link2WebDbContext();
            var userSettingInDb = context.UserSettings.Where(u => u.UserId.Equals(userId)).Count(u => u.Setting == userSetting.Setting) > 0;

            if (userSettingInDb)
            {
                var user = context.UserSettings.FirstOrDefault(u => u.UserId == userId && u.Setting == userSetting.Setting);
                if (user != null) user.ValueInt = userSetting.ValueInt;
            }
            else
            {
                context.UserSettings.Add(userSetting);
            }

            context.SaveChanges();
        }
    }
}