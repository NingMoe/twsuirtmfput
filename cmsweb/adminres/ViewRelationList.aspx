<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewRelationList.aspx.vb" Inherits="adminres_ViewRelationList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>  
	<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="/cmsweb/js/dragDiv.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divFrm">          
         
        <input type="button" value='添加资源' onclick="showMessageBox(600,500,'/cmsweb/cmsothers/ResourceSelect_View.aspx','',true,true,false);" />
        <asp:Button ID="btnAdd" runat="server" style="display:none;" Text="添加" />
        <asp:Button ID="btnRelation" runat="server" Text="添加关联" />
        <asp:Button ID="btnShow" runat="server" Text="添加显示" />
        <asp:Button ID="btnSave" runat="server" Text="保存" />
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <asp:Repeater ID="repResource" runat="server">
                    <ItemTemplate>
                        <td valign="top">
                            <table cellpadding="0" cellspacing="0" border="0" style="margin:10px;border-left:1px solid #C4D9F9; border-right:1px solid #C4D9F9; border-bottom:1px solid #C4D9F9;">
                                <tr>
                                    <td style="background:#639ACE;"><asp:CheckBox ID="chkAllSelected" runat="server" onclick="checkAllCheckBox(this);" /></td>
                                    <td style="background:#639ACE; height:30px; padding-left:5px; padding-right:5px;">资源名称：<%#Eval("NAME").ToString & Eval("ID").ToString %><asp:TextBox ID="txtResID" runat="server" style="display:none;" Text='<%# Eval("ID") %>'></asp:TextBox><asp:TextBox ID="txtResName" runat="server" style="display:none;" Text='<%# Eval("Name") %>'></asp:TextBox> </td>
                                    <td style="background:#639ACE;"><asp:LinkButton ID="lbtnDel" runat="server" Width="20px" Height="19px" OnClientClick="return window.confirm('您确认要删除？');"><img src="../images/del.gif" border="0" /></asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="width:100%; height:200px; overflow-y:auto; overflow-x:hidden;">
                                        <asp:DataGrid ID="dgResourceColumn" runat="server" DataKeyField="CD_COLNAME" AutoGenerateColumns="false" ShowHeader="false" GridLines="none" CssClass="ListTable1" Width="99%">
                                            <Columns>  
                                                <asp:TemplateColumn>
                                                    <ItemTemplate><asp:CheckBox ID="chkSelected" runat="server" onclick="clickCheckBox(this);" /></ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-Width="100%">
                                                    <ItemTemplate>
                                                        <%# Eval("CD_DISPNAME") %> 
                                                        <asp:TextBox ID="txtColName" runat="server" style="display:none;" Text='<%# Eval("CD_DISPNAME") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns> 
                                        </asp:DataGrid></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="background:#639ACE; height:30px;">&nbsp;关联关系</td>
                <td>&nbsp;&nbsp;</td>
                <td style="background:#639ACE;">&nbsp;显示字段</td>
            </tr>
            <tr>
                <td style="border-left:1px solid #C4D9F9; border-right:1px solid #C4D9F9; border-bottom:1px solid #C4D9F9;">
                    <div style="float:left; height:200px; overflow-y:auto; overflow-x:hidden; width:100%;">
                    <table cellpadding="0" cellspacing="0" border="0">
                    <asp:Repeater ID="repRelation" runat="server">
                        <ItemTemplate>
                            <tr style="background:#C4D9F9;">
                                <td style="width:200px; height:25px; "><%#Eval("RESOURCENAME1").ToString() %></td> 
                                <td style="width:200px;"><%#Eval("RESOURCENAME2").ToString() %></td>
                                <td><asp:TextBox ID="txtJoin" runat="server" Width="30px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:DataGrid ID="dgRelation" runat="server" AutoGenerateColumns="false" CssClass="ListTable1" ShowHeader="false" GridLines="none" Width="100%">
                                        <Columns>
                                            <asp:TemplateColumn ItemStyle-Width="200px">
                                                <ItemTemplate><span style="color:#0000ff;"><%#Eval("COLUMNDISPNAME1").ToString() %></span></ItemTemplate> 
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="200px">
                                                <ItemTemplate><span style="color:#0000ff;"><%#Eval("COLUMNDISPNAME2").ToString()%></span></ItemTemplate> 
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate><asp:LinkButton ID="lbtnDel" runat="server" Width="20px" Height="19px" OnClientClick="return window.confirm('您确认要删除？');"><img src="../images/del.gif" border="0" /></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns> 
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table> 
                    </div>
                </td>
                <td>&nbsp</td>
                <td style="border-left:1px solid #C4D9F9; border-right:1px solid #C4D9F9; border-bottom:1px solid #C4D9F9;">
                    <div style="float:left; height:200px; overflow-y:auto; overflow-x:hidden; width:100%;">
                    <asp:DataGrid ID="dgShow" runat="server" AutoGenerateColumns="false" CssClass="ListTable1" ShowHeader="false" GridLines="none">
                        <Columns>
                            <asp:TemplateColumn ItemStyle-Width="200px">
                                <ItemTemplate><span style="color:#0000ff;"><%# Eval("ResourceName").ToString() & "." %></span><%# Eval("COLUMNDISPNAME").ToString() %></ItemTemplate> 
                            </asp:TemplateColumn> 
                             <asp:TemplateColumn>
                                <ItemTemplate><asp:LinkButton ID="lbtnDel" runat="server" Width="20px" Height="19px" OnClientClick="return window.confirm('您确认要删除？');"><img src="../images/del.gif" border="0" /></asp:LinkButton></ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns> 
                    </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    <asp:TextBox ID="txtResourcesID" runat="server" Height="100" Width="200" style="display:none;"></asp:TextBox>
    </div>
    </form>
</body>
</html> 
<script language="javascript">
document.getElementById("divFrm").style.height=document.documentElement.offsetHeight-30; 

function GetResourceSelectID(ResourceID)
{
    document.getElementById("txtResourcesID").value=document.getElementById("txtResourcesID").value+","+ResourceID;
   
    document.getElementById("btnAdd").click();
} 

function checkAllCheckBox(obj)
{  
    var dgId=obj.id.substring(0,obj.id.indexOf("chkAllSelected"))+"dgResourceColumn";
    
    var inputList=document.getElementById(dgId).getElementsByTagName("input");
    for(var i=0;i<inputList.length;i++)
    {
        if(inputList[i].type=="checkbox")
        {
            inputList[i].checked=obj.checked;
        }
    } 
}
 

function clickCheckBox(obj)
{
//debugger;
	var strRecIds = "";
	var strPre = "";
	var o=obj.parentNode.parentNode.parentNode.parentNode;
	
	var chkId=obj.id.substring(0,obj.id.indexOf("dgResourceColumn"))+"chkAllSelected";
	if(obj.checked)
	{
	    var isSelected=true;
	    var inputList=o.getElementsByTagName("input");
	    for(var i=0;i<inputList.length;i++)
        {
            if(inputList[i].type=="checkbox")
            {
                if(!inputList[i].checked)
                {
                    isSelected=false;
                    break;
                }            
            }
        } 
        document.getElementById(chkId).checked=isSelected;
    }
    else
    {
        document.getElementById(chkId).checked=false;
    } 
}
 
</script>