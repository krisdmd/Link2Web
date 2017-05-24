using Google.Apis.Json;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Link2Web.DAL
{
    public class EfDataStore: IDataStore
    {
        private string _userId;

        public EfDataStore()
        {
            _userId = HttpContext.Current.User.Identity.GetUserId();
        }
        public async Task ClearAsync()
        {
            using (var context = new Link2WebDbContext())
            {
                var objectContext = ((IObjectContextAdapter)context).ObjectContext;
                await objectContext.ExecuteStoreCommandAsync("TRUNCATE TABLE [dbo.AnalyticsTokens]");
            }
        }

        public async Task DeleteAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            using (var context = new Link2WebDbContext())
            {
                var item = context.GoogleUsers.FirstOrDefault(x => x.UserId == _userId);
                if (item != null)
                {
                    context.GoogleUsers.Remove(item);
                    await context.SaveChangesAsync();
                }
            }
        }

        public Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            using (var context = new Link2WebDbContext())
            {
                var item = context.GoogleUsers.FirstOrDefault(x => x.UserId == _userId);
                T value = item == null ? default(T) : NewtonsoftJsonSerializer.Instance.Deserialize<T>(item.RefreshToken);
                return Task.FromResult<T>(value);
            }
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            using (var context = new Link2WebDbContext())
            {
                string json = NewtonsoftJsonSerializer.Instance.Serialize(value);

                var item = await context.GoogleUsers.SingleOrDefaultAsync(x => x.UserId == _userId);

                if (item == null)
                {
                    context.GoogleUsers.Add(new Entities.GoogleUser
                    {
                        Username = key,
                        RefreshToken = json,
                        UserId = HttpContext.Current.User.Identity.GetUserId()
                    });
                }
                else
                {
                    item.RefreshToken = json;
                }

                await context.SaveChangesAsync();
            }
        }

        private static string GenerateStoredKey(string key, Type t)
        {
            return $"{t.FullName}-{key}";
        }
    }
}