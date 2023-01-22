using Application.DTOs.SensorType;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class SensorTypeMockData
    {
        public static List<SensorTypeBaseDTO> GetSensorTypes()
        {
            return new List<SensorTypeBaseDTO>
            {
                new SensorTypeBaseDTO
                {
                    TypeName = "Test Sensor Type 1",
                    Unit = "Test Unit 1"
                },
                new SensorTypeBaseDTO
                {
                    TypeName = "Test Sensor Type 2",
                    Unit = "Test Unit 2"
                },
                new SensorTypeBaseDTO
                {
                    TypeName = "Test Sensor Type 3",
                    Unit = "Test Unit 3"
                }
            };
        }

        public static SensorType GetSensorTypeEntity()
        {
            return new SensorType
            {
                TypeName = "Test Sensor Type 1",
                Unit = "Test Unit 1"
            };
        }

        public static List<SensorType> GetSensorTypeEntities()
        {
            return new List<SensorType>
            {
                new SensorType
                {
                    TypeName = "Test Sensor Type 1",
                    Unit = "Test Unit 1"
                },
                new SensorType
                {
                    TypeName = "Test Sensor Type 2",
                    Unit = "Test Unit 2"
                },
                new SensorType
                {
                    TypeName = "Test Sensor Type 3",
                    Unit = "Test Unit 3"
                }
            };
        }

        public static SensorType NewSensorTypeEntity()
        {
            return new SensorType
            {
                TypeName = "Test Sensor Type 4",
                Unit = "Test Unit 4"
            };
        }
    }
}
