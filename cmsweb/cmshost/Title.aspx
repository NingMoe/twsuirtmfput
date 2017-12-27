<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.Title" CodeFile="Title.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>导航栏</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script src="/cmsweb/script/poslib.js" type="text/javascript"></script>
		<script src="/cmsweb/script/menu4.js" type="text/javascript"></script>
		<script src="/cmsweb/script/scrollbutton.js" type="text/javascript"></script>
		<script language="javascript">
<%
        If CmsPass() Is Nothing Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
%>
<!--
	//系统菜单 
	var mnuSysFunc = new Menu();
	var mnuSysmgr = new Menu();
	var mnuTools = new Menu();
	var mnuHelp = new Menu();
	var tmp;
 
	
	function showSysFuncMenu(el){
		var left=posLib.getScreenLeft(el);
		var top=posLib.getScreenTop(el) + el.scrollHeight;
		mnuSysFunc.invalidate();
		mnuSysFunc.show(left, top);
	}

	function showSysMenu(el){
		var left=posLib.getScreenLeft(el);
		var top=posLib.getScreenTop(el) + el.scrollHeight;
		mnuSysmgr.invalidate();
		mnuSysmgr.show(left, top);

		//var left=posLib.getScreenLeft(el);
		//var top=posLib.getScreenTop(el)+posLib.getHeight(el);
		//mnuSysmgr.invalidate();
		//left = left - mnuSysmgr.getPreferredWidth() + posLib.getWidth(el);
		//mnuSysmgr.show( left, top );
	}

	function showToolsMenu(el){
		var left=posLib.getScreenLeft(el);
		var top=posLib.getScreenTop(el) + el.scrollHeight;
		mnuTools.invalidate();
		mnuTools.show(left, top);
	}

	function showHelpMenu(el){
		var left=posLib.getScreenLeft(el);
		var top=posLib.getScreenTop(el) + el.scrollHeight;
		mnuHelp.invalidate();
		mnuHelp.show(left, top);
	}

	//----------------------------------------------------------------------------
 
	
	<%If CmsFunc.IsEnable("FUNC_FULLDB_SEARCH") = True AndAlso CmsPass.Employee.Type <> EmployeeType.SysAdmin AndAlso CmsPass.EmpIsSysSecurity = False Then%>
		mnuSysFunc.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_SYSFUNC_SEARCHDOC")%>","/cmsweb/cmsdocument/DocSearchFullDb.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员、部门管理员、ASP管理员可以看到邮件短信群发
	<%If CmsFunc.IsEnable("FUNC_BATCHSEND") AndAlso (CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin) Then %>
		mnuSysFunc.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_BATCH_SEND")%>","/cmsweb/cmsbatchsend/BatchSendList.aspx?mtstype=5", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//菜单：提取系统日志。所有人都可以提取技术日志，为了方便为客户解决问题
	mnuSysFunc.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_GETLOG")%>","/cmsweb/adminsys/SysGetLog.aspx", "") );
	tmp.target = "cmsbody";
	//----------------------------------------------------------------------------

	//----------------------------------------------------------------------------
	//系统管理菜单
	//仅系统管理员可以看到部门管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp = new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_DOMAIN")%>","/cmsweb/adminsys/ImportDomainUsers.aspx"));
		tmp.target = "cmsbody";
	<%End If%>
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin Then %>
		mnuSysmgr.add(tmp = new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_DEP")%>","/cmsweb/adminsys/DepartmentManager.aspx","/cmsweb/images/tree/dep_real.gif"));
		tmp.target = "cmsbody";
	<%End If%>
	
	//仅系统管理员和部门管理员可以看到人员管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin Then %>
		mnuSysmgr.add(tmp = new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EMP")%>","/cmsweb/adminsys/EmployeeFrmBody.aspx"));
		tmp.target = "cmsbody";
	<%End If%>
	
	 
		//仅系统管理员可以看到在线用户管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin and CmsConfig.GetInt("SYS_CONFIG", "ValidateOnlineUser") = 1 Then %>
		mnuSysmgr.add(tmp =new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_ONLINEUSER")%>","/cmsweb/adminsys/OnlineUserList.aspx"));
		tmp.target = "cmsbody";
	<%End If%>
	
	//仅系统管理员和部门管理员可以看到资源管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_RES")%>","/cmsweb/adminres/ResourceFrameBody.aspx", "/cmsweb/images/tree/res_empty.gif") );
		tmp.target = "cmsbody";
	<%End If%>
	
	
	//仅系统管理员和部门管理员可以看到权限管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_RIGHT")%>","/cmsweb/cmsrights/RightFrmBody.aspx.aspx", "/cmsweb/images/titleicon/key1.gif") );
		tmp.target = "cmsbody";
	<%End If%>
	
	
	mnuSysmgr.add(new MenuSeparator() );

	//仅系统管理员和部门管理员可以看到系统计算公式
	<%If CmsFunc.IsEnable("FUNC_FORMULA") AndAlso (CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsDepAdmin) Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_FORMULA")%>","/cmsweb/adminres/FieldAdvCalculationList.aspx?mnuresid=0&backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员可以看到行过滤设置
	<%If CmsFunc.IsEnable("FUNC_ROWWHERE") AndAlso CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_GENERAL_ROWFILTER")%>","/cmsweb/adminres/MTableSearch.aspx?mtstype=3&mnufromadmin=admin&backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_PERSONAL_ROWFILTER")%>","/cmsweb/adminres/MTableSearch.aspx?mtstype=4&mnufromadmin=admin&backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员可以看到列表统计
	<%If CmsFunc.IsEnable("FUNC_MULTITABLE_SEARCH") AndAlso CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_STATISTICS")%>","/cmsweb/adminres/MTableSearch.aspx?mtstype=1&mnufromadmin=admin&backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "/cmsweb/images/titleicon/serch.gif") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员可以看到邮件服务器管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EMAIL_CONFIG")%>","/cmsweb/cmsbatchsend/BatchSendEmailSetting.aspx?emailset_type=SYS_SMTP", "/cmsweb/images/titleicon/email.gif") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员可以看到设置客户编码
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_CLIENT_CODE")%>","/cmsweb/adminsys/SysClientCode.aspx?backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//仅系统管理员可以看到日志管理
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsSysSecurity Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_LOG")%>","/cmsweb/adminsys/SysLogManager.aspx", "/cmsweb/images/titleicon/sub.gif") );
		tmp.target = "cmsbody";
	<%End If%>
	
	mnuSysmgr.add(new MenuSeparator());

	//仅系统管理员可以看到系统参数设置
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuSysmgr.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_SYS_OPTION")%>","/cmsweb/cmsdev/DevAppConfig.aspx?isfrom=admin&conffile=app_config.xml&backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>
	
	
	 
	//----------------------------------------------------------------------------

	//----------------------------------------------------------------------------
	//工具Tools菜单
	//mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EDIT_PASS")%>", "javascript:OpenPasswordEdit();", "/cmsweb/images/titleicon/key4.gif"));
	mnuTools.add(new MenuSeparator() );
	mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EDIT_PASS")%>","/cmsweb/cmsothers/UpdatePassword.aspx", "") );
	tmp.target = "cmsbody";
	//mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EDIT_PROFILE")%>", "javascript:OpenUserInfoEdit();"));
	mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_EDIT_PROFILE")%>","/cmsweb/cmsothers/UpdateUserProfile.aspx", "") );
	tmp.target = "cmsbody";
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		//更新系统数据库
		mnuTools.add(new MenuSeparator() );
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_UPDATE_DATABASE")%>","/cmsweb/adminsys/SysDbUpdate.aspx", "") );
		tmp.target = "cmsbody";
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_UPDATE_SYSTEM")%>","/cmsweb/adminsys/SysConfigUpdate.aspx", "") );
		tmp.target = "cmsbody";
		mnuTools.add(new MenuSeparator() );
	<%End If%>

	//仅系统管理员可以看到系统注册，ASP服务不必看到此菜单
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_PRODCODE")%>","/cmsweb/adminres/admincode/CodeMgrProduct.aspx", ""));
		tmp.target = "cmsbody";
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_LICCODE")%>","/cmsweb/adminres/admincode/CodeMgrLicense.aspx", ""));
		tmp.target = "cmsbody";
		mnuTools.add(new MenuSeparator() );
	<%End If%>
	
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then %>
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MAINTAIN_DATABASE")%>","/cmsweb/adminsys/SysDbMaintain.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//菜单：系统调试
	<%If CmsPass.Employee.Type = EmployeeType.SysAdmin Then%>
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_SYSTEM_DEBUG")%>","/cmsweb/adminsys/SysDebug.aspx?backpage=/cmsweb/adminres/ResourceFrameBody.aspx", "") );
		tmp.target = "cmsbody";
	<%ElseIf CmsPass.EmpIsDepAdmin Then%>
		mnuTools.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_SYSTEM_DEBUG")%>","/cmsweb/adminsys/SysDebug.aspx?backpage=/cmsweb/cmshost/CmsFrmBody.aspx", "") );
		tmp.target = "cmsbody";
	<%End If%>

	//跳出密码修改对话框
	//function OpenPasswordEdit(){
	//	window.open("/cmsweb/cmsothers/UpdatePassword.aspx", 'passedit', "left=200,top=200,height=150,width=325,status=no,toolbar=no,menubar=no,location=no,resizable=no"); 
	//}
	//跳出用户基本信息修改框
	//function OpenUserInfoEdit(){
	//	window.open("/cmsweb/cmsothers/UpdateUserProfile.aspx", 'useredit', "left=200,top=200,height=240,width=405,status=no,toolbar=no,menubar=no,location=no,resizable=no"); 
	//}
	//提取系统错误信息
	//function GetSysErrorInfo(){
	//	window.open("SysGetLog.aspx", 'syserror', "left=200,top=200,height=165,width=405,status=no,toolbar=no,menubar=no,location=no,resizable=no"); 
	//}
	//----------------------------------------------------------------------------

	//----------------------------------------------------------------------------
	//帮助菜单
	mnuHelp.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_USER_MANUAL")%>", "javascript:OpenHelpDialog('help/CMS用户使用手册.htm');", "/cmsweb/images/titleicon/book.gif"));
	mnuHelp.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_SYSTEM_MANUAL")%>", "javascript:OpenHelpDialog('help/CMS系统管理手册.htm');"));
	<%If CmsConfig.NoCorpInfoShowed() = False Then%>
		mnuHelp.add(new MenuSeparator() );
		mnuHelp.add(tmp=new MenuItem("<%= CmsMessage.GetUI(CmsPass, "TITLE_MENU_ABOUT")%>", "javascript:OpenAboutUs('/cmsweb/cmsothers/aboutus.htm');"));
	<%End If%>
	//tmp.target = "new";

	//跳出帮助手册
	function OpenHelpDialog(url){
		//window.showModalDialog(url, "", "dialogHeight:580px; dialogWidth:680px; center;yes"); 
		window.open(url, 'help', "left=20,top=20,height=550,width=750,scrollbars=yes,status=no,toolbar=no,menubar=no,location=no,resizable=yes"); 
	}

	//跳出“关于本系统”
	function OpenAboutUs(url){
		window.showModalDialog(url, "aboutus", "dialogHeight:390px; dialogWidth:586px; center;yes");
		//window.open(url, 'aboutus', "left=20,top=20,height=550,width=750,scrollbars=yes,status=no,toolbar=no,menubar=no,location=no,resizable=yes"); 
	}
	//----------------------------------------------------------------------------
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="header_level1" height="26" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td style="HEIGHT: 25px;" align="left" id="tdSystem" runat="server" nowrap>
						&nbsp;<IMG src="/cmsweb/images/rect.gif" align="absMiddle" border="0" width="6" height="7">&nbsp;
						<%If CmsPass.Employee.Type <>  EmployeeType.SysAdmin AndAlso CmsPass.EmpIsSysSecurity = False Then%>
						<asp:hyperlink id="lnkHome" runat="server" Target="_top" NavigateUrl="/cmsweb/cmshost/CmsFrame.aspx">系统首页</asp:hyperlink>&nbsp;&nbsp;&nbsp;<%End If%>
						<%If CmsPass.Employee.Type <>  EmployeeType.SysAdmin AndAlso CmsPass.EmpIsSysSecurity = False Then%>
						<asp:hyperlink id="lnkWorkflow" runat="server" Target="_top">工作流</asp:hyperlink>&nbsp;&nbsp;&nbsp;
						<%End If%> 
						<%If CmsPass.EmpIsSysSecurity = False Then%>
						<asp:hyperlink id="lnkSysFunc" runat="server"></asp:hyperlink>&nbsp;&nbsp;&nbsp;
						<%End If%>
						<%If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsSysSecurity OrElse CmsPass.EmpIsDepAdmin Then%>
						<asp:hyperlink id="lnkSystem" runat="server"></asp:hyperlink>&nbsp;&nbsp;&nbsp;
						<%End If%>
						<asp:hyperlink id="lnkTools" runat="server" ></asp:hyperlink>
						<asp:hyperlink id="lnkHelp" runat="server"></asp:hyperlink>&nbsp;&nbsp;&nbsp;
						 
						<asp:hyperlink id="lnkExit" runat="server" NavigateUrl="#" onclick='Exit();'></asp:hyperlink>
						 [&nbsp;<asp:label id="lblEmpName" runat="server" ForeColor="Black"></asp:label>&nbsp;]
					</td>
					<td style="HEIGHT: 25px;" align="left" id="tdEmployye" runat="server" nowrap>
					    &nbsp;<IMG src="/cmsweb/images/rect.gif" align="absMiddle" border="0" width="6" height="7">&nbsp;
					    <asp:HyperLink ID="lnkDomain" runat="server" NavigateUrl="/cmsweb/adminsys/ImportDomainUsers.aspx" Target="cmsbody"></asp:HyperLink>&nbsp;&nbsp;
					    <asp:HyperLink ID="lnkDept" runat="server" NavigateUrl="/cmsweb/adminsys/DepartmentManager.aspx" Target="cmsbody"></asp:HyperLink>&nbsp;&nbsp;
					    <asp:HyperLink ID="lnkEmp" runat="server" NavigateUrl="/cmsweb/adminsys/EmployeeFrmBody.aspx" Target="cmsbody"></asp:HyperLink>	
					    <asp:hyperlink id="lnkExit1" runat="server" NavigateUrl="#" onclick='Exit();'></asp:hyperlink>
						 [&nbsp;<asp:label id="lblEmpName1" runat="server" ForeColor="Black"></asp:label>&nbsp;]				   
					</td> 
					<td style="WIDTH: 130px; HEIGHT: 25px" align="right">
					
						 <asp:LinkButton id="btn_Exit" Runat="server" Text="退出" style="display:none;"></asp:LinkButton> 
						<%If CmsConfig.NoCorpInfoShowed() = False Then%>
							<IMG title="三盟软件(中国.上海)。客服邮箱service@unionsoft.cn；客服电话021-64739258" height="22" src="../images/logo.gif" align="absMiddle" border="0">
						<%End If%>
					</td>
					<TD style="WIDTH: 1px;HEIGHT: 25px" vAlign="bottom" align="left"><asp:Panel id="Panel1" runat="server" Width="1px" Height="1px">&nbsp;</asp:Panel>
					</TD>
				</tr>
				<tr>
					<td bgColor="#ffffff" colSpan="3" height="2"></td>
					<TD bgColor="#ffffff" height="2"></TD>
				</tr>
				<tr>
					<td colSpan="3" height="1"></td>
					<TD height="1"></TD>
				</tr>
			</table>
		</form>
		<script>
			window.setTimeout("location.href = location.href;",300000);	//	5'刷新一次
			function Exit()
			{
			    document.getElementById("<%=btn_Exit.ClientID %>").click();
			}
			
			
		</script>
	</body>
</HTML>
