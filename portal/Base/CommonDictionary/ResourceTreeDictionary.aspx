<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ResourceTreeDictionary.aspx.cs" Inherits="Base_CommonDictionary_ResourceTreeDictionary" %>

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
                    var strResName=node.attributes.text;
                    $("#hidYHBMID").val(strID);
                    $("#hidYHBMMC").val(node.text);
                    if (type != "dept") {
                        selectedResourceData(strID, strResName);
                    } 
                }
            });
        });
         
        function selectedResourceData(ResID, ResName) {
            // debugger
            var str = '<%=Params %>'.split(',');
            for (var i = 0; i < str.length; i++) {
                if (str[i] != "") {
                    var strCol = str[i].split("=");
                    if (strCol[1] == "ResID") $("#" + strCol[0]).val(ResID);
                    else if (strCol[1] == "ResName") window.parent.$(strCol[0]).attr("value", ResName);
                }
            }
            $('#ChooseTYWindow').dialog('close');
        }
    </script>
    <ul id="tt" url="../CommonHandler/ResourceTreeData.ashx?PID=-1" animate="true"></ul>

</body>
</html>


