using BaseDomain.IRepository;
using BaseDomain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseInfrastruct.Repository
{
    /// <summary>
    /// 泛型领域模型(领域模型,命令模型,事件模型)仓储
    /// </summary>
    /// <typeparam name="TDomainModel">泛型领域模型(领域模型,命令模型,事件模型)</typeparam>
    /// <typeparam name="TContext">泛型数据库上下文</typeparam>
    public class RepositoryCore<TDomainModel, TContext> : IRepositoryCore<TDomainModel> where TDomainModel : AggregateRootCore<object> where TContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly TContext _db;

        /// <summary>
        /// 领域模型(领域模型,命令模型,事件模型)集合
        /// </summary>
        protected readonly DbSet<TDomainModel> _models;

        public RepositoryCore(TContext context)
        {
            _db = context;
            _models = _db.Set<TDomainModel>();
        }

        public async Task<List<TDomainModel>> GetAllAsync()
        {
            return await _models.ToListAsync();
        }

        public async Task<TDomainModel> GetByIdAsync(object id)
        {
            return await _models.FindAsync(id);
        }

        public async Task AddAsync(TDomainModel domainModel)
        {
            await _db.AddAsync(domainModel);
        }

        public async Task UpdateAsync(TDomainModel domainModel)
        {
            _models.Update(domainModel);
            await Task.CompletedTask;
        }

        public async Task DeleteByIdAsync(object id)
        {
            _models.Remove(await _models.FindAsync(id));
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
