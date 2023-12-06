using Microsoft.EntityFrameworkCore;

namespace Frency.DataAccess.Extensions
{
    public static class EFLinqExtension
    {
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, string[] IncludeProperties) where TEntity : class
        {
            foreach (var prop in IncludeProperties)
            {
                source = source.Include(prop);
            }

            return source;
        }
    }
}
