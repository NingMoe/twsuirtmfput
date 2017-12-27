<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeEdit.aspx.cs" Inherits="Base_Organization_EmployeeEdit" %>


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
    <script src="../Project/js.js"></script>
    <script src="../../Scripts/CommonCloseWindow.js"></script>
    <script type="text/javascript">
        var SaveType = '<%=SaveType %>'
        var GSID = "";
        var GSMC = "";
        var RecID = ""
        var OldUserid = "";
        var KeyWord = '<%=KeyWord %>'
        var SelectUserRecid = "";
        var systemUserRecid = '<%=SaveID %>'
        $(document).ready(function () {
            $("th").css("padding-right", "5px")

            $('#div_更改归属部门').remove();
            if (SaveType == "1") {
                LoadFrm();
            }

            $("#DIV用户信息设置").css("height", $(window).height() - 10)

            if (KeyWord == "EmployeeInfo") {
                $("input[minput=true],select[minput=true]").after(" <span style='color: red'>*</span>")
                $("#GetEmp").hide()
            }
            else {
                $("input,select,textarea").attr("readonly", "readonly")
                $("input,select,textarea").removeAttr("onfocus");

                $("#<%=ResID %>_登录账户").removeAttr("readonly").attr("minput", "true")
                $("#<%=ResID %>_员工编号").css("width", "75%")
            }
            SetBackgroundColor();
            $("#div_薪资").find("input[Sum=Sum]").each(function () {
                $(this).keyup(function () {
                    SumXZ()
                })
            });
            if ("<%=SearchType %>".indexOf("readonly") != -1) {
                var textInput = $("#div_全部").find("input:text");
                var radioInput = $("#div_全部").find("input:radio");
                var textarea = $("#div_全部").find("textarea");
                var select = $("#div_全部").find("select");
                var checkboxInput = $("#div_全部").find("input:checkbox");
                checkboxInput.attr("disabled", "disabled");
                radioInput.attr("disabled", "disabled");
                textInput.attr("readonly", "readonly");
                textarea.attr("readonly", "readonly");
                select.attr("disabled", "disabled");
                $("#fnChildSave").hide();
            }
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
                 UserDefinedSql:"1300",
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
                con = " and a.id <> " + systemUserRecid
            }

            var SelectRecordQueryParams = {
                UserDefinedSql: "1300",
                Condition: " and 账号='" + SystemUserId + "'"+ con,
                ROW_NUMBER_ORDER: ""
            }
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByUserDefinedSql&NoPaging=1",
                data: SelectRecordQueryParams,
                success: function (obj) {
                    if (obj.total > 0) {
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

            var node = $("#归属部门").tree('getSelected');

            if ('<%=RecID %>' != "") {
                if (node == null) {
                    emptyinput.css("border", "1px solid red")
                    alert("归属部门未选择，请检查！")
                    return false
                }
                else {
                    //if (node.attributes.noSelect == "1" || node.id == "0") {
                    //    alert("所选部门无效，请检查！")
                    //    return false
                    //}
                    GSID = node.id
                    GSMC = node.text
                    $("#<%=ResID %>_部门ID").val(GSID)
                    $("#<%=ResID %>_部门").val(GSMC)
                }
            }
            return true
        }


        function LoadFrm() {

            if (SelectUserRecid == "")
                SelectUserRecid = '<%=RecID %>'

            if (SelectUserRecid != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=" + SelectUserRecid ,
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            }
                        }
                        LoadXZ();
                        OldUserid = $("#<%=ResID %>_登录账户").val();
                    }
                });
            }
        }

        function LoadXZ() {
            $.ajax({
                type: "POST",
                url: encodeURI("../Common/Ajax_Request.aspx?typeValue=GetChildDataByParent&ParentResID=" + "<%=ResID%>" + "&ResID=" + "<%=XZResID%>" + "&ParentRecID=" + "<%=RecID%>"),
                success: function (centerJson) {

                    var jsonList = eval("(" + centerJson + ")").rows;
                    for (var i = 0; i < jsonList.length; i++) {
                        for (var key in jsonList[i]) {
                            $("#<%=XZResID %>_" + key).val(jsonList[i][key]);
                        }
                    }

                    $("#<%=XZResID %>_RecID").val(jsonList[0].ID)
                    SumXZ()
                }
            });
        }

        function SumXZ() {
            var s = 0;
            $("#div_薪资").find("input[Sum=Sum]").each(function () {
                var je = $(this).val();
                var numje = parseFloat(je)
                if (!isNaN(numje)) {
                    s += numje;
                }
            });
            $("#<%=XZResID %>_新定薪资").val(s)
        }


        function fnChildSave() {//保存方法
            if (!CheckMinput())
                return false;

            if (CheckValue("div<%=ResID %>FormTable")) {
                $("#fnChildSave").attr("disabled", true);
                var jsonStr1 = "[{";
                jsonStr1 += GetFromJson("<%=ResID %>");
                jsonStr1 += "}]";

                if (SelectUserRecid == "")
                    SelectUserRecid = '<%=RecID %>'

                var ChildJson = GetChildInfo("Table_薪资", "<%=XZResID %>");

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { "Json": "" + jsonStr1 + "", "ChildJson": "" + ChildJson + "", "ChildResID": "<%=XZResID %>" },
                    url: encodeURI("../Common/CommonAjax_Request.aspx?typeValue=SaveInfoByParentAndChild&ResID=<%=ResID %>&RecID=<%=RecID %>"),
                    success: function (obj) {
                        if (obj.success || obj.success == "true") {
                            alert("保存成功！");
                            window.parent.parent.ParentCloseWindow();
                            window.parent.RefreshGridByChildFrame('<%=gridID %>');
                          } else {
                              alert("保存失败,请刷新页面！");
                          }
                      }
                });
              }
          }


          function GetChildInfo(DivID, ResID) {
              if (DivID == "" || ResID == "") return "";
              if ($("#" + DivID).length == 0) return "";
              var strJson = "";
              var json = "";
              $("#" + DivID).find("input,select,textarea").each(function () {
                  var id = $(this).attr("id");
                  if (id.indexOf(ResID + "_") >= 0) {
                      var strColName = $(this).attr("id").split("_")[1];
                      json += ",'" + strColName + "':'" + $(this).val() + "'";
                  }
              });

              json = json.substr(1, json.length - 1);
              strJson += ",{" + json + "}";
              if (strJson.trim() != "")
                  strJson = "[" + strJson.substr(1, strJson.length - 1) + "]";
              return strJson;
          }


          function ononExpandPanel(o) {
              //var id = $(o).attr("id")
              //$("#DIV用户信息设置").find(".easyui-panel[collapsible=true][id!=" + id + "]").panel("collapse")
          }

          function ChangeDep() {
              openDictionary("ChangeDep.aspx?DeptID=" + '<%=DeptID%>', 500, 400, "更换部门");
        }

        function GetEmp() {
            openDictionary("ChangeEmp.aspx?DeptID=" + '<%=DeptID%>', 500, 400, "选择人员");
        }

        function GetValueFunciton(o) {
            if (o.Type == 1) {
                $("#<%=ResID %>_部门ID").val(o.Change_部门ID)
                $("#<%=ResID %>_部门").val(o.Change_部门)
            }
            else {
                $("#<%=ResID %>_员工编号").val(o.Change_员工ID)
                SelectUserRecid = o.Change_员工ID
                LoadFrm()
            }
        }

    </script>
</head>
<body>

    <form id="form1" runat="server" style="width: 100%;">
        <div title="" id="DIV用户信息设置" class="easyui-panel" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 100%;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1">
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
            <div id="div_全部">
                <div id="div_基本信息" title="基本信息" class="easyui-panel" collapsible="true" data-options="onBeforeExpand:function(){ ononExpandPanel(this)}" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" style="border-bottom: none;">
                        <table width="99%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                            <tr>
                                <th style="width: 11%">登录账户</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_登录账户" type="text" class="box3" style="width: 90%" readonly="readonly" />
                                </td> 
                                <th style="width: 11%">员工名称</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_员工名称" type="text" class="box3" minput="true" style="width: 90%" />
                                </td>
                                <th style="width: 11%">员工状态</th>
                                <td style="width: 22%">
                                     <select id="<%=ResID %>_状态" style="width: 90%">
                                        <option value=""></option>
                                        <option value="正式">正式</option>
                                        <option value="离职">离职</option>
                                        <option value="实习">实习</option>
                                        <option value="临聘">临聘</option>
                                        <option value="试用">试用</option>
                                        <option value="其他">其他</option>
                                    </select>
                                </td>
                            </tr>

                            <tr>
                                <th style="width: 11%">身份证号</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_身份证号" type="text" class="box3" style="width: 90%" minput="true" />
                                </td>
                                <th style="width: 11%">邮箱</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_邮箱" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">手机</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_手机" type="text" class="box3" style="width: 90%" minput="true" />
                                </td>
                            </tr>

                            <tr>
                                <th style="width: 11%">部门</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_部门" type="text" class="box3" style="width: 75%" minput="true" value="<%=DeptName %>" readonly="readonly" />
                                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true,disabled:false" onclick="ChangeDep()" style="margin-left: 1%" id="ChangeDep"></a>
                                    <input id="<%=ResID %>_部门ID" type="hidden" value="<%=DeptID %>" />
                                </td>
                                <th style="width: 11%">职务</th>
                                <td style="width: 22%">
                                    <select id="<%=ResID %>_职务" style="width: 90%">
                                        <option value=""></option>
                                        <option value="董事长">董事长</option>
                                        <option value="总经理">总经理</option>
                                        <option value="副总经理">副总经理</option>
                                        <option value="主管领导">主管领导</option>
                                        <option value="部门主任">部门主任</option>
                                        <option value="部门副主任">部门副主任</option>
                                        <option value="部门秘书">部门秘书</option>
                                        <option value="主管会计">主管会计</option>
                                        <option value="人力资源">人力资源</option>
                                        <option value="网站管理员">网站管理员</option>
                                        <option value="会计">会计</option>
                                        <option value="出纳">出纳</option>
                                        <option value="物资管理员">物资管理员</option>
                                    </select>
                                    <!--<input id="<%=ResID %>_职务" type="text" class="box3" style="width: 90%" />-->
                                </td>
                                <th style="width: 11%">职务级别</th>
                                <td style="width: 22%">
                                    <select id="<%=ResID %>_职务级别" style="width: 90%">
                                        <option value=""></option>
                                        <option value="董事长">董事长</option>
                                        <option value="总经理">总经理</option>
                                        <option value="副总经理">副总经理</option>
                                        <option value="主管领导">主管领导</option>
                                        <option value="部门主任">部门主任</option>
                                        <option value="部门副主任">部门副主任</option>
                                        <option value="部门秘书">部门秘书</option>
                                        <option value="主管会计">主管会计</option>
                                        <option value="人力资源">人力资源</option>
                                        <option value="网站管理员">网站管理员</option>
                                        <option value="会计">会计</option>
                                        <option value="出纳">出纳</option>
                                        <option value="物资管理员">物资管理员</option>
                                    </select> 
                                </td>
                            </tr>

                            <tr>
                                <th style="width: 11%">性别</th>
                                <td style="width: 22%">
                                    <select id="<%=ResID %>_性别" style="width: 90%">
                                        <option value=""></option>
                                        <option value="男">男</option>
                                        <option value="女">女</option>
                                    </select>
                                </td>
                                <th style="width: 11%">出生日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_出生日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">民族</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_民族" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>

                            <tr>
                                <th style="width: 11%">籍贯</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_籍贯" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">出生地</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_出生地" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">政治面貌</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_政治面貌" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">考勤号码</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_考勤号码" type="text" class="box3" style="width: 90%" />
                                </td>
                                
                                <th style="width: 11%"></th>
                                <td style="width: 22%"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="div_其他信息" data-options="onBeforeExpand:function(){ ononExpandPanel(this)}" title="其他信息" class="easyui-panel" collapsible="true" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" style="border-bottom: none;">
                        <table width="99%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table_其他信息">

                            <tr>
                                <th style="width: 11%">人员类型</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_人员类型" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">合同起始日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_合同起始日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">合同结束日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_合同结束日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">试用期起始日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_试用期起始日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">试用期结束日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_试用期结束日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">最高学历</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_最高学历" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">最高学位</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_最高学位" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">毕业院校</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_毕业院校" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">专业名称</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_专业名称" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">毕业时间</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_毕业时间" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">现居住地址</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_现居住地址" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">户籍详细地址</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_户籍详细地址" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">婚姻状况</th>
                                <td style="width: 22%">
                                    <select id="<%=ResID %>_婚姻状况" style="width: 90%">
                                        <option value=""></option>
                                        <option value="单身">单身</option>
                                        <option value="已婚">已婚</option>
                                        <option value="离异">离异</option>
                                        <option value="丧偶">丧偶</option>
                                    </select>
                                </td>
                                <th style="width: 11%">从事工作年限</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_从事工作年限" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">入职日期</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_入职日期" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">注会取得时间</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_注会取得时间" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">注税取得时间</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_注税取得时间" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">专业技术职称</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_专业技术职称" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">技术职称取得时间</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_专业技术职称取得时间" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th style="width: 11%">会计上岗证</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_会计上岗证" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">会计上岗证取得时间</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_会计上岗证取得时间" type="text" class="box3" style="width: 90%" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">计算机水平</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_计算机水平" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">计算机水平其他</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_计算机水平其他" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%">计算机水平等级</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_计算机水平等级" type="text" class="box3" style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">英语水平</th>
                                <td style="width: 22%">
                                    <input id="<%=ResID %>_英语水平" type="text" class="box3" style="width: 90%" />
                                </td>
                                <th style="width: 11%"></th>
                                <td style="width: 22%"></td>
                                <th style="width: 11%"></th>
                                <td style="width: 22%"></td>
                            </tr>
                            <tr>
                                <th style="width: 11%">备注</th>
                                <td colspan="5">
                                    <textarea id="<%=ResID %>_备注" class="box3" style="width: 97%; height: 50px; text-align: left"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="div_薪资" data-options="onBeforeExpand:function(){ ononExpandPanel(this)}" title="薪资基础信息" class="easyui-panel" collapsible="true" style="overflow-x: hidden; overflow-y: auto; padding: 5px; border-bottom: none; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" style="border-bottom: none;">
                        <table width="99%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table_薪资">

                            <tr>
                                <th style="width: 11%">基本月薪</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_基本月薪" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="基本月薪" sum="Sum" />
                                </td>
                                <th style="width: 11%">专业津贴</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_专业津贴" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="专业津贴" sum="Sum" />
                                </td>
                                <th style="width: 11%">职务津贴</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_职务津贴" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="职务津贴" sum="Sum" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">交通津贴</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_交通津贴" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="交通津贴" sum="Sum" />
                                </td>
                                <th style="width: 11%">后续教育津贴</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_后续教育津贴" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="后续教育津贴" sum="Sum" />
                                </td>
                                <th style="width: 11%">社会保险</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_社会保险" type="text" class="box3" style="width: 90%" fieldtype="num" sum="Sum" fieldtitle="社会保险" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 11%">住房公积金</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_住房公积金" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="住房公积金" />
                                </td>
                                <th style="width: 11%">加班津贴</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_加班津贴" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="加班津贴" />
                                </td>
                                <th style="width: 11%">新定薪资</th>
                                <td style="width: 22%">
                                    <input id="<%=XZResID %>_新定薪资" type="text" class="box3" style="width: 90%" fieldtype="num" fieldtitle="新定薪资" readonly="readonly" />
                                    <input id="<%=XZResID %>_RecID" type="hidden" value="<%=XZRecID %>" />
                                </td>
                            </tr>
                        </table>
                    </div>
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
