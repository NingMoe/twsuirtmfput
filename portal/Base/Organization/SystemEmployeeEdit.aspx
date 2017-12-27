<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemEmployeeEdit.aspx.cs" Inherits="Base_Organization_SystemEmployeeEdit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="../../CSS/treeCss_Organization.css" rel="stylesheet" />
    <style type="text/css">
        .minputBox {
            border: 1px solid red;
        }
    </style>
    <%=GetScript1_4_3 %>
    <%-- <script src="../../Scripts/jquery-1.8.0.min.js"></script>--%>
    <script src="../../Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="../../Scripts/CommonCloseWindow.js"></script>
    <script type="text/javascript">
        var SaveType = 0
        var GSID = "";
        var GSMC = "";
        var RecID = ""
        var OldUserid = "";
        var NoSaveEmployeeInfo = false;
        var KeyWord = '<%=KeyWord %>'
        var IsSysRecID = '<%=IsSysRecID %>'
        var SelectUserRecid = "";
        var systemUserRecid = '<%=SaveID %>'
        $(document).ready(function () {
            $("th").css("padding-right", "5px")

            if (IsSysRecID == "1") {
                LoadSystemFrm();
                NoSaveEmployeeInfo = true;
            }
            else if ("<%=RecID %>" != "") {
                LoadFrm();
            }
            else {
                NoSaveEmployeeInfo = true;
                $("#Table1").show();
                $("#<%=ResID %>_登录账户").removeAttr("readonly").attr("minput", "true")
                $("#<%=ResID %>_员工编号").attr("readonly", "readonly");
            }

            $("#DIV用户信息设置").css("height", $(window).height() - 10)

            SetBackgroundColor();
        });

    function setTree() {
        $('#归属部门').tree({
            url: "../Organization/ZTH_GetInfo_ajax.aspx?typeValue=GetSelectDepartmentTree",
            queryParams: {
                Node: "<%=DeptID %>",
                    userid: '<%=CurrentUserID%>'
                },
                loadFilter: function (r) {
                    // alert(JSON.stringify(r))
                    var data = eval(r);
                    if (data.d) {
                        return data;
                    } else {
                        return data;
                    }
                },
                onLoadSuccess: function (node, data) {
                    LoadFrm();
                    GetBM()
                }
            });
        }

        function GetBM() {
            var HOSTID = "<%=DeptID %>"
            var node = $('#归属部门').tree('find', HOSTID);
            if (node) {
                $("#归属部门").tree('select', node.target);
                var Parentnode = $('#归属部门').tree('getParent', node.target);
                if (Parentnode) {
                    $("#归属部门").tree('expand', Parentnode.target);
                }
            }
        }

        function setSelect() {
            var SelectRecordData1 = {
                InitializationStr: "#归属部门",
                key: "#归属部门",
                ResID: "",
                keyWordValue: "LCXMBHCX",
                idField: "归属部门",
                textField: "归属部门",
                RecID: "",
                //deafultValue: "ZSXX_Y002_2016_03_001",
                KeyWidth: 233,
                Keyheight: 25,
                multiple: true,
                hasInitializationEvent: true,
                HasLastOperation: true,
                PercentageWidth: 80,
                panelWidth: 600,
                QueryKeyField: "归属部门",
                UserDefinedSql: "1300",
                Columns: "单位#单位,部门#部门",
                ROW_NUMBER_ORDER: ""
            }
            InitializationComboGrid(SelectRecordData1)
            $("#职务").combogrid('setValues', "<%=DeptName%>")
        }

        function SaveInfo() {
            if (KeyWord == "SystemEmployeeInfo") {
                CheckID()
            }
            else {
                fnChildSave();
            }
        }


        function CheckID() {
            var SystemUserId = $("#<%=ResID %>_登录账户").val()
            var con = "";
            if (SaveType == "1") {
                if (OldUserid != SystemUserId && OldUserid != "" && SaveType == "1") {
                    if (!confirm("您确定要更改此人的账号么？\r\n更改账号会影响原账号所有的功能，不建议更改！！！")) {
                        $("#<%=ResID %>_登录账户").val(OldUserid);
                        alert("账号已还原！")
                        return;
                    }
                }
                if (systemUserRecid != "")
                    con = " and a.id <> " + systemUserRecid
            }

           
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../Common/Ajax_Request.aspx?typeValue=GetData",
                data: {
                    "ResID": "1300",
                    "Condition": " 账号='" + SystemUserId + "'" + con
                },
                success: function (obj) {
                    if (obj.rows.length > 0) {
                        alert("该账号已存在，请更换账号名！")
                    }
                    else {
                        fnChildSave();
                    }
                }
            });
        }

        function CheckMinput() {
            $("input,select").css("border", "1px solid #bfc5c3")
            var emptyinput = $("input[minput=true][value=],select[minput=true][value=]")
            if (emptyinput.length > 0) {
                emptyinput.css("border", "1px solid red")
                alert("您有必填项未填，请检查！")
                return false
            }
            return true;
        }


        function LoadSystemFrm() {
         
            $.ajax({
                type: "POST",
                url: "../Common/Ajax_Request.aspx?typeValue=GetDataBySql",
                data: {
                    "sqlData": "SELECT a.ID, EMP_ID 登录账户 ,EMP_NAME 员工名称,Domain_mobile 手机, EMP_EMAIL 邮箱 ,b.NAME 部门  FROM dbo.CMS_EMPLOYEE AS a INNER JOIN dbo.CMS_DEPARTMENT AS b  ON a.HOST_ID=b.id ",
                    "Condition": " and ID= '" + "<%=RecID %>" + "'"
                },
                success: function (centerJson) {
                    var jsonList = eval("(" + centerJson + ")").rows;
                    for (var i = 0; i < jsonList.length; i++) {
                        for (var key in jsonList[i]) {
                            $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                        }
                    }
                    $("input,select,textarea").attr("readonly", "readonly")
                    SetBackgroundColor();
                }
            });
        }


        function LoadFrm() {

            if (SelectUserRecid == "")
                SelectUserRecid = '<%=RecID %>'

            if (SelectUserRecid != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=" + SelectUserRecid,
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            }
                        }
                        if (KeyWord == "SystemEmployeeInfo") {
                            if ($("#<%=ResID %>_登录账户").val() != "") {
                                SaveType = "1"
                                systemUserRecid = "";
                            }
                            else
                                $("#系统账户ID").val(systemUserRecid)
                        }

                        OldUserid = $("#<%=ResID %>_登录账户").val();

                        $("input,select,textarea").attr("readonly", "readonly")
                        $("input,select,textarea").removeAttr("onfocus");

                        if (SaveType == "1") {
                            // $("#Table1").hide();
                        }
                        else {
                            $("#Table1").show();
                            $("#<%=ResID %>_登录账户").removeAttr("readonly").attr("minput", "true")
                        }
                        SetBackgroundColor();
                    }
                });
            }
        }
        function saveSystemUser() {
            var HOSTID = $("#<%=ResID %>_部门ID").val()
            var jsonStr1 = "[{"
            jsonStr1 += "'" + "ID" + "':'" + "<%=SaveID %>"  + "',";
            jsonStr1 += "'" + "RESID" + "':'" + "1300" + "',";
            jsonStr1 += "'" + "HOST_ID" + "':'" + HOSTID + "',";
            jsonStr1 += "'" + "EMP_ID" + "':'" + $("#<%=ResID %>_登录账户").val() + "',";
            jsonStr1 += "'" + "EMP_NAME" + "':'" + $("#<%=ResID %>_员工名称").val() + "',";
            jsonStr1 += "'" + "EMP_TYPE" + "':'" + "" + "',";
            jsonStr1 += "'" + "EMP_HANDPHONE" + "':'" + $("#<%=ResID %>_手机").val() + "',";
            jsonStr1 += "'" + "EMP_EMAIL" + "':'" + $("#<%=ResID %>_邮箱").val() + "',";
            jsonStr1 += "}]";
            //alert(HOSTID)
            $.ajax({
                type: "POST",
                dataType: "json",
                data: {
                    "JArray": "[" + jsonStr1 + "]",
                    "SaveTableName": 'CMS_EMPLOYEE',
                    "type": SaveType,
                    "UserID": "<%=CurrentUser.ID %>"
                },
                url: "/portal/Base/Organization/ZTH_GetInfo_ajax.aspx?typeValue=SaveHstListByJArray",
                success: function (obj) {
                    if (obj > 0) {
                        alert("保存成功！");
                        window.parent.parent.ParentCloseWindow();
                        window.parent.RefreshGridByChildFrame('<%=gridID %>');
                    } else {
                        alert("保存失败,请刷新页面！");
                    }
                },
                error: function (obj) {
                    debugger
                }
            });
        }

        function saveSystemUser2() {
            var HOSTID = $("#<%=ResID %>_部门ID").val()
            $("#<%=SystemResID %>_登录帐号").val($("#<%=ResID %>_登录账户").val())
            $("#<%=SystemResID %>_中文名").val($("#<%=ResID %>_员工名称").val())
            $("#<%=SystemResID %>_手机").val($("#<%=ResID %>_手机").val())
            $("#<%=SystemResID %>_电子邮件").val($("#<%=ResID %>_邮箱").val())
           
            var jsonStr1 = "[{";
            jsonStr1 += GetFromJson("<%=SystemResID %>");
            jsonStr1 += "}]";
            systemUserRecid = "";

            $.ajax({
                type: "POST",
                dataType: "json",
                data: { "Json": "" + jsonStr1 + "" },
                url: "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=<%=SystemResID %>&RecID=" + systemUserRecid ,
                success: function (obj) {
                    if (obj.success) {
                        alert("保存成功！");
                        SaveType = "1";
                        window.parent.parent.ParentCloseWindow();
                        window.parent.RefreshGridByChildFrame('<%=gridID %>');
                    } else {
                        alert("保存失败,请刷新页面！");
                    }
                }
            });
        }



        function fnChildSave() {//保存方法
             
            if (!CheckMinput())
                return false;

            if (NoSaveEmployeeInfo) {
                saveSystemUser()
                return;
            }

            if (CheckValue("div<%=ResID %>FormTable")) {
                $("#fnChildSave").attr("disabled", true);
                var jsonStr1 = "[{";
                jsonStr1 += GetFromJson("<%=ResID %>");
                jsonStr1 += "}]";

                if (SelectUserRecid == "")
                    SelectUserRecid = '<%=RecID %>'

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { "Json": "" + jsonStr1 + "" },
                    url: "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=<%=ResID %>&RecID=" + SelectUserRecid ,
                    success: function (obj) {
                        if (obj.success || obj.success == "true") {
                            if (KeyWord == "SystemEmployeeInfo") {
                                saveSystemUser()
                            }
                            else {
                                alert("保存成功！");
                                window.parent.parent.ParentCloseWindow();
                                window.parent.RefreshGridByChildFrame('<%=gridID %>');
                            }
                        } else {
                            alert("保存失败,请刷新页面！");
                        }
                    }
                });
            }
        }

        function ChangeDep() {
            openDictionary("ChangeDep.aspx?DeptID=" + '<%=DeptID%>', 500, 400, "更换部门");
         }

         function GetValueFunciton(o) {
             if (o.Type == 1) {
                 $("#<%=ResID %>_部门ID").val(o.Change_部门ID)
                $("#<%=ResID %>_部门").val(o.Change_部门)
            }
        }

    </script>
</head>
<body>

    <form id="form1" runat="server" style="width: 100%;">
        <div title="" id="DIV用户信息设置" class="easyui-panel" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 100%;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1" style="display: none">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px; float: left" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="border: 0px;" onclick="SaveInfo(); return false;" /></a>
                        <div style="float: right; margin-top: 1px; margin-right: 2%; display: none" id="禁用DIV">
                            <input type="checkbox" id="SDJY" />
                            是否禁用
                        </div>
                    </td>
                </tr>
            </table>
            <div id="div_基本信息" title="基本信息" class="easyui-panel" collapsible="true" data-options="onBeforeExpand:function(){ ononExpandPanel(this)}" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 99%">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table width="99%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                        <tr>
                            <th style="width: 11%">登录账户</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_登录账户" type="text" class="box3" style="width: 90%" readonly="readonly" minput="true" />
                                <input id="系统账户ID" type="hidden" />
                            </td>
                            <th style="width: 11%">员工编号</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_员工编号" type="text" class="box3" style="width: 90%" />
                            </td>
                            <th style="width: 11%">员工名称</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_员工名称" type="text" class="box3" minput="true" style="width: 90%" />
                            </td>
                        </tr>

                        <tr>
                            <th style="width: 11%">部门</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_部门" type="text" class="box3" style="width: 75%" minput="true" value="<%=DeptName %>" readonly="readonly" />
                                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true,disabled:false" onclick="ChangeDep()" style="margin-left: 1%" id="ChangeDep"></a>
                                <input id="<%=ResID %>_部门ID" type="hidden" value="<%=DeptID %>" />
                            </td>
                            <th style="width: 11%">邮箱</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_邮箱" type="text" class="box3" style="width: 90%" />
                            </td>
                            <th style="width: 11%">手机</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_手机" type="text" class="box3" style="width: 90%" />
                            </td>
                        </tr>
                    </table> 
                   
                    <input id="<%=SystemResID %>_禁用" type="hidden" value="0" /> 
                     <input id="<%=SystemResID %>_登录帐号" type="hidden"  />  
                       <input id="<%=SystemResID %>_中文名" type="hidden"  />  
                     <input id="<%=SystemResID %>_用户类型" type="hidden" value="0" /> 
                    <input id="<%=SystemResID %>_手机" type="hidden"  />
                      <input id="<%=SystemResID %>_电子邮件" type="hidden"  />
                </div>
            </div>
        </div>
        <input type="hidden" value="" id="DictionaryConditon" />
        <input type="hidden" value="" id="childForm" />
        <input type="hidden" value="" id="ajaxinfo" />
        <div closed="true" class="easyui-window" id="divDictionary" style="overflow: hidden;" />
    </form>
</body>
</html>
