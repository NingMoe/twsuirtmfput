<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptEdit.aspx.cs" Inherits="Base_Organization_DeptEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=this.GetScript1_4_3 %>
    <script src="../../Scripts/CommonCloseWindow.js"></script>
    <script type="text/javascript">

        var ActionType = '<%=ActionType %>'
        var ChangeBMID =""
        var 上级部门 = '<%=PIDName %>'
        var 上级部门ID = '<%=PID %>'
        var SaveType ="0"
        $(document).ready(function () {
 
            if ('<%=DeptID %>' == "" && ActionType == "Edit") {
                alert("根部门不允许更改！")
                CommonCloseWindow("<%=OpenDiv%>", "", "")
            }

            $("#上级部门").val(上级部门)

            $("input[minput=true],select[minput=true]").after(" <span style='color: red'>*</span>")
             
            if(ActionType == "Edit")
            {
                SaveType = "1";
               
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: {
                        "sqlData": 'SELECT * FROM dbo.CMS_DEPARTMENT WHERE ID=<%=DeptID %>',
                        "Condition": " "
                    },
                    url: "../Common/Ajax_Request.aspx?typeValue=GetDataBySql",
                    success: function (obj) {
                        if (obj.rows.length > 0) {
                            $("#<%=TableName %>_Name").val(obj.rows[0].NAME)
                            $("#<%=TableName %>_ShowOrder").val(obj.rows[0].SHOW_ORDER)
                            $("#<%=TableName %>_DepartmentAbbreviation").val(obj.rows[0].DepartmentAbbreviation)
                        }
                    },
                    error:function(p)
                    {
                        debugger
                    }
                });
            }
            SetBackgroundColor();
        });


        function fnSave() {
            if (!CheckValue('form1'))
                return false;
            if (CheckValue("form1")) {
                var jsonStr1 = "[{"
                jsonStr1 += "'" + "SHOW_ORDER" + "':'" + $("#<%=TableName %>_ShowOrder").val() + "',";
                jsonStr1 += "'" + "NAME" + "':'" + $("#<%=TableName %>_Name").val() + "',";
                jsonStr1 += "'" + "DepartmentAbbreviation" + "':'" + $("#<%=TableName %>_DepartmentAbbreviation").val() + "'";

                if (SaveType == "0") {
                    jsonStr1 += ",'" + "ID" + "':'" + "<%=SaveID %>" + "',";
                    jsonStr1 += "'" + "ICON_NAME" + "':'" + "ICON_DEP_REAL" + "',";
                    jsonStr1 += "'" + "PID" + "':'" + "<%=DeptID %>" + "',";
                    jsonStr1 += "'" + "DEP_TYPE" + "':'0'";
                }
                else {
                    if (上级部门ID != '<%=DeptID%>' && 上级部门ID != '<%=PID%>' )
                        jsonStr1 += ",'" + "PID" + "':'" + 上级部门ID + "'";
                }
                jsonStr1 += "}]";

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: {
                        "JArray": "[" + jsonStr1 + "]",
                        "SaveTableName": '<%=TableName %>',
                        "type": SaveType,
                        "UpdateCon": (SaveType==1? "ID='<%=DeptID %>'":""),
                        "UserID": "<%=CurrentUser.ID %>"
                    },
                    url: "/portal/Base/Organization/ZTH_GetInfo_ajax.aspx?typeValue=SaveHstListByJArray",
                    success: function (obj) {
                        if (obj > 0) {
                            alert("保存成功！");
                            SaveType = "1";
                            // reloadtree();
                            window.parent.parent.ParentCloseWindow();
                            window.parent.RefreshGridByChildFrame('<%=GridID %>');
                        <%--    window.parent.reloadPage()
                            CommonCloseWindow("<%=OpenDiv%>", "", "")--%>
                        } else {
                            alert("保存失败,请刷新页面！");
                        }
                    },
                    error: function (obj) {
                        debugger
                    }
                });
            }
        }
         
        function ChangeDep() {
             
            openDictionary("ChangeDep.aspx?DeptID=" + 上级部门ID + "&NoSelectDeptID=" + '<%=DeptID + "," %>', 500, 400, "更换部门");
 
        }

        function GetValueFunciton(o)
        {
            var 部门ID = o.Change_部门ID
            if (部门ID != '<%=DeptID%>' && 部门ID != 上级部门ID) {
                上级部门 = o.Change_部门;
                上级部门ID = 部门ID
                $("#上级部门").val(上级部门)

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: {
                        "DepartID": 上级部门ID,
                       "MyDepartID": '<%=DeptID%>'
                    },
                    url: "/portal/Base/Organization/ZTH_GetInfo_ajax.aspx?typeValue=GetDepartOrder",
                    success: function (obj) {
                        $("#<%=TableName %>_ShowOrder").val(obj)
                    }
                });
            }
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con" id="div<%=TableName %>FormTable" style="overflow-x: hidden; overflow-y: auto; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=TableName %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a>
                        <%--<a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>--%>
                    </td>
                </tr>
            </table>
            <div title="基本信息" id="div_基本信息" class="easyui-panel" style="overflow: hidden; padding: 0px; border-bottom: none; margin-right: 0; width: 100%;">
                <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width: 100%;">
                    <tr>
                        <th style="width: 30%">上级部门</th>
                        <td style="width: 70%">&nbsp;<input id="上级部门" type="text" class="box3" minput="true"  style="width: 80%" readonly="readonly" />
                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true,disabled:false" onclick="ChangeDep()" style="margin-left: 1%" id="ChangeDep"></a>
                        </td>
                    </tr>
                    <tr>
                        <th style="width: 30%">部门名称</th>
                        <td style="width: 70%">&nbsp;<input id="<%=TableName %>_Name" type="text" class="box3" minput="true" fieldtitle="部门名称"  style="width: 80%" />
                           </td>
                    </tr> 
                    <tr>
                        <th style="width: 30%">部门简称</th>
                        <td style="width: 70%">&nbsp;<input id="<%=TableName %>_DepartmentAbbreviation" type="text" class="box3" style="width: 80%" />
                            <input id="<%=TableName %>_ShowOrder" type="hidden" class="box3" value="<%=ShowOrder %>" style="width: 80%" />
                           </td>
                    </tr>
                </table>
            </div>
        </div>

        <input type="hidden" value="" id="DictionaryConditon" />  
        <input type="hidden" value="" id="childForm" /> 
        <input type="hidden" value="" id="ajaxinfo" />
        <div closed="true" class="easyui-window" id="divDictionary" style="overflow: hidden;" />

    </form>
</body>
</html>
