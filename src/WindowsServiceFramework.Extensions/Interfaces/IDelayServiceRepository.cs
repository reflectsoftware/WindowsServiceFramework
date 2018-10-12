using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsServiceFramework.Extensions.Models;

namespace WindowsServiceFramework.Extensions.Interfaces
{
    public interface IDelayServiceRepository
    {
        Task AddAsync(DelayServiceModel entity);
        Task DeleteAsync(DelayServiceModel entity);
        Task<IEnumerable<DelayServiceModel>> GetScheduledRequestsAsync(DateTime now);
    }
}
