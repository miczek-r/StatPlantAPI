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
    public class TestTriggerRepository : IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestTriggerRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewTrigger()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var newTrigger = TriggerMockData.NewTriggerEntity();
            var sut = new TriggerRepository(_context);

            await sut.AddAsync(newTrigger);
            await sut.SaveAsync();

            _context.Triggers.Count().Should().Be(TriggerMockData.GetTriggerEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveTrigger()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var triggerToDelete = _context.Triggers.First();
            var sut = new TriggerRepository(_context);

            sut.Delete(triggerToDelete);
            await sut.SaveAsync();

            _context.Triggers.Count().Should().Be(TriggerMockData.GetTriggerEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnTriggerCollection()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var sut = new TriggerRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(TriggerMockData.GetTriggerEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnTriggerCollection()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var sut = new TriggerRepository(_context);

            var result = await sut.GetAllBySpecAsync(new TriggerSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnTrigger()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var sut = new TriggerRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Trigger>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnTriggerCollection()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var sut = new TriggerRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnTrigger()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            var sut = new TriggerRepository(_context);

            var result = await sut.GetBySpecAsync(new TriggerSpecification(x => x.Id == 2));

            result.Should().NotBeNull();
            result!.Id.Should().Be(2);
        }

        [Fact]
        public async Task Update_ShouldChangeTrigger()
        {
            _context.Triggers.AddRange(TriggerMockData.GetTriggerEntities());
            _context.SaveChanges();
            _context.Triggers.First().Name.Should().Be("Test Trigger 1");
            var triggerToUpdate = _context.Triggers.First();
            triggerToUpdate.Name = "Test Updated Trigger";
            var sut = new TriggerRepository(_context);

            sut.Update(triggerToUpdate);
            await sut.SaveAsync();

            _context.Triggers.First().Name.Should().Be("Test Updated Trigger");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
