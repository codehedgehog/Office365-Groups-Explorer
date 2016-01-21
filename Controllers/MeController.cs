﻿using O365Groups.Models;
using O365Groups.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace O365Groups.Controllers
{
	[Authorize]
	public class MeController : Controller
	{
		// GET: Me
		public ActionResult Index()
		{
			return View();
		}
		public async Task<ActionResult> MyGroups()
		{
			List<Group> groups = new List<Group>();

			ViewBag.Title = "Groups I've Joined";
			ViewBag.EnableSearch = false;

			string APIURL = String.Format("{0}/beta/me/joinedGroups",
																			SettingsHelper.MSGraphResourceId);
			ViewBag.Message = "API URL: " + APIURL;

			try
			{
				groups = await HttpHelper.GetGroups(APIURL);
			}
			catch (WebException webException)
			{
				if (webException.Response != null)
				{
					using (var reader = new StreamReader(webException.Response.GetResponseStream()))
					{
						var responseContent = reader.ReadToEnd();
						ViewBag.Message = responseContent;
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = ex.Message;
			}

			return View(groups);

		}

	}
}