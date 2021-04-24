using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Paging;

namespace Study.PaymentGateway.Domain.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task InsertAsync(T entity);

        Task<bool> UpdadateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<IReadOnlyList<T>> GetByIdAsync(Guid id);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<PagedResult<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int currentPage, int itemsPerPage);
    }
}