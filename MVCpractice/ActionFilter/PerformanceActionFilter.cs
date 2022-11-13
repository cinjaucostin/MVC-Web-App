using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MVCpractice.Models;
using System.Linq;
using System.Text;

namespace MVCpractice.ActionFilter
{
    public class PerformanceActionFilter : Attribute, IActionFilter
    {
        private DateTime startTime;
        private readonly ILogger<PerformanceActionFilter> logger;
        private readonly CRMContext db;

        public PerformanceActionFilter(ILogger<PerformanceActionFilter> logger, CRMContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            startTime = DateTime.Now;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string detailsPath = context.ActionDescriptor.DisplayName;
            DateTime currentTime = DateTime.Now;
            var timePassed = currentTime - startTime;
            
            string message = stringBuilder.Append($"Performance Info: Action {detailsPath} took {timePassed.TotalMilliseconds} ms.").ToString();

            if (timePassed.TotalMilliseconds < 10)
            {
                message = stringBuilder.Append($"Performance Info: Action {detailsPath} took {timePassed.TotalMilliseconds} ms.").ToString();
            }
            else if (timePassed.TotalMinutes >= 10)
            {
                message = stringBuilder.Append($"Performance Error: Take care, action {detailsPath} lasted for {timePassed.TotalMilliseconds} ms.").ToString();
            }

            db.Add<Log4NetInfo>(new Log4NetInfo
            {
                Info = message
            });

            //if (timePassed.TotalMilliseconds < 10)
            //{
            //    logger.LogInformation($"Action {detailsPath} took {timePassed.TotalMilliseconds} ms.");
            //}
            //else if(timePassed.TotalMinutes >= 10)
            //{
            //    logger.LogWarning($"Take care, action {detailsPath} lasted for {timePassed.TotalMilliseconds} ms");
            //}

        }

    }
}
