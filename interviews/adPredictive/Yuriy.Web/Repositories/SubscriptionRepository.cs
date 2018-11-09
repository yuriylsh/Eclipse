using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;
using Yuriy.Web.Data;

namespace Yuriy.Web.Repositories
{
    public class SubscriptionRepository: ISubscriptionRepository
    {
        private readonly AdPredictiveContext _context;

        public SubscriptionRepository(AdPredictiveContext context) => _context = context;

        public async Task<IEnumerable<Subscription>> GetByUser(int id) 
            => await _context.NotificationSubscription.FromSql(@"
SELECT NotificationType.Id, NotificationType.[Name], CASE WHEN unsubscribes.[User] IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS IsUnsubscribed
  FROM dbo.NotificationType
	   LEFT JOIN dbo.NotificationUnsubscribe unsubscribes
	          ON NotificationType.Id = unsubscribes.Type AND unsubscribes.[User] = {0}"
                , id).ToArrayAsync();

        public async Task<bool> AllNotificationTypesExist(int[] ids)
            => (await _context.NotificationType.Where(x => ids.Contains(x.Id)).CountAsync()) == ids.Length;

        public async Task UnsubscribeFromNotifications(int userId, IEnumerable<int> notificationTypes)
        {
            var typeIds = notificationTypes.ToArray();
            var existingTypeIds = await GetExisting(userId, typeIds).Select(x => x.Type).ToArrayAsync();
            _context.NotificationUnsubscribe.AddRange(
                typeIds.Except(existingTypeIds).Select(notificationType => new NotificationUnsubscribe
                {
                    User = userId,
                    Type = notificationType
                }));
            await _context.SaveChangesAsync();
        }

        public async Task SubscribeToNotifications(int userId, IEnumerable<int> notificationTypes)
        {
            _context.NotificationUnsubscribe.RemoveRange(await GetExisting(userId, notificationTypes).ToArrayAsync());
            await _context.SaveChangesAsync();
        }

        private IQueryable<NotificationUnsubscribe> GetExisting(int userId, IEnumerable<int> notificationTypes) 
            => _context.NotificationUnsubscribe
                .Where(x => x.User == userId && notificationTypes.Contains(x.Type));
                
    }
}