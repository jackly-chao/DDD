using AutoMapper;
using BaseApplication.ViewModel;
using BaseDomain.Model;

namespace BaseApplication.AutoMapper
{
    /// <summary>
    /// 自动映射文件
    /// </summary>
    /// <typeparam name="TDomainModel">泛型领域模型</typeparam>
    /// <typeparam name="TViewModel">泛型视图模型</typeparam>
    public class AutoMapperProfileCore<TDomainModel, TViewModel> : Profile where TDomainModel : AggregateRootCore<object> where TViewModel : ViewModelCore<object>
    {
        public AutoMapperProfileCore()
        {
            #region 领域模型->视图模型
            CreateMap<TDomainModel, TViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));
            #endregion

            #region 视图模型->领域模型
            CreateMap<TViewModel, TDomainModel>()
                .ForPath(d => d.Id, o => o.MapFrom(s => s.Id));
            #endregion
        }
    }
}
