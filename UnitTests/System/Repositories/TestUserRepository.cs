using Core.Entities;
using Core.Specifications;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.System.Repositories
{
    public class TestUserRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestUserRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewUser()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var newUser = UserMockData.NewUserEntity();
            var sut = new UserRepository(_context);

            await sut.AddAsync(newUser);
            await sut.SaveAsync();

            _context.Users.Count().Should().Be(UserMockData.GetUserEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveUser()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var userToDelete = _context.Users.First();
            var sut = new UserRepository(_context);

            sut.Delete(userToDelete);
            await sut.SaveAsync();

            _context.Users.Count().Should().Be(UserMockData.GetUserEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnUserCollection()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var sut = new UserRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(UserMockData.GetUserEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnUserCollection()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var sut = new UserRepository(_context);

            var result = await sut.GetAllBySpecAsync(new UserSpecification(x => x.Email.EndsWith(".com")));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnNull()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var sut = new UserRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnUserCollection()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var sut = new UserRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Email.EndsWith(".com"));

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnUser()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            var sut = new UserRepository(_context);

            var result = await sut.GetBySpecAsync(new UserSpecification(x => x.FirstName == "TestFirstName2"));

            result.Should().NotBeNull();
            result!.Email.Should().Be("test2@user.com");
        }

        [Fact]
        public async Task Update_ShouldChangeUser()
        {
            _context.Users.AddRange(UserMockData.GetUserEntities());
            _context.SaveChanges();
            _context.Users.First().FirstName.Should().Be("TestFirstName1");
            var userToUpdate = _context.Users.First();
            userToUpdate.FirstName = "UpdatedFirstName";
            var sut = new UserRepository(_context);

            sut.Update(userToUpdate);
            await sut.SaveAsync();

            _context.Users.First().FirstName.Should().Be("UpdatedFirstName");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
