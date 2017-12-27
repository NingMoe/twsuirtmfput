<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/ecmascript">
        function (value, row, index) {
            var s='';
            s+='<a href="javascript:void(0)" class="easyui-linkbutton"  data-options="iconCls:\'\',plain:true"  onclick=PreviewFile('+row.ResID+','+row.ID+',"test",this) >查阅</a>';
            s+='<a href="javascript:void(0)" class="easyui-linkbutton"  data-options="iconCls:\'\',plain:true"  onclick=DownloadFile('+row.ResID+','+row.ID+',this) >下载</a>';
            return s;
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
