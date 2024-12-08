using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;
using Microsoft.Extensions.Logging;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(MomPosContext context, ILogger<CategoryRepository> logger)
        : base(context, logger)
    {
    }
}
