<%@ Page 
    Title="Release Manager: Release WIP"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<ReleaseViewModel>" %>
    
<%@ Import Namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="ReleaseManager.Web.HtmlHelpers" %>
<%@ Import Namespace="ReleaseManager.Web.Models" %>

<asp:Content ID="ReleaseWip" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle"><%=Model.Name%> - Work in Progress</span>
    <span class="headerLinks"><%=Html.ReleaseHeaderLinks(Model.Name)%></span>
</div>
<table class="itemsTable">
    <tr>
        <th>Revision</th>
        <th>Time</th>
        <th>Author</th>
        <th colspan="3">Jira Tickets</th>
        <th>Status</th>
        <th>Propogated Status</th>
    </tr>
    <%
    foreach (var version in Model.Versions)
    {
    %>
        <tr class="itemHeader" >
            <td colspan="8">
                <%=Html.VersionLink(version, version.ComponentName, "Detail")%>
            </td>
        </tr>
        <%
        if (version.WipDescending.Count() == 0)
        {
        %>
            <tr><td colspan="8">No work in progress</td></tr>
        <%
        }
        else
        {
            IList<RevisionViewModel> revisions = version.WipDescending.ToList();
            for (int index = 0; index < revisions.Count(); index +=1 )
            {
                var revision = revisions[index];
                %>
                <tr>
                    <td><%=revision.Number %></td>
                    <td><%=revision.Time %></td>
                    <td><%=revision.Author %></td>
                    <%
                    Html.RenderPartial("IssueCells", revision.Issues);
                    Html.RenderPartial("ReleaseStatusCell", revision.ReleaseStatus);
                    Html.RenderPartial("ReleaseStatusCell", revision.PropogatedStatus);
                    %>
                </tr>
                <%
            }
        }
    }
    %>
</table>

</asp:Content>