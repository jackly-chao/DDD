using AutoMapper;
using CApplication.ViewModel;
using CDomain.Model;

namespace CApplication.AutoMapper
{
    /// <summary>
    /// 自动映射文件
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region 领域模型->视图模型
            CreateMap<UserDomainModel, UserViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id));

            //CreateMap<CPermission, CPermissionViewModel>()
            //    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            //    .ForMember(d => d.PermissionId, o => o.MapFrom(s => s.PermissionId))
            //    .ForMember(d => d.PermissionName, o => o.MapFrom(s => s.PermissionName))
            //    .ForMember(d => d.PermissionPath, o => o.MapFrom(s => s.PermissionPath)); 
            #endregion

            #region 视图模型->领域模型
            CreateMap<UserViewModel, UserDomainModel>()
                .ForPath(d => d.Id, o => o.MapFrom(s => s.Id));

            //CreateMap<CUserViewModel, CUserRegisterCommandModel>()
            //    .ConstructUsing(d => new CUserRegisterCommandModel(d.Id, d.UserName, d.Password, "", "", ""));

            //CreateMap<CUserViewModel, CUserLoginCommandModel>()
            //    .ConstructUsing(d => new CUserLoginCommandModel(d.UserName, d.Password));

            //CreateMap<CUserViewModel, CUserLogoutCommandModel>()
            //    .ConstructUsing(d => new CUserLogoutCommandModel(d.Id, d.UserName));

            //CreateMap<CPermissionViewModel, CPermission>()
            //    .ForPath(d => d.Id, o => o.MapFrom(s => s.Id))
            //    .ForPath(d => d.PermissionId, o => o.MapFrom(s => s.PermissionId))
            //    .ForPath(d => d.PermissionName, o => o.MapFrom(s => s.PermissionName))
            //    .ForPath(d => d.PermissionPath, o => o.MapFrom(s => s.PermissionPath)); 
            #endregion
        }
    }
}
