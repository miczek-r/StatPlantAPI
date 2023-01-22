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
    public class TestEventRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestEventRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewEvent()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var newEvent = EventMockData.NewEventEntity();
            var sut = new EventRepository(_context);

            await sut.AddAsync(newEvent);
            await sut.SaveAsync();

            _context.Events.Count().Should().Be(EventMockData.GetEventEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveEvent()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var eventToDelete = _context.Events.First();
            var sut = new EventRepository(_context);

            sut.Delete(eventToDelete);
            await sut.SaveAsync();

            _context.Events.Count().Should().Be(EventMockData.GetEventEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnEventCollection()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var sut = new EventRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(EventMockData.GetEventEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnEventCollection()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var sut = new EventRepository(_context);

            var result = await sut.GetAllBySpecAsync(new EventSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnEvent()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var sut = new EventRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Event>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnEventCollection()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var sut = new EventRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnEvent()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            var sut = new EventRepository(_context);

            var result = await sut.GetBySpecAsync(new EventSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeEvent()
        {
            _context.Events.AddRange(EventMockData.GetEventEntities());
            _context.SaveChanges();
            _context.Events.First().Title.Should().Be("Test Event 1");
            var eventToUpdate = _context.Events.First();
            eventToUpdate.Title = "Test Updated Event";
            var sut = new EventRepository(_context);

            sut.Update(eventToUpdate);
            await sut.SaveAsync();

            _context.Events.First().Title.Should().Be("Test Updated Event");
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
