<%@ Page 
    Title="Release Manager: Version Detail"
    Language="C#" 
    MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<VersionViewModel>" %>
    
<%@ Import namespace="ReleaseManager.Web.Models"%>

<asp:Content ID="VersionDetail" ContentPlaceHolderID="MainContent" runat="server">

<%
    ViewData["Title"] = "Detail";
    Html.RenderPartial("VersionDetail", Model);
%>

<div class="itemHeader">Commit History</div>

<%
using (Html.BeginForm("Save", "Version", FormMethod.Post))
{
    Response.Write(Html.HiddenFor(m => m.ReleaseName));
    Response.Write(Html.HiddenFor(m => m.ComponentName));
    Html.RenderPartial("RevisionList", Model.Revisions.OrderByDescending(r => r.Number).ToList());
}
%>

</asp:Content>