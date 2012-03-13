<%@ Page
    Title="Release Manager: New Component"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<ComponentViewModel>" %>
   
<%@ Import Namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="NewComponentMain" ContentPlaceHolderID="MainContent" runat="server">
   
<div class="contentHeader" >
    <span class="headerTitle">New Component</span>
</div>
<%
using (Html.BeginForm("Create", "Component", FormMethod.Post)) {
    %>
    <table class="propertiesTable">
        <tr>
            <td style="width:15%">Name:</td>
            <td><%=Html.EditorFor(m => m.Name)%></td>
        </tr>
        <tr>
            <td>Location:</td>
            <td><%=Html.EditorFor(m => m.Location)%></td>
        </tr>
    </table>
    <input type="submit" value="Save" />
<% } // End form %>

</asp:Content>
