<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCalculationSchedule" validateRequest="false" CodeFile="FieldAdvCalculationSchedule.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>��ʱ��������</title>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="/cmsweb/css/cmsstyle.css" type=text/css rel=stylesheet >
  </HEAD>
<body>
<form id=Form1 method=post runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  <tr>
    <td width=4></td>
    <td>
      <TABLE class=table_level2 cellSpacing=0 cellPadding=0 width=624 border=0 
      style="WIDTH: 624px; HEIGHT: 288px">
        <TR>
          <TD class=header_level2 colSpan=2 height=22><b 
            >��ʱ��������</b></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 6px" align=right width=104 
          ><FONT face=����></FONT></TD>
          <TD style="HEIGHT: 6px"><FONT face=���� 
            ></FONT></TD></TR>
        <TR height=25>
          <TD style="WIDTH: 104px; HEIGHT: 22px" align=right width=104><asp:label id=Label2 runat="server">��ǰ��Դ��</asp:label></TD>
          <TD style="HEIGHT: 22px"><asp:label id=lblResName runat="server"></asp:label></TD></TR>
        <TR height=25>
          <TD style="WIDTH: 104px; HEIGHT: 22px" align=right width=104><asp:label id=Label3 runat="server">��ǰ�ֶΣ�</asp:label></TD>
          <TD style="HEIGHT: 22px"><asp:label id=lblFieldName runat="server"></asp:label></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 1px" align=right width=104 
          ><FONT face=����></FONT></TD>
          <TD style="HEIGHT: 1px"><FONT face=���� 
            ></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 8px" align=right width=104 
          ></TD>
          <TD style="HEIGHT: 8px"><asp:radiobutton id=rdoStartSchedule runat="server" Text="���ö�ʱ����" GroupName="SCHEDULESTART"></asp:radiobutton><FONT 
            face=����>&nbsp;<asp:radiobutton id=rdoStopSchedule runat="server" Text="ͣ�ö�ʱ����" GroupName="SCHEDULESTART" Checked="True"></asp:radiobutton></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 21px" align=right width=104 
          ></TD>
          <TD style="HEIGHT: 21px"><FONT face=���� 
            ></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 29px" align=right width=104 
          ><asp:label id=Label4 runat="server">����ʱ�����ã�</asp:label></TD>
          <TD style="HEIGHT: 29px"><FONT face=���� 
            ><asp:radiobutton id=rdoOccurEvery runat="server" Text="ÿ" GroupName="OCCURTIME" Checked="True"></asp:radiobutton><asp:textbox id=txtIntervalTime runat="server" MaxLength="2" Width="40px"></asp:textbox><asp:dropdownlist id=ddlTimeUnit runat="server" Width="72px"></asp:dropdownlist><asp:label id=Label6 runat="server">����һ��</asp:label></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 29px" align=right width=104 
          ></TD>
          <TD style="HEIGHT: 29px"><asp:radiobutton id=rdoOccurOnce runat="server" Text="ÿ������һ����ʱ��" GroupName="OCCURTIME"></asp:radiobutton><asp:textbox id=txtOnceTime runat="server" Width="244px"></asp:textbox><asp:label id=Label5 runat="server"> (��ʽ�� 3:00,18:35)</asp:label></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 16px" vAlign=top align=right 
          width=104><FONT face=���� 
            ></FONT></TD>
          <TD style="HEIGHT: 16px"><FONT face=���� 
            ></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 3px" align=right width=104 
          ><FONT face=����><asp:label id=Label1 runat="server">����ѡ�</asp:label></FONT></TD>
          <TD style="HEIGHT: 3px"><FONT face=���� 
            ><asp:checkbox id=chkRunAtStart runat="server" Text="����ʱ����һ��" Checked="True"></asp:checkbox></FONT></TD></TR>
        <TR>
          <TD style="WIDTH: 104px; HEIGHT: 9px" align=right width=104 
          ></TD>
          <TD style="HEIGHT: 9px"><FONT face=���� 
            ></FONT></TD></TR>
        <TR height=25>
          <TD style="WIDTH: 104px"></TD>
          <td><FONT face=����></FONT><asp:button id=btnConfirm runat="server" Text="ȷ��" Width="72px"></asp:button><asp:button id=btnCancel runat="server" Text="�˳�" Width="72px"></asp:button></td></TR></TABLE></td></tr></TABLE></form>
	</body>
</HTML>
