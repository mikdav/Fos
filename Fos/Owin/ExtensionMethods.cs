using System;
using System.Collections.Generic;
using Fos.Logging;
using Owin;

namespace Fos.Owin
{
	public static class ExtensionMethods
	{
        /// <summary>
        /// Log statistics and show a perty page at <paramref name="relativePath"/>, accessible only if <paramref name="configureAuthentication"/> allows so.
        /// This method is just a way to <see cref="Use"/> an internal middleware that serves a page with the logged statistics.
        /// </summary>
        /// <param name="server">The instance of <see cref="Fos.FosSelfHost"/> that represents your server.</param> 
        /// <param name="aggregationInterval">The aggregation interval to show time aggregated statistics such as visit numbers. The logger will have lots of work to do if this is too small, so balance this carefully.</param>
        /// <remarks>You can use <see cref="Fos.Owin.ShuntMiddleware"/> to shunt requests to a certain path to a different <see cref="Owin.IAppBuilder"/> to serve the statistics page.</remarks>
        public static IAppBuilder UseStatisticsLogging(this IAppBuilder builder, FosSelfHost server, TimeSpan aggregationInterval)
        {
            if (server == null)
                throw new ArgumentNullException("server");
            
            var logger = new Fos.Logging.StatsLogger(aggregationInterval);
            server.StatisticsLogger = logger;
            builder.Use<StatsPageMiddleware>(logger);

            return builder;
        }

        public static string RequestPath(this IDictionary<string, object> context)
        {
            return (string)context["owin.RequestPathBase"] + (string)context["owin.RequestPath"];
        }
	}
}
