String.prototype.trim = function(){ 
	return this.replace(/(^\s*)|(\s*$)/g, ""); 
}

/*入口*/
function CheckValue(frm){	
	var o;
	var i;
	//判断所有的text框
	o=frm.tags("input");
	for (i=0;i<o.length;i++){
		if (CheckValueType(o[i])==false) return false;
	}
	//判断所有的textarea框
	o=frm.tags("textarea");
	for (i=0;i<o.length;i++)
	{
		if (CheckValueType(o[i])==false) return false;
	}
	//判断所有的select框
	o=frm.tags("select");
	for (i=0;i<o.length;i++){
		if (CheckOptionValueType(o[i])==false) return false;
	}
	return true;
} 

/********************************************/
function CheckValueType(o) {
    alert(o.mInput);
	//检查是否为必输项;
    if (o.mInput == "true") {
        alert(o.value.trim());
		if (o.value.trim()=="")
		{
			alert(o.FieldTitle+'不能为空！');
			o.focus();
			return false;
		}
	}
	//类型判断;
	switch (o.FieldType)
	{
		case "num":
			if (isNaN(o.value))
			{
				alert(o.FieldTitle+'必须为数字！');
				o.focus();
				o.select();
				return false;
			}
			break;
		case "username":
			if(!Verifyname(o.value)){
				alert(o.FieldTitle+'只能是字母，数字，－＿');
				o.focus();
				o.select();
				return false;
			}
			break;
		case "email":
			if (o.value!="") {
				var s=o.value
				var re=/^\b[\w\.-]+@[\w-]+\.(([A-Z]{2,6})|([A-Z]{2,6}\.([A-Z]{2,6}))|([A-Z]{2}\.cn))\b$/i
				if(!re.exec(s)){
					alert(o.FieldTitle+'格式不正确！');
					o.focus();
					o.select();
					return false;
				}	
			 }
			 break;
		case "date":
			if (o.value!="") {
				if (!isDate(o.value)){
					alert(o.FieldTitle+'必须为日期格式！\n 如:2004-12-12 或者 2004/12/12');
					o.focus();
					o.select();
					return false;
				}
			}
			break;
		case "time":
			if (o.value!="") {
				if (!isTime(o.value)){
					alert(o.FieldTitle+'必须为时间格式！\n 如:18:30:60 或者 8:25');
					o.focus();
					o.select();
					return false;
				}
			}
			break;
		case "datetime":
			if (o.value!="") {
				if (!isDateTime(o.value)){
					alert(o.FieldTitle+'必须为日期时间格式格式！\n 如:2004-12-12 或者 2004-12-12 15:30 或者 2004/12/12 15:30:28');
					o.focus();
					o.select();
					return false;
				}
			}
			break;
		case "ph":
	    if(o.value!="")
			{
			    var str=o.value
			    var re1 = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/
			    var re2=/^[1]+[3,5]+\d{9}?$/
			    if(re1.exec(str) || re2.exec(str))
			    {
			        break;	
			    }
			    else
			    {
			        alert(o.FieldTitle + "的格式不正确.ex:139********或021-58******或021-58******-分机号");
			        
					o.focus();
					o.select();
					return false;
			    }
			}
			break;
		default:
			break;
		
		}
	return true;
}

/********************************************/
function CheckOptionValueType(o){
    //选择项不能为空
	if (o.mInput=="true"){
		if (o.options(o.selectedIndex).text.trim()=="")
		{
			alert(o.FieldTitle+'不能为空！');
			o.focus();
			return false;
		}
	}
	return true;
}

/*输入的是不是字母*/
function IsAlpha(cCheck){
	return ((('a'<=cCheck) && (cCheck<='z')) || (('A'<=cCheck) && (cCheck<='Z')))
}

/*
是不是输入了正确的用户名
用户名的判断条件:只能是由 数字、字母、-、_组成的字符串。
对应fType:username
*/
function Verifyname(str){
	for (nIndex=0; nIndex<str.length; nIndex++){
		cCheck = str.charAt(nIndex);
		if (!(!isNaN(cCheck) || IsAlpha(cCheck) || cCheck=='-' || cCheck=='_' )){
			return false;
		}
	}
	return true;					
}

/*
日期判断
对应fType:date
*/
function isDate(strValue){
	try{
		strValue = strValue.trim();
		var pattern = /\d{4}-\d{1,2}-\d{1,2}|\d{4}\/\d{1,2}\/\d{1,2}/;	
		if( strValue.replace(pattern, "") != "" ) 
			return false;
		else {
			var parts = strValue.split(/-|\//);
			var y = parseFloat(parts[0]);
			var m = parseFloat(parts[1]);
			var d = parseFloat(parts[2]);
			
			switch(m) {
				case 1: case 3: case 5: case 7: case 8: case 10: case 12:
					return ( 0<d && d < 32 );
				case 4: case 6: case 9: case 11:
					return ( 0<d && d < 31 );
				case 2:
					if( isLeapYear(y) ) return ( 0<d && d<30 );
					else return ( 0<d && d<29 );
				default:
					return false;
			}
		}
	}catch(ex){
		return false;
	}
	
	return true;
}

/*
时间判断
对应fType:date
*/
function isTime(strValue) {
	strValue = strValue.trim();
	try{
		var pattern1 = /\d{1,2}:\d{1,2}:\d{1,2}/;
		var pattern2 = /\d{1,2}:\d{1,2}/;
		var pattern3 = /\d{1,2}/;
		if( strValue.replace(pattern1, "") != "" && strValue.replace(pattern2, "") != "" && strValue.replace(pattern3, "") != ""){
			return false;
		}else{
			var parts = strValue.split(/:/);
			if (parts.length == 1){
				var h = parseInt(parts[0]);
    			if (h<0 || h>23){
    				return false;
    			}
			}else if (parts.length == 2){
    			var h = parseInt(parts[0]);
    			var m = parseInt(parts[1]);
    			if (h<0 || h>23 || m<0 || m>=60){
    				return false;
    			}
			}else if (parts.length == 3){
    			var h = parseInt(parts[0]);
    			var m = parseInt(parts[1]);
    			var s = parseInt(parts[2]);
    			if (h<0 || h>23 || m<0 || m>=60 || s<0 || s>=60){
    				return false;
    			}
			}
		}
	}catch(ex){
		return false;
	}
	
	return true;
}

/*
日期和时间判断
对应fType:datetime
*/
function isDateTime(strValue) {
	try{
		strValue = strValue.trim();
		var pos = 0;
		try{
			pos = strValue.indexOf(' '); //  /*\s*/
		}catch(ex){
			//return false;
		}
		
		if (pos == -1){
			if (!isDate(strValue)){
				return false;
			}
		}else{
			var strDate = strValue.substring(0, pos);
			var strTime = strValue.substring(pos);
			strDate = strDate.trim();
			strTime = strTime.trim();
			if (!isDate(strDate)){
				return false;
			}
			if (!isTime(strTime)){
				return false;
			}
		}
	}catch(ex){
		return false;
	}
    
    return true;
}

function isLeapYear(iYear) {
	return !(iYear%4 != 0 || (iYear%100 == 0 && iYear%400 != 0));
}
