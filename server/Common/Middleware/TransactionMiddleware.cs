using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Common.Middleware
{
    public class TransactionMiddleware<TDbContext> where TDbContext : DbContext
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TDbContext dbContext)
        {
            // Solo abrimos transacción si hay cambios pendientes
            if (context.Request.Method == HttpMethods.Post ||
                context.Request.Method == HttpMethods.Put ||
                context.Request.Method == HttpMethods.Delete)
            {

                await using var transaction = await dbContext.Database.BeginTransactionAsync();

                try
                {
                    await _next(context);

                    // Si todo salió bien (no excepciones ni 4xx/5xx manuales)
                    if (context.Response.StatusCode < 400)
                    {
                        await dbContext.SaveChangesAsync(); // Asegura persistencia
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
