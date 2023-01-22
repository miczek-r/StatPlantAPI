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
    public class TestHubRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestHubRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewHub()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var newHub = HubMockData.NewHubEntity();
            var sut = new HubRepository(_context);

            await sut.AddAsync(newHub);
            await sut.SaveAsync();

            _context.Hubs.Count().Should().Be(HubMockData.GetHubEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveHub()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var hubToDelete = _context.Hubs.First();
            var sut = new HubRepository(_context);

            sut.Delete(hubToDelete);
            await sut.SaveAsync();

            _context.Hubs.Count().Should().Be(HubMockData.GetHubEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnHubCollection()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var sut = new HubRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(HubMockData.GetHubEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnHubCollection()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var sut = new HubRepository(_context);

            var result = await sut.GetAllBySpecAsync(new HubSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnHub()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var sut = new HubRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Hub>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnHubCollection()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var sut = new HubRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnHub()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            var sut = new HubRepository(_context);

            var result = await sut.GetBySpecAsync(new HubSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeHub()
        {
            _context.Hubs.AddRange(HubMockData.GetHubEntities());
            _context.SaveChanges();
            _context.Hubs.First().Name.Should().Be("Test Hub 1");
            var hubToUpdate = _context.Hubs.First();
            hubToUpdate.Name = "Test Updated Hub";
            var sut = new HubRepository(_context);

            sut.Update(hubToUpdate);
            await sut.SaveAsync();

            _context.Hubs.First().Name.Should().Be("Test Updated Hub");
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
