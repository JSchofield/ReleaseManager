﻿<%@ Control 
    Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<bool>" %>

<input name="[<%=Model.  %>].Active" type="hidden" value="<%=Model%>">
<input checked="checked" class="check-box" name="[0].Active" type="checkbox" value="<%=Model%>">