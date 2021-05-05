using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Study.PaymentGateway.Domain.Entities.Paging;
using Study.PaymentGateway.Domain.Entities.Payments;
using Study.PaymentGateway.Domain.Repository;
using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;
using Study.PaymentGateway.Repository.MongoDB.Entities.Payment;

namespace Study.PaymentGateway.Repository.MongoDB.Repository
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(IMongoDBConfiguration mongoDBConfiguration, IMapper mapper)
            : base(mongoDBConfiguration, mapper)
        {
        }

        public async Task InsertAsync(Payment entity)
        {
            var payment = this.mapper.Map<PaymentMongo>(entity);

            await this.mongoDBConfiguration.Payment.InsertOneAsync(payment);
        }

        public async Task<bool> UpdadateAsync(Payment entity)
        {
            var payment = this.mapper.Map<PaymentMongo>(entity);

            // var filter = Builders<PaymentMongo>.Filter.ElemMatch<PaymentMongo>(p => p.Id, entity.Id);
            var filter = Builders<PaymentMongo>.Filter.Eq(p => p.Id, entity.Id);

            var updateResult = await this.mongoDBConfiguration.Payment.ReplaceOneAsync(filter, payment);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            var filter = Builders<PaymentMongo>.Filter.Where(w => w.Id == id);

            var paymentsMongo = await this.mongoDBConfiguration.Payment.FindAsync(filter);

            var payments = this.mapper.Map<Payment>(paymentsMongo.FirstOrDefault());

            return payments;
        }

        public async Task<IReadOnlyList<Payment>> GetPaymentByMerchantIdAsync(Guid id)
        {
            var filter = Builders<PaymentMongo>.Filter.Eq(payment => payment.MerchantId, id);

            var updateResult = await this.mongoDBConfiguration.Payment.FindAsync(filter);

            return this.mapper.Map<List<Payment>>(updateResult.ToList());
        }

        public async Task<PagedResult<Payment>> GetPagedAsync(Expression<Func<Payment, bool>> predicate, int currentPage, int itemsPerPage)
        {
            var mappedPredicate = this.mapper.Map<Expression<Func<PaymentMongo, bool>>>(predicate);

            var query = this.mongoDBConfiguration.Payment.Find(mappedPredicate);

            return await this.Paginate(query, currentPage, itemsPerPage);
        }

        public async Task<PagedResult<Payment>> GetPaymentByCardNumberAsync(long cardNumber, int currentPage, int itemsPerPage)
        {
            var filter = Builders<PaymentMongo>.Filter.Eq(p => p.Card.Number, cardNumber);

            var query = this.mongoDBConfiguration.Payment.Find(filter);

            return await this.Paginate(query, currentPage, itemsPerPage);
        }

        private async Task<PagedResult<Payment>> Paginate(IFindFluent<PaymentMongo, PaymentMongo> query, int currentPage, int itemsPerPage)
        {
            var paged = new PagedResult<Payment>();

            paged.TotalItems = await query.CountDocumentsAsync();

            var records = query.Skip(currentPage * itemsPerPage).Limit(itemsPerPage).ToList();

            var mappedRecords = this.mapper.Map<List<Payment>>(records);

            paged.Records = mappedRecords;

            return paged;
        }
    }
}