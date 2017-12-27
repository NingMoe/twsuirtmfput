<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeniorSearchForm.aspx.cs" Inherits="Base_CommonPage_SeniorSearchForm" %>

<!DOCTYPE html>

<html>
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title></title>
    <%=this.GetScript1_4_3  %>
    <style type="text/css">
        label {
            width: 50px;
        }
    </style>

    <script type="text/javascript">

        var CommonPath = GetApplicationPath() + "Base/";
        var parentIndex = 0
        $(document).ready(function () {

            for (var u = 0; u < parent.window.length; u++) {
                if (parent.window[u].CommonSearchFieldStr != undefined) {
                    parentIndex = u;
                }
            }

            $('input[name=OP]').combobox({
                data: parent.window[parentIndex].GerOP("", '1'),
                valueField: 'id',
                textField: 'text',
                editable: false,
                height: 30,
                width: 100,
                value: "包含",
                prompt: "请选择链接符",
                onSelect: function (rec) {
                    ChangeStr()
                }
            })

            $('input[name=SFV]').textbox({
                prompt:'请输入关键字',
                required:false,
                height:30,
                onChange: function (newValue, oldValue) {
                    ChangeStr()
                }
            })

            $('input[name=SFV_S]').textbox({
                prompt: '请输入数字',
                required: false,
                height: 30,
                onChange: function (newValue, oldValue) {
                    ChangeStr()
                }
            })

            
            $('input[name=OP_Number]').combobox({
                data: parent.window[parentIndex].GerOP("Number", '1'),
                valueField: 'id',
                textField: 'text',
                editable: false,
                height: 30,
                width: 100,
                value: "等于",
                prompt: "请选择链接符",
                onSelect: function (rec) {
                    ChangeStr()
                }
            })

            $('input[name=OP_Date]').combobox({
                data: parent.window[parentIndex].GerOP("Date",'1'),
                valueField: 'id',
                textField: 'text',
                editable: false,
                height: 30,
                width: 100,
                value: "日期区间",
                prompt: "请选择链接符",
                onSelect: function (rec) {
                    ChangeOP_Date($(this)[0].id, rec.text)
                    ChangeStr()
                }
            })


            $('input[name=SF1]').datebox({
                prompt: "开始日期",
                height: 30,
                width: 80,
                //buttonText: 'Search',
                //value: parent.window[parentIndex].GetTodayStr(),
                onSelect: function (date) {
                    ChangeStr()
                },
                onClickButton: function (index) {
                    
                }
            })
           
            $('input[name=SF2]').datebox({
                prompt: "结束日期",
               // buttonText: 'Search',
                height: 30,
                width:80,
                //value: parent.window[parentIndex].GetTodayStr(),
                onSelect: function (date) {
                    ChangeStr()
                },
                onClickButton: function (index) {
                   
                }
            });

            if ('<%=HideQueryBox%>' == "") {
                $('#SearchStr').textbox({
                    iconAlign: 'left',
                    height: 50,
                    width: 600,
                    editable: false,
                    readonly: true,
                    multiline: true,
                    prompt: '即将获取您的查询条件',
                    onClickIcon: function (index) {
                    },
                    onClickButton: function (index) {
                    }
                })
                $('#SearchStr').next().css("margin-left", "20px")
            }
            else
            {
                $('#SearchStr').css("display", "none");
                $('#ClearBin').css("margin-left", "300px");
                $('#SearchBin').css("margin-right", "300px");
            }
           
        });

        function ChangeOP_Date(oid, t) {
            var f = oid.split('_')[1];
           
            if (t == "日期区间") {
                $('#SF2_4_' + f).next().show()
                $('#SF1_4_' + f).datebox({
                    prompt: "开始日期",
                    buttonText: '',
                    width: 80
                })
            }
            else {
                $('#SF2_4_' + f).next().hide()
                $('#SF1_4_' + f).datebox({
                    prompt: "指定日期",
                    buttonText: '',
                    width: 165
                })
            }
        }

        function SetSearchStr() {
            ChangeStr();
            //parent.SeniorSearchStr = $('#SearchStr').textbox("getValue");
            parent.window[parentIndex].StartSeniorSearch(parent.SeniorSearchStr)
            parent.ParentCloseWindow(); 
        }

        function ClearSearchStr()
        {
            $('input:hidden').each(function () {
                if ($(this)[0].id.indexOf("SF_") > -1)
                {
                    $(this).textbox('reset');
                }
                else if ($(this)[0].id.indexOf("SF1_") > -1) {
                    $(this).datebox('reset');
                }
                else if ($(this)[0].id.indexOf("SF2_") > -1) {
                    $(this).datebox('reset');
                }
            })

            parent.SeniorSearchStr = "";
            if ('<%=HideQueryBox%>' == "") {
                $('#SearchStr').textbox('reset');
            }
        }


        function ChangeStr()
        {
            var Str=""
            $(".am-form-group .SFSF").each(function (i, b) {
                var t = b.id.split('_')[1];
                var f = b.id.split('_')[2];
                var op = " like "

                if (f.indexOf(")") > -1) {
                    f = f.replace(/\(/g, '').replace(/\)/g, '')
                    //alert(f)
                }
                else {
                    //op = $('#OP_' + f).combo('getValue');
                }

                if (t == 4) {

                    var StartDay = $('#SF1_' + t + "_" + f).datebox('getValue');
                    var EndDay = $('#SF2_' + t + "_" + f).datebox('getValue');

                    if (op == 'Between...And...') {
                        if (StartDay != "" && EndDay != "") {
                            if (Str != "") Str += " and "
                            Str += " ( " + f + " between '" + StartDay + "' and '" + EndDay + "' ) ";
                        }

                    }
                    else {
                        if (StartDay != "") {
                            if (Str != "") Str += " and "
                            Str += f + " " + op + "  '" + StartDay + "' "
                        }
                    }
                }
                else if (b.value != "") {
                    if (Str != "") Str += " and "
                    if (op == 'like') {
                        Str += f + " " + op + "  '%" + b.value + "%' "
                    }
                    else {
                        Str += f + " " + op + "  '" + b.value + "' "
                    }
                }
            })
            if (Str != "") {
                Str = " and " + Str
                parent.SeniorSearchStr = Str;
                if ('<%=HideQueryBox%>' == "") {
                    $('#SearchStr').textbox("setValue", Str)
                }
            }
        }
    </script>


</head>
<body>
    <div style="width: 100%; margin-left: auto; margin-right: auto; height: 100%">
        <div class="easyui-panel" title="<%="【" + SearchTitle + "】检索字段" %>" style="width: 100%; height: 350px">
            <div style="padding: 10px 20px; margin-left: auto; margin-right: auto; height: 350px">
                <form action="" class="am-form" id="SeniorSearch_Form" style="100%">

                    <%
                        int N = 1;
                        string StyleStr = "style='width:50%;display: inline'";
                        foreach (ReadDataColumnSet vf in CommonSearchField)
                        {
                    %>

                    <%
                        if (N % 2 == 1)
                        {
                    %>
                    <div class="form-group" style="width: 100%; margin-top: 5px">
                        <span class="am-form-group" style="width: 50%; margin-left: 5px">
                            <%
                                }
                                else
                                { %>
                            <span class="am-form-group" style="width: 50%; margin-left: 20px">
                                <%
                                    }

                                    if (vf.FieldType == 4)
                                    {
                                %>
                                <span style="width: 100px; display: inline-block">
                                    <label for="<%= "SF_" + vf.FieldName %>" style="margin-left: 20px;"><%= vf.FieldName %></label></span>
                                <input id="<%= "OP_" + vf.FieldName %>" name="OP_Date">
                                <input   data-options="prompt:'请输入关键字',required:false,height:30" id="<%= "SF1_" + vf.FieldType + "_" + vf.FieldName %>" class="SFSF" name="SF1" style="display: inline; margin-left: 20px" />
                                <input  data-options="prompt:'请输入关键字',required:false,height:30" name="SF2"  id="<%= "SF2_" + vf.FieldType + "_" + vf.FieldName %>" style=" display: inline; margin-left: 20px" />

                                <%
                                    }
                                    else if (vf.FieldType == 2 || vf.FieldType == 3)
                                    {
                                %>
                                <span style="width: 100px; display: inline-block">
                                    <label for="<%= "SF_" + vf.FieldName %>" style="margin-left: 20px;"><%= vf.FieldName %></label></span>
                                <input id="<%= "OP_" + vf.FieldName %>" name="OP_Number">
                               <input id="<%= "SF_" + vf.FieldType + "_" + vf.FieldName %>" name="SFV_S" class="SFSF" style="width: 165px; display: inline; margin-left: 20px"  >
                                <%
                                    }
                                    else
                                    {
                                %>
                                <span style="width: 100px; display: inline-block">
                                    <label for="<%= "SF_" + vf.FieldName %>" style="margin-left: 20px;"><%= vf.FieldName %></label></span>
                                <input id="<%= "OP_" + vf.FieldName %>" name="OP">
                                 <input id="<%= "SF_" + vf.FieldType + "_" + vf.FieldName %>" name="SFV" class="SFSF"  style="width: 165px; display: inline; margin-left: 20px"  >

                               <%-- <input class="easyui-textbox" data-options="prompt:'请输入关键字',required:false,height:30" id="<%= "SF_" + vf.FieldType + "_" + vf.FieldName %>" name="SFV" style="width: 150px; display: inline; margin-left: 20px" onchange="ChangeStr()" />--%>
                                <%
                                    }
                                %>
                            </span>
                            <%

                                if (N % 2 == 0)
                                {
                            %>
                    </div>
                    <%
                        }
                        N++; %>

                    <%
                        }
                    %>
                    <br />
                    <br />

                </form>
            </div>
        </div>

    </div>
 
    <div style="margin-left: auto; margin-right: auto; margin-top:10px">
        <div>
        <a href="#" id="ClearBin" class="easyui-linkbutton" data-options="iconCls:'icon-large-claer',size:'large',iconAlign:'top'" onclick="ClearSearchStr()" style="margin-left:50px;" >清空输入</a>
          <input id="SearchStr" style="margin-left:20px" >
          <a href="#" id="SearchBin" class="easyui-linkbutton" data-options="iconCls:'icon-large-search',size:'large',iconAlign:'top'" style="margin-right:50px;float:right" onclick="SetSearchStr()">开始查询</a></div>
        
    </div>

</body>
</html>
