using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef.UnitOfWork
{
    public interface IDbContextProvider<TDbContext> where TDbContext: DbContext
    {
        Task<TDbContext> GetAsync();
    }
}