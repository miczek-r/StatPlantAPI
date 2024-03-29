﻿using Application.DTOs.SensorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ISensorDataService
    {
        Task<IEnumerable<SensorDataBaseDTO>> GetAll();
        Task Create(SensorDataCreateDTO sensorCreateDTO);
        Task Remove(int id);
        Task<SensorDataDetailsDTO> GetDetails(SensorDataGetDetailsDTO sensorDataGetDetailsDTO);
    }
}
