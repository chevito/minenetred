using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Core
{
    internal static class TimeEntryPostHelper
    {
        public static string GetTimeEntryJsonFormat(string authKey, int issueId, string spentOn, double hours, int activityId, string comments)
        {
            var formatedEntryString = new JObject();
            formatedEntryString.Add("issue_id", issueId);
            formatedEntryString.Add("spent_on", spentOn);
            formatedEntryString.Add("hours", hours);
            formatedEntryString.Add("activity_id", activityId);
            formatedEntryString.Add("comments", comments);
            var jsonObject = new JObject();
            jsonObject.Add("time_entry", formatedEntryString);
            var toContent = JsonConvert.SerializeObject(jsonObject, SerializerHelper.Settings);
            return toContent;
        }
    }
}
