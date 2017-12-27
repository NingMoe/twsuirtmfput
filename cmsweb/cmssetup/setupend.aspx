<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SetupEnd" CodeFile="SetupEnd.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">系统初始化－成功</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<TR>
					<TD>
						<TABLE class="form_table" cellSpacing="0" cellPadding="8">
							<TR>
								<TD class="form_header" align="center" height="19">
									<asp:Label id="Label1" runat="server" Font-Names="宋体" ForeColor="Red" Font-Bold="True" Font-Size="14px">安 装 完 成</asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="left" width="100%" height="19"><BR>
									<FONT color="#ff0066">企业数据流系统已经成功安装和初始化，请务必仔细阅读下述本系统使用的简要说明。<BR>
									</FONT>
									<BR>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="left" width="100%" height="19">
									<P><STRONG>统一登录页面：</STRONG>http://服务器地址/cmsweb<BR>
										服务器地址是指您当前正安装系统的服务器域名或IP地址。本系统所有功能（系统管理、内容管理、系统初始化等）的入口均使用上述统一登录页面。<BR>
										<BR>
										<STRONG>如何进入系统管理？</STRONG>
										<BR>
										在上述登录页面输入帐号admin，密码为空，进入系统管理页面后请先更改admin帐号的密码，然后根据系统管理手册来创建部门组织机构、人员和具体业务应用。<BR>
										<BR>
										<STRONG>如何进入Unionsoft 企业管理应用平台？</STRONG><BR>
										在上述登录页面输入帐号（系统管理中创建的人员帐号）和密码后单击“确认”。<BR>
										<BR>
										<STRONG>如何正式注册？<BR>
										</STRONG>您可以选择邮件注册、传真注册、电话注册三种方法之一。<BR>
										<BR>
										邮件注册：按以下格式发送电子邮件至<A href="mailto:service@unionsoft.cn">service@unionsoft.cn</A><BR>
										产品码：xxxx-xxxx-xxxx-xxxx（在CD盒上）<BR>
										购买方：xxx（企业名称）<BR>
										行业：xxx（如：金融、钢铁、化工等）<BR>
										联系人：xxx<BR>
										联系电话：xxx<BR>
										联系地址：xxx<BR>
										邮编：xxx<BR>
										<BR>
										传真注册：按以上格式填写信息后传真至：021-54653583<BR>
										<BR>
										电话注册：请致电本公司客户服务电话：021-64739258。<BR>
										<BR>
										<STRONG>为什么要正式注册？</STRONG>
										<BR>
										本系统安装完毕后请向软件厂商正式注册，注册后三盟软件会提供注册码，之后本系统才可以无时间和防盗版技术等任何限制地正常运行，同时三盟软件可以为您提供产品使用咨询和软件升级优惠等全面的优质服务。如果不注册，本系统将在6个月后暂停使用（绝对不影响系统数据），直到正式注册后才能重新启用。<BR>
										<BR>
										<STRONG>如何咨询产品使用信息？</STRONG><BR>
										请发送电子邮件至客服邮箱<A href="mailto:service@unionsoft.cn">service@unionsoft.cn</A>或者致电客服电话021-64739258。</P>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="19"><BR>
									<BR>
									<asp:button id="btnConfirm" runat="server" Text="回到登录页面" Width="96px"></asp:button>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="left" width="100%" height="19"><P>&nbsp;</P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
