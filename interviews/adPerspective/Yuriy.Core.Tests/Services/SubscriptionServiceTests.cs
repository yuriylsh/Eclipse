using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;
using Yuriy.Core.Services;

namespace Yuriy.Core.Tests.Services
{
    public class SubscriptionServiceTests
    {
        private readonly SubscriptionService _subject;
        private readonly Mock<ISubscriptionRepository> _repoMock;

        public SubscriptionServiceTests()
        {
            _repoMock = new Mock<ISubscriptionRepository>();
            _subject = new SubscriptionService(_repoMock.Object);
        }

        [Fact]
        public async Task ValidateUpdates_DuplicatedIds_FailsValidation()
        {
            _repoMock.Setup(x => x.AllNotificationTypesExist(It.IsAny<int[]>())).ReturnsAsync(true);

            var result = await _subject.ValidateUpdates(new []
            {
                new SubscriptionUpdate{Id = 123, IsUnsubscribed = true}, 
                new SubscriptionUpdate{Id = 123, IsUnsubscribed = true}, 
            });

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdates_NonExistingIds_FailsValidation()
        {
            var ids = new[] { 1, 2, 3 };
            var subscriptionUpdates = ids.Select(id => new SubscriptionUpdate { Id = id }).ToArray();
            _repoMock.Setup(x => x.AllNotificationTypesExist(ids)).ReturnsAsync(false);

            var result = await _subject.ValidateUpdates(subscriptionUpdates);

            result.Should().BeFalse();
        }
        [Fact]
        public async Task ValidateUpdates_DistinctExistingIds_FailsValidation()
        {
            var ids = new[] { 1, 2, 3 };
            var subscriptionUpdates = ids.Select(id => new SubscriptionUpdate { Id = id }).ToArray();
            _repoMock.Setup(x => x.AllNotificationTypesExist(ids)).ReturnsAsync(true);

            var result = await _subject.ValidateUpdates(subscriptionUpdates);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ApplyUpdates_CombinationOfSubsAndUnsubs_AppliesToDb()
        {
            var updates = new []
            {
                new SubscriptionUpdate{ Id = 1, IsUnsubscribed = true},
                new SubscriptionUpdate{ Id = 2, IsUnsubscribed = false},
                new SubscriptionUpdate{ Id = 3, IsUnsubscribed = false},
                new SubscriptionUpdate{ Id = 4, IsUnsubscribed = true},
            };
            const int userId = 1234;

            await _subject.ApplyUpdates(userId, updates);

            _repoMock.Verify(x => x.SubscribeToNotifications(userId, It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new []{ 2 , 3}))));
            _repoMock.Verify(x => x.UnsubscribeFromNotifications(userId, It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new []{ 1 , 4}))));

        }

        [Fact]
        public async Task ApplyUpdates_NoSubscribes_NoCallToDbToSubscribe()
        {
            var updates = new []
            {
                new SubscriptionUpdate{ Id = 1, IsUnsubscribed = true},
                new SubscriptionUpdate{ Id = 4, IsUnsubscribed = true},
            };
            const int userId = 1234;

            await _subject.ApplyUpdates(userId, updates);

            _repoMock.Verify(x => x.UnsubscribeFromNotifications(userId, It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new []{ 1 , 4}))));
            _repoMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ApplyUpdates_NoUnsubscribes_NoCallToDbToUnsubsribe()
        {
            var updates = new []
            {
                new SubscriptionUpdate{ Id = 2, IsUnsubscribed = false},
                new SubscriptionUpdate{ Id = 3, IsUnsubscribed = false},
            };
            const int userId = 1234;

            await _subject.ApplyUpdates(userId, updates);

            _repoMock.Verify(x => x.SubscribeToNotifications(userId, It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new []{ 2 , 3}))));
            _repoMock.VerifyNoOtherCalls();

        }
    }
}