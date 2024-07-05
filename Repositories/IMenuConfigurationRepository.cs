using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;

namespace MomPosApi.Repositories {
    public interface IMenuConfigurationRepository : IRepository<MenuConfiguration> {
        Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync();
    }

}