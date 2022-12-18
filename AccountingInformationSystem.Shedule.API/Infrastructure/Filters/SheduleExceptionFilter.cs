using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountingInformationSystem.Shedules.API.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class SheduleExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                _ => new BadRequestObjectResult(context.Exception.Message)
            };
            context.ExceptionHandled = true;
        }
    }
}
