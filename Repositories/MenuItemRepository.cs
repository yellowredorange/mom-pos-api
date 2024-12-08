using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

public class MenuItemRepository : Repository<MenuItem>
{
    public MenuItemRepository(MomPosContext context, ILogger<MenuItemRepository> logger) : base(context, logger)
    {
    }
}