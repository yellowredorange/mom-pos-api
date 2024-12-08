using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;

public class ResetDatabase
{
    private readonly MomPosContext _context;
    public ResetDatabase(MomPosContext context)
    {
        _context = context;
    }
    public async Task Reset()
    {
        // 禁用所有外鍵約束
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");

        // 刪除所有資料
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'DELETE FROM ?'");

        // 重新啟用所有外鍵約束
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");

        // 重置所有表的 IDENTITY
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT(''?'', RESEED, 0)'");

        // 刷新上下文
        await _context.Database.CloseConnectionAsync();
        await _context.Database.OpenConnectionAsync();
    }
}
