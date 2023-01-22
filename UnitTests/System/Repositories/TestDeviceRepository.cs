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
    public class TestDeviceRepository : IDisposable
    {
        protected readonly IdentityDbContext _context;
        public TestDeviceRepository()
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new IdentityDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddAsync_AddNewDevice()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var newDevice = DeviceMockData.NewDeviceEntity();
            var sut = new DeviceRepository(_context);

            await sut.AddAsync(newDevice);
            await sut.SaveAsync();

            _context.Devices.Count().Should().Be(DeviceMockData.GetDeviceEntities().Count() + 1);
        }

        [Fact]
        public async Task Delete_RemoveDevice()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var deviceToDelete = _context.Devices.First();
            var sut = new DeviceRepository(_context);

            sut.Delete(deviceToDelete);
            await sut.SaveAsync();

            _context.Devices.Count().Should().Be(DeviceMockData.GetDeviceEntities().Count() - 1);
        }

        [Fact]
        public async Task GetAllAsync_ReturnDeviceCollection()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var sut = new DeviceRepository(_context);

            var result = await sut.GetAllAsync();

            result.Should().HaveCount(DeviceMockData.GetDeviceEntities().Count);
        }

        [Fact]
        public async Task GetAllBySpecAsync_ReturnDeviceCollection()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var sut = new DeviceRepository(_context);

            var result = await sut.GetAllBySpecAsync(new DeviceSpecification(x => x.Id < 3));

            result.Should().HaveCount(2);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnDevice()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var sut = new DeviceRepository(_context);

            var result = await sut.GetByIdAsync(1);

            result.Should().BeOfType<Device>();
        }

        [Fact]
        public async Task GetByLambdaAsync_ReturnDeviceCollection()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var sut = new DeviceRepository(_context);

            var result = await sut.GetByLambdaAsync(x => x.Id < 3);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBySpecAsync_ReturnDevice()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            var sut = new DeviceRepository(_context);

            var result = await sut.GetBySpecAsync(new DeviceSpecification(x => x.UUID.Contains("22")));

            result.Should().NotBeNull();
            result!.Id.Should().Be(2);
        }

        [Fact]
        public async Task Update_ShouldChangeDevice()
        {
            _context.Devices.AddRange(DeviceMockData.GetDeviceEntities());
            _context.SaveChanges();
            _context.Devices.First().UUID.Should().Be("1234567890");
            var deviceToUpdate = _context.Devices.First();
            deviceToUpdate.UUID = "0987654321";
            var sut = new DeviceRepository(_context);

            sut.Update(deviceToUpdate);
            await sut.SaveAsync();

            _context.Devices.First().UUID.Should().Be("0987654321");
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
