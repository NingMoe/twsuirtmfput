//屏蔽页面中不可编辑的文本框中的backspace按钮事件  
function keydown(e) {  
  var isie = (document.all) ? true : false;  
  var key;  
  var ev;  
  if (isie){ //IE和谷歌浏览器  
       key = window.event.keyCode;  
       ev = window.event;  
   } else {//火狐浏览器  
       key = e.which;  
       ev = e;  
   }  

   if (key == 8) {//IE和谷歌浏览器  
       if (isie) {  
           if (document.activeElement.readOnly == undefined || document.activeElement.readOnly == true) {  
               return event.returnValue = false;  
           }   
       } else {//火狐浏览器  
          if (document.activeElement.readOnly == undefined || document.activeElement.readOnly == true) {  
               ev.which = 0;  
               ev.preventDefault();  
           }  
       }  
   }  
}
 function SetBackgroundColor(){
       var inputObj = document.getElementsByTagName("input");
       for (i=0;i<inputObj.length;i++){

           if (inputObj[i].readOnly) {
               inputObj[i].style.backgroundColor = "#dcdcdc";
           } else {
               inputObj[i].style.backgroundColor = "";
           }
       }
       var textareaObj = document.getElementsByTagName("textarea");
       for (i=0;i<textareaObj.length;i++){
           
           if(textareaObj[i].readOnly)
           {
               textareaObj[i].style.backgroundColor = "#dcdcdc";
          } else {
              textareaObj[i].style.backgroundColor = "";
          }
       }
//       var selectObj = document.getElementsByTagName("select");
//       for (i=0;i<selectObj.length;i++){
//           if(selectObj[i].disable)
//           {
//              selectObj[i].style.backgroundColor="#dcdcdc";
//           }
//       }   
   }
   document.onkeydown = keydown;


   //这个方法是把JS里面的对象转成字符串的方法
   function Obj2str(o) {
       if (o == undefined) {
           return '';
       }
       var r = [];
       if (typeof o == "string")
           return o.replace(/([\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t");
       if (typeof o == "object") {
           if (!o.sort) {
               for (var i in o) {
                   r.push("\'" + i + "\':'" + Obj2str(o[i]) + "'");
               }
               if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                   r.push("toString:" + o.toString.toString());
               }
               r = "{" + r.join() + "}"
           } else {
               for (var j = 0; j < o.length; j++) {
                   r.push(Obj2str(o[j]))
               }
               r = "[" + r.join() + "]";
           }
           return r;
       }
       return o.toString().replace(/\"\:/g, "':''");
   }

   function GetFromJsonByDiv(ResID, Div) {

       if (Div == "")
           return "";

       if ($("#" + Div).length == 0)
           return "";
       var json = "";
       var fieldName = "";
       var fieldValue = "";
       for (var i = 0; i < $("#" + Div).find("input").length; i++) {
           if ($("#" + Div).find("input")[i].id.indexOf(ResID + "_") == 0) {
               var id = $("#" + Div).find("input")[i].id;
               if ($("#" + Div).find("input")[i].type == "checkbox") {
                   if ($("#" + id).is(':checked')) {
                       fieldValue = 1;
                   }
                   else fieldValue = 0;
               }
               else {
                   fieldValue = $("#" + id).val();
               }
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       for (var j = 0; j < $("#" + Div).find("textarea").length; j++) {
           if ($("#" + Div).find("textarea")[j].id.indexOf(ResID + "_") == 0) {
               var id = $("#" + Div).find("textarea")[j].id;
               fieldValue = $("#" + id).val();
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       for (var k = 0; k < $("#" + Div).find("select").length; k++) {
           if ($("#" + Div).find("select")[k].id.indexOf(ResID + "_") == 0) {
               var id = $("#" + Div).find("select")[k].id;
               fieldValue = $("#" + id).val();
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       return json;
   }


   function GetFromJson(ResID) { 
       var json="";
       var fieldName="";
       var fieldValue="";
       for (var i = 0; i < $("input").length; i++) {
           if ($("input")[i].id.indexOf(ResID + "_") == 0) {
               var id = $("input")[i].id;
               if ($("input")[i].type == "checkbox") {
                   if ($("#" + id).is(':checked')) {
                       fieldValue = 1;
                   }
                   else fieldValue = 0;
               }
               else {
                       fieldValue = $("#" + id).val();
               }
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       for (var j = 0; j < $("textarea").length; j++) {
           if ($("textarea")[j].id.indexOf(ResID+"_") == 0) {
               var id = $("textarea")[j].id;
               fieldValue = $("#" + id).val();
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       for (var k = 0; k < $("select").length; k++) {
           if ($("select")[k].id.indexOf(ResID + "_") == 0) 
           {
               var id = $("select")[k].id;
               fieldValue = $("#" + id).val();
               fieldName = id.replace(ResID + "_", "");
               json += "'" + fieldName + "':'" + fieldValue + "',";
           }
       }
       return json;
   }



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

   //JS去字符串前后空格方法
   function trimStr(str) {
       str = str.replace(/^(\s|\u00A0)+/, '');
       for (var i = str.length - 1; i >= 0; i--) {
           if (/\S/.test(str.charAt(i))) {
               str = str.substring(0, i + 1);
               break;
           }
       }
       return str;
   }



