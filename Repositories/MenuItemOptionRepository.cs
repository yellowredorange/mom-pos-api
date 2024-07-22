using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

public class MenuItemOptionRepository : Repository<MenuItemOption> {
    public MenuItemOptionRepository(MomPosContext context, ILogger<MenuItemOptionRepository> logger) : base(context, logger) {
    }
}