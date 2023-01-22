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
    public class TestConditionRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestConditionRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewCondition()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var newCondition = ConditionMockData.NewConditionEntity();
            var sut = new ConditionRepository(_context);

            await sut.AddAsync(newCondition);
            await sut.SaveAsync();

            _context.Conditions.Count().Should().Be(ConditionMockData.GetConditionEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveCondition()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var conditionToDelete = _context.Conditions.First();
            var sut = new ConditionRepository(_context);

            sut.Delete(conditionToDelete);
            await sut.SaveAsync();

            _context.Conditions.Count().Should().Be(ConditionMockData.GetConditionEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnConditionCollection()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var sut = new ConditionRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(ConditionMockData.GetConditionEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnConditionCollection()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var sut = new ConditionRepository(_context);

            var result = await sut.GetAllBySpecAsync(new ConditionSpecification(x=>x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnCondition()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var sut = new ConditionRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Condition>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnConditionCollection()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var sut = new ConditionRepository(_context);

            var result = await sut.GetByLambdaAsync(x=> x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnCondition()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            var sut = new ConditionRepository(_context);

            var result = await sut.GetBySpecAsync(new ConditionSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeCondition()
        {
            _context.Conditions.AddRange(ConditionMockData.GetConditionEntities());
            _context.SaveChanges();
            _context.Conditions.First().Value.Should().Be(1.1f);
            var conditionToUpdate = _context.Conditions.First();
            conditionToUpdate.Value = 11.1f;
            var sut = new ConditionRepository(_context);

            sut.Update(conditionToUpdate);
            await sut.SaveAsync();

            _context.Conditions.First().Value.Should().Be(11.1f);
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
