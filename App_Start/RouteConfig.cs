using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace O365Groups
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
					name: "Me",
					url: "me/{action}",
					defaults: new { controller = "Me", action = "Index" }
			);
			routes.MapRoute(
					name: "Account",
					url: "account/{action}",
					defaults: new { controller = "Account", action = "SignIn" }
			);
			routes.MapRoute(
					name: "Default",
					url: "{controller}/{id}/{action}",
					defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
