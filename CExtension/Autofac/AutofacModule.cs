using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using CApplication.AutoMapper;
using CApplication.Service;
using CDomain.Bus;
using CDomain.Notification.Handler;
using CDomain.Notification.Model;
using CExtension.AOP;
using CInfrastruct.Repository;
using CInfrastruct.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Module = Autofac.Module;

namespace CExtension.Autofac
{
    // 参考:https://github.com/autofac/Examples/blob/master/src/AspNetCore3Example/AutofacModule.cs
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region 数据库上下文
            //builder.RegisterType<CInfrastruct.Context.Context>()
            //    .Named<DbContext>("Server=47.107.70.43;User ID=sa;Password=cqTravel@uat;Database=CQTravelX;")
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<CInfrastruct.EventSourceContext.EventSourceContext>()
            //    .Named<DbContext>("Server=47.107.70.43;User ID=sa;Password=cqTravel@uat;Database=CQTravelX;")
            //    .InstancePerLifetimeScope();
            #endregion

            #region 仓储,包括事件溯源仓储
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(d => d.Name.Contains("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //        builder.RegisterAssemblyTypes(typeof(EventRepository).Assembly)
            //.Where(d => d.Name.Contains("Repository"))
            //.InstancePerDependency();

            //        builder.RegisterAssemblyTypes(typeof(EventSourceRepository).Assembly)
            //            .Where(d => d.Name.Contains("Repository"))
            //            .AsImplementedInterfaces()
            //            .InstancePerDependency();
            builder.RegisterType<HttpContextAccessor>()
              .As<IHttpContextAccessor>();

            //builder.RegisterType<EventRepository>()
            //    .As<IEventRepository>();

            //builder.RegisterType<EventSourceRepository>()
            // .As<IEventSourceRepository>();

            #endregion

            builder.RegisterType<ServiceAOP>();

            #region 工作单元
            builder.RegisterAssemblyTypes(typeof(UnitOfWork).Assembly)
                .Where(d => d.Name.Contains("UnitOfWork"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            #endregion

            #region 通知处理程序,目的是在控制器中能拿到通知模型
            builder.RegisterType<NotificationHandler>()
                .As<INotificationHandler<NotificationModel>>();
            #endregion

            #region 总线处理程序
            builder.RegisterAssemblyTypes(typeof(Bus).Assembly)
                .Where(d => d.Name.Contains("Bus"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            #endregion

            #region 自动映射
            builder.RegisterAutoMapper(typeof(AutoMapperProfile).Assembly);
            #endregion

            #region 服务
            var serviceAssembly = Assembly.Load("CApplication");

            builder.RegisterAssemblyTypes(serviceAssembly)
                   .Where(d => d.Name.Contains("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors()
                   //.EnableClassInterceptors()
                   .InterceptedBy(typeof(ServiceAOP))
                   ;

            builder.RegisterAssemblyTypes(typeof(PermissionService).Assembly)
                  .Where(d => d.Name.Contains("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerDependency();
            #endregion
        }
    }
}
