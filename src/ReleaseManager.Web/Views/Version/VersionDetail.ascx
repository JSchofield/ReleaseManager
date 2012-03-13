<%@ Control 
    Language="C#"
    Inherits="System.Web.Mvc.ViewUserControl<VersionViewModel>" %>

<%@ Import Namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="ReleaseManager.Web.HtmlHelpers" %>

<div class="contentHeader" >
    <span class="headerTitle">
        <%=Html.ReleaseLink(Model.ReleaseName, Model.ReleaseName, "Summary") %>:
        <%=Model.ReleaseComponent.Name + " - " + ViewData["Title"] %>
    </span>
    <%=Html.VersionHeaderLinks(Model) %>
</div>
<table class="propertiesTable">
    <tr>
        <td>Repository:</td>
        <td><%=Model.ComponentName%></td>
    </tr>
    <tr>
        <td>Highest Releasable Revision:</td>
        <td><%=Model.HighestReleasableRevision.Number %></td>
    </tr>
    <tr>
        <td>Blocking Revision:</td>
        <td><%=Model.BlockingRevision.Number %></td>
    </tr>
</table>