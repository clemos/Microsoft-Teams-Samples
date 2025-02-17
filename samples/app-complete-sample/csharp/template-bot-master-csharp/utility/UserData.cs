﻿using Microsoft.Teams.TemplateBotCSharp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Teams.TemplateBotCSharp.utility
{
    public class UserData
    {
        public string ComposeExtensionCardType { get; set; }

        public string BotId { get; set; }
        public string ChannelId { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public string ServiceUrl { get; set; }
        public List<WikiHelperSearchResult> ComposeExtensionSelectedResults { get; set; }
    }
}