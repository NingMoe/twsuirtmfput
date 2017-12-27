function AddBeizhu()
{
    var rows = ChildGrid.datagrid('getSelections');
    if (rows.length > 0) {
        openDialog(CommonPath + "HR/AddOrEditorKQBZ.aspx?ygzh=" + rows[0].账号 + "&ygxm=" + rows[0].姓名 + "&bzrq=" + rows[0].遍历日期 + "&OpenDiv=divReportPage&gridID=" + ChildGridID , "修改备注", 500, 250)
    }
    else
    {
        alert("请选择一条记录！")
    }
}


function BatchAddJBJL() {
     
    //alert(SelectYear)
    //alert(SelectMonth)
      
    var rows = ChildGrid.datagrid('getRows');
    if (rows.length > 0) {
        
        var SelectUserID = rows[0].账号
        alert(SelectUserID)
        openDialog(CommonPath + "HR/BatchAddJBJL.aspx?SelectYear=" + SelectYear + "&SelectMonth=" + SelectMonth + "&SelectUserID=" + SelectUserID + "&OpenDiv=divReportPage&gridID=" + ChildGridID, "批量补加班记录", $(window).width() * 0.9, 400)
    }
    else {
        alert("请选择一条记录！")
    }
     
}


function EditJBJL() {
    var rows = ChildGrid.datagrid('getSelections');
    if (rows.length > 0) {
        openDialog(CommonPath + "HR/AddOrEditorJBXX.aspx?RecID=" + rows[0].ID + "&OpenDiv=divReportPage&gridID=" + ChildGridID, "修改加班记录", 700, 400)
    }
    else {
        alert("请选择一条记录！")
    }

}


function QXFFJJ() {
    var checkedObj = CenterGrid.datagrid('getSelections');
    if (checkedObj.length > 0) {
        var SaveRecIDStr = "";
        var SaveJson = "";
        for (var i = 0; i < checkedObj.length; i++) {
            if (SaveRecIDStr != "") {
                SaveRecIDStr += ","
            }
            SaveRecIDStr +=  checkedObj[i].ID;
            SaveJson += "[{";
            SaveJson += "'" + "奖金发放状态" + "':'" + "" + "',";
            SaveJson += "'" + "奖金发放时间" + "':'" + "" + "'";
            SaveJson += "}],";
        }
         
        BatchSaveWDInfo("439332828407", SaveRecIDStr, SaveJson, "取消发放成功！", _UserID, "", "", _GridID.replace("#", ""))
      
    }
    else {
        alert("至少选择一条记录！")
    }

}
