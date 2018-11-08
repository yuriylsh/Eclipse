using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using Yuriy.Core.Repositories;
using Yuriy.Core.Services;

namespace Yuriy.Core.Tests.Services
{
    public partial class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _subject;
        private readonly MockUser[] _users;

        public UserServiceTests()
        {
            _users = new []{ new MockUser{Id = 1}, new MockUser{Id = 2 }};
            _userRepositoryMock = new Mock<IUserRepository>();
            _subject = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUser_NullId_ReturnsFirstUser()
        {
            
            _userRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(_users);

            var result = await _subject.GetUser(null);

            result.Should().Be(_users[0]);
        }

        [Fact]
        public async Task GetUser_GivenId_ReturnsUserWithGivenId()
        {
            const int id = 33;
            var userFromRepo = _users[1];
            _userRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(userFromRepo);

            var result = await _subject.GetUser(id);

            result.Should().Be(userFromRepo);
        }
    }
}