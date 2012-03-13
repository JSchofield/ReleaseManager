<%@ Page
    Title="Release Manager: Release Tickets"
    Language="C#" 
    MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<ReleaseViewModel>" %>

<%@ Import namespace="ReleaseManager.Web.Models"%>
<%@ Import namespace="ReleaseManager.Web.HtmlHelpers"  %>

<asp:Content ID="ReleaseTickets" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle"><%=Model.Name %> - Tickets</span>
    <span class="headerLinks"><%=Html.ReleaseHeaderLinks(Model.Name)%></span>
</div>
<table class="itemsTable">
    <tr> 
        <th width="100px">Key</th> 
        <th>Status</th> 
        <th>Assignee</th> 
        <th>Summary</th>    
    </tr> 
    <%
        foreach (var version in Model.Versions)
        {
%>
        <tr class="itemHeader">
            <td colspan="4">
            <%=Html.VersionLink(version, version.ComponentName, "Detail")%>
            </td>
        </tr>
    <%
            if (version.AllIssues.Count() == 0)
            {
            %>
            <tr>
                <td colspan="4">No tickets</td>
            </tr>
            <%
            }
            else
            {
                foreach (var issue in version.AllIssues)
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
            }
        }
        %>
</table>

</asp:Content>
