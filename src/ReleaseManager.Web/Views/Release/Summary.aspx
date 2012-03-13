<%@ Page
    Title="Release Manager: Release Summary"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<ReleaseViewModel>" %>

<%@ Import namespace="ReleaseManager.Web.Models"  %>
<%@ Import namespace="ReleaseManager.Web.HtmlHelpers"  %>

<asp:Content ID="ReleaseSummary" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle"><%=Model.Name %> - Summary</span>
    <span class="headerLinks"><%=Html.ReleaseHeaderLinks(Model.Name)%></span>
</div>
<table class="itemsTable">
    <tr>
        <th>Component</th>
        <th colspan="2">Revision Range</th>
        <th>Highest<br />Releasable<br />Revision</th>
        <th>Revision<br />Selected<br />for Release</th>
        <th>Releasable Commits/<br />Total Commits</th>
        <th>Blocker</th>
    </tr>
    <%
    foreach (var version in Model.Versions)
    {
    %>
        <tr>
            <td><%=Html.VersionLink(version, version.ComponentName, "Detail") %></td>
            <td><%=version.StartRevisionNumber %></td>
            <td><%=version.EndRevisionNumber %></td>
            <td><%=version.HighestReleasableRevision.Number %></td>
            <td><%=version.SelectedRevision %></td>
            <td class="graphContainer"><%=Html.ReleaseStatusGraph(version) %></td>
            <td><%=version.BlockingRevision.Number %></td>
        </tr>
    <%
    }
    %>
</table>

</asp:Content>