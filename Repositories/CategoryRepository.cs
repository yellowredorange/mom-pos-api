using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

public class CategoryRepository : Repository<Category> {
    public CategoryRepository(MomPosContext context) : base(context) {
    }
}