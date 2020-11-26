using System.ComponentModel.DataAnnotations;

namespace BaseApplication.ViewModel
{
    /// <summary>
    /// 泛型视图模型(DTO)抽象基类
    /// </summary>
    /// <typeparam name="TId">泛型ID</typeparam>
    public abstract class ViewModelCore<TId>
    {
        [Key]
        public TId Id { get; set; }
    }
}
