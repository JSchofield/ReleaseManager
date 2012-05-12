<%@ Page 
    Title="Release Manager: Release List" 
    Language="C#" 
    MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<IList<ComponentViewModel>>" %>

<%@ Import namespace="System.Web.Mvc.Html" %>
<%@ Import namespace="ReleaseManager.Web.Models" %>

<asp:Content ID="ComponentList" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentHeader" >
    <span class="headerTitle">Components</span>
    <span class="headerLinks"><%=Html.RouteLink("New Component", "NewComponent", new object { }, new { id = "addComponent" })%></span>
</div>
<%
using (Html.BeginForm("Save", "Component", FormMethod.Post)) 
{
%>
    <table class="itemsTable leftAligned">
        <tr>
            <th width="250px">Name</th>
            <th width="50px">Active</th>
            <th>Location</th>
        </tr>
        <%
            for (int index = 0; index < Model.Count(); index += 1)
            {
                %>
                <tr>
                    <td>
                        <%=Html.HiddenFor(m => m[index].Name) %>
                        <%=Model[index].Name %>
                    </td>
                    <td><%=Html.EditorFor(m => m[index].Active) %></td>
                    <td><%=Html.EditorFor(m => m[index].Location) %></td>
                </tr>
                <%
            }
    %>
    </table>
    <input type="submit" value="Save" id="save" />
<%
}
%>
</asp:Content>
