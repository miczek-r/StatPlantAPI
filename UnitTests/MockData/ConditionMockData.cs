using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class ConditionMockData
    {
        public static List<Condition> GetConditionEntities()
        {
            return new List<Condition>{
                new Condition
                {
                    Inequality = Core.Enums.Inequality.LOWER,
                    SensorTypeId = 1,
                    Value = 1.1f,
                    TriggerId = 1
                },
                new Condition
                {
                    Inequality = Core.Enums.Inequality.HIGHER,
                    SensorTypeId = 2,
                    Value = 2.2f,
                    TriggerId = 2
                },
                new Condition
                {
                    Inequality = Core.Enums.Inequality.EQUAL,
                    SensorTypeId = 3,
                    Value = 3.3f,
                    TriggerId = 3
                }
            };
        }

        public static Condition NewConditionEntity() {
            return new Condition
            {
                Inequality = Core.Enums.Inequality.LOWER,
                SensorTypeId = 4,
                Value = 4.4f,
                TriggerId = 4
            };
        }
    }
}

