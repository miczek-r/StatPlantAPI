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
    public class TestSensorDataRepository : IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestSensorDataRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewSensorData()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var newSensorData = SensorDataMockData.NewSensorDataEntity();
            var sut = new SensorDataRepository(_context);

            await sut.AddAsync(newSensorData);
            await sut.SaveAsync();

            _context.SensorDatas.Count().Should().Be(SensorDataMockData.GetSensorDataEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveSensorData()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sensorDataToDelete = _context.SensorDatas.First();
            var sut = new SensorDataRepository(_context);

            sut.Delete(sensorDataToDelete);
            await sut.SaveAsync();

            _context.SensorDatas.Count().Should().Be(SensorDataMockData.GetSensorDataEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnSensorDataCollection()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sut = new SensorDataRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(SensorDataMockData.GetSensorDataEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnSensorDataCollection()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sut = new SensorDataRepository(_context);

            var result = await sut.GetAllBySpecAsync(new SensorDataSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnSensorData()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sut = new SensorDataRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<SensorData>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnSensorDataCollection()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sut = new SensorDataRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnSensorData()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            var sut = new SensorDataRepository(_context);

            var result = await sut.GetBySpecAsync(new SensorDataSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeSensorData()
        {
            _context.SensorDatas.AddRange(SensorDataMockData.GetSensorDataEntities());
            _context.SaveChanges();
            _context.SensorDatas.First().Value.Should().Be(1.1f);
            var sensorDataToUpdate = _context.SensorDatas.First();
            sensorDataToUpdate.Value = 11.1f;
            var sut = new SensorDataRepository(_context);

            sut.Update(sensorDataToUpdate);
            await sut.SaveAsync();

            _context.SensorDatas.First().Value.Should().Be(11.1f);
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
