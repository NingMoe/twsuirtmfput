<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="Base_PublicPage_UploadFile" %>
<%-- uploadify--%>
<script src="../../Scripts/uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>
<script src="../../Scripts/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
<script src="../../Scripts/layer-v2.0/layer/layer.js" type="text/javascript"></script>
<link href="../../Scripts/layer-v2.0/layer/skin/layer.css" rel="stylesheet" />
<script src="../../Scripts/viewer-master/jqthumb.min.js" type="text/javascript"></script>
<style>
    .CommonViewFile {
        text-decoration: none;
        border-bottom: 1px solid blue; /* #ccc换成链接的颜色 */
        color: blue;
    }

    .tableChildList td a.CommonViewFile {
        color: blue;
    }

        .tableChildList td a.CommonViewFile:hover {
            color: red;
            border-bottom: 1px solid red; /* #ccc换成链接的颜色 */
        }
</style>
<script type="text/javascript">

    //var CommonPath = window.location.protocol + "//" + window.location.host + "/portal2015/Base/"

    var CommonPath = GetApplicationPath();
    //附件上传
    //function loadUploadField() {

    var IsNotViewFiles = '<%=IsNotViewFiles%>'
    $(document).ready(function () {
        if ($("#Uploadqzfj<%=ResID%>Uploader").length > 0)
            return;
        $("#Uploadqzfj<%=ResID%>").uploadify({
            'method': "post",
            'uploader': CommonPath + 'Scripts/uploadify-v2.1.4/uploadify.swf',
            'script': CommonPath + 'Base/Common/UpLoadAjax.aspx?typeValue=SaveUploadInfo&ResID=<%=ResID %>',
            'cancelImg': CommonPath + 'Scripts/uploadify-v2.1.4/cancel.png',
            'folder': '<%=Savefolder%>',
            'buttonImg': CommonPath + 'images/uploadImg.jpg',
            'queueID': 'fileqzfjQueue<%=ResID %>',
            'height': 27,
            'width': 120,
            'scriptData': { 'ResID': '<%=ResID %>' },
            'auto': true,
            'multi': true,
            'sizeLimit': 50000000,
            'onSelect': function (Event, queueID, fileObj) {
                if (fileObj.size > 50000000) {
                    alert("文件 [" + fileObj.name + "] 大小超出50MB,请重新选择要上传的文件！");
                    return false;
                }
            },
            'onAllComplete': function (event, data) {
                if (data != null) {
                    alert("已成功上传" + data.filesUploaded + "个附件,失败" + data.errors + "个!");
                    LoadFiles($("#<%=ResID%>_Json"), $("#qzfjcontent"), "qzfj");
                } else {
                    alert('上传失败！');
                }
            },
            onError: function (event, queueId, fileObj, errorObj) {
                debugger
            }
        });
        debugger
        if (IsNotViewFiles == "")
            ViewFiles();
    });


    //JS截取字符串实际长度
    function splitStrAndLen(str, Strlen) {
        ///<summary>获得字符串实际长度，中文2，英文1</summary>
        ///<param name="str">要获得长度的字符串</param>
        var realLength = 0, len = str.length, charCode = -1;
        for (var i = 0; i < len; i++) {
            charCode = str.charCodeAt(i);
            if (charCode >= 0 && charCode <= 128) {
                realLength += 1;
            } else {
                realLength += 2;
            }
            if (realLength > Strlen) {
                return str.substring(0, i);
                break;
            }
            if (i == len - 1) {
                return str;
            }
        }
    };


    function ViewFiles() {
        var ParentRecID = "<%=ParentRecID%>"
        try {
            if (DynamicView != undefined && DynamicView == true) {
                if (DynamicRecid != undefined && DynamicRecid != "") {
                    ParentRecID = DynamicRecid;
                }
            }
        }
        catch (ex) {
        }

        $.ajax({
            type: "POST",
            url: CommonPath + "Base/Common/UpLoadAjax.aspx?typeValue=GetUploadInfo&ResID=<%=ResID%>&ParentResID=<%=ParentResID%>&ParentRecID=" + ParentRecID,
            success: function (result) {
                if (result != "") {
                    var o = eval(result);
                    parent._FileObj = o
                    $("#tableFilesList tr:not(.tdHead)").remove()
                    for (var i = 0; i < o.length; i++) {
                        var str = "<tr>"
                        //str += "<td>&nbsp;" + o[i]["DocName"] + "</td>"
                       // str += "<td>&nbsp;<a class=\"CommonViewFile\"  DocHostName='" + o[i]["DocHostName"] + "'  href='#' onclick='CommonViewFile(" + i + ",\"" + o[i]["DocHostName"] + "\",\"" + o[i]["DocName"] + "\")'>" + o[i]["DocName"] + "</a></td>"
                        str += "<td>&nbsp;" + o[i]["DocName"] + "</td>"
                        str += "<td style=' text-align:center;'>" + o[i]["DocSize"] + "KB</td>"
                        str += "<td style=' text-align:center;'>" + o[i]["CreateDate"] + "</td>"
                       str += "<td style=' text-align:center;'><a href='#' CommonDownFile='CommonDownFile'  onclick='CommonDownFile(this,\"" + o[i]["DocHostName"] + "\",\"" + o[i]["DocName"] + "\")' ><img src='" + CommonPath + "images/download.png' style='border:0px;vertical-align:bottom;width:16px;height:16px;'></a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='#' deleteFilesImg='deleteFilesImg'   onclick='DeleteDoc(this,\"" + o[i]["ID"] + "\")'><img src='" + CommonPath + "images/del.gif' style='border:0px;vertical-align:bottom;' ></a></td>"
                        str += "</tr>";
                        $("#tableFilesList").append(str);
                    }
                }

                $(".CommonViewFile").mouseenter(function () {
                    var DocHostName = $(this).attr("DocHostName");
                    var type = CheckFileType(DocHostName)
                    if (type != 3) return
                    var info = "<img src='" + DocHostName + "'></img>"
                    $("#BigImg").attr("src", DocHostName);
                    $("#BigImgDIV").show()
                    $("#SmallImg").attr("basesrc", DocHostName);
                    GetSmallImage($(this))
                }).mouseleave(function () {
                    layer.closeAll('tips');
                })

                if ($.isFunction(window.OtherFunciton)) {
                    OtherFunciton();
                }
               // 隐藏下载按钮
               // $("a[CommonDownFile]").remove();
            }
        });

    }
    function LoadFiles(ControlJson, objcontent, str) {
        $.ajax({
            type: "POST",
            url: CommonPath + "Base/Common/UpLoadAjax.aspx?typeValue=LoadUploadInfo,
            success: function (result) {
           
                // debugger
                var o = eval(result);

                var UploadFileName = o[0].UploadFileName;

                var FileSize = o[0].FileSize.split("|");
                var UploadPathUrl = (o[0].UploadPathUrl + "/").replace("//", "/");

                var fj = UploadFileName.split("|");
                var today = new Date();
                var year = today.getFullYear() + "年";
                var month = (today.getMonth() + 1).toString() + "月";
                var day = today.getDate() + "日";

                var strJson = "";
                for (var i = 0; i < fj.length; i++) {
                    var tuName = "";
                    var FileExt = fj[i].substring(fj[i].lastIndexOf(".") + 1, fj[i].length);
                    if (fj[i].split("[_]").length > 0) {
                        tuName = fj[i].split("[_]")[0];
                    } else {
                        tuName = fj[i];
                    }

                    strJson = "{'RecID':'0','*DocHostName*':'" + UploadPathUrl + fj[i] + "','DOC2_NAME':'" + tuName + "','DOC2_EXT':'" + FileExt + "','DOC2_SIZE':'" + FileSize[i] + "','*IsUpDirectory*':'true','DOC2_RESID1':'<%=ResID %>'}"

                    objcontent.append($(" <div id='" + str + (fj[i].toString()).substring(0, fj[i].indexOf('.')) + "' style=\"padding:3px 9px 5px 0px;width:100%;\" onmouseover='showFiles(this)' onmouseout='hideFiles(this)'><input name='FileJson' type='hidden' value=\"" + strJson + "\" /><a href='#' CommonDownFile='CommonDownFile'   target='_blank' style='width:60%;' onclick='CommonDownFile(this,\"" + UploadPathUrl + fj[i] + "\")' >" + tuName + "." + FileExt + "</a>&nbsp;&nbsp;&nbsp;&nbsp;<a id='a" + str + fj[i] + "' class='" + str + "' href='#' style='display:none'  onclick='deleteFiles(this,\"" + UploadPathUrl + fj[i] + "\")'>删除</a></div>"));
                }
                ControlJson.val(ControlJson.val() + strJson);
                if ($.isFunction(window.OtherFunciton)) {
                    OtherFunciton();
                }
            }
        });
    }

    function GetSmallImage(obj) {
        if ($("#BigImg").width() < 200 && $("#BigImg").height() < 200) {
            layer.tips($('#BigImgDIV').html(), obj, {
                tips: [1, '#FFFFCC']
            });
        }
        else {
            $('#SmallImg').jqthumb({
                width: 200,
                height: 200,
                source: 'basesrc',
                done: function () {
                    layer.tips($('#SmallImgDIV').html(), obj, {
                        tips: [1, '#FFFFCC']
                    });
                }
            });
        }
    }

    function CommonViewFile(index, DownFileUrl, DocName) {
        var type = CheckFileType(DownFileUrl)
        if (type == 0 || type == 3) {
            if (type == 3) {
                var url = "../portal/Base/CommonPage/ViewImg.aspx?ResID=<%=ResID %>&RecID=<%=RecID %>&FileIndex=" + index;
                parent.OpenLayerByUrl("图片在线预览", url, "100%", "100%", "");
               <%-- window.open("../CommonPage/ViewImg.aspx?ResID=<%=ResID %>&RecID=<%=RecID %>&DownFileUrl=" + encodeURI(DownFileUrl), "_parent")--%>
            } else {
                window.open(DownFileUrl)
            }
        }
        else if (type == 4) {
            window.open("../CommonPage/ViewPDF.aspx?DownFileUrl=" + encodeURI(DownFileUrl))
        }
        else if (type == 2) {
            window.open(window.location.protocol + "//" + window.location.host + "/webflow/Document/NewOfficeEdit/NewOfficeEditor.aspx?IsCheckOut=2&ResourceID=" + "" + "&DocumentPath=" + encodeURI(DownFileUrl) + "&IsHideBtnSave=1&IsShowBtnPrint=1&PortalPath=" + "&keyWord=")
        }
        else {
            alert("未知的文件类型，无法在线查阅！")
        }
}

function CheckFileType(DownFileUrl) {
    var arr = DownFileUrl.split('.')
    var hz = arr[arr.length - 1]
    var type = 0;

    if (hz == "txt") {
        type = 1;
        return type;
    }
    else if (hz == "doc" || hz == "docx" || hz == "xls" || hz == "xlsx" || hz == "ppt" || hz == "pptx") {
        type = 2;
        return type;
    }
    else if (hz == "pdf") {
        type = 4;
        return type;
    }

    var b = /\w+([.jpg|.png|.gif|.swf|.bmp|.jpeg]){1}$/;
    var t_value = DownFileUrl.toLowerCase();
    var a = b.test(t_value);
    if (a) {
        type = 3;
    }
    return type;
}


function CommonDownFile(o, DownFileUrl, DocName) {
    //alert(DownFileUrl)
    window.open("../ArchivesFileHandle/DownFile.aspx?DownFileUrl=" + DownFileUrl + "&DocName=" + DocName)
    //window.open(DownFileUrl)
}

function showFiles(obj) {
    var $v = $(obj);
    var id = $v.parent().attr("id");
    if (id == "qtfjcontent") {
        $v.find(".qtfj").show();
    } else {
        $v.find(".qzfj").show();
    }
}
function hideFiles(obj) {
    var $h = $(obj);
    $h.find(".qtfj").hide();
    $h.find(".qzfj").hide();
}

function DeleteDoc(obj, RecID) {
    if (window.confirm("您确认删除？")) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: CommonPath + "Base/Common/UpLoadAjax.aspx?typeValue=DeleteDoc&ResID=<%=ResID %>&RecID=" + RecID,
            success: function (result) {
                if (result.success || result.success == "true") {
                    $(obj).closest('tr').remove();
                    alert("操作成功！");
                } else {
                    alert("操作失败,请刷新页面！");
                }
            }
        });
    }
}



function deleteFiles(obj, FilePathUrl) {
    var $p = $(obj);
    var a = $p.attr("id");
    var id = a.substr(5, a.length);
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "FilePathUrl": "" + FilePathUrl + "" },
        url: CommonPath + "Base/Common/UpLoadAjax.aspx?typeValue=DeleteFile",
        success: function (result) {
            if (result.success || result.success == "true") {

                $p.parent().remove();
                alert("操作成功！");
            } else {
                alert("操作失败,请刷新页面！");
            }
        }
    });

}

function GetFilesJson() {
    var strJson = "";
    $("#qzfjcontent input[name='FileJson']").each(function () {
        strJson += "," + this.value;
    });
    
    if (strJson.trim() != "") strJson = strJson.substr(1, strJson.length - 1);
    return "[" + strJson + "]";
    
}
</script>





<div title="附件信息" id="divFiles" class="easyui-panel" collapsible="true"  style="overflow: hidden; padding: 0px; border-bottom: none; margin: 0px;">
    <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2" style="width: 100%; overflow-x: hidden;">
        <tr id="Uploader">
            <th style="height: 29px; width: 100px;">附件上传</th>
            <td id="qzfjcontent" style="height: 29px; width: 660px; padding: 5px;">
                <input id="<%=ResID%>_Json" type="hidden" class="box3" style="width: 260px;" />
            </td>
            <td style="padding: 5px;">
                <div id="QZFJ">
                    <div id="uploadqzfjFileID<%=ResID %>">
                        <div id="fileqzfjQueue<%=ResID %>"></div>
                        <input type="file" name="uploadify" id="Uploadqzfj<%=ResID %>" />
                    </div>
                    <div id="uploadqzfjFieldName<%=ResID %>" style="display: none;" />

                </div>
            </td>
        </tr>
    </table>

    <table border="0" class="tableChildList" cellspacing="0" cellpadding="0" id="tableFilesList" style="width: 100%; overflow-x: hidden; margin-top: 10px; margin-bottom: 20px;">
        <tr class="tdHead">
            <td style="width: 70%;">&nbsp;附件名称1</td>
            <td style="width: 10%; text-align: center;">附件大小</td>
            <td style="width: 10%; text-align: center;">上传时间</td>
            <td style="width: 10%;">&nbsp;</td>
        </tr>
    </table>
    <div id="SmallImgDIV" style="display: none">
        <img id="SmallImg" /></div>
    <div id="BigImgDIV" style="position: absolute; left: 2000px;display:none">
        <img id="BigImg" /></div>
</div>
