<%@ Page
    Title="Release Manager: Version Tickets"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<VersionViewModel>" %>

<%@ Import namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="ReleaseManager.Web.HtmlHelpers" %>
<%@ Import namespace="ReleaseManager.Web.Models"%>

<asp:Content ID="VersionDetail" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle">
        <%=Html.ReleaseLink(Model.ReleaseName, Model.ReleaseName, "Summary") %>:
        <%=Model.ReleaseComponent.Name%>
        - Tickets
    </span>
    <%=Html.VersionHeaderLinks(Model) %>
</div>
<table class="propertiesTable">
    <tr>
        <td>Tickets Ready:</td>
        <td>?</td>
    </tr>
    <tr>
        <td>Tickets Not Ready</td>
        <td>?</td>
    </tr>
</table>
<table class="itemsTable runOnTable">
    <tr> 
        <th width="100px">Key</th> 
        <th>Status</th> 
        <th>Assignee</th> 
        <th>Summary</th>    
    </tr> 
    <%
    foreach (var issue in Model.AllIssues)
    {
    %>
        <tr>
            <td><a href="<%=issue.Link%>" ><%=issue.Key%></a></td>
            <td><%=issue.Status%></td>
            <td><%=issue.Assignee%></td>
            <td><%=issue.Summary%></td>
        </tr>
    <%
    }
    %>
</table>
</asp:Content>