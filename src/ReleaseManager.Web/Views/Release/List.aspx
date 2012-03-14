<%@ Page 
    Title="Release Manager: Release List" 
    Language="C#" 
    MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<ReleaseViewModel>>" %>

<%@ Import namespace="ReleaseManager.Web.HtmlHelpers" %>
<%@ Import namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>

<asp:Content ID="ReleaseList" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle">Releases</span>
    <span class="headerLinks"><%=Html.RouteLink("New Release", "NewRelease", new object { }, new { id = "addRelease" })%></span>
</div>
<table class="itemsTable leftAligned">
    <tr>
        <th>Name</th>
        <th width="100px">Release Date</th>
        <th>Release Manager</th>
        <th width="50px" />
    </tr>
    <%
    foreach(var release in Model)
    {
        %>
        <tr>
            <td><%=Html.ReleaseLink(release.Name, release.Name, "Summary")%></td>
            <td><%=release.ReleaseDate%></td>
            <td><%=release.ReleaseManager%></td>
            <td class="deleteLink"><%=Html.ReleaseLink("delete", release.Name, "Delete") %></td>
        </tr>
        <%
    }
    %>
</table>
</asp:Content>
