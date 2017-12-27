function CommonCloseWindow(opendiv, gridID, ChildgridID) {
    
    if (parent.document.getElementById(opendiv) != null) {
        parent.$("#" + opendiv).dialog('close');
        if (parent.document.getElementById(gridID) != null)
            parent.$("#" + gridID).datagrid("reload");
        if (parent.document.getElementById(ChildgridID) != null)
            parent.$("#" + ChildgridID).datagrid("reload");
    }

    if (parent.$("#" + gridID).length > 0)
        parent.$("#" + gridID).datagrid("reload");
    if (parent.$("#" + ChildgridID).length > 0)
        parent.$("#" + ChildgridID).datagrid("reload");

    if ($("#" + opendiv).length > 0)
        $("#" + opendiv).dialog('close')

    if ($("#" + gridID).length>0)
        $("#" + gridID).datagrid("reload");
    if ($("#" + ChildgridID).length > 0)
        $("#" + ChildgridID).datagrid("reload");

    for (var i = 0; i < parent.window.length; i++) {
        if (parent.window[i].document.getElementById(opendiv) != null) {
            $(parent.window[i].document.getElementById(opendiv)).dialog("close");
        }

        if (parent.window[i].document.getElementById(gridID) != null)
            parent.window[i].$("#" + gridID).datagrid("reload")

        if (parent.window[i].document.getElementById(ChildgridID) != null)
            parent.window[i].$("#" + ChildgridID).datagrid("reload")
    }
}

