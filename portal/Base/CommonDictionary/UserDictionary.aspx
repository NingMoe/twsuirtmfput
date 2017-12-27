<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDictionary.aspx.cs" Inherits="Base_CommonDictionary_UserDictionary" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <link href="../../CSS/treeCss_Organization.css" rel="stylesheet" />
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            var FrameHeight = document.documentElement.clientHeight;
            var westModel_accordionHeight = FrameHeight - 125;
            //点击按钮的时候加载 
            $("#UserTreePanel").panel({
                width: 200,
                iconCls: 'icon-search',
                title: '',
                height: westModel_accordionHeight
            });
            $('#tt').tree({

                onClick: function (node) {
                    if (node.attributes != null) {
                        var type = node.attributes.Type;
                        var UserID = node.attributes.UserID;
                        var UserName = node.text;
                        if (type == "员工") {
                            window.parent.GetUserInfo(UserID,UserName);
                            window.parent.CloseDictionary();
                        } 
                    }
                }
            });
        });
    </script>
    <div  id="UserTreePanel"  fit="true"   border="false">
    <ul id="tt" url="../CommonHandler/UserTreeData.ashx" animate="true"></ul> </div>
</body>
</html>