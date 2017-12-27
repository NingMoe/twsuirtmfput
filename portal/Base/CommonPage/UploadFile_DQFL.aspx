<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile_DQFL.aspx.cs" Inherits="Base_AJSJ_UploadFile_DQFL" %>

<%@ Register Src="~/Base/CommonControls/UploadFile.ascx" TagPrefix="uc1" TagName="UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <%=this.GetScript1_4_3   %>
    <link href="../../Scripts/layer-v2.0/layer/skin/layer.css" rel="stylesheet" />
    <script src="../../Scripts/layer-v2.0/layer/layer.js"></script>
    <script src="../../Scripts/CommonCloseWindow.js"></script>
</head>
<body>
    <script type="text/javascript" language="javascript">
        var OptionModel = eval('[<%=OptionModelStr%>]')[0]
        $(document).ready(function () {
            $("#XZMB").hide()
            $(".tableChildList").hide()
            $("#Uploader th").css("width", "200px")
        });

        function GDFL_New() {
            var o = eval('[' + $("#<%=ResID%>_Json").val() + ']')
            if ($("[name=FileJson]").length == 0) {
                alert("请先上传Excel文件！")
                return
            }

            if (o.length > 0) {
                var Docurl = o[0]["*DocHostName*"]
                var Docext = o[0].DOC2_EXT
                var Docname = o[0].DOC2_NAME
                var excelFilePath = ""
                var argTableResid = ""
                var KeyWordStr = '<%=KeyWord%>'
                var tip = "";
                var IsDown = false;
                var baseURL = window.location.protocol + "//" + window.location.host + "/webflow/Document/ToExcel/DoEventByExcel_ajax.aspx"
                var ImportDataOptionModel = parent.ImportDataOptionModel;
                var DC_Params = {
                    argUserID: '<%=UserID%>',
                    argCity: '<%=City%>'
                }
                if (ImportDataOptionModel == undefined || ImportDataOptionModel == "") {       
                    if (KeyWordStr == "DLKQ") {
                        OptionModel.KeyType = "导入考勤记录";
                        OptionModel.ReadSheetStr = "三盟"
                        argTableResid = "434801499553";
                        tip = "【导入考勤记录】导入成功";
                        DC_Params.argYear = '<%=argYear%>'
                        DC_Params.argMonth = '<%=argMonth%>'
                        DC_Params.argTableResid = argTableResid
                    }
                }
                else {
                    OptionModel = ImportDataOptionModel
                    DC_Params = parent.ImportParams
                    tip = parent.ImportParams.tip;
                }
                OptionModel.StartRowNum = 1;
                OptionModel.StartColumnNum = 1;
                OptionModel.FilePath = Docurl;
                OptionModel.HasTitle = true;
                DC_Params.GetExcelHelperOptionModelStr = JSON.stringify(OptionModel);
                var index = layer.load(1); //换了种风格
                $.ajax({
                    type: "POST",
                    data: DC_Params,
                    url: encodeURI(baseURL),
                    success: function (o) {
                        layer.close(index);
                        var Result = eval('[' + o + ']')[0];
                        if (Result.ErrorStr != "") {
                            alert(Result.ErrorStr)
                        }
                        else {
                            if (IsDown) {
                                alert(Result.ErrorStr)
                            }
                            else {
                                alert(tip + Result.Result)
                            }
                        }
                        CommonCloseWindow('<%=OpenDiv%>', '<%=gridID%>', "")
                    },
                    error: function (o) {
                        layer.close(index);

                        debugger
                    }
                })
            }
            else {
                alert("请先上传Excel文件！")
            }
        }
      
    </script>

    <form id="form1" runat="server">

        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: hidden; position: relative; border: none;"   >
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;
                           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true,disabled:false" onclick="GDFL_New()" id="DRSJ" >导入Excel数据</a> &nbsp;&nbsp;  
                         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true,disabled:false" onclick="XZMB()" id="XZMB">下载Excel模板</a>
                    </td>
                </tr>
            </table>

            <div class="easyui-panel" border="true" style="border-bottom: none;overflow-x: hidden;">
                <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width: 100%;">
                    <tr>
                        <uc1:UploadFile runat="server" ID="UploadFile1" />
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
