<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmployeeVirtualDep.aspx.vb" Inherits="adminsys_EmployeeVirtualDep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>		
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	<style>
        .GOGG02-FixedHeader   
          {   
          position:relative;   
          top:expression(this.offsetParent.scrollTop-2);            
          }
    </style>
    
    
        
 <script language="javascript">
<!--
function RowLeftClickNoPost(src){
	try{
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
		//var h=document.documentElement.offsetHeight-340;
		//document.getElementById("frmEmpDep").src="EmployeeVirtualDep.aspx?uid="+src.USERID+"&h="+h;
	}catch(ex){
	}
} 
-->
 </script>
			
			
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; border-top:1px solid #639ACE;">
    <TABLE class="toolbar_table" cellSpacing="0" border="0">
		<TR>
			<TD width="5"></TD>
			<TD noWrap align="left" style="font-weight:bold;">人员所属虚拟部门列表</TD>  
		</TR>
	</TABLE>
    <div style='BORDER-RIGHT:white 1px solid;BORDER-TOP:white 1px solid;LEFT:0px;OVERFLOW-y:auto; overflow-x:hidden; BORDER-LEFT: white 1px solid;WIDTH:465px;BORDER-BOTTOM: white 1px solid; POSITION:relative; TOP: 0px; HEIGHT:<%=Convert.ToInt32(Request("h"))-20 %>;'>   
        <asp:DataGrid id="DataGrid1" runat="server">
        <Headerstyle forecolor="Black" cssclass="GOGG02-FixedHeader"></Headerstyle>   
        </asp:DataGrid>						        
    </div>		</div>	
    </form>
</body>
</html>
