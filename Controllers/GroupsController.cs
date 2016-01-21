﻿using Newtonsoft.Json;
using O365Groups.Models;
using O365Groups.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace O365Groups.Controllers
{
	[Authorize]
	public class GroupsController : Controller
	{
		// GET: Groups
		public async Task<ActionResult> Index()
		{
			List<Group> groups = new List<Group>();

			ViewBag.Title = "All Groups";
			ViewBag.EnableSearch = true;

			string APIURL = SettingsHelper.MSGraphResourceId + "/beta/myorganization/groups";
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

		[HttpPost]
		public async Task<ActionResult> Index(string search)
		{
			List<Group> groups = new List<Group>();

			if (String.IsNullOrEmpty(search))
			{
				ViewBag.Message = "Enter search value";
				return View(groups);
			}

			ViewBag.Title = "Group search";
			ViewBag.EnableSearch = true;

			string apiUrl = String.Format("{0}/beta/myorganization/groups?$filter=startswith(displayName,'{1}')",
																				SettingsHelper.MSGraphResourceId, search);

			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				groups = await HttpHelper.GetGroups(apiUrl);
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

		public async Task<ActionResult> Details(string id)
		{
			Group group = null;

			ViewBag.Title = "Group Details";

			string apiUrl = String.Format("{0}/beta/myorganization/groups/{1}", SettingsHelper.MSGraphResourceId, id);
			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				string responseContent = await HttpHelper.GetHttpResource(apiUrl);
				group = JsonConvert.DeserializeObject<Group>(responseContent);
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
			return View(group);
		}

		public async Task<ActionResult> Conversations(string id)
		{
			List<Conversation> convos = new List<Conversation>();

			ViewBag.Title = "Group Conversations";
			ViewBag.GroupId = id;

			string apiUrl = String.Format("{0}/beta/myorganization/groups/{1}/conversations", SettingsHelper.MSGraphResourceId, id);
			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				string responseContent = await HttpHelper.GetHttpResource(apiUrl);
				var responseObject = JsonConvert.DeserializeObject<GetConversationsResponse>(responseContent);
				foreach (var item in responseObject.value)
				{
					convos.Add(item);
				}
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
			return View(convos);
		}

		public async Task<ActionResult> Threads(string id)
		{
			List<ConversationThread> threads = new List<ConversationThread>();

			ViewBag.Title = "Group ConversationThreads";
			ViewBag.GroupId = id;

			string apiUrl = String.Format("{0}/beta/myorganization/groups/{1}/threads", SettingsHelper.MSGraphResourceId, id);
			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				string responseContent = await HttpHelper.GetHttpResource(apiUrl);
				var responseObject = JsonConvert.DeserializeObject<GetThreadsResponse>(responseContent);
				foreach (var item in responseObject.value)
				{
					threads.Add(item);
				}
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
			return View(threads);
		}

		public async Task<ActionResult> Events(string id)
		{
			List<Event> events = new List<Event>();

			ViewBag.Title = "Group Events";
			ViewBag.GroupId = id;

			string apiUrl = String.Format("{0}/beta/myorganization/groups/{1}/events", SettingsHelper.MSGraphResourceId, id);
			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				string responseContent = await HttpHelper.GetHttpResource(apiUrl);
				var responseObject = JsonConvert.DeserializeObject<GetEventsResponse>(responseContent);
				foreach (var item in responseObject.value)
				{
					events.Add(item);
				}
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
			return View(events);

		}

		public async Task<ActionResult> Files(string id)
		{
			List<O365Groups.Models.DriveItem> files = new List<O365Groups.Models.DriveItem>();

			ViewBag.Title = "Group Files";
			ViewBag.GroupId = id;

			string apiUrl = String.Format("{0}/beta/myorganization/groups/{1}/drive/root/children", SettingsHelper.MSGraphResourceId, id);
			ViewBag.Message = "API URL: " + apiUrl;

			try
			{
				string responseContent = await HttpHelper.GetHttpResource(apiUrl);
				var responseObject = JsonConvert.DeserializeObject<GetFilesResponse>(responseContent);

				foreach (var item in responseObject.value)
				{
					files.Add(item);
				}
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
			return View(files);
		}
	}
}