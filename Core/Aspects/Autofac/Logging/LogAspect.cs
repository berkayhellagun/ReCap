using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        readonly LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type logger)
        {
            if (logger.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception("This is not validator type.");
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(logger);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameter = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameter.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetail = new LogDetail
            {
                LogParameters = logParameter,
                MethodName = invocation.Method.Name
            };

            return logDetail;
        }
    }
}
