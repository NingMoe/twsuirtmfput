<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Center.aspx.cs" Inherits="Center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
    <link href="../../CSS/treeCss.css" rel="stylesheet" />
</head>
<body class="easyui-layout"  >
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#Resource_frame").css("width", $("#centerModel_id").width());
            $("#Resource_frame").css("height", $("#centerModel_id").height());
        });
        function fnParentFormListDialog(url, DialogWidth, DialogHeight, title) {
            window.parent.fnParentFormListDialog(url, DialogWidth, DialogHeight, title);
        }
    </script>
	<div region="west" id="westModel_id" split="true"  style="width:200px;" href="ResourceTree.aspx?ObjectID=<%=ObjectID %>&GainerType=<%=GainerType %>"></div>
    <div region="center"  id="centerModel_id" style="overflow:hidden;">
        <iframe  id="Resource_frame"  frameborder="0" scrolling="no" >
        </iframe>
    </div>
</body>
</html>
