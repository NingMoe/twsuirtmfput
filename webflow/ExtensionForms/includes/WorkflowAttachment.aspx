<input type="hidden" id="RecId" value="<%=ViewState("_Id")%>" />

<script type="text/javascript" src="../scripts/swfupload.js"></script>
<script type="text/javascript" src="../scripts/swfupload.queue.js"></script>
<script type="text/javascript" src="../scripts/fileprogress.js"></script>
<script type="text/javascript" src="../scripts/handlers.js"></script>
<script type="text/javascript" src="../scripts/jquery-1.4.2.min.js"></script>

<script type="text/javascript">
var swfu;
window.onload = function() {
	var RecId = "0";
	if (document.getElementById("RecId")!=null) RecId=document.getElementById("RecId").value;
 
	var settings = {
		flash_url : "../scripts/swfupload.swf",
		upload_url: "../common/FileWebSave.aspx?action=upload&RecId=" + RecId,
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

