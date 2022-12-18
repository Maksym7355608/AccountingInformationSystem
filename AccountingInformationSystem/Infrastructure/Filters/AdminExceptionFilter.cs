using AccountingInformationSystem.Administration.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountingInformationSystem.Filters
{
    public class AdminExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                AuthorizeException => new UnauthorizedObjectResult(context.Exception.Message),
                InvalidInputException => new BadRequestObjectResult(context.Exception.Message),
                _ => new BadRequestObjectResult(context.Exception.Message)
            };
            context.ExceptionHandled = true;
        }
    }
}
