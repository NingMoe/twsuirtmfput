<%@ Page Language="VB" AutoEventWireup="false" CodeFile="field.aspx.vb" Inherits="exdtc_field" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
    <script language="javascript">
    function addrelation()
    {
        var elfrom = document.getElementById("drpDataFrom");
        var elto = document.getElementById("drpDataTo");
        var el = document.getElementById("fieldrelation");
        var item = document.createElement("OPTION");
        item.text = elfrom.options[elfrom.selectedIndex].text + " -> " + elto.options[elto.selectedIndex].text;
        item.value = elfrom.options[elfrom.selectedIndex].value + " -> " + elto.options[elto.selectedIndex].value;
        el.add(item); 
    }
    function removerelation()
    {
        var el = document.getElementById("fieldrelation");
        if (el.selectedIndex>=0) el.remove(el.selectedIndex);
    }
    function submitrelation()
    {
        var relation = "";
        var relation1 = "";
        var el = document.getElementById("fieldrelation");
        for (var i=0;i<el.options.length;i++)
        {
            relation = relation + el.options[i].value + "|";
        }
        document.getElementById("relation").value = relation;
        
        for (var i=0;i<el.options.length;i++)
        {
            relation1 = relation1 + el.options[i].text + "|";
        }
        document.getElementById("relation1").value = relation1;
        form1.submit();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <asp:DropDownList ID="drpDataFrom" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="drpDataTo" runat="server"></asp:DropDownList>
        <input type="button" name="add" value="添加到列表"  onclick="addrelation();"/>
        <input type="button" name="ok" value="确定" onclick="submitrelation();" id="Button1"/>
        <input type="button" name="cancel" value="取消" />
        <input type="hidden" name="relation" id="relation" />
        <input type="hidden" name="relation1" id="relation1" />
    </div>
    
    <div style="overflow-y:auto;height:400px;margin-top:2px;border:0px solid;">
        <asp:ListBox style="width:300px;height:400px;border:1px solid #fff000;" id="fieldrelation" runat="server" ondblclick="removerelation()"></asp:ListBox>
    </div>
    </form>
</body>
</html>
