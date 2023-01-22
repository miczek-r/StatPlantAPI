using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class SensorMockData
    {
        public static Sensor GetSensorEntity()
        {
            return new Sensor
            {
                Id = 1,
                SensorName = "Test Sensor 1",
            };
        }

        public static List<Sensor> GetSensorEntities()
        {
            return new List<Sensor>
            {
            new Sensor
            {
                SensorName = "Test Sensor 1",
                SensorType = SensorTypeMockData.GetSensorTypeEntity()
            },
            new Sensor
            {
                SensorName = "Test Sensor 2",
                SensorType = SensorTypeMockData.GetSensorTypeEntity()
            },
            new Sensor
            {
                SensorName = "Test Sensor 3",
                SensorType = SensorTypeMockData.GetSensorTypeEntity()
            }
            };
        }

        public static Sensor NewSensorEntity()
        {
            return new Sensor
            {
                SensorName = "Test Sensor 4",
                SensorType = SensorTypeMockData.GetSensorTypeEntity()
            };
        }
    }
}
