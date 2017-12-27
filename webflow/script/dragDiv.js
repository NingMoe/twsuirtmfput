/* *******************************************************************
http://www.unionsoft.cn/
TEL：021-64739258
FAX：021-54653583
ADD：Room915A Jindu Building,NO277 Wuxing Road,Shanghai Chian
******************************************************************* */

/*CSS*/
document.write("<style>");
document.write(".mesWindow{border:#666 0px solid;background:#fff;border:0px solid #C4D9F9;}");
document.write(".mesWindowTop{margin:5px;font-size:15px;text-align:right;vertical-align:middle;height:20px;background-image:url(../images/bg0_06.jpg);}");
document.write(".mesWindowContent{margin:0px;background:#FFFFFF;}");
document.write(".mesWindowclose{margin:0px;height:15px;width:28px;border:none;cursor:pointer;text-align:center;vertical-align:middle;}");
document.write(".close{color:#1c4c75;height:10px;width:10px;border:0px solid white;cursor:hand;font-size:11px;}");
document.write(".p{font-family:Arial,Helvetica,sans-serif;font-size:12px;font-weight:normal;color:#1c4c75;}");
document.write("</style>");

var isIe=(document.all)?true:false;
var move=false;

//主调函数  参数:（wWidth:显示窗体的宽度；hHeight:显示窗体的高度；Url:显示窗体的URL地址；title;标题；center:是否在窗体中间显示；IsTitleBar:是否显示标题栏(无标题栏时，不可拖动、不可手动关闭)）
function showMessageBox(Width,Height,Url,title,center,IsTitleBar)
{   //debugger;
    if (center==null) center = true;
    if(IsTitleBar==null) IsTitleBar=true;
    closeWindow();    
    CreateShieldBackground(30);
    GenerateTopDiv(Width,Height,Url,title,center,IsTitleBar);    
}

//最上层的操作层
function GenerateTopDiv(divWidth,divHeight,Url,title,center,IsTitleBar)
{
    var clientWidth=document.documentElement.scrollWidth;
    var clientHeight=document.documentElement.scrollHeight;
    var pos;
    if (center == false)
	{
        pos = mousePosition(event,event.srcElement);
    }
    else
    {
       pos = {x:((document.body.clientWidth -divWidth)/2  + document.body.scrollLeft), y:((document.body.clientHeight-divHeight)/2+document.body.scrollTop), w:0,h:0}
    }

    var divHtml=document.createElement("div");
    divHtml.id="mesWindow";
    divHtml.className="mesWindow";
    divHtml.innerHTML = IsTitleBar==true ? "<div class=\"mesWindowTop\"><table cellpadding='0' cellspacing='0' border='0' width='100%' height='18'><tr onmousedown=\"StartDrag(this);\" onmouseup=\"StopDrag(this);\" onmousemove='Drag(this,event);'><td width='50%' style='vertical-align:bottom;'>&nbsp;&nbsp;"+title+"</td><td width='50%' align='right' valign='middle'><a onclick='closeWindow();' class=\"close\"><img src=\"../images/close.gif\" style=\"'width:13px; height:13px; border:0px;\" align=\"absmiddle\" /></a>&nbsp;</td></tr></table></div>" : "";
    divHtml.innerHTML = divHtml.innerHTML + "<div class='mesWindowContent' id='mesWindowContent'><iframe id='ifDisplay' frameborder=0 style='width:"+divWidth+"px; height:"+divHeight+"px' src='" + Url + "'></iframe></div>";
    var style="left:"+pos.x+"px;top:"+pos.y+"px;position:absolute;width:"+divWidth+"px;";
    style += "background:#E2EBF4;border:1px solid #a6d0e7;" 
    divHtml.style.cssText=style;
    document.body.appendChild(divHtml);
}

//添加半透明层
function CreateShieldBackground(shield)//ShieldDiv
{
    var width=document.body.scrollWidth;
    var height=document.body.scrollHeight;
    if(document.body.clientWidth>=width)
    {
        width=document.body.clientWidth;
        width = width - 19;
    }
    else
    {
		width = width - 19;
    }
    if(document.body.clientHeight>=height)
    {
        height=document.body.clientHeight;
        height = height+15;
    }
    else
    {
		height = height;
    }
    
    var back=document.createElement("div");
    back.id="shieldback";
    var style ="top:0px;left:0px;position:absolute;background:#666000;padding:0px;width:"+width+"px;height:"+height+"px;";
    style += (isIe)?"filter:alpha(opacity=0);":"opacity:0;";
    back.style.cssText = style;
    document.body.appendChild(back);
    
    if(isIe) back.filters.alpha.opacity=shield;
}

//获取单击控件的坐标及高宽度 
function mousePosition(ev,srcElement)
{
    return {x:ev.clientX ,y:ev.clientY - srcElement.offsetHeight,w:srcElement.offsetWidth,h:srcElement.offsetHeight};
}

//拖动div
function Drag(obj,ev)                   
{
     if(move)
     {
          var oldwin=obj.parentElement.parentElement.parentElement.parentNode;
          var bWidth=parseInt(document.documentElement.scrollWidth);
          var bHeight=parseInt(document.documentElement.scrollHeight);
          var Left=0;
          var Top =0;
          if(parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2<=0) Left=0;
          else if(parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2>=parseInt(bWidth)-parseInt(oldwin.style.width.split('px')[0])-2)
            Left=parseInt(bWidth)-parseInt(oldwin.style.width.split('px')[0])-2;    
          else Left=parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2;
          
          if(ev.clientY-10<=0) Top=0;
          else if(ev.clientY-10>=parseInt(bHeight)-parseInt(obj.offsetHeight))
            Top=parseInt(bHeight)-parseInt(obj.offsetHeight);
          else Top=ev.clientY-10;
            
        oldwin.style.top=Top;
        oldwin.style.left=Left;
    }
}

function StopDrag(obj)
{
    obj.style.cursor="auto";
    obj.releaseCapture();
    move=false;
}

function StartDrag(obj)                       
{
    if(event.srcElement.parentElement.tagName.toUpperCase()=="TR")
    {
        obj.setCapture();
        obj.style.cursor="move";
        move=true;
    } 
}

//Close Window
function closeWindow()
{
    if(document.getElementById('shieldback')!=null)
    {
        document.getElementById('shieldback').parentNode.removeChild(document.getElementById('shieldback'));
    }
    if(document.getElementById('mesWindow')!=null)
    {
        document.getElementById('mesWindow').parentNode.removeChild(document.getElementById('mesWindow'));
    }
    document.body.style.overflow ="auto";
    
}

