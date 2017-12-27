<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataKeyFormList.aspx.cs" Inherits="Base_DataKeyFormList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据字典管理</title>
</head>

<body>
    <table id="tg" class="easyui-treegrid" title="数据字典管理" style="width: 100%;"
        data-options="iconCls: 'icon-view',rownumbers: true,animate: true,collapsible: false,fitColumns: true,url: 'Ajax_Request.aspx?typeValue=GetDataKeyList',method: 'post',idField: 'ID',treeField: 'KeyTitle'">
        <thead>
            <tr>
                <th data-options="field:'ID',width:50,editor:'text',align:'center'">ID</th>            
                <th data-options="field:'KeyCode',width:50,align:'center'">编号</th>
                <th data-options="field:'KeyTitle',width:50,editor:'text'">字典标题</th>
                <th data-options="field:'KeyValue',width:50,align:'right',align:'center'">数据字典值</th>
                <th data-options="field:'KeySort',width:50,align:'center'">字典排序</th>
                <th data-options="field:'KeyDesc',width:50,align:'center'">字典描述</th>
            </tr>
        </thead>
    </table>
    <div closed="true" class="easyui-window" id="divParentContent" style="overflow: hidden;" />
    <script type="text/javascript">
        $(document).ready(function () {
            var heightBody = document.documentElement.clientHeight + 10;
            $('#tg').treegrid({
                onLoadSuccess: function () {
                    $('#tg').treegrid('collapseAll');
                }
            });
            $('#tg').datagrid({
                height: heightBody,
                toolbar: [{
                    iconCls: 'icon-add',
                    text: "添加主项",
                    disabled: false,
                    handler: function () {
                        FormDialog('DataKeyAdd.aspx', '添加主项', 500, 320);
                    }
                }, '-', {
                    iconCls: 'icon-ok',
                    text: "添加子项",
                    disabled: false,
                    handler: function () {
                        var row = $('#tg').datagrid('getSelected');
                        if (row == null) {
                            $.messager.alert('提示', '请选中一条主记录', 'info');
                        }
                        else {
                            if (row.KeyCode == null || row.KeyCode=="") {
                                $.messager.alert('提示', '该项不能添加子项', 'info');
                            }
                            else {
                                FormDialog("DataKeyAdd.aspx?ParentKey=" + row.ID + "", '添加子项', 500, 320);
                            }
                        }
                    }
                }, '-', {
                    iconCls: 'icon-edit',
                    text: "修改",
                    disabled: false,
                    handler: function () {
                        var row = $('#tg').datagrid('getSelected');
                        if (row == null){
                            $.messager.alert('提示', '请选择要修改的记录!', 'info');
                        }
                        else {
                            FormDialog("DataKeyAdd.aspx?KeyCode=" + row.KeyCode + "&ResID=<%=ResID %>&RecID=" + row.ID + "", '修改信息', 500, 320);
                    }
                    }
                }, '-', {
                    iconCls: 'icon-no',
                    disabled: false,
                    text: "删除",
                    handler: function () {
                        var rec = $('#tg').datagrid('getSelected');
                        if (rec == null) {
                            $.messager.alert('提示', '请选择一条要删除的记录', 'info');
                        }
                        else {
                            $.ajax({
                                type: "POST",
                                url: "Ajax_Request.aspx?typeValue=KeyDataDelete&ResID=<%=ResID %>&RecID=" + rec.ID,
                                success: function (strJSON) {
                                    var obj = eval("(" + strJSON + ")");
                                    if (obj.success || obj.success == "true") {
                                        alert("删除成功！");                                   
                                    } else {
                                        alert("删除失败！");
                                    }
                                    $('#tg').treegrid('reload');
                                }
                            });
                        }
                    }
                }]
            });
        });

        function FormDialog(url, title, width, height) {
            $('#divParentContent').append($("<iframe scrolling='no' id='FromInfo' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
                title: title,
                width: width,
                height: height,
                cache: false,
                closed: true,
                shadow: false,
                closable: true,
                draggable: true,
                resizable: false,
                modal: true
            });
            $('#FromInfo')[0].src = encodeURI(url);
            $('#divParentContent').dialog('open');
        }

        function ParentCloseWindow() {
            $('#divParentContent').dialog('close');
            $('#tg').treegrid('reload');
        }
    </script>
</body>
</html>
