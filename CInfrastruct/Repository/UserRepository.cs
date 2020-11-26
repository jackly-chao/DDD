using CDomain.IRepository;
using CDomain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CInfrastruct.Repository
{
    /// <summary>
    /// 用户领域模型(领域模型,命令模型,事件模型)仓储
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly Context.Context _db;

        /// <summary>
        /// 领域模型(领域模型,命令模型,事件模型)集合
        /// </summary>
        private readonly DbSet<UserDomainModel> _models;

        public UserRepository(Context.Context context)
        {
            _db = context;
            _models = _db.Set<UserDomainModel>();
        }

        public async Task<List<UserDomainModel>> GetAllAsync()
        {
            return await _models.ToListAsync();
        }

        public async Task<UserDomainModel> GetByIdAsync(int id)
        {
            return await _models.FindAsync(id);
        }

        public async Task AddAsync(UserDomainModel domainModel)
        {
            await _db.AddAsync(domainModel);
        }

        public async Task UpdateAsync(UserDomainModel domainModel)
        {
            _models.Update(domainModel);
            await Task.CompletedTask;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _models.Remove(await _models.FindAsync(id));
        }

        public async Task<UserDomainModel> GetByMobileAsync(string mobile)
        {
            return await _models.FirstOrDefaultAsync(d => d.Mobile == mobile);
        }

        public async Task<UserDomainModel> GetByUserNameAsync(string userName)
        {
            return await _models.FirstOrDefaultAsync(d => d.UserName == userName);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
