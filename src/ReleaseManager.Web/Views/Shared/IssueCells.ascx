<%@ Control 
    Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IssueViewModel>>" %>
    
<%@ Import Namespace="ReleaseManager.Web.Models" %>

<td class="lightRightBorder">
<%
    foreach (IssueViewModel issue in Model)
    {
        Response.Write("<a href=\"" + issue.Link + "\">" + issue.Key + "</a><br/>");
    }
%>
</td>
<td class="lightRightBorder">
<%
    foreach (IssueViewModel issue in Model)
    {
        Response.Write(issue.Assignee + "<br/>");
    }
%>
</td>
<td>
<%
    foreach (IssueViewModel issue in Model)
    {
        Response.Write(issue.Status + "<br/>");
    }
%>
</td>