<%@ Page Language="VB" AutoEventWireup="false" CodeFile="config.aspx.vb" Inherits="exdtc_config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	<script>
	function InputCheck()
	{   
	  //  if(deptree.currentNode.icon == 'ICON_RES_TWOD')
       // {
            //alert(deptree.currentNode.sourceIndex)
            document.location.href="field.aspx?fromresid=<%=Request.QueryString("mnuresid") %>&toresid=" + deptree.currentNode.sourceIndex.split("_")[1];
            return;
       // }
       // else
       // {
       //     alert('请选择有效的目标数据分类!');
       //     return false;   
       // }
	}
	</script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="background-color:#e7ebef">
    <input type="button" name="ok" value="确定" onclick="InputCheck()" id="Button1"/>
    <input type="button" name="cancel" value="取消" />
    <input type="hidden" name="nodeid" />
    </div>
    
    <div style="overflow-y:auto;height:400px;margin-top:2px;border:1px solid;">
    <%LoadDepartTree()%>
    </div>
    
    </form>
</body>
</html>
