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
    public class TestSensorRepository: IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestSensorRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewSensor()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var newSensor = SensorMockData.NewSensorEntity();
            var sut = new SensorRepository(_context);

            await sut.AddAsync(newSensor);
            await sut.SaveAsync();

            _context.Sensors.Count().Should().Be(SensorMockData.GetSensorEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveSensor()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sensorToDelete = _context.Sensors.First();
            var sut = new SensorRepository(_context);

            sut.Delete(sensorToDelete);
            await sut.SaveAsync();

            _context.Sensors.Count().Should().Be(SensorMockData.GetSensorEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnSensorCollection()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sut = new SensorRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(SensorMockData.GetSensorEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnSensorCollection()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sut = new SensorRepository(_context);

            var result = await sut.GetAllBySpecAsync(new SensorSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnSensor()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sut = new SensorRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Sensor>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnSensorCollection()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sut = new SensorRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnSensor()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            var sut = new SensorRepository(_context);

            var result = await sut.GetBySpecAsync(new SensorSpecification(x => x.Id == 3));

            result.Should().NotBeNull();
            result!.Id.Should().Be(3);
        }

        [Fact]
        public async Task Update_ShouldChangeSensor()
        {
            _context.Sensors.AddRange(SensorMockData.GetSensorEntities());
            _context.SaveChanges();
            _context.Sensors.First().SensorName.Should().Be("Test Sensor 1");
            var sensorToUpdate = _context.Sensors.First();
            sensorToUpdate.SensorName = "Test Updated Sensor";
            var sut = new SensorRepository(_context);

            sut.Update(sensorToUpdate);
            await sut.SaveAsync();

            _context.Sensors.First().SensorName.Should().Be("Test Updated Sensor");
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
