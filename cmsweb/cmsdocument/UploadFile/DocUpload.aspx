<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocUpload.aspx.vb" Inherits="UploadFile_DocUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style>
		BODY { MARGIN: 0px; FONT-FAMILY: Arial, Helvetica, sans-serif; Font-size:12px;}
	</style>
	<SCRIPT LANGUAGE="JavaScript" src="js/swfupload.js"></SCRIPT>
	<script type="text/javascript" src="js/swfupload.queue.js"></script>
	<script type="text/javascript" src="js/fileprogress.js"></script>
	<script type="text/javascript" src="js/handlers.js"></script>
	<script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
	<link href="css/css.css" type="text/css">
	
	<script type="text/javascript">
	var swfu;
	window.onload = function() { 

		var settings = {
			flash_url : "/cmsweb/cmsdocument/UploadFile/js/swfupload.swf",
			upload_url: "/cmsweb/cmsdocument/UploadFile/FileWebSave.aspx?resid=<%=ResID %>",
			post_params: {"PHPSESSID" : ""},
			file_size_limit : "100 MB",
			file_types : "*.*",
			file_types_description : "All Files",
			file_upload_limit : 100,
			file_queue_limit : 0,
			custom_settings : {
				progressTarget : "fsUploadProgress",
				cancelButtonId : "btnCancel"
			},
			debug: false,

			//Button settings
			button_image_url: "",
			button_width: "90",
			button_height: "17",
			button_placeholder_id: "spanButtonPlaceHolder",
			button_text: '<span class="theFont">点击上传附件</span>',
			button_text_style: ".theFont { font-size: 12;color: #F00000; }",
			button_text_left_padding: 12,
			button_text_top_padding: 2,


			// The event handler functions are defined in handlers.js
			file_queued_handler : fileQueued,
			file_queue_error_handler : fileQueueError,
			file_dialog_complete_handler : fileDialogComplete,
			upload_start_handler : uploadStart,
			upload_progress_handler : uploadProgress,
			upload_error_handler : uploadError,
			upload_success_handler : uploadSuccess,
			upload_complete_handler : uploadComplete,
			queue_complete_handler : queueComplete	// Queue plugin event
		};
		swfu = new SWFUpload(settings);
	};			
	</script>
		
		
</head>
<body>
   <form id="Form1" method="post" runat="server">
			<table cellpadding='0' cellspacing='0' border='0' width="700" style="MARGIN-LEFT:10px">
				<tr>
					<td width="50" style="FONT-SIZE: 12px" height="30">附件：</td>
					<td width="650"><span id="spanButtonPlaceHolder"></span></td>
				</tr>
				<tr>
					<td colspan="2">
						<div id="div1" style="OVERFLOW-Y:auto; OVERFLOW-X:hidden; WIDTH:100%">
							<div class="fieldset flash" id="fsUploadProgress" style="FONT-SIZE: 12px"></div>
							<div>
								<span id="spanButtonPlaceHolder1"></span><input id="btnCancel" type="hidden" value="Cancel All Uploads" style="FONT-SIZE: 8pt; MARGIN-LEFT: 2px; HEIGHT: 29px">
							</div>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="font-size:12">关键字：<asp:TextBox ID="txtKEYWORDS" Runat="server" Width="250px"></asp:TextBox></td>
				</tr>
				<tr><td colspan="2" style="font-size:12">备&nbsp;&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="txtCOMMENTS" Runat="server" TextMode="MultiLine" Width="250px" ></asp:TextBox></td></tr>
				<tr>
					<td colspan="2" height="30"><asp:Button ID="btnSubmit" Runat="server" Text="确定"></asp:Button></td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		document.getElementById("div1").style.height =document.documentElement.offsetHeight-130;
		</script>
</body>
</html>
