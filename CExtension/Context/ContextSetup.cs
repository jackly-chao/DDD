using CInfrastruct.EventSourceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CExtension.Context
{
    public static class ContextSetup
    {
        public static void ContextServiceSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //数据库上下文
            services.AddDbContext<CInfrastruct.Context.Context>(options => options.UseSqlServer("Server=47.107.70.43;User ID=sa;Password=cqTravel@uat;Database=CQTravelX;"));
            //事件溯源数据库上下文
            services.AddDbContext<EventSourceContext>(options => options.UseSqlServer("Server=47.107.70.43;User ID=sa;Password=cqTravel@uat;Database=CQTravelX;"));
        }
    }
}
