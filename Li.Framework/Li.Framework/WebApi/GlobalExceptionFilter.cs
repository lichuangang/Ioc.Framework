using Li.Framework.Core.Ioc;
using Li.Framework.Core.Log4Net;
using Li.Framework.Dtos;
using Li.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Li.Framework.Extensions;

namespace Li.Framework.WebApi
{
    /* ==============================================================================
     * 描述：全局异常处理
     * 创建人：李传刚 2017/7/20 16:37:56
     * 使用时在WebApiConfig中
     * config.Filters.Add(new GlobalExceptionFilter());
     * ==============================================================================
     */
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var result = R.Fail(actionExecutedContext.Exception.Message);

            if (actionExecutedContext.Exception is BusinessException)
            {
                result.Code = ((BusinessException)actionExecutedContext.Exception).Code ?? 0;
            }
            else
            {
                var message = string.Format(
                    "未捕获的异常，异常信息为：【{0}】 参数信息:【{1}】",
                    actionExecutedContext.Exception,
                    actionExecutedContext.ActionContext.ActionArguments.Serialize());
                var logger = ContainerManager.Resolve<ILoggerFactory>().Create(actionExecutedContext.ActionContext.GetType());
                logger.Error(message);
            }

            actionExecutedContext.Response = new HttpResponseMessage
            {
                Content = new StringContent(result.Serialize())
            };
        }
    }
}
