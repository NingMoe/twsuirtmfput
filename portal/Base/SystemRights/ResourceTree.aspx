<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ResourceTree.aspx.cs" Inherits="SystemRights_ResourceTree" %>

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
                    var strID = node.attributes.strID; 
                    $("#hidYHBMID").val(strID);
                    $("#hidYHBMMC").val(node.text); 
                    if (type == "ICON_RES_EMPTY") {
                        var Url = encodeURI("List.aspx?ResID=" + strID + "&GainerType=<%=GainerType %>&ResName=" + node.text + "&ObjectID=<%=ObjectID %>");
                        document.getElementById("Resource_frame").src = encodeURI(Url);
                    }
                    else if (type != "dept" && type != "ICON_RES_EMPTY") {
                        var Url = "FormList.aspx?ResID=" + strID;
                        document.getElementById("Resource_frame").src = encodeURI(Url);
                    } 
                }
            });
        });
    </script>
    <ul id="tt" url="../CommonHandler/ResourceTreeData.ashx?ResID=<%=CommonProperty.PortalResourceID %>&IsEnable=1&IsShowDefaultMenu=0" animate="true"></ul>

</body>
</html>
