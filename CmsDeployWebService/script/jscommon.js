//open a new window
//lsh
function MM_openBrWindow(theURL,winName,features,top,left) { //v2.0 1
if (document.all)
var xMax = screen.width, yMax = screen.height;
else
if (document.layers)
var xMax = window.outerWidth, yMax = window.outerHeight;
else
var xMax = 640, yMax=480;
if(!top)
	top = 300;
if(!left)
	left = 480;
var xOffset = (xMax - left)/2, yOffset = (yMax - top)/2;
  window.open(theURL,winName,features+',screenX='+xOffset+',screenY='+yOffset+', top='+yOffset+',left='+xOffset+"'");
}

function strLength(str)
	//判断字符串长度
	{
		var l=str.length;
		var n=l;
		for (var i=0;i<l;i++)
		{
			if (str.charCodeAt(i)<0||str.charCodeAt(i)>255) n++;
		}
		return n ;
	}

function PopupDialog(ctrlobjs,Url,dialogArguments,width,height)
{
	if (width) {
	} else {
		//width = "280px";
		width = "290px";
	}
	if (height) {
	} else {
		//height = "175px";
		height = "193px";
	}
	//弹出一个小小的窗口
	//lsh
	var len = ctrlobjs.length;
	var ctrlobj;
	if(len == null)
		ctrlobj = ctrlobjs;
	else
		ctrlobj = ctrlobjs[0];
	if(ctrlobj.readOnly || ctrlobj.disabled)
		return false;
	if(dialogArguments==null)
		dialogArguments = "";
	showx = event.screenX - event.offsetX - 4 - 210 ; // + deltaX;
	showy = event.screenY - event.offsetY + 18; // + deltaY;
	newWINwidth = 210 + 4 + 18;
	var retval = window.showModalDialog(Url, dialogArguments, "dialogWidth:" + width + "; dialogHeight:" + height + "; dialogLeft:"+showx+"px; dialogTop:"+showy+"px; status:no; directories:yes;scrollbars:no;Resizable=no; "  );
	if( retval != null ){
//		alert(ctrlobj.tagName);
		if(len == null)
			ctrlobj.value = retval;
		else
			{
				for(var i = 0;i<len;i++)
					ctrlobjs[i].value = retval;
			}
	}else{
		//alert("canceled");
	}
}

//根据ID获取Value
//Note:不能用于Select
//lsh
function getValueByID(id)
{
	id = ""+id;
//	alert(id);
	var IDs = document.all.item(id);
	if(IDs==null)
		return "";
	var length = IDs.length;
	if(length==null)
		return IDs.value;
	else
		return IDs(0).value;
}

//根据ID设置Value
//Note:不能用于Select
//lsh
function setValueByID(id)
{
	var IDs = document.all.items("'"+id+"'");
	if(IDs==null)
		return "";
	var length = IDs.length;
	if(length==null)
		return IDs.value;
	else
		return IDs[0].value;
}

//设置初始值
//Note:不能用于Select
//Author:LSH liyonghai@163.net
	function setDefaultValue(id,value){
		//alert(id);
		var src=document.all.item(id);
		if(src==null){
			return false;
		}
		var length = src.length;
		if(length!=null)
		{
			for(var i = 0;i<length;i++)
			{
				src[i].value = value;
			}
			return;
		}
		else
		{
			src.value=value;
			return;
		}
	}
	

//Javascript里面去掉字符串两边的空格
String.prototype.Trim = function() 
{ 
return this.replace(/(^\s*)|(\s*$)/g, ""); 
} 
String.prototype.LTrim = function() 
{ 
return this.replace(/(^\s*)/g, ""); 
} 
String.prototype.RTrim = function() 
{ 
return this.replace(/(\s*$)/g, ""); 
} 
//***************

//获取URL中的参数值
function getUrlParam(paramName){
	var hrefstr = window.location.href;
	var pos = hrefstr.indexOf("?")
	var parastr = hrefstr.substring(pos+1); //获取URL后的参数字符串

	var para = parastr.split("&");
	var tempstr="";
	for(i=0;i<para.length;i++){
		tempstr = para[i];
		pos = tempstr.indexOf("=");
		if(tempstr.substring(0,pos) == paramName){
			return tempstr.substring(pos+1);
		}
	}
	return "";
}

