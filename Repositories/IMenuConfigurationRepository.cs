using MomPosApi.Models;

public interface IMenuConfigurationRepository : IRepository<MenuConfiguration> {
  Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync();
}