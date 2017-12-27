<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInfo.aspx.cs" Inherits="Base_PrintInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>条码打印</title>
    <%=this.GetScript1_4_3%>
    <script src="../../Scripts/jquery-barcode.js" type="text/javascript"></script>
</head>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var Piece="<%=Piece %>";
        var FormNumber="<%=FormNumber %>";
        for (var i =0 ; i < Piece; i++) {
            if (i == Piece-1) {
                $("#allCode").append("<div id='bcTarget" + i + "'  class='barcodeImg'></div>")
            } else {
                $("#allCode").append("<div style='page-break-after:always;'><div id='bcTarget" + i + "'  class='barcodeImg'></div></div>")
            }
            $("#bcTarget"+i).empty().barcode(FormNumber + "_" + (i + 1), "code128", { barWidth:1.5, barHeight: 60, showHRI: true });
        }
    });
</script>
<body>
    <form id="form1" runat="server">
        <div id="allCode"></div>	
    </form>
</body>
</html>
