using Application.DTOs.SensorData;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class SensorDataMockData
    {
        public static SensorDataCreateDTO NewSensorData()
        {
            return new SensorDataCreateDTO
            {
                Id = "testSensorDataId1",
                MacAddress = "testMacAddress1",
                Data = new List<SensorDataSingleDTO>
                {
                    new SensorDataSingleDTO
                    {
                        Key = "TestKey1",
                        Value = 1.0f
                    },
                    new SensorDataSingleDTO
                    {
                        Key = "TestKey2",
                        Value = 2.0f
                    },
                    new SensorDataSingleDTO
                    {
                        Key = "TestKey3",
                        Value = 3.0f
                    }
                }
            };
        }

        public static SensorDataGetDetailsDTO GetSensorDataDetailsRequest()
        {
            return new SensorDataGetDetailsDTO
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                DeviceId = 1,
                SensorType = 1
            };
        }

        public static SensorDataDetailsDTO GetSensorDataDetails()
        {
            return new SensorDataDetailsDTO
            {
                TypeOfSensor = "TestSensor1",
                DeviceInformations = "TestDeviceInfromations1",
                MeasurementUnit = "TestMeasurementUnit1",
                SensorDataRecords = new List<SensorDataRecordDTO>
                {
                    new SensorDataRecordDTO
                    {
                        DateOfMeasurement = DateTime.Now,
                        MeasuredValue = 2.3f,
                    },
                     new SensorDataRecordDTO
                    {
                        DateOfMeasurement = DateTime.Now,
                        MeasuredValue = 5.3f,
                    }
                },
                LatestRecordedValue = new SensorDataRecordDTO
                {
                    DateOfMeasurement = DateTime.Now,
                    MeasuredValue = 5.3f,
                }
            };
        }

        public static List<SensorData> GetSensorDataEntities()
        {
            return new List<SensorData>
            {
                new SensorData
                {
                    Value = 1.1f,
                    DateOfMeasurement = DateTime.Now
                },
                 new SensorData
                {
                    Value = 2.2f,
                    DateOfMeasurement = DateTime.Now
                },
                  new SensorData
                {
                    Value = 3.3f,
                    DateOfMeasurement = DateTime.Now
                }
            };
        }

        public static SensorData NewSensorDataEntity()
        {
            return new SensorData
            {
                Value = 4.4f,
                DateOfMeasurement = DateTime.Now
            };
        }
    }
}
