<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="UserTree.aspx.cs" Inherits="SystemRights_UserTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tt').tree({
                onClick: function (node) {      
                    var type = node.attributes.Type;
                    var UserID = node.attributes.UserID;
                    var Url = "<%=FromUrl %>";
                    if (Url.indexOf("?") >= 0) Url += "&";
                    else Url += "?";
                    Url += "ObjectID=" + UserID;
                    if (type == "员工") {
                        Url += "&GainerType=emp";
                        document.getElementById("center_frame").src = encodeURI(Url);
                    } else {
                        if (UserID != "0") Url += "&Condition=部门ID=" + UserID + "&Params=DeptID="+UserID;
                        Url += "&GainerType=dept";
                        document.getElementById("center_frame").src = encodeURI(Url);
                    }
                }
            });
        });
    </script>
    <ul id="tt" url="../CommonHandler/UserTreeData.ashx?IsLoadUser=<%=IsLoadUser %>" animate="true"></ul>

</body>
</html>
