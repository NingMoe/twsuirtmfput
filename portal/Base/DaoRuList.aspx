<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaoRuList.aspx.cs" Inherits="Base_DaoRuList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>基础薪资导入</title>
    <script type="text/javascript" src="../Scripts/jquery-1.8.0.min.js"></script>
    <link  href="../Scripts/jquery-easyui-1.4.3/themes/default/easyui.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../Scripts/easyui-lang-zh_CN.js"></script>
    <link  href="../Scripts/jquery-easyui-1.4.3/demo/demo.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="../Scripts/jquery-easyui-1.4.3/themes/icon.css" rel="stylesheet" /> 
</head>
<body>
    <div style="padding:10px 0px 20px 0px;">
	   <form id="importFileForm" method="post" enctype="multipart/form-data">
	    	<table style="width:100%" cellpadding="5">
	    		<tr>
	    			<td>请选择:</td>
	    			<td>
                        <input class="easyui-filebox" name="fileImport" id="fileImport" data-options="prompt:'导入时只更新记录，不新增记录',accept:'application/vnd.ms-excel'" style="width:100%" />
	    			</td>
	    		</tr>	
                <tr>
                    <td colspan="2"><label id="fileName" /></td>
                </tr>    	 
	    		<tr>
	    			<td colspan="2" style="color:#ff6a00">说明:导入的Excel中“列名及列数量”要和导出时的模板一致，请勿修改！</td>
	    		</tr>	    		 
	    	</table>
	    </form>
	 </div>
    <div data-options="region:'south',border:false" style="text-align:right;padding:10px 0 0;">
				<a class="easyui-linkbutton" id="btnImportFile" data-options="iconCls:'icon-ok'" href="javascript:void(0)" onclick="ImportFileClick()" style="width:60px">导入</a>
				<a class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" href="javascript:void(0)" onclick="javascript:window.parent.ParentCloseWindow();" style="width:60px">取消</a>
			</div>

    <script type="text/javascript">
        function ImportFileClick() {
            $("#btnImportFile").linkbutton("disable");
            //获取上传文件控件内容
            var files = $("#fileImport").filebox('getValue');
            //判断是否选择文件
            if (files == "") { alert('上传错误，请选择文件'); return false; }
            //获取文件类型名称
            var filetype = files.substring(files.lastIndexOf('.'), files.length);
            var fileName = files.substring(files.lastIndexOf('\\') + 1, files.length);

            //这里限定上传文件文件类型必须为Excel
            if (filetype == '.xlsx' || filetype == '.xls') {
                //将文件名和文件大小显示在前端label文本中
                document.getElementById('fileName').innerHTML = "<span style='color:Blue'>文件名: " + fileName + "</span>";
                //获取form数据,并提交
                $('#importFileForm').form('submit', {
                    url: 'Common/UpLoadAjax.aspx?typeValue=ImportFile&ResID=<%=ResID %>',
                    success: function (result) {
                        var ret = eval('(' + result + ')');
                        if (ret.success || ret.success == "true") {
                            alert("导入成功！");
                            window.parent.closeWindow1();
                        }
                        else
                            alert("导入失败！");
                        $("#btnImportFile").linkbutton("enable");
                    }
                });
            }
            else {
                alert("文件类型错误！");
            }
        }

    </script>
</body>
</html>
