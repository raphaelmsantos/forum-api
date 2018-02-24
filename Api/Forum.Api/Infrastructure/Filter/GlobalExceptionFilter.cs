using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RaphaelSantos.Framework.Business;
using System;

namespace Forum.Api.Infrastructure.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        public GlobalExceptionFilter()
        {
        }        

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is RuleException)
            {
                var ruleEx = context.Exception as RuleException;
                var rule = ruleEx.Rule ?? new BrokenRule();

                if (string.IsNullOrWhiteSpace(rule.Message))
                    rule.Message = ruleEx.Message;

                context.Result = new ObjectResult(ruleEx.Rule)
                {
                    StatusCode = 400,
                    DeclaredType = typeof(BrokenRule)
                };
            }
            else
            {
                var response = new GenericErrorResult
                {
                    Message = context.Exception.Message,
                    StackTrace = context.Exception.StackTrace
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(GenericErrorResult)
                };
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
