/************************************ class calendar ************************************/
//类：日历
//事例 var oDate = new calendar("oDate","2002-1-1 23:59:59")
var m_visibleCalendar;
function calendar(id,initDate,width,format) {
	if (width) {
	} else {
		width = "116px";
	}
	switch (format) {
		case "yyyy-MM-dd hh:mm":
			break;
		case "yyyy-MM-dd":
			break;
		default:
			format = "yyyy-MM-dd hh:mm:ss";
	}
	this.format = format;
	this.id = id;
	var today = new Date();
	var year;
	var month;
	var date;

	this.Date = myDate_ParseDate(initDate);
	var months = Array("一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月");
	var weekdays = Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
//style	
	document.write("<style type='text/css'>")
	document.write(".Calendar_Day_Selected {font-size:9pt;cursor:hand;vertical-align:center;background:white;text-align:center;background-image:url(../images/common/label38x14.gif);}")
	document.write(".Calendar_Day_Today {font-size:9pt;cursor:hand;vertical-align:center;text-align:center;background-image:url(../images/common/label38x14_2.gif);background-repeat:no-repeat;}")
	document.write(".Calendar_Day_Holiday {font-size:9pt;cursor:hand;background:red;vertical-align:center;text-align:center}")
	document.write(".Calendar_Day_Normal {font-size:9pt;cursor:hand;vertical-align:center;text-align:center}")
	document.write(".Calendar_Day_Other {font-size:9pt;cursor:hand;vertical-align:center;text-align:center;text:#c0c0c0}")
	document.write(".Calendar_Text {font-size:10pt;background-color:#c0c0c0;vertical-align:center;text:#ffffff;text-align:center}")
	document.write("</style>")
//时间输入框
	document.write("<table cellspacing=0 cellpadding=0 border=0 id=" + id + " style='layer-out:fixed'>")
//

	document.write("<input id=txtDate type=hidden style='border:0;z-index:0;font-size:9pt;width:" + width + ";' onkeydown='" + id + ".entDateArea();' onclick='" + id + ".selDateArea()' onblur='" + id + ".setDateArea()'>")

//下拉式时间选单	
	document.write("<tr><td id=tdCalendar style='display:none;'>")
	document.write("<table onclick='event.cancelBubble=true;' cellspacing=0 cellpadding=0 border=0 align=center width=268px height=135px style='position:absolute;table-layout:fixed;border-left:1 inset;border-top:1 inset;border-bottom:1 inset black;border-right:1 inset black;' bgcolor=white>")
	document.write("<tr height=24px bgcolor=#e0e0e0>")
	document.write("<td width=134px>")
	document.write("<select id=selMonth onchange='" + id + ".setMonth();' style='width:100%'>")
	for (var i=0; i<12; i++) {
		document.write("<option>" + months[i] + "</option>")
	}
	document.write("</select>")
	document.write("</td>")
	document.write("<td width=120px align=left><input maxlength=4 border=0 id=txtYear disabled=true style='width:100%'></td>")
	document.write("<td>")
	document.write("<table height=100% cellspacing=0 cellpadding=0 border=0 align=center>")
	document.write("<tr><td><input type='button' style='font-size:6.5pt;height:10px;width:11px;background-image:url(../images/cb/arrow_up11x9.gif); background-repeat: no-repeat; background-position: center center;' onclick='" + id + ".setYear(1);this.blur();'></td></tr>")
	document.write("<tr><td></td></tr>")
	document.write("<tr><td><input type='button' style='font-size:6.5pt;height:10px;width:11px;background-image:url(../images/cb/arrow_down11x9.gif); background-repeat: no-repeat; background-position: center center;' onclick='" + id + ".setYear(-1);this.blur();'></td></tr>")
	document.write("</table>")
	document.write("</td></tr>")
	document.write("<tr valign=top>")
	document.write("<td colspan=3>")
	document.write("<table width=100% id=monthlyCalendar cellspacing=0 cellpadding=0 border=0 align=center>")
	document.write("<tr style='font-family:宋体;background-color:#ffffff;color:#d0d0d0'>");
	for (var i=0;i<7;i++) {
		document.write("<td width=38 style='font-size:9pt;border-top:2px solid white;border-bottom:1px solid black'>" + weekdays[i] + "</td>")
	}
	document.write("</tr>")
	for (var i=0;i<6;i++) {
		document.write("<tr height=12px width=100%>");
		for (var j=0;j<7;j++) {
			document.write("<td onclick=" + id + ".setDay(this.innerText)></td>");
		}
		document.write("</tr>");
	}
	document.write("<tr><td colspan=7 onclick='" + id + ".setDay(new Date());' style='font-size:9pt;cursor:hand;font-weight:bold;'><img height=14px src=../images/label38x14_2.gif>")
	document.write("<span id=spnToday>")
	document.write("&nbsp;&nbsp;今天：" + today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate())
	document.write("</span></td></tr><tr><td colspan=7></td></tr></table></td></tr></table></td></tr></table>")

	this.getValue = calendar_getValue;
	this.drawMonthlyCalendar = calendar_drawMonthlyCalendar;
	this.selDay = calendar_selDay;
	this.setDateArea = calendar_setDateArea;
	this.entDateArea = calendar_entDateArea;
	this.selDateArea = calendar_selDateArea;
	this.showCalendar = calendar_showCalendar;
	this.hiddenCalendar = calendar_hiddenCalendar;
	this.setDay = calendar_setDay;
	this.setMonth = calendar_setMonth;
	this.setYear = calendar_setYear;
	this.makeDate = calendar_makeDate;

	var rootNode = document.all(id);
	this.txtDate = rootNode.all("txtDate");
	this.tdCalendar = rootNode.all("tdCalendar");
	this.yearNode = rootNode.all("txtYear");
	this.monthNode = rootNode.all("selMonth");
	this.monthlyCalendar =  rootNode.all("monthlyCalendar");
	this.todayNode = rootNode.all("spnToday");
	this.txtDate.value = this.getValue();
	this.txtDate.blur();
}
function calendar_getValue() {
	return myDate_parseString(this.Date,this.format);
}
/*操作日历的时间栏*/
function calendar_entDateArea() {
	if (event) {
		event.returnValue = false;
		var keyCode = event.keyCode;
//keycode: 37.← 38.↑ 39.→ 40.↓		
		if ((keyCode>=37 && keyCode <41) || (keyCode>=48 && keyCode<58) || (keyCode>=96 && keyCode<106)) {
			if ((this.keyDownCount)>0) {
				var cursorPos = getCursorPosInTextRange(this.txtDate);
				var count;
				var startPos;
				var isSetDate = false;
				if (cursorPos<5) {
					startPos = 0;
					count = 4;
				} else if (cursorPos<8) {
					startPos = 5;
					count = 2;
				} else if (cursorPos<11) {
					startPos = 8;
					count = 2;
				} else if (cursorPos<14) {
					startPos = 11;
					count = 2;
				} else if (cursorPos<17) {
					startPos = 14;
					count = 2;
				} else if (cursorPos<20) {
					startPos = 17;
					count = 2;
				}
				var dateStr = this.txtDate.value;
				var selStr = dateStr.substr(startPos,count);
				if ((keyCode==37)) {//如果 37.←， 向前移位
					startPos = startPos - 3;
					if (startPos<5) {
						startPos = 0;
						count = 4;
					}
					this.makeDate(dateStr,false)
					this.keyDownCount = count;
				} else if (keyCode==38) {//如果 38.↑
					selStr = parseInt(selStr,10) + 1;
					selStr = "0000" + selStr;
					selStr = selStr.substr(selStr.length-count,count);
					dateStr = dateStr.substr(0,startPos) + selStr + dateStr.substr(startPos+count);
					this.keyDownCount = count;
					this.makeDate(dateStr,true)
				} else if (keyCode==39) {//如果 39.→，向后移位
					startPos = startPos + 3;
					if (startPos>2 && startPos<8) {
						startPos = 5;
						count = 2;
					} else if (startPos>this.format.length-2) {
						startPos = this.format.length-2;
					}
					this.makeDate(dateStr,false)
					this.keyDownCount = count;
				} else if (keyCode==40) {//如果 40.↓
					selStr = parseInt(selStr,10) - 1;
					if (startPos==11) {
						selStr = (24 + selStr) % 24;
					} else if (startPos==14 || startPos==17) {
						selStr = (60 + selStr) % 60;
					}
					selStr = "0000" + selStr;
					selStr = selStr.substr(selStr.length-count,count);
					dateStr = dateStr.substr(0,startPos) + selStr + dateStr.substr(startPos+count);
					this.keyDownCount = count;
					this.makeDate(dateStr,true)
				} else {//如果输入的是数字,重新构造字符串
					var num;
					if (keyCode>=48 && keyCode<58) {
						var num = keyCode - 48;
					} else if (keyCode>=96 && keyCode<106) {
						var num = keyCode - 96;
					}
					if (this.keyDownCount==count) {
						selStr = "0000";
					}
					selStr = selStr + num;
					selStr = selStr.substr(selStr.length-count,count);
					dateStr = dateStr.substr(0,startPos) + selStr + dateStr.substr(startPos+count);
					this.keyDownCount--;
					if (this.keyDownCount==0) {
						this.makeDate(dateStr,false);
						this.keyDownCount = count;
					} else {
						this.txtDate.value = dateStr;
					}
				}
				selCharsInTextRange(this.txtDate,startPos,count);
			}			
		}
	}
}
function calendar_selDateArea() {
	var cursorPos = getCursorPosInTextRange(this.txtDate);
	var count;
	var startPos;
	if (cursorPos<5) {
		startPos = 0;
		count = 4;
	} else if (cursorPos<8) {
		startPos = 5;
		count = 2;
	} else if (cursorPos<11) {
		startPos = 8;
		count = 2;
	} else if (cursorPos<14) {
		startPos = 11;
		count = 2;
	} else if (cursorPos<17) {
		startPos = 14;
		count = 2;
	} else if (cursorPos<20) {
		startPos = 17;
		count = 2;
	}
	
	if (this.keyDownCount!=0) {
		this.setDateArea();
	}
	this.keyDownCount = count;
	selCharsInTextRange(this.txtDate,startPos,count);
}
function calendar_setDateArea() {
	var date = this.makeDate(this.txtDate.value,false);
	if (date) {
		this.Date = date;
	}
	this.txtDate.innerText = this.getValue();
}
function calendar_showCalendar() {
	if (m_visibleCalendar) {
		m_visibleCalendar.hiddenCalendar();
	}
	this.monthNode.selectedIndex = this.Date.getMonth();
	this.yearNode.value = this.Date.getFullYear();
	this.drawMonthlyCalendar();
	this.selDay(this.Date.getDate());
	this.tdCalendar.style.display = "";
	m_visibleCalendar = this;
}
function calendar_hiddenCalendar() {
	return_Value();
}
function calendar_setDay(day){
	if (day) {
		if (typeof(day)=="object") {
			this.Date = day;
		} else {
			this.Date.setDate(parseInt(day,10));
		}
		this.txtDate.value = this.getValue();
		this.hiddenCalendar();
	}
}
function calendar_setMonth(){
	var dateStr = this.Date.getFullYear() + "-" + (this.monthNode.selectedIndex + 1) + "-" + this.Date.getDate() + " " + this.Date.getHours() + ":" + this.Date.getMinutes() + ":" + this.Date.getSeconds();
	this.makeDate(dateStr,true);
	this.drawMonthlyCalendar();
}
function calendar_setYear(increase) {
	var year = (parseInt(this.yearNode.value,10) + parseInt(increase,10)) % 10000;
	this.yearNode.value = year;
	var dateStr = year + "-" + (this.Date.getMonth() + 1) + "-" + this.Date.getDate() + " " + this.Date.getHours() + ":" + this.Date.getMinutes() + ":" + this.Date.getSeconds();
	this.makeDate(dateStr,true);
	this.drawMonthlyCalendar();
}
//标记选中的日期
function calendar_selDay(day) {
	if (!day || day=="") {
		day = 1;
	} else {
		day = parseInt(day,10);
	}
	var selCell = this.monthlyCalendar.p_SelCell;
	if (selCell) {
		selCell.className = "Calendar_Day_Normal";
	}
	var month = this.monthNode.selectedIndex;
	var year = parseInt(this.yearNode.value,10);
	var firstDate = new Date(year,month,1);
	var weekday = firstDate.getDay();
	var rowIndex = Math.floor((day + weekday - 1) / 7);
	var colIndex = ((day + weekday) - 1) % 7;
	selCell = this.monthlyCalendar.rows(rowIndex+1).cells(colIndex);
	selCell.className = "Calendar_Day_Selected";
	this.monthlyCalendar.p_SelCell =selCell;
}
//刷新月历
function calendar_drawMonthlyCalendar(day) {
//清空月历
	for (var i=1;i<7; i++) {
		for (var j=0; j<7; j++) {
			var selCell = this.monthlyCalendar.rows(i).cells(j);
			selCell.innerHTML = "";
			selCell.className = "";
		}
	}
//计算每月的天数
	var month = this.Date.getMonth();
	var year = this.Date.getFullYear();
	var firstDate = new Date(year,month,1);
	var weekday = firstDate.getDay();
	var lastDay = myDate_getLastDayInMonth(year,month);
//填写月历
	for (var i=1;i<=lastDay;i++) {
		var rowIndex = Math.floor((i + weekday - 1) / 7);
		var colIndex = ((i + weekday) - 1) % 7
		var selCell = this.monthlyCalendar.rows(rowIndex+1).cells(colIndex);
		selCell.innerText = i;
		selCell.className  = "Calendar_Day_Normal";
	}
	var lastColumn = Math.ceil((lastDay + weekday) / 7);
	if (lastColumn<5) {
		this.monthlyCalendar.rows(5).style.display = "none";
	} else {
		this.monthlyCalendar.rows(5).style.display = "";
	}
	if (lastColumn<6) {
		this.monthlyCalendar.rows(6).style.display = "none";
	} else {
		this.monthlyCalendar.rows(6).style.display = "";
	}
	var today = new Date();
	if (this.yearNode.value==today.getFullYear() && this.monthNode.selectedIndex==today.getMonth()) {
		var date = today.getDate();
		var rowIndex = Math.floor((date + weekday - 1) / 7);
		var colIndex = ((date + weekday) - 1) % 7;
		this.monthlyCalendar.rows(rowIndex+1).cells(colIndex).className = "Calendar_Day_Today";
	}
	if (day) {
		this.selDay(day);
	}
}
function calendar_makeDate(dateStr,increase) {
	var dates = dateStr.split(/\-|:|\s/);
	var year = parseInt(dates[0],10);
	var month = parseInt(dates[1],10)-1;
	var day = parseInt(dates[2],10);
	var hour = 0;
	var minute = 0;
	var second = 0;
	
	var lastDay = myDate_getLastDayInMonth(year,month);
	if (dates.length>3) {
		hour = parseInt(dates[3],10);
	}
	if (dates.length>4) {
		minute = parseInt(dates[4],10);
	}
	if (dates.length>5) {
		second = parseInt(dates[5],10);
	}
	if (year!=this.Date.getFullYear()) {
		if (increase) {
			year = year % 10000;
		}
		day = day<lastDay?day:lastDay;
	} else if (month!=this.Date.getMonth()) {
		if (increase) {
			month = (month+12) % 12;
		}
		day = day<lastDay?day:lastDay;
	} else if (day!=this.Date.getDate()) {
		if (increase) {
			day = day % lastDay;
			day = (day==0)?lastDay:day;
		}
	} else if (hour!=this.Date.getHours()) {
			hour = hour % 24;
	} else if (minute!=this.Date.getMinutes()) {
			minute = minute % 60;
	} else if (second!=this.Date.getSeconds()) {
			second = second % 60;
	}
	if (year>=0 && year<9999 && month>=0 && month<12 && day>0 && day<=lastDay) {
		this.Date = new Date(year,month,day,hour,minute,second);
	}
	this.txtDate.value = this.getValue();
}

/************************************ Date Function ************************************/
function myDate_ParseDate(dateStr) {
	var year;
	var month;
	var date;
	if (dateStr.match(/\d\d\d\d\/|-[01]*\d\/|-[0123]*\d/ig)==null) {
		return null;
	}
	var dates = dateStr.split(/\/|-|:|\s/);
	var year = parseInt(dates[0],10);
	if (year<=0 || year >10000) {
		return null;
	}
	var month = (parseInt(dates[1],10) + 11) % 12;
	var day = parseInt(dates[2],10);
	var lastDay = myDate_getLastDayInMonth(year,month);
	if (day<=0 || day >lastDay) {
		return null;
	}
	var hour = 0;
	var minute = 0;
	var second = 0;
	if (dates.length>3) {
		hour = parseInt(dates[3],10);
		if (hour>=24 || hour<0) {
			return null;
		}
	}
	if (dates.length>4) {
		minute = (parseInt(dates[4],10)+60) % 60;
		if (minute>=60 || hour<0) {
			return null;
		}
	}
	if (dates.length>5) {
		if (second>=60 || second<0) {
			return null;
		}
		second = (parseInt(dates[5],10)+60) % 60;
	}
	return new Date(year,month,day,hour,minute,second);
}

function myDate_parseString(date,format) {
	if (format) {
	} else {
		format = "yyyy-MM-dd hh:mm:ss";
	}
	if (typeof(date)!="string") {
		var dateStr="";
		dateStr = "000" + date.getFullYear() + "-";
		dateStr = dateStr.substr(dateStr.length-5);
		var tmp = date.getMonth()+1;
		if (tmp>9) {
			dateStr += tmp + "-" ;
		} else {
			dateStr += "0" + tmp + "-" ;
		}
		tmp = date.getDate();
		if (tmp>9) {
			dateStr += tmp;
		} else {
			dateStr += "0" + tmp;
		}
		if (format.length>10) {
			tmp = date.getHours();
			if (tmp>9) {
				dateStr += " " + tmp;
			} else {
				dateStr += " 0" + tmp;
			}
			tmp = date.getMinutes();
			if (tmp>9) {
				dateStr += ":" + tmp;
			} else {
				dateStr += ":" + "0" + tmp;
			}
		}
		if (format.length>16) {
			tmp = date.getSeconds();
			if (tmp>9) {
				dateStr += ":" + tmp;
			} else {
				dateStr += ":0" + tmp;
			}
		}
		return dateStr;
	} else {
		return "";
	}
}

function myDate_compareDates(date1,date2) {	
	if (typeof(date1)=="string") {
		date1 = myDate_ParseDate(date1);
	}
	if (typeof(date2)=="string") {
		date2 = myDate_ParseDate(date2);
	}
	date1 = date1.valueOf();
	date2 = date2.valueOf();
	if (date1>date2) {
		return 1;
	} else if (date1==date2) {
		return 0;
	} else {
		return -1;
	}
}
//获取某年某月的最后一天
function myDate_getLastDayInMonth(fullYear,month) {
	var lastDay;
	if (month==1) {
		if (fullYear % 4) {
			lastDay = 28;
		} else if (fullYear % 100) {
			lastDay = 29;
		} else if (fullYear % 400) {
			lastDay = 28;
		} else {
			lastDay = 29;
		}
	}
	else if (month==3 || month==5 || month==8 || month==10) {
		lastDay = 30;
	}
	else {
		lastDay = 31;
	}
	return lastDay;
}

//获取TextRange中的光标位置
function getCursorPosInTextRange(textArea)
{
	var currentRange=document.selection.createRange();
	var allRange=textArea.createTextRange();
	var len=0;
	while(currentRange.compareEndPoints("StartToStart",allRange)>0)
	{
	  currentRange.moveStart("character",-1);
	  len++;
	}
	return len;
}
//选定TextRange中指定的字符串
function selCharsInTextRange(textArea, startPosition, count)
{
	var txtRng = textArea.createTextRange();
	if (count) {
		txtRng.moveStart("character",startPosition);
		txtRng.moveEnd("character", startPosition + count - textArea.value.length - 1);
	} else {
		txtRng.move("character",startPosition);
	}
	txtRng.expand("character");
	txtRng.select(); 
}
