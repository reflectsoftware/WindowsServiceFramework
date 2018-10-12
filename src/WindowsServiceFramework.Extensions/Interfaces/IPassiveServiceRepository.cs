using System;
using System.Linq;
using System.Threading.Tasks;
using WindowsServiceFramework.Extensions.Models;

namespace WindowsServiceFramework.Extensions.Interfaces
{
    public interface IPassiveServiceRepository
    {
        Task<PassiveServiceModel> GetAsync(Func<IQueryable<PassiveServiceModel>, IQueryable<PassiveServiceModel>> queryable);
        Task InitializeSeedAsync(string serviceName);
        Task<bool> ObtainOwnershipAsync(string serviceName, Guid activeId, int timeWindow);
    }
}
