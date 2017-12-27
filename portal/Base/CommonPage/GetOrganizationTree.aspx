<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetOrganizationTree.aspx.cs" Inherits="ExtensionForms_common_GetOrganizationTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=GetScript1_4_3 %>
    <link href="../../CSS/treeCss_Organization.css" rel="stylesheet" />

    <script type="text/javascript">
        var SaveType = '<%=SaveType %>'
        var GSID = "";
        var GSMC = "";
        var RecID = ""
        var OldUserid = "";
        var NoSelectDeptID = '<%=NoSelectDeptID %>'
        var IsMultiSelect = '<%=IsMultiSelect%>'
        var IsLoadUser = '<%=IsLoadUser%>'
        var type = '<%=type%>'
        var IsSelectCompany = '<%=IsSelectCompany%>'
        var IsGetParentNode = '<%=IsGetParentNode%>'
        $(document).ready(function () {
            $('#SearchInput').textbox({
                buttonIcon: 'icon-search',
                prompt:"可以检索部门或姓名...",
                
                onChange: function (newValue, oldValue) {
                   // alert(newValue)
                },
                onClickButton: function () {
                    //alert($(this).val())
                    search($(this).val())
                }
            })

            $('#SearchInput').textbox('textbox').keyup(function (e) {
                search($(this).val())
            });

            $('#选择部门').tree({
                url: "../Organization/ZTH_GetInfo_ajax.aspx?typeValue=GetOrganizationTree&IsMultiSelect=" + IsMultiSelect + "&IsLoadUser=" + IsLoadUser + "&argType=" + type + "&IsSelectCompany=" + IsSelectCompany + "&UserID=test",
                checkbox: IsMultiSelect == "1",
                onClick: function (node) {
                    //var type = node.attributes.Type;
                    //var UserID = node.attributes.UserID;
                    //var Url = "";
                    //if (Url.indexOf("?") >= 0) Url += "&";
                    //else Url += "?";
                    //Url += "ObjectID=" + UserID;
                    //if (type == "员工") {
                    //    Url += "&GainerType=emp";
                    //    document.getElementById("center_frame").src = encodeURI(Url);
                    //} else {
                    //    if (UserID != "0") Url += "&Condition=部门ID=" + UserID + "&Params=DeptID=" + UserID;
                    //    Url += "&GainerType=dept";
                    //    document.getElementById("center_frame").src = encodeURI(Url);
                    //}
                },
                onLoadSuccess: function (node, data) {
                    //disableMyCheck()
                    //GetBM()
                }
            });
            $('#TreeDiv').height(parent.$(window).height() - $('#Table1').height() - 10);
        });


        function disableMyCheck() {
            //var roots = $('#选择部门').tree('getRoots');
            //for (var i = 0; i < roots.length; i++)
            //{

            //}
            //$('#选择部门').tree('disableCheck', "124");//禁用 
        }


        function GetBM() {
            var HOSTID = "<%=DeptID %>"
            var node = $('#选择部门').tree('find', HOSTID);
            if (node) {
                $("#选择部门").tree('select', node.target);
                var Parentnode = $('#选择部门').tree('getParent', node.target);
                if (Parentnode) {
                    $("#选择部门").tree('expand', Parentnode.target);
                }
            }
        }


        function onChangeOfMultiSelect() {

            var nodeList = $("#选择部门").tree('getChecked');
            var Change_部门ID = "";
            var Change_部门 = "";
            if (nodeList == null || nodeList.length == 0) {
                alert("部门未选择，请检查！")
                return
            }
            else {

                //if (NoSelectDeptID.toString().indexOf(node.id + ",") > -1) {
                //    alert("所选部门无效，请检查！")
                //    return false
                //}

                //if (node.attributes.noSelect == "1" || node.id == "0") {
                //    alert("所选部门无效，请检查！")
                //    return false
                //}
                
                for (var i = 0; i < nodeList.length; i++) {
                    if (nodeList[i].children == undefined || IsGetParentNode == "1") {
                        var IsGet = false;
                        if (IsLoadUser == "1") {
                            if (nodeList[i].attributes.Type == "员工") {
                                IsGet = true;
                            }
                        }
                        else {
                            IsGet = true;
                        }

                        if (IsGet) {
                            if (Change_部门ID != "") Change_部门ID += ","
                            if (Change_部门 != "") Change_部门 += ","

                            if (type == "HasEmployeeAccount") {
                                Change_部门ID += nodeList[i].attributes.UserID
                            }
                            else {
                                if (type == "GetSqlStr")
                                    Change_部门ID += "''" + nodeList[i].id + "''"
                                else
                                    Change_部门ID += nodeList[i].id
                            }
                            Change_部门 += nodeList[i].text
                        }
                    }
                }
            }

            var o = {
                Type: 1,
                Change_部门ID: Change_部门ID,
                Change_部门: Change_部门
            }

            if ($.isFunction(parent.ChangeValue)) {
                parent.ChangeValue(o);
            }
            else {
                parent.parent.ChangeValue(o);
            }
            parent.CloseDictionary();
        }


        function onChange() {
             
            if (IsMultiSelect == "1") {
                onChangeOfMultiSelect()
                return;
            }


            var node = $("#选择部门").tree('getSelected');
            if (node == null) {
                alert("部门未选择，请检查！")
                return
            }
            else {

                //if (NoSelectDeptID.toString().indexOf(node.id + ",") > -1) {
                //    alert("所选部门无效，请检查！")
                //    return false
                //}

                //if (node.attributes.noSelect == "1" || node.id == "0") {
                //    alert("所选部门无效，请检查！")
                //    return false
                //}

                if (IsSelectCompany == "1") {
                    if (node.attributes.Type != "公司" && node.id != "0") {
                        alert("请选择公司！")
                        return false
                    }
                    GetCompanyAllDepartment(node.id, node.text);
                    return;
                }


               
                if (IsLoadUser == "1") {
                    if (node.attributes.Type != "员工") {
                        alert("请选择员工！")
                        return false
                    }
                }
              
                if (type == "GetSqlStr") {
                    if (IsLoadUser == "1") {
                        GSID = "''" + node.attributes.UserID + "''"
                        GSMC = "''" + node.text + "''"
                    }
                    else {
                        GSID = "''" + node.id + "''"
                        GSMC = "''" + node.text + "''"
                    }
                }
                else {
                    if (IsLoadUser == "1") {
                        GSID = node.attributes.UserID
                        GSMC = node.text                       
                    }
                    else {
                        GSID = node.id
                        GSMC = node.text
                    }
                }
            }

            var o = {
                Type: 1,
                openType: type,
                Change_部门ID: GSID,
                Change_部门: GSMC
            }

            if ($.isFunction(parent.ChangeValue)) {
                parent.ChangeValue(o);
            }
            else {
                parent.parent.ChangeValue(o);
            }
            parent.CloseDictionary();
        }


        function GetCompanyAllDepartment(CompanyID, CompanyName) {
            var AllDepartment = "";
            $.ajax({
                type: "POST",
                async: false,
                url: "../Organization/ZTH_GetInfo_ajax.aspx?typeValue=GetCompanyAllDepartment&CompanyID=" + CompanyID + "&UserID=test",
                success: function (obj) {
                    var o = eval(obj)
                    for (var i = 0; i < o.length; i++) {
                        if (AllDepartment != "") AllDepartment += ","
                        AllDepartment += "''" + o[i] + "''"
                    }
                    var Change = {
                        Type: 1,
                        openType:type,
                        Change_部门ID: AllDepartment,
                        Change_部门: CompanyName,
                        CompanyID: CompanyID
                    }
                    parent.ChangeValue(Change);
                    parent.CloseDictionary();
                }
            })
        }
         
        function search(searchkey) {
            $('#选择部门').tree('collapseAll');
            $('#选择部门').tree('doFilter', searchkey);
            if (searchkey == "") {
                $('#选择部门').tree('collapseAll');
                $('#选择部门').tree('expand', $('#选择部门').tree('find', 0).target);
            }
            else
                $('#选择部门').tree('expandAll');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1">
            <tr>
                <td colspan="8" valign="middle">&nbsp;<a style="border: 0px; float: left; margin-top: 5px; margin-left: 5px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_choice.gif"
                    style="border: 0px;" onclick="onChange(); return false;" /></a>
                </td>
                <td style="text-align:right;padding-right:5px;">
                    <input class="easyui-textbox" id="SearchInput"  style="width:250px;height:25px;"   >
                </td>
            </tr>
        </table>
        <div style="width: 99%; overflow-y: auto; margin: 5px;" id="TreeDiv">
            <ul id="选择部门"></ul>
        </div>
    </form>
</body>
</html>
