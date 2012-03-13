<%@ Control 
    Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<ReleaseManager.Web.Models.ReleaseStatusViewModel>" %>

<%
    string cellClass = "cell" + Model.ReleaseStatus.Replace(" ", string.Empty);
%>

<td class="<%=cellClass%>"><%=Model.ReleaseStatus%></td>
