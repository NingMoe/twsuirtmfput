<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Thumbnails_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
        <tr>
            <td width="100%">
                <TABLE class="toolbar_table" cellSpacing="0" border="0">
					<TR>
						<TD width="8"></TD>
						<TD vAlign="bottom" noWrap align="left" width="55"><asp:image id="Image1" runat="server" ImageUrl="/cmsweb/images/imagebuttons/save.gif"></asp:image>&nbsp;<asp:linkbutton  id="lnkSave" Runat="server" OnClientClick="return Validate();">保存</asp:linkbutton></TD>
						<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
						</TD>
						 
						<TD noWrap align="left" width="55"><asp:image id="Image3" runat="server" ImageUrl="/cmsweb/images/imagebuttons/exit.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkExit" Runat="server">退出</asp:linkbutton></TD>
						<TD align="right">&nbsp;
							<asp:label id="lblHeader" runat="server"></asp:label><asp:label id="lblHeaderAction1" runat="server" ForeColor="Red" ></asp:label></TD>
							<td align="right">生成缩略图</td>
					</TR>
				</TABLE>
            </td>
        </tr>
        <tr>
            <td>
                <div style="margin:5px; border:1px solid #C4D9F9; width:50%; min-height:400px; height:400px;">
                    <table cellpadding="0" cellspacing="0" border=0 style="margin:5px;">
                        <tr>
                            <td>
                                宽：<asp:TextBox ID="txtWidth" runat="server" Text="100"></asp:TextBox>
                            </td> 
                        </tr>
                        <tr>
                            <td>                            
                                高：<asp:TextBox ID="txtHeight" runat="server" Text="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server"  />                 
                            </td> 
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="img1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        
    </table>        
    
    </div>
    </form>
</body>
</html>


<script language="javascript">
    function Validate()
    {
        var w=document.getElementById("txtWidth");
        var h=document.getElementById("txtHeight");
        var f=document.getElementById("FileUpload1");
        if(w.value=="")
        {
            alert('宽不能为空！');
            o.focus(); 
            return false;
        }
        else if(h.value=="")
        {
            alert('高不能为空！');
            o.focus(); 
            return false;
        }
        else if (isNaN(w.value))
		{
			alert("宽必须是数字.");
			o.focus(); 
			return false;
		}
		else if (isNaN(h.value))
		{
			alert("高必须是数字.");
			o.focus(); 
			return false;
		}
		else if(f.value=="")
		{
		    alert("请选择有效附件！");
			o.focus(); 
			return false;
		} 
        return true;
		
    }
    
    
    alert(window.parent.frames["content"].opener.location.href);
    
    
</script>
