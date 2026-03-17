using Microsoft.AspNetCore.Mvc.Filters;

namespace DTOs_AutoMapper.Filters
{
    public class LogActionFilter : IActionFilter,IResultFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action Started");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action Completed");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Result started");       
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Result Ended");
        }
    }
}
