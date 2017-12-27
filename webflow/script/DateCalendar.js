/*------------------------------------------------------------------------
  日期控件
  function Cal_dropdown(edit,min,max)
    弹出日历，可不给参数min和max，参数edit必须有   

  function Cal_datevalid(edit,min,max)
    检查edit中值是否为大于等于min，小于等于max的有效日期格式字符串。
    是则返回 true，否则返回false
    可不给参数min和max(字符串格式)
    参数edit必须有，如果edit无，则必须是：edit为edit和img的父亲(如span、div)的第一个元素
    
*/

var Cal_popup = window.createPopup();
var Cal_edit;
var Cal_editdate = new Date();
var Cal_maxdate;
var Cal_mindate;

function Cal_clearTime(thedate)
{
  thedate.setHours(0);
  thedate.setMinutes(0);
  thedate.setSeconds(0);
  thedate.setMilliseconds(0);
}

var Cal_today = new Date();
Cal_clearTime(Cal_today);

//检查日期//
//editctrl：日期控件输入框
// false: 日期错误

function ChkCZDate(editctrl)
{
if(editctrl.value=='')
{ alert('请输入日期！')
  editctrl.select();
  editctrl.focus();
  return false;}
if(!Cal_datevalid(editctrl,'1910-1-1','3000-1-1'))
{
alert('日期格式不正确,日期有效范围为1910年到3000年');
editctrl.focus();
return false;
}
else
 return true;
}
 
 

 

function Cal_decDay(thedate,days)
{
  if (!days) days = 1;
  thedate.setTime(thedate - days*24*60*60*1000);
}

function Cal_incMonth(year,month)
{
  if (month == 11) {
    month = 0;
    year++;
  } else month++;
  Cal_writeHTML(year,month);
}

function Cal_decMonth(year,month)
{
  if (month == 0) {
    month = 11;
    year--;
  } else month--;
  Cal_writeHTML(year,month);
}

function Cal_decYear(year,month)
{
  Cal_writeHTML(year-1,month);
}

function Cal_incYear(year,month)
{
  Cal_writeHTML(year+1,month);
}

function Cal_writeHTML(theyear,themonth)
{
  var html=
    '<div id="Cal_div1" style="width:231px;FONT-SIZE:9pt;background-color:#fffef5;border:black 1px solid">'+
    '<TABLE style="border-bottom:black 1px solid;FONT-SIZE: 9pt;background-color:#0fa8c7;color:white;'+
    'padding-top:2px;font-weight:bold;text-align:center" '+
    'cellSpacing="0" cellPadding="0" width="100%" border="0">'+
    '<TR><TD style="cursor:hand" align="left" width=24 onmouseover="this.style.background=' +
    "'#ffb468';" + '"' + ' onmouseout="this.style.background=' + "'#0fa8c7';" + '"' +
    ' onclick="parent.Cal_decYear(' + theyear + ',' + themonth + ');" '+
    '><年</TD>'+
    '<TD style="cursor:hand" align="left" width=24 onmouseover="this.style.background=' +
    "'#ffb468';" + '"' + ' onmouseout="this.style.background=' + "'#0fa8c7';" + '"' +
    ' onclick="parent.Cal_decMonth(' + theyear + ',' + themonth + ');" '+
    '><月</TD>'+
    '<TD align="center">';
    
  html += theyear + '年' + (themonth + 1) + '月</TD>'+
    '<TD style="cursor:hand" align="right" width=24 onmouseover="this.style.background=' +
    "'#ffb468';" + '"' + ' onmouseout="this.style.background=' + "'#0fa8c7';" + '"' +
    ' onclick="parent.Cal_incMonth(' + theyear + ',' + themonth + ');" '+
    '>月></TD>'+
    '<TD style="cursor:hand" align="right" width=24 onmouseover="this.style.background=' +
    "'#ffb468';" + '"' + ' onmouseout="this.style.background=' + "'#0fa8c7';" + '"' +
    ' onclick="parent.Cal_incYear(' + theyear + ',' + themonth + ');" '+
    '>年></TD>';
    
  html +=
    '</TR></TABLE>'+
    '<TABLE style="FONT-SIZE: 9pt;font-weight:bold;text-align:center;border-bottom:black 1px solid" '+
    'cellSpacing="2" cellPadding="0" width="100%" border="0">'+
    '<TR><TD>日</TD><TD>一</TD><TD>二</TD><TD>三</TD><TD>四</TD><TD>五</TD><TD>六</TD>'+
    '</TR></table>'+
    '<TABLE style="FONT-SIZE: 9pt;text-align:center;cursor:hand" cellSpacing="2" '+
    'cellPadding="0" width="100%" border="0">';

  var day1 = new Date(theyear,themonth,1);
  if (day1.getDay()!=0)
  Cal_decDay(day1,day1.getDay()); // 日历开始日
  for (var i=1;i<=6;i++) {
    html += '<TR>';
    for (var j=1;j<=7;j++) {
      html += '<TD';
      if (day1.getTime()==Cal_today.getTime())
        html += ' style="color:blue"';
      else
      if (day1.getTime()==Cal_editdate.getTime())
        html += ' style="color:red"';
      else
      if (day1.getMonth() != themonth)
        html += ' style="color:#aaaaaa"';
      html += ' onmouseover="this.style.background=' +
              "'#ffb468';" + '"'+
              ' onmouseout="this.style.background=' +
              "'#fffef5';" + '"';
      html += ' onclick="parent.Cal_clickday('+day1.getTime() + ');"';
      html +='>' + day1.getDate() + '</TD>';
      Cal_decDay(day1,-1);
    }
    html += '</TR>';
    if (day1.getMonth() != themonth) break;
  }
    
  html +=
    '</TABLE>'+
    '<div style="border-top:black 1px solid;text-align:center;padding:2px">今天是 '+
    '<span style="color:blue;cursor:hand;text-decoration:underline" onclick="javascript:parent.Cal_clickday('+
    Cal_today.getTime() + ');">'+
    Cal_today.getFullYear() + '-' + (Cal_today.getMonth()+1) + '-' + Cal_today.getDate() +
    '</span></div>'+
    '</div>';
  Cal_popup.document.body.innerHTML = html;
  if (Cal_popup.isOpen) // 重新调整显示高度
    Cal_popup.show(0, Cal_edit.offsetHeight, 231, Cal_popup.document.all("Cal_div1").offsetHeight,Cal_edit);
}

// 字符串转换为日期
function Cal_strtodate(str)
{
  var date = Date.parse(str);
  if (isNaN(date)) {
    date = Date.parse(str.replace(/-/g,"/")); // 识别日期格式：YYYY-MM-DD
    if (isNaN(date)) date = 0;
  }
  return(date);
}

//返回日期间相差的天数
function Cal_DateDiff(Date1, Date2)
{
    return (Date2-Date1)/(24*60*60*1000);
}

//返回日期间相差的月数(最大误差小于一个月)
function Cal_MonthDiff(DateA, DateB)
{
    Date1=new Date();
    Date2=new Date();
    Date1.setTime(DateA);
    Date2.setTime(DateB);
    months = (Date2.getFullYear() - Date1.getFullYear()) * 12;
    addmonths = Date2.getMonth() - Date1.getMonth();
    months = months + addmonths;
    if(Date2.getDate() < Date1.getDate())
        months--;
    return months;
}

// 弹出日历，可不给参数min和max，参数edit必须有
function Cal_dropdown(edit,min,max) {
  if (!edit) {
    edit = window.event.srcElement.parentElement.children(0);
    if ((!edit.type) || (edit.type.toLowerCase() != "text")) return;
  }
  Cal_edit = edit;
  var date = Cal_strtodate(edit.value);
  if (date == 0) date = Cal_today.getTime();
  if (max) Cal_maxdate = Cal_strtodate(max);
  else Cal_maxdate=0;
  if (min) Cal_mindate = Cal_strtodate(min);
  else Cal_mindate = 0;
  Cal_editdate.setTime(date);
  Cal_writeHTML(Cal_editdate.getFullYear(),Cal_editdate.getMonth());
  Cal_popup.show(0, edit.offsetHeight, 231, 149,edit);
  Cal_popup.show(0, edit.offsetHeight, 231, Cal_popup.document.all("Cal_div1").offsetHeight,edit);
}

// 点击日期
function Cal_clickday(day,edit)
{
  if (Cal_maxdate != 0) day = Math.min(day,Cal_maxdate);
  day = Math.max(day,Cal_mindate);
  Cal_editdate.setTime(day);
  var im;
  var id;
  if ((Cal_editdate.getMonth()+1)<10)
  {
   im = Cal_editdate.getMonth()+1;
   im = '0' + im;
  }
  else
   im = Cal_editdate.getMonth()+1;
  if ((Cal_editdate.getDate())<10)
   id = "0"+Cal_editdate.getDate();
  else
   id = Cal_editdate.getDate();
  Cal_edit.value = Cal_editdate.getFullYear() + "-" + im + "-"
                   + id;
  Cal_popup.hide();
  Cal_edit.fireEvent("onkeydown");
  Cal_edit.focus();
}

function Cal_datevalid(edit,min,max)
{
  // 检查edit中值是否为大于等于min，小于等于max的有效日期格式字符串。
 
  var date = Cal_strtodate(edit.value);
  if (date == 0) return false;
  if (max) {
    var max = Cal_strtodate(max);
    if ((max!=0)&&(date>max)) return false;
  }
  if (min) {
    var min = Cal_strtodate(min);
    if ((min!=0)&&(date<min)) return false;
  }
  date = new Date(date);
  edit.value = date.getFullYear() + "-" + (date.getMonth()+1) +
               "-" + date.getDate();
  return true;
}