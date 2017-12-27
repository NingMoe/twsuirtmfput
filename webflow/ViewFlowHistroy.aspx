<%@ Reference Control="~/controls/ctlflowdiagram.ascx" %>
<%@ Reference Control="~/controls/ctlflowhistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowHistory" Src="controls/CtlFlowHistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowDiagram" Src="controls/CtlFlowDiagram.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.ViewFlowHistroy" CodeFile="ViewFlowHistroy.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.RecordEditBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>流程信息查看</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/flowstyle.css" rel="stylesheet" type="text/css">
		<script>
		function ViewAttachment(DocumentID){
			window.open("DownloadFile.aspx?ResourceID=<%=ResourceID%>&DocumentID="+DocumentID)
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<iewc:tabstrip id="TabStrip1" runat="server" TabDefaultStyle="background-color:#000000;font-family:verdana;font-weight:bold;font-size:10pt;color:#ffffff;width:130;height=22;text-align:center"
							TabHoverStyle="background-color:#777777" TabSelectedStyle="background-color:#ffffff;color:#000000"
							TargetID="MultiPage1" BorderColor="Black" BorderWidth="1px">
							<iewc:Tab Text="流程历史(图形)"></iewc:Tab>
							<iewc:Tab Text="流转历史(信息)"></iewc:Tab>
						</iewc:tabstrip>
					</td>
				</tr>
				<tr>
					<td>
						<iewc:multipage id="MultiPage1" runat="server" Height="470px" Width="100%" BorderColor="Black" BorderWidth="1px"
							BackColor="#ffffff">
							<iewc:pageview ID="t1">
								<uc1:CtlFlowDiagram id="CtlFlowDiagram1" runat="server"></uc1:CtlFlowDiagram>
							</iewc:pageview>
							<iewc:pageview ID="t2">
								<uc1:CtlFlowHistory id="CtlFlowHistory1" runat="server"></uc1:CtlFlowHistory>
							</iewc:pageview>
						</iewc:multipage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

