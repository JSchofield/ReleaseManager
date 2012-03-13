<%@ Control 
    Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<ReleaseManager.Web.Models.ReleaseStatusViewModel>" %>

<%
    string cellClass = "";
    if (Model != null)
    {
        cellClass = "cell" + Model.ReleaseStatus.Replace(" ", string.Empty);
    }
%>

<td class="<%=cellClass%>">
<%=Html.DropDownListFor(m => m.ReleaseStatus, new SelectList(Model.AllowableValues, Model.ReleaseStatus))%>