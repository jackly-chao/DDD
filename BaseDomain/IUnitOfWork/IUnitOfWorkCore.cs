using System;
using System.Threading.Tasks;

namespace BaseDomain.IUnitOfWork
{
    /// <summary>
    /// 泛型工作单元接口,这里的提交才会将数据更新到数据库,之所以加这个,是考虑到跨库/表事务
    /// </summary>
    public interface IUnitOfWorkCore : IDisposable
    {
        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 通过事务异步提交
        /// </summary>
        /// <returns></returns>
        Task<int> CommitByTransactionAsync();
    }
}
