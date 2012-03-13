<%@ Page
    Title="Release Manager: Release Edit"
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<ReleaseUpdateModel>" %>
   
<%@ Import Namespace="ReleaseManager.Web.Models" %>
<%@ Import Namespace="ReleaseManager.Web.HtmlHelpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script language="javascript">

    function setClassOnRowInputs(cell) {

        var checkbox = cell.getElementsByTagName("INPUT")[0];

        // get parents until we get to the row element;
        var parent = checkbox;
        while(parent.tagName != "TR") {
            parent = parent.parentNode;
        }

        // find child inputs
        var inputs = parent.getElementsByTagName("INPUT");
        for (var index = 0; index < inputs.length; index += 1) {
            var input = inputs[index];
            if (input != checkbox) {
                input.className = (checkbox.checked ? "includedComponent" : "disincludedComponent");
            }
        }
    }

    function endOnSelectedRevisions() {
        
        var index = 0;
        while (true) {
            var active = document.getElementById("Components_" + index + "__Included");
            var selected = document.getElementById("Components_" + index + "__SelectedRevision");
            var end = document.getElementById("Components_" + index + "__EndRevision");
            if (selected == null) { break; }
            if (active.checked && selected.value  != "") {
                end.value = selected.value;
            }
            index += 1;
        }

    }

</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
<div class="contentHeader" >
    <span class="headerTitle"><%=Model.Name %> - Edit</span>
    <span class="headerLinks"><%=Html.ReleaseHeaderLinks(Model.Name)%></span>
</div>
<%
using (Html.BeginForm("Save", "Release", FormMethod.Post)) {
    Response.Write(Html.Hidden("originalName", Model.Name));
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
            <th>End Revision<br />    
                <input type="button" value="Set to Selected" onclick="endOnSelectedRevisions()"/>
            </th>
            <th>Selected<br />Revision</th>
        </tr>
    <%
    for(int index = 0; index < Model.Components.Count(); index +=1)
    {
    %>
    <tr>
        <td>
            <%=Html.EditorFor(m => m.Components[index].Name)%>        
        </td>
        <td onclick="setClassOnRowInputs(this)"> 
            <%=Html.EditorFor(m => m.Components[index].Included) %></td>
        <td><%=Html.EditorFor(m => m.Components[index].StartRevision)%></td>
        <td><%=Html.EditorFor(m => m.Components[index].EndRevision)%></td>
        <td><%=Html.EditorFor(m => m.Components[index].SelectedRevision)%></td>
    </tr>
    <%
    }
    %>
    </table>
    <input type="submit" value="Save" />


<% } // End form %>

</asp:Content>
