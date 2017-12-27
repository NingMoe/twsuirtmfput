<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceFormRouter" CodeFile="ResourceFormRouter.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>ResourceFormRouter</title>
	<meta name=GENERATOR content="Microsoft Visual Studio .NET 7.1">
	<meta name=CODE_LANGUAGE content="Visual Basic .NET 7.1">
	<meta name=vs_defaultClientScript content=JavaScript>
	<meta name=vs_targetSchema content=http://schemas.microsoft.com/intellisense/ie5>
	<LINK rel=stylesheet type=text/css href="../css/cmstree.css" >
  </HEAD>
<body>
	<form id="Form1" method="post" runat="server">
	<div style="margin-left:5px;">
	    <asp:DataGrid ID="dgForm" runat="server" AutoGenerateColumns="false" Width="45%">
	        <Columns> 
	            <asp:TemplateColumn HeaderText="��������">
	                <ItemTemplate>
	                    <%#IIf(Convert.ToInt32(Eval("FormCategory").ToString()) = 0, Eval("FormName").ToString(), "�û��Զ��崰��")%>
	                </ItemTemplate>
	            </asp:TemplateColumn>
	            <asp:BoundColumn DataField="RouterValue" HeaderText="Ӧ������"></asp:BoundColumn>
	            <asp:BoundColumn DataField="FORMURL" HeaderText="��������"></asp:BoundColumn> 
	            <asp:TemplateColumn>
	                <ItemTemplate ><asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="return window.confirm('��ȷ��Ҫɾ����');" CommandArgument='<%#Eval("ID") %>' OnClick="lbtnDel_Click">ɾ��</asp:LinkButton></ItemTemplate>
	            </asp:TemplateColumn>  
	        </Columns>
	    </asp:DataGrid>
	    <table cellpadding="0" cellspacing="0" border="0" width="" style="margin-top:20px;">
	        <tr>
	             <td>�������</td>
	            <td>
	                <asp:RadioButtonList ID="radFormCategory" runat="server" RepeatDirection="horizontal" onclick="SetForm();">
	                    <asp:ListItem Value="0">ϵͳ����</asp:ListItem>
	                    <asp:ListItem Value="1">�û��Զ��崰��</asp:ListItem>
	                </asp:RadioButtonList>	            
	            </td>
	        </tr>
	        <tr id="trFormName">
	             <td>�������ƣ�</td>
	            <td>
	                <asp:RadioButtonList id="radFormName" runat="server"></asp:RadioButtonList>
	            </td>
	        </tr>
	        <tr>
	            <td>Ӧ��������</td>
	            <td>
	                <asp:TextBox ID="txtRouterValue" Runat="server"></asp:TextBox>
	            </td>
	        </tr>
	        <tr id="trUrl" style="display:block;">
	            <td>�������ӣ�</td>
	            <td>
	                <asp:TextBox ID="txtUrl" Runat="server"></asp:TextBox>
	            </td>
	        </tr>
	    </table>		
		<asp:Button ID="btnUpdate" Runat="server" Text="��������" OnClientClick="return FormValidate();"></asp:Button>
        <asp:Button ID="btnExit" runat="server" Text="�˳�" />
    </div>
	</form>
  </body>
</HTML>

<script language="javascript">
function SetForm()
{ 
    var a=document.getElementById("radFormCategory").cells.length;  
    for(var i=0;i<a;i++)
    {
        var ss="radFormCategory_"+i;  
        if(document.getElementById(ss).checked) //ע��checked����д��Checked��Ҫ��Ȼ���ɹ�
        {
            if(document.getElementById(ss).value=="0")
            {
                document.getElementById("trFormName").style.display="block";
                document.getElementById("trUrl").style.display="none";
            }
            else
            {
                document.getElementById("trFormName").style.display="none";
                document.getElementById("trUrl").style.display="block";
            } 
        }      

    }
}

function FormValidate()
{//debugger;
    var a=document.getElementById("radFormCategory").cells.length;  
    var index =0;
    for(var i=0;i<a;i++)
    {
        var FormCategoryID="radFormCategory_"+i;  
        if(document.getElementById(FormCategoryID).checked) //ע��checked����д��Checked��Ҫ��Ȼ���ɹ�
        { 
            if(document.getElementById(FormCategoryID).value=="0")
            {
                 if(!ValidateFormName())
                 {
                    alert('��ѡ����Ч�Ĵ������ƣ�');
                    return false;
                 }
            }
            else
            {
                if(document.getElementById("txtUrl").value=="")
                {
                     alert('������ȷ�ϵĴ���·����');
                     document.getElementById("txtUrl").focus();
                    return false;
                }
            } 
            index=parseInt(index)+1;
            break;
        } 
    }
    if(parseInt(index)>0)
    {
        return true;
    }
    else
    {
        alert('��ѡ����Ч�������');
        return false;
    }
    
}


function ValidateFormName()
{
    var a=document.getElementById("radFormName").cells.length;  
    for(var i=0;i<a;i++)
    {
        var ss="radFormName_"+i; 
        var bb=document.getElementById(ss);
        if(document.getElementById(ss).checked) //ע��checked����д��Checked��Ҫ��Ȼ���ɹ�
        {
           return true;
        }      
    }
    return false;
}
SetForm();
</script>