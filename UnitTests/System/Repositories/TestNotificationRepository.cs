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
    public class TestNotificationRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestNotificationRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewNotification()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var newNotification = NotificationMockData.NewNotificationEntity();
            var sut = new NotificationRepository(_context);

            await sut.AddAsync(newNotification);
            await sut.SaveAsync();

            _context.Notifications.Count().Should().Be(NotificationMockData.GetNotificationEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveNotification()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var notificationToDelete = _context.Notifications.First();
            var sut = new NotificationRepository(_context);

            sut.Delete(notificationToDelete);
            await sut.SaveAsync();

            _context.Notifications.Count().Should().Be(NotificationMockData.GetNotificationEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnNotificationCollection()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var sut = new NotificationRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(NotificationMockData.GetNotificationEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnNotificationCollection()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var sut = new NotificationRepository(_context);

            var result = await sut.GetAllBySpecAsync(new NotificationSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnNotification()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var sut = new NotificationRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Notification>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnNotificationCollection()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var sut = new NotificationRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnNotification()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            var sut = new NotificationRepository(_context);

            var result = await sut.GetBySpecAsync(new NotificationSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeNotification()
        {
            _context.Notifications.AddRange(NotificationMockData.GetNotificationEntities());
            _context.SaveChanges();
            _context.Notifications.First().Title.Should().Be("Test Notification 1");
            var notificationToUpdate = _context.Notifications.First();
            notificationToUpdate.Title = "Test Updated Notification";
            var sut = new NotificationRepository(_context);

            sut.Update(notificationToUpdate);
            await sut.SaveAsync();

            _context.Notifications.First().Title.Should().Be("Test Updated Notification");
        }

        [Fact]
        public async Task AddSpecIncludes_ReturnIQueryable() { }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
