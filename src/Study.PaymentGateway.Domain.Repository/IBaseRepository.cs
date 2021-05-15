using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Study.PaymentGateway.Domain.Entities.Paging;

namespace Study.PaymentGateway.Domain.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void InsertAsync(T entity);

        Task<bool> UpdadateAsync(T entity);

        Task<T> GetByIdAsync(Guid id);

        Task<PagedResults<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int currentPage, int itemsPerPage);
    }
}