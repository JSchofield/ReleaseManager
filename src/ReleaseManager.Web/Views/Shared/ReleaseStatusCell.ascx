<%@ Control 
    Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<ReleaseManager.Web.Releasability>" %>

<%
    string cellClass = "cell" + Model.ToString().Replace(" ", string.Empty);
%>

<td class="<%=cellClass%>"><%=Model.ToString()%></td>
