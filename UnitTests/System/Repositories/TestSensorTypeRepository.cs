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
    public class TestSensorTypeRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestSensorTypeRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewSensorType()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var newSensorType = SensorTypeMockData.NewSensorTypeEntity();
            var sut = new SensorTypeRepository(_context);

            await sut.AddAsync(newSensorType);
            await sut.SaveAsync();

            _context.SensorTypes.Count().Should().Be(SensorTypeMockData.GetSensorTypeEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveSensorType()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sensorTypeToDelete = _context.SensorTypes.First();
            var sut = new SensorTypeRepository(_context);

            sut.Delete(sensorTypeToDelete);
            await sut.SaveAsync();

            _context.SensorTypes.Count().Should().Be(SensorTypeMockData.GetSensorTypeEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnSensorTypeCollection()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sut = new SensorTypeRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(SensorTypeMockData.GetSensorTypeEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnSensorTypeCollection()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sut = new SensorTypeRepository(_context);

            var result = await sut.GetAllBySpecAsync(new SensorTypeSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnSensorType()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sut = new SensorTypeRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<SensorType>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnSensorTypeCollection()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sut = new SensorTypeRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnSensorType()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            var sut = new SensorTypeRepository(_context);

            var result = await sut.GetBySpecAsync(new SensorTypeSpecification(x => x.Id == 2));

            result.Should().NotBeNull();
            result!.Id.Should().Be(2);
        }

        [Fact]
        public async Task Update_ShouldChangeSensorType()
        {
            _context.SensorTypes.AddRange(SensorTypeMockData.GetSensorTypeEntities());
            _context.SaveChanges();
            _context.SensorTypes.First().TypeName.Should().Be("Test Sensor Type 1");
            var sensorTypeToUpdate = _context.SensorTypes.First();
            sensorTypeToUpdate.TypeName = "Test Updated Sensor Type";
            var sut = new SensorTypeRepository(_context);

            sut.Update(sensorTypeToUpdate);
            await sut.SaveAsync();

            _context.SensorTypes.First().TypeName.Should().Be("Test Updated Sensor Type");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
