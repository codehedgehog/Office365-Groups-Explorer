﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace O365Groups.Models
{
	public class Group
	{
		public string accessType { get; set; }
		public bool allowExternalSenders { get; set; }
		public bool autoSubscribeNewMembers { get; set; }
		public string description { get; set; }
		public string displayName { get; set; }
		public string[] groupTypes { get; set; }
		public string id { get; set; } // identifier
		public bool isSubscribedByMail { get; set; }
		public string mail { get; set; }
		public bool mailEnabled { get; set; }
		public string mailNickname { get; set; }
		public string onPremisesLastSyncDateTime { get; set; }  //timestamp
		public string onPremisesSecurityIdentifier { get; set; }
		public bool? onPremisesSyncEnabled { get; set; }
		public string[] proxyAddresses { get; set; }
		public bool securityEnabled { get; set; }
		public int unseenCount { get; set; }
		public string visibility { get; set; }
	}

}