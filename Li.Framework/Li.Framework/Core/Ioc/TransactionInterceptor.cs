using Castle.DynamicProxy;
using Li.Framework.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Li.Framework.Core.Ioc
{
    /* ==============================================================================
     * 描述：事务拦截器
     * 创建人：李传刚 2017/7/19 8:52:08
     * ==============================================================================
     */
    public class TransactionInterceptor : IInterceptor
    {
        // 是否开发模式
        private bool isDev = false;

        public void Intercept(IInvocation invocation)
        {
            if (!isDev)
            {
                MethodInfo methodInfo = invocation.MethodInvocationTarget;
                if (methodInfo == null)
                {
                    methodInfo = invocation.Method;
                }

                TransactionHandlerAttribute transaction =
                    methodInfo.GetCustomAttributes<TransactionHandlerAttribute>(true).FirstOrDefault();
                if (transaction != null)
                {
                    TransactionOptions transactionOptions = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOptions.IsolationLevel = transaction.IsolationLevel;
                    //设置事务超时时间为60秒
                    transactionOptions.Timeout = new TimeSpan(0, 0, transaction.Timeout);
                    using (TransactionScope scope = new TransactionScope(transaction.ScopeOption, transactionOptions))
                    {
                        try
                        {
                            //实现事务性工作
                            invocation.Proceed();
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            // 记录异常
                            throw ex;
                        }
                    }
                }
                else
                {
                    // 没有事务时直接执行方法
                    invocation.Proceed();
                }
            }
            else
            {
                // 开发模式直接跳过拦截
                invocation.Proceed();
            }
        }
    }
}
