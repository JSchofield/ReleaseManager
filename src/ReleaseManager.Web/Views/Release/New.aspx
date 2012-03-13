<%@ Page
    Title="Release Manager: Release New"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<ReleaseUpdateModel>" %>
   
<%@ Import Namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="NewReleaseHeader" ContentPlaceHolderID="HeadContent" runat="server">

<script language="javascript">

    function setClassOnRowInputs(cell) {

        var checkbox = cell.getElementsByTagName("INPUT")[0];

        // get parents until we get to the row element;
        var parent = checkbox;
        while (parent.tagName != "TR") {
            parent = parent.parentNode;
        }

        // find child inputs
        var inputs = parent.getElementsByTagName("INPUT");
        for (var index = 0; index < inputs.length; index += 1) {
            var input = inputs[index];
            if (input != checkbox) {
                input.className = (checkbox.checked ? "visibleInput" : "hiddenInput");
            }
        }
    }

</script>

</asp:Content>

<asp:Content ID="NewReleaseMain" ContentPlaceHolderID="MainContent" runat="server">
   
<div class="contentHeader" >
    <span class="headerTitle">New Release</span>
</div>
<%
using (Html.BeginForm("Create", "Release", FormMethod.Post)) {
    %>
    <table class="propertiesTable">
        <tr>
            <td style="width:15%">Name:</td>
            <td><%=Html.EditorFor(m => m.Name)%></td>
        </tr>
        <tr>
            <td>Release Manager:</td>
            <td><%=Html.EditorFor(m => m.ReleaseManager)%></td>
        </tr>
        <tr>
            <td>Release Date:</td>
            <td><%=Html.EditorFor(m => m.ReleaseDate)%></td>
        </tr>
    </table>
    <table class="itemsTable runOnTable" >
        <tr>
            <th>Component</th>
            <th style="width:70px;">Included</th>
            <th>Start Revision</th>
            <th>End Revision</th>
        </tr>
    <%
    for(int index = 0; index < Model.Components.Count(); index +=1)
    {
    %>
    <tr>
        <td>
            <%=Html.EditorFor(m => m.Components[index].Name)%>        
        </td>
        <td><!-- onclick="setClassOnRowInputs(this)"> -->
            <%=Html.EditorFor(m => m.Components[index].Included) %></td>
        <td><%=Html.EditorFor(m => m.Components[index].StartRevision)%></td>
        <td><%=Html.EditorFor(m => m.Components[index].EndRevision)%></td>
    </tr>
    <%
    }
    %>
    </table>
    <input type="submit" value="Save" />
<% } // End form %>

</asp:Content>
