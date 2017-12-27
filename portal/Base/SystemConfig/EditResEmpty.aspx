<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditResEmpty.aspx.cs" Inherits="Base_EditResEmpty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/kindeditor-4.1.11/kindeditor/kindeditor-all-min.js"></script>
    <link href="../../Scripts/kindeditor-4.1.11/kindeditor/themes/default/default.css" rel="stylesheet" />

      <%=this.GetScript1_4_3   %>

    <script type="text/javascript" language="javascript">
        var TitleColName = "";
        var ColName = "";

        var editor = KindEditor.editor({
            fieldName: 'imgFile',  //不要修改
            allowFileManager: true,
            allowUpload: true, //允许上传图片
            uploadJson: '../common/upload_json.ashx?fileName=Icon',
            fileManagerJson: '../common/file_manager_json.ashx',
            afterUpload: function (data) {
            },
            afterError: function (str) {
                alert('自定义错误信息: ' + str);
            }
        });


        function GetHeadPortrait() {

            editor.loadPlugin('image', function () {
                editor.plugin.imageDialog({
                    showRemote: false,
                    imageUrl: $('#HeadIMG').val(),
                    clickFn: function (url, title, width, height, border, align) {
                        $('#<%=ResID %>_资源图标').val(url);
                        $('#HeadIMG').attr("src", url);
                        editor.hideDialog();
                    }
                });
            })
            }


            $(document).ready(function () {
                SetBackgroundColor();
                //如果主表记录ID和主表ResID不为空，并且 RecID 为空 （代表是添加记录），则执行下列方法
                //读取并自动完成主表的记录               
                $("#GetHeadPortraitButton").css("cursor", "pointer").click(function () {
                   
                    GetHeadPortrait();
                });

                if ('<%=ResID %>' != "") {
                    $.ajax({
                        type: "POST",
                        url: "Ajax_Request.aspx?typeValue=GetDataByResEmpty&ResID=<%=ResID %>",
                        success: function (centerJson) {
                            centerJson = centerJson.substring(centerJson.indexOf('"rows":') + 7, centerJson.indexOf('"footer":') - 1);
                            var jsonList = eval("(" + centerJson + ")");
                            for (var i = 0; i < jsonList.length; i++) {
                                for (var key in jsonList[i]) {

                                    if ($("#<%=ResID %>_" + key).attr("type") == "checkbox") {
                                    if (jsonList[i][key] == "1" || jsonList[i][key] == "是" || jsonList[i][key] == "True" || jsonList[i][key] == "true") $("#<%=ResID %>_" + key).attr("checked", "true");
                                }
                                else if (key == "打开方式") {
                                    strValue = jsonList[i][key];
                                    if (strValue == "_blank" || strValue == "_parent" || strValue == "_search" || strValue == "_self" || strValue == "_top") {
                                        $("#chk" + strValue).attr("checked", "true");
                                    }
                                    else $("#ch_txt").val(strValue);
                                }
                                else {
                                    $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                                }

                                    var url = $('#<%=ResID %>_资源图标').val();
                                if (url != "")
                                    $('#HeadIMG').attr("src", url);
                            }
                        }
                    }
                    });
            }
                SetBackgroundColor();
            });

        function fnSave() {
            if (CheckValue("div<%=ResID %>FormTable")) {
                $("#fnChildSave").attr("disabled", true);
                var ResourceTarget = "";
                var IsEnable = 0;
                $("input[name='chk_ResourceTarget']:checked").each(function () {
                    ResourceTarget = this.value;
                })
                var jsonStr1 = "[{";
                jsonStr1 += GetFromJson("<%=ResID %>");
            jsonStr1 += "'打开方式':'" + ResourceTarget + "'";

            jsonStr1 += "}]";
           
            $.ajax({
                type: "POST",
                dataType: "json",
                data: { "Json": "" + jsonStr1 + "" },
                url: "Ajax_Request.aspx?typeValue=SaveResEmptyInfo&ResID=<%=ResID %>",
                    success: function (obj) {
                        if (obj.success || obj.success == "true") {
                            alert("保存成功！");
                            window.parent.ParentCloseWindow();
                            window.parent.RefreshGrid("CenterGridResEmpty");

                        } else {
                            alert("保存失败,请刷新页面！");
                        }
                    }
                });
            }
        }

        function ClickCheckBox1(obj) {
            var check = $(obj).attr("checked");
            $("input[name='chk_ResourceTarget']:checked").each(function () {
                $("#" + this.id).attr("checked", 'false');
            })
            $(obj).attr("checked", check);
        }
    </script>

    </head>
<body>

    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: hidden; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="配置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 11%">资源名称 </th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_资源名称" type="text" class="box3" style="width: 80%;" />
                            </td>
                            <th style="width: 11%">关联参数关键字</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_资源说明信息" type="text" class="box3" />&nbsp;<a onclick="">选择</a>
                            </td>                        
                        </tr>
                        <tr>
                             <th>菜单是否启用 </th>
                             <td>
                                <input id="<%=ResID %>_是否启用" type="checkbox" />启用                                 
                            </td>
                             <th>是否默认菜单 </th>
                             <td>
                                <input id="<%=ResID %>_是否默认" type="checkbox" />是默认菜单
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 11%">资源链接路径 </th>
                            <td colspan="3">
                                <textarea id="<%=ResID %>_资源链接" class="box3" style="width: 98%; height: 100px;"></textarea>
                            </td>                          
                        </tr>
                        <tr>
                           <th>资源图标</th>
                           <td colspan="3">
                                <div id="GetHeadPortraitButton" style="height:60px;margin-top:8px; width:200px; text-align:left;margin-left:30px;">
                                    <input id="<%=ResID %>_资源图标" name="<%=ResID %>_资源图标" type="hidden" noxh="NoXH" />
                                    <img src="TXSC.png" id="HeadIMG" style="height:100%;" />
                                </div>
                               <span style="float:right;color:#ff6a00; margin-top:-30px; " >门户一级菜单，需上传图标，其余子菜单，不用上传！</span>
                            </td>
                        </tr>
                        <tr>
                            <th>资源链接打开方式  </th>
                            <td colspan="3">&nbsp;
                                <input type="checkbox" name="chk_ResourceTarget" value="_blank" id="chk_blank" onclick='ClickCheckBox1(this)'/>_blank&nbsp;&nbsp;
                                <input type="checkbox" name="chk_ResourceTarget" value="_parent" id="chk_parent" onclick='ClickCheckBox1(this)' />_parent&nbsp;&nbsp;
                                <input type="checkbox" name="chk_ResourceTarget" value="_search" id="chk_search" onclick="ClickCheckBox1(this)" />_search&nbsp;&nbsp;
                                <input type="checkbox" name="chk_ResourceTarget" value="_self" id="chk_self" onclick="ClickCheckBox1(this)" />_self&nbsp;&nbsp;
                                <input type="checkbox" name="chk_ResourceTarget" value="_top" id="chk_top" onclick="ClickCheckBox1(this)" />_top&nbsp;&nbsp;
                                <input type="text" id="ch_txt" style="width: 60px;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="ChooseTYWindow" style="overflow-x: hidden; overflow: scroll; position: relative; display: none;"></div>
        <input type="hidden" value="" id="DictionaryConditon" />
    </form>
</body>
</html>
<script type="text/javascript">
    $("#div<%=ResID %>FormTable").css("height", document.documentElement.clientHeight);
</script>
