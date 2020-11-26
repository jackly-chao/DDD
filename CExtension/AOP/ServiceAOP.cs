using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace CExtension.AOP
{
    public class ServiceAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //去执行当前的方法
            invocation.Proceed();
        }
    }
}
