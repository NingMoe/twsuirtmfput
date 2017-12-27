<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeEmp.aspx.cs" Inherits="Base_Organization_ChangeDep" %>

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

        $(document).ready(function () {
            $('#选择人员').tree({
                onClick: function (node) {
                    var type = node.attributes.Type;
                    var UserID = node.attributes.UserID;
                    var Url = "";
                    if (Url.indexOf("?") >= 0) Url += "&";
                    else Url += "?";
                    Url += "ObjectID=" + UserID;
                    if (type == "员工") {
                        Url += "&GainerType=emp";
                        document.getElementById("center_frame").src = encodeURI(Url);
                    } else {
                        if (UserID != "0") Url += "&Condition=部门ID=" + UserID + "&Params=DeptID=" + UserID;
                        Url += "&GainerType=dept";
                        document.getElementById("center_frame").src = encodeURI(Url);
                    }
                },
                onLoadSuccess: function (node, data) {
                    $('#选择人员').tree("expandAll")
                }
            });
            $('#TreeDiv').height(parent.$("#FromPage").height() - $('#Table1').height() - 5);
        });
         
        function onChange() {
            var node = $("#选择人员").tree('getSelected');
            if (node == null) {
                alert("人员未选择，请检查！")
                return
            }
            else {
                var type = node.attributes.Type;
                if (type == "员工") {
                    GSID = node.attributes.UserID
                    GSMC = node.text
                }
                else {
                    alert("请选择人员！")
                    return
                }
            }

            parent.parent.ChangeValue({
                Type: 2,
                Change_员工ID: GSID,
                Change_员工名称: GSMC
            });
            parent.CloseDictionary();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1"  >
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px; float: left;margin-top:5px;margin-left:5px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_choice.gif"
                        style="border: 0px;" onclick="onChange(); return false;" /></a>
                    </td>
                </tr>
            </table>
        <div id="TreeDiv" style=" width: 99%; overflow-y: auto; margin: 5px;">
              <ul id="选择人员" url="../CommonHandler/UserTreeData.ashx?IsLoadUser=1" animate="true"></ul>
        </div>
    </form>
</body>
</html>
