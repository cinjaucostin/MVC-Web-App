using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCpractice.ActionFilter
{
    public class PerformanceActionFilter : Attribute, IActionFilter
    {
        private DateTime startTime;
        private readonly ILogger<PerformanceActionFilter> logger;

        public PerformanceActionFilter(ILogger<PerformanceActionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            startTime = DateTime.Now;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string detailsPath = context.ActionDescriptor.DisplayName;
            DateTime currentTime = DateTime.Now;
            var timePassed = currentTime - startTime;

            if(timePassed.TotalMilliseconds < 10)
            {
                logger.LogInformation($"Action {detailsPath} took {timePassed.TotalMilliseconds} ms.");
            }
            else if(timePassed.TotalMinutes >= 10)
            {
                logger.LogWarning($"Take care, action {detailsPath} lasted for {timePassed.TotalMilliseconds} ms");
            }

        }

    }
}
