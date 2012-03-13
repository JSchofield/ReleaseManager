<%@ Control 
    Language="C#"
    Inherits="System.Web.Mvc.ViewUserControl<IList<RevisionViewModel>>" %>

<%@ Import Namespace="ReleaseManager.Web.Models" %>
   
<table class="itemsTable" >
    <tr>
        <th>Selected</th>
        <th>Revision</th>
        <th>Time</th>
        <th>Author</th>
        <th colspan="3">Jira Tickets</th>
        <th>Status</th>
        <th>Override</th>
        <th>Propogated Status</th>
    </tr>
    <%
    if (Model.Count() == 0)
    {
    %>
        <tr><td colspan="10">No commits</td></tr>
    <%
    }
    else
    {
        for(int index = 0; index < Model.Count(); index += 1)
        {
            RevisionViewModel revision = Model[index];
            string rowClass = "";
            if (revision.Selected)
            {
                rowClass = "selectedRevision";
            }
            if (revision.Blocking)
            {
                rowClass = "blockingRevision";
            }
                
            %>
            <tr class="<%=rowClass%>" title="<%=Model[index].Message %>">
                <td><%=Html.RadioButton("Selected", Model[index].Number, Model[index].Selected)%></td>
                <td><%=Html.EditorFor(m => m[index].Number)%></td>
                <td><%=revision.Time %></td>
                <td><%=revision.Author %></td>
                <%Html.RenderPartial("IssueCells", revision.Issues); %>
                <%=Html.DisplayFor(m => m[index].NaturalReleaseStatus, "ReleaseStatusCell") %>
                <%=Html.EditorFor(m => m[index].UserOverride, "ReleaseStatusOverride") %>
                <%=Html.DisplayFor(m => m[index].PropogatedStatus, "ReleaseStatusCell") %>
            </tr>
            <%
        }
    }
    %>
</table>
<input type="submit" value="Save" />
