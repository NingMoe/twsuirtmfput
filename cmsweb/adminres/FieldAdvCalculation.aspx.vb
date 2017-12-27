Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculation
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblResName As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSelfTable As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ddlSubRes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFSignMINN As System.Web.UI.WebControls.Button

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
        End If
        If VLng("PAGE_FMLTYPE") = 0 Then
            ViewState("PAGE_FMLTYPE") = RLng("urlfmltype")
        End If
        If VLng("PAGE_AIID") = 0 Then
            ViewState("PAGE_AIID") = RLng("urlfmlaiid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        ddlSubTables.AutoPostBack = True
        txtFormulaRight.ReadOnly = True

        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtResName.ReadOnly = True

        'btnHelp.Attributes.Add("onClick", "window.open('/cmsweb/help/help_formula.html', '_blank', 'left=10,top=10,height=580,width=775,status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes'); return false;")

        '------------------------------------------------------------------------------------------------------
        '函数的Tooltip说明信息

        '功能链接的注释信息
        lbtnChooseOtherRes.ToolTip = "添加计算公式中所需要字段所在的资源。"

        '辅助功能按钮
        btnAddColumn.ToolTip = "添加选中的字段至计算公式中。"
        btnFSignConstant.ToolTip = "添加输入框中的字符串至计算公式中。"
        btnFSignLeftBigbracket.ToolTip = "函数中的参数部分的起始符号。"
        btnFSignRightBigbracket.ToolTip = "函数中的参数部分的结束符号。"
        btnFSignLeftbracket.ToolTip = "用于表达式中改变计算优先级的表达式起始符号。"
        btnFSignRightbracket.ToolTip = "用于表达式中改变计算优先级的表达式结束符号。"
        btnFSignComma.ToolTip = "计算公式系统中所有元素的分隔符号。"
        btnSemicolon.ToolTip = "计算公式中组之间的分隔符号。"
        btnFSignSpace.ToolTip = "仅作为排版用，在计算公式中无实际作用。"

        '基本计算公式
        btnFSignBase.ToolTip = "根据指定表达式进行四则基本运算并返回常量。现在所有函数的参数和条件等式的两边都已经支持任意表达式和任意多层函数嵌套，所以此函数已经不建议使用，仅为支持历史版本而存在。格式：	[BASE]{参数}"
        btnFSignMax2.ToolTip = "返回指定2个参数值中的最大值，可以是数字或者文字比较大小。格式：	[MAX2]{参数1, 参数2}"
        btnFSignMIN2.ToolTip = "返回指定2个参数值中的最小值，可以是数字或者文字比较大小。格式：	[MIN2]{ 参数1, 参数2}"
        btnFSignUPLONG.ToolTip = "向上取整函数。譬如2.1取整为3；5.89取整为6。格式：	[UPLONG]{参数1}"
        btnFSignCLONG.ToolTip = "四舍五入取整函数。譬如2.1取整为2；5.89取整为6。格式：	[CLONG]{参数1}"
        btnFSignNONZERO.ToolTip = "取第一值不等于0的参数值（数字型）。格式：	[NONZERO]{参数1, 参数2, 参数3, …}"
        btnFSignUSERID.ToolTip = "返回当前用户的登录帐号。格式：	[USERID]{}"
        btnFSignUSERNAME.ToolTip = "返回当前用户的姓名。格式：	[USERNAME]{}"
        btnFSignIIF.ToolTip = "满足所有条件则返回True值，否则返回False值，允许有任意多个条件。True值和False值也可以是表达式。格式：	[IIF]{条件1, 条件2, …, True值, False值}"
        btnFSignIIFOR.ToolTip = "满足所有条件之一则返回True值，所有条件都不满足则返回False值。True值和False值也可以是表达式。格式：	[IIFOR]{条件1, 条件2, …, True值, False值}"
        btnFSignIIF3.ToolTip = "满足所有条件则返回Part1值，有任何一个条件满足则返回Part2值，所有条件都不满足则返回Part3值。Part1值、Part2值和Part3值可以是表达式。格式：	[IIF3]{条件1, 条件2, …, Part1值, Part2值, Part3值}"
        btnIIFGRP.ToolTip = "条件组用分号格开。满足第一组中条件11至条件1n，则返回值1，满足第二组中条件21至条件2n，则返回值2，以此类推。当满足一个条件组后，后面的条件和值不再运算，如果所有条件组都不满足，则返回False值。格式：	[IIFGRP](条件11, 条件12, ..., 条件1n, 值1; 条件21, 条件22, ..., 条件2n, 值2; ...; 条件n1, 条件n2, ..., 条件nn, 值n; False值)。举例：	[IIFGRP]{x>y, x>=z, 0; a=b, 1; c>=d, c<200, 2; 3}"
        btnFSignAllotDate.ToolTip = "返回本系统的分帐系统的启用日期。格式：	[ALLOTDATE]{}"
        btnFSignCrtTime.ToolTip = "返回当前记录的创建时间所在的字段内部名称：CRTTIME。格式：	[CRTTIME]{}"
        btnFSignAllotComp.ToolTip = "获取分帐系统的启用日期的判断条件，一般用于表间函数的条件参数中。返回举例： CRTTIME>='2005-1-1' (注：2005-1-1即系统中的分帐系统启用日期)。格式：	[ALLOTDATE]{}"
        btnFSignUABS.ToolTip = "正数取原值，负数取0。譬如UABS(-2.1)取0；5.89取整为5.89。格式：	[UABS]{任意表达式}"
        btnFSignUpRmb.ToolTip = "返回指定参数数值对应的中文金额大写。返回值如：壹仟贰佰叁拾肆元。无有效参数返回空值。格式：	[UABS]{参数}"
        btnFSignSum.ToolTip = "根据指定条件进行表间统计，返回统计值。格式：	[SUM]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。"
        btnFSignAvg.ToolTip = "根据指定条件进行表间统计，返回统计值。格式：	[AVG]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。"
        btnFSignMax.ToolTip = "根据指定条件进行表间统计，返回统计值。格式：	[MAX]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。"
        btnFSignMin.ToolTip = "根据指定条件进行表间统计，返回统计值。格式：	[MIN]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。"
        btnFSignCount.ToolTip = "统计指定表单中满足指定条件的记录行数。格式：	[COUNT]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。表间字段：任意取的一个表间字段，只是从中提取表单名称。"
        btnFSignOne.ToolTip = "返回指定表单中满足指定条件的指定字段的值。格式：	[ONE]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。表间字段：任意取的一个表间字段，只是从中提取表单名称。"
        btnFSignPOne.ToolTip = "返回主表中指定字段的值。格式：	[PONE]{主表字段}"
        btnFIRST.ToolTip = "返回满足条件的指定表单内的所有记录中的第一条记录的指定字段的值。格式：	[FIRST]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。表间字段：任意取的一个表间字段，只是从中提取表单名称。"
        btnLAST.ToolTip = "返回满足条件的指定表单内的所有记录中的最后一条记录的指定字段的值。格式：	[LAST]{表间字段, 条件1, 条件2, …}。说明：	条件语句的格式：A) 表间字段=表间字段；B) 表间字段=常量；C) 表间字段=本表字段。表间字段：任意取的一个表间字段，只是从中提取表单名称。"
        btnNORELCOND.ToolTip = "指定表间函数的SQL语句中不包含主关联字段条件。使用时只能作为表间函数（如SUM、AVG、MAX、MIN等表间计算函数）的最后一个参数。格式：	[NORELCOND]{}"
        btnLENGTH.ToolTip = "返回第一个参数作为字符串的长度。格式：	[LENGTH]{参数1}"
        btnFIRSTNUM.ToolTip = "返回第一个参数中的第一个出现的数字（整数或小数）。格式：	[FIRSTNUM]{参数1}"


        '时间计算公式
        btnDateNow.ToolTip = "返回当前时间，格式如：2005-1-20 9:30:50。所以时间函数中的唯一一个以当前时间为基准的时间函数。格式：	[NOW]{}"
        btnFSignToday.ToolTip = "返回当前日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[TODAY]{}  [TODAY]{时间参数1}"
        btnFSignNowTime.ToolTip = "返回当前时间。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NOWTIME]{}  [NOWTIME]{时间参数1}"
        btnTOMORROW.ToolTip = "返回明天日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[TOMORROW]{}  [TOMORROW]{时间参数1}"
        btnFSignYear.ToolTip = "返回指定日期（第一个参数）加指定时间间隔（第二个参数）的计算结果日期时间。格式：	[YEAR]{日期参数1，时间间隔}"
        btnFSignMonth.ToolTip = "返回指定日期（第一个参数）加指定时间间隔（第二个参数）的计算结果日期时间。格式：	[MONTH]{日期参数1，时间间隔}"
        btnFSignDate.ToolTip = "返回指定日期（第一个参数）加指定时间间隔（第二个参数）的计算结果日期时间。"
        btnFSignDIFFYEAR.ToolTip = "返回指定日期1（第一个参数）加指定日期2（第二个参数）的年份间隔。年间隔以365天为一个单位。格式：	[DIFFYEAR]{日期参数1，日期参数2}"
        btnFSignDIFFMONTH.ToolTip = "返回指定日期1（第一个参数）加指定日期2（第二个参数）的月份间隔。月间隔以30天为一个单位。格式：	[DIFFMONTH]{日期参数1，日期参数2}"
        btnFSignDIFFDAY.ToolTip = "返回指定日期1（第一个参数）加指定日期2（第二个参数）的天数间隔。格式：	[DIFFDAY]{日期参数1，日期参数2}"
        btnFSignBIRTHDAY.ToolTip = "返回指定生日日期（第一个参数）对应的今年生日日期。格式：	[DIFFYEAR]{生日日期参数1}"
        btnFSignWeekday.ToolTip = "返回指定日期（第一个参数）的星期几名称（星期一、星期二、…）。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[WEEKDAY]{}   [WEEKDAY]{基准日期参数}"
        btnWEEK2DATE.ToolTip = "返回以记录创建时间为基准，加上指定日期间隔数（必须大于等于0，小于等于7）的计算结果日期。即返回7天内的指定间隔的日期。格式：	[WEEK2DATE]{日期间隔参数}"
        btnFSignCurYear.ToolTip = "返回当前的年份。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURYEAR]{}  [CURDAY]{时间参数1}"
        btnFSignCurMonth.ToolTip = "返回当前的月份。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURMONTH]{}  [CURMONTH]{时间参数1}"
        btnFSignCurDay.ToolTip = "返回当前的天（月份中的天，1－31）。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURDAY]{}  [CURDAY]{时间参数1}"
        btnCURHOUR.ToolTip = "返回当前的小时。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURHOUR]{}  [CURHOUR]{时间参数1}"
        btnCURMIN.ToolTip = "返回当前的分钟。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURMIN]{}  [CURMIN]{时间参数1}"
        btnCURSEC.ToolTip = "返回当前的秒钟。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURSEC]{}  [CURSEC]{时间参数1}"
        btnPREVWK_MON.ToolTip = "返回上周星期一的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[PREVWK_MON]{}  [PREVWK_MON]{时间参数1}"
        btnPREVWK_SAT.ToolTip = "返回上周星期六的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[PREVWK_SAT]{}  [PREVWK_SAT]{时间参数1}"
        btnTHISWK_MON.ToolTip = "返回本周星期一的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[THISWK_MON]{}  [THISWK_MON]{时间参数1}"
        btnTHISWK_SAT.ToolTip = "返回本周星期六的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[THISWK_SAT]{}  [THISWK_SAT]{时间参数1}"
        btnNEXTWK_MON.ToolTip = "返回下周星期一的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NEXTWK_MON]{}  [NEXTWK_MON]{时间参数1}"
        btnNEXTWK_SAT.ToolTip = "返回下周星期六的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NEXTWK_SAT]{}  [NEXTWK_SAT]{时间参数1}"
        btnNNEXTWK_MON.ToolTip = "返回下下周星期一的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NNEXTWK_MON]{}  [NNEXTWK_MON]{时间参数1}"
        btnFSignPREVMONTH_FD.ToolTip = "返回上月第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[PREVMONTH_FD]{}  [PREVMONTH_FD]{时间参数1}"
        btnFSignCURMONTH_FD.ToolTip = "返回本月第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURMONTH_FD]{}  [CURMONTH_FD]{时间参数1}"
        btnFSignNEXTMONTH_FD.ToolTip = "返回下月第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NEXTMONTH_FD]{}  [NEXTMONTH_FD]{时间参数1}"
        btnNNEXTMONTH_FD.ToolTip = "返回下下月第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NNEXTMONTH_FD]{}  [NNEXTMONTH_FD]{时间参数1}"
        btnFSignPREVQTR_FD.ToolTip = "返回上季度第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[PREVQTR_FD]{}  [PREVQTR_FD]{时间参数1}"
        btnFSignCURQTR_FD.ToolTip = "返回本季度第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[CURQTR_FD]{}  [CURQTR_FD]{时间参数1}"
        btnFSignNEXTQTR_FD.ToolTip = "返回下季度第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NEXTQTR_FD]{}  [NEXTQTR_FD]{时间参数1}"
        btnPREVYEAR_FD.ToolTip = "返回上年第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[PREVYEAR_FD]{}  [PREVYEAR_FD]{时间参数1}"
        btnTHISYEAR_FD.ToolTip = "返回今年第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[THISYEAR_FD]{}  [THISYEAR_FD]{时间参数1}"
        btnNEXTYEAR_FD.ToolTip = "返回明年第一天的日期。以第一个参数作为基准时间，无参数则以记录创建时间作为基准，第一个参数为函数NOW则表示以每次记录操作时间为基准。格式：	[NEXTYEAR_FD]{}  [NEXTYEAR_FD]{时间参数1}"
        btnFSignEXTRACT_YEAR.ToolTip = "返回第一个时间参数中的年份。格式：	[EXTRACT_YEAR]{时间参数1}"
        btnFSignEXTRACT_MONTH.ToolTip = "返回第一个时间参数中的月份。格式：	[EXTRACT_MONTH]{时间参数1}"
        btnFSignEXTRACT_DAY.ToolTip = "返回第一个时间参数中的天（如：2005-3-28则返回28）。格式：	[EXTRACT_DAY]{时间参数1}"


        '逻辑计算公式
        btnFSignAND.ToolTip = "所有条件参数满足则正常返回，否则抛出异常。一般用于保存记录时校验输入信息。格式：	[AND]{条件1, 条件2, …}"
        btnFSignOR.ToolTip = "所有条件参数满足则正常返回，否则抛出异常。一般用于保存记录时校验输入信息。格式：	[AND]{条件1, 条件2, …}"
        btnISNUM.ToolTip = "校验指定的参数是否是数字，是则返回true，否则返回false。使用时一般作为AND函数、OR函数的一个条件参数。格式：	[ISNUM]{参数1}"
        btnFSignINTTIMES.ToolTip = "整除校验公式。第一个参数除以第二个参数，余数为0（整除）则正常返回，否则抛出异常。格式：	[INTTIMES]{参数1, 参数2}"

        'SQL函数
        btnFSignSQL.ToolTip = "支持计算公式内置SQL语句。SQL语句中需要外部传入的参数用||包括，||内是当前资源的某字段的值。格式：	[SQL]{SQL语句}。举例：	[SQL]{SELECT SUM(COL1) FROM TABLE1 WHERE COL2='张三'}    [SQL]{SELECT SUM(COL1) FROM TABLE1 WHERE COL2='|COL101|' AND COL3 LIKE '%|COL102|%'}"
        '------------------------------------------------------------------------------------------------------
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim lngDictResID As Long
        If RStr("selresid") <> "" Then '刚从选择资源窗体回来
            lngDictResID = RLng("selresid")
        Else
            lngDictResID = VLng("PAGE_RESID")
            Session("PAGE_CMSCAL_RESOURCES") = Nothing '必须
            Session("CMSPAGE_FML_VERIFYNAME") = Nothing
            Session("CMSPAGE_FML_VERIFYTIP") = Nothing
            Session("CMSPAGE_FMLDESC") = Nothing
            Session("CMSPAGE_FMLEXPR") = Nothing
            Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = Nothing
        End If

        '--------------------------------------------------------------------------
        '获取指定字段资源和字段的公式信息
        Dim datFml As DataFormula = Nothing
        If VLng("PAGE_AIID") > 0 Then
            datFml = CTableColCalculation.GetFormulaByAiid(CmsPass, VLng("PAGE_AIID"))
        Else
            datFml = CTableColCalculation.GetFormulaByFmlLeftColumn(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
        Dim blnHaveInitFormula As Boolean = False
        If datFml Is Nothing Then
            datFml = New DataFormula
            blnHaveInitFormula = False
        Else
            blnHaveInitFormula = True
        End If
        '--------------------------------------------------------------------------

        '--------------------------------------------------------------------------
        If VLng("PAGE_FMLTYPE") = FormulaType.IsVerify Then  '是校验公式
            lblColDesc.Text = "校验公式名称"
            If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
                txtVerifyName.Text = SStr("CMSPAGE_FML_VERIFYNAME")
            Else
                txtVerifyName.Text = datFml.strCDJ_COLNAME
            End If
            lblVerifyDesc.Text = "校验公式提示信息"
            lblVerifyDesc.Visible = True
            If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
                txtVerifyTip.Text = SStr("CMSPAGE_FML_VERIFYTIP")
            Else
                txtVerifyTip.Text = datFml.strCDJ_VERIFY_TIP
            End If
            txtVerifyTip.Visible = True

            If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
                txtFormulaRight.Text = SStr("CMSPAGE_FMLDESC")
                chkIncludeArithmetic.Checked = CBool(Session("CMSPAGE_FML_INCLUDE_ARITHMETIC"))
            Else
                txtFormulaRight.Text = datFml.strCDJ_FORMULA_DESC
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = datFml.strCDJ_FMLRIGHT_EXPR

                chkIncludeArithmetic.Checked = CBool(IIf(datFml.intCDJ_NO_ARITHMETIC = 0, True, False))
                Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = chkIncludeArithmetic.Checked
            End If
        Else '是计算公式
            lblColDesc.Text = "字段名称"
            Dim strColDispName As String = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            Dim strColType As String = CTableStructure.GetColTypeDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            txtVerifyName.Text = strColDispName & " (" & strColType & ")"
            txtVerifyName.ReadOnly = True

            lblVerifyDesc.Text = ""
            lblVerifyDesc.Visible = False
            txtVerifyTip.Text = ""
            txtVerifyTip.Visible = False

            If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
                txtFormulaRight.Text = SStr("CMSPAGE_FMLDESC")
                chkIncludeArithmetic.Checked = CBool(Session("CMSPAGE_FML_INCLUDE_ARITHMETIC"))
            Else
                txtFormulaRight.Text = datFml.strCDJ_FORMULA_DESC.Substring(datFml.strCDJ_FORMULA_DESC.IndexOf("=") + 1)
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = datFml.strCDJ_FMLRIGHT_EXPR

                chkIncludeArithmetic.Checked = CBool(IIf(datFml.intCDJ_NO_ARITHMETIC = 0, True, False))
                Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = chkIncludeArithmetic.Checked
            End If
        End If

        'If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
        '    If CType(Session("CMSPAGE_FML_RUNBEFSAVE"), Boolean) Then
        '        rdoRunBeforeSave.Checked = True
        '        rdoRunAfterSave.Checked = False
        '    Else
        '        rdoRunBeforeSave.Checked = False
        '        rdoRunAfterSave.Checked = True
        '    End If
        '    chkRunRecordAdd.Checked = CType(Session("CMSPAGE_FML_CALOCCASION_ADD"), Boolean)
        '    chkRunRecordEdit.Checked = CType(Session("CMSPAGE_FML_CALOCCASION_EDIT"), Boolean)
        '    chkRunRecordDel.Checked = CType(Session("CMSPAGE_FML_CALOCCASION_DEL"), Boolean)
        'Else
        'End If
        '--------------------------------------------------------------------------

        ShowSubRelTables(CmsPass, lngDictResID) '关联子资源列表的选择为空
        LoadResColumns(lngDictResID, ListBox1)
        PrepareFuncShow() 'Enable或Disable功能键
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        PrepareFuncShow() 'Enable或Disable功能键
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If txtFormulaRight.Text.Trim() = "" Then
                PromptMsg("请输入有效的公式!")
                Return
            End If

            If VLng("PAGE_FMLTYPE") = FormulaType.IsVerify Then '是校验公式
                Dim strVerifyName As String = txtVerifyName.Text.Trim()
                If strVerifyName = "" Then
                    PromptMsg("校验公式的名称不能为空!")
                    Return
                End If
                Dim strVerifyTip As String = txtVerifyTip.Text.Trim()
                If strVerifyTip = "" Then
                    PromptMsg("校验公式的提示信息不能为空!")
                    Return
                End If

                Dim strFmlDesc As String = txtFormulaRight.Text.Trim()
                ViewState("PAGE_AIID") = CTableColCalculation.SaveFormula(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_AIID"), FormulaType.IsVerify, strVerifyName, SStr("CMSPAGE_FMLEXPR"), strFmlDesc, txtVerifyTip.Text.Trim(), FormulaOccasion.Unknown, chkIncludeArithmetic.Checked)
            Else '计算公式
                Dim strColDispName As String = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
                Dim strFmlDesc As String = strColDispName & "=" & txtFormulaRight.Text.Trim()
                ViewState("PAGE_AIID") = CTableColCalculation.SaveFormula(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_AIID"), FormulaType.IsCalculation, VStr("PAGE_COLNAME"), SStr("CMSPAGE_FMLEXPR"), strFmlDesc, "", FormulaOccasion.Unknown, chkIncludeArithmetic.Checked)
            End If

            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Try
            CTableColCalculation.DelFormula(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))

            PromptMsg("成功删除字段的计算公式！")
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnBackFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFormula.Click
        Try
            Dim strDesc As String = SStr("CMSPAGE_FMLDESC").Trim()
            Dim strExpr As String = SStr("CMSPAGE_FMLEXPR").Trim()
            If strDesc = "" Or strExpr = "" Then
                '已经为空，不必回滚
                Session("CMSPAGE_FMLDESC") = ""
                txtFormulaRight.Text = SStr("CMSPAGE_FMLDESC")
                Session("CMSPAGE_FMLEXPR") = ""
                Return
            End If

            Dim i As Integer
            Dim pos As Integer = -1
            Dim chr As Char
            Dim len As Integer = strExpr.Length
            For i = (len - 1) To 0 Step -1
                chr = strExpr.Chars(i)
                If chr = "," Or chr = "}" Or chr = "{" Or chr = ")" Or chr = "(" Or chr = "=" Or chr = ">" Or chr = "<" Or chr = "!" Or chr = "+" Or chr = "-" Or chr = "*" Or chr = "/" Then
                    pos = i
                    Exit For
                End If
            Next
            If pos = -1 Then
                Session("CMSPAGE_FMLDESC") = ""
                txtFormulaRight.Text = SStr("CMSPAGE_FMLDESC")
                Session("CMSPAGE_FMLEXPR") = ""
                Return
            End If

            strExpr = strExpr.Substring(0, pos)
            pos = strDesc.LastIndexOf(chr)
            strDesc = strDesc.Substring(0, pos)
            Session("CMSPAGE_FMLDESC") = strDesc
            txtFormulaRight.Text = strDesc
            Session("CMSPAGE_FMLEXPR") = strExpr
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnAddColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddColumn.Click
        If ListBox1.SelectedItem Is Nothing OrElse ListBox1.SelectedItem.Text = "" Then
            PromptMsg("请选择有效的字段")
            Return
        End If
        Try
            Dim strColName As String = ListBox1.SelectedItem.Text
            Dim intPos As Integer = strColName.IndexOf(" (")
            strColName = strColName.Substring(0, intPos).Trim()

            Dim strResID As String = ddlSubTables.Items(ddlSubTables.SelectedIndex).Value
            If VLng("PAGE_RESID") = CLng(strResID) Then '是本资源
                txtFormulaRight.Text = txtFormulaRight.Text & "[" & strColName & "]"
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "[" & ListBox1.SelectedItem.Value & "]"
            Else '表间资源的字段，则必须添加成控件名称
                txtFormulaRight.Text = txtFormulaRight.Text & "[" & ddlSubTables.SelectedItem.Text & "::" & strColName & "]"
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "[" & TextboxName.GetCtrlName(ListBox1.SelectedItem.Value, CLng(strResID)) & "]"
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnFSignAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignAdd.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "+"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "+"
    End Sub

    Private Sub btnFSignSubtract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSubtract.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "-"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "-"
    End Sub

    Private Sub btnFSignMultiply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMultiply.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "*"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "*"
    End Sub

    Private Sub btnFSignDivision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignDivision.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "/"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "/"
    End Sub

    Private Sub btnFSignLeftbracket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignLeftbracket.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "("
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "("
    End Sub

    Private Sub btnFSignRightbracket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignRightbracket.Click
        txtFormulaRight.Text = txtFormulaRight.Text & ")"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & ")"
    End Sub

    Private Sub btnFSignLeftBigbracket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignLeftBigbracket.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "{"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "{"
    End Sub

    Private Sub btnFSignRightBigbracket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignRightBigbracket.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "}"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "}"
    End Sub

    Private Sub btnFSignConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignConstant.Click
        txtFormulaRight.Text = txtFormulaRight.Text & txtFSignConstant.Text.Trim()
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & txtFSignConstant.Text.Trim()
    End Sub

    Private Sub btnFSignSum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSum.Click
        AddFunction(CmsFormula.FUNC_SUM)
    End Sub

    Private Sub btnFSignAvg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignAvg.Click
        AddFunction(CmsFormula.FUNC_AVG)
    End Sub

    Private Sub btnFSignMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMax.Click
        AddFunction(CmsFormula.FUNC_MAX)
    End Sub

    Private Sub btnFSignMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMin.Click
        AddFunction(CmsFormula.FUNC_MIN)
    End Sub

    Private Sub btnFSignCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCount.Click
        AddFunction(CmsFormula.FUNC_COUNT)
    End Sub

    Private Sub btnNORELCOND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNORELCOND.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_NORELCOND)
    End Sub

    Private Sub btnFSignOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignOne.Click
        AddFunction(CmsFormula.FUNC_ONE)
    End Sub

    Private Sub btnFIRST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFIRST.Click
        AddFunction(CmsFormula.FUNC_FIRST)
    End Sub

    Private Sub btnLAST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLAST.Click
        AddFunction(CmsFormula.FUNC_LAST)
    End Sub

    Private Sub btnFSignPOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignPOne.Click
        AddFunction(CmsFormula.FUNC_PONE)
    End Sub

    Private Sub btnLENGTH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLENGTH.Click
        AddFunction(CmsFormula.FUNC_LENGTH)
    End Sub

    Private Sub btnFIRSTNUM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFIRSTNUM.Click
        AddFunction(CmsFormula.FUNC_FIRSTNUM)
    End Sub

    Private Sub btnFSignBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignBase.Click
        AddFunction(CmsFormula.FUNC_BASE)
    End Sub

    Private Sub btnFSignMax2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMax2.Click
        AddFunction(CmsFormula.FUNC_MAX2)
    End Sub

    Private Sub btnFSignMIN2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMIN2.Click
        AddFunction(CmsFormula.FUNC_MIN2)
    End Sub

    Private Sub btnFSignIIF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignIIF.Click
        AddFunction(CmsFormula.FUNC_IIF)
    End Sub

    Private Sub btnFSignIIFOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignIIFOR.Click
        AddFunction(CmsFormula.FUNC_IIFOR)
    End Sub

    Private Sub btnFSignIIF3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignIIF3.Click
        AddFunction(CmsFormula.FUNC_IIF3)
    End Sub

    Private Sub btnIIFGRP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIIFGRP.Click
        AddFunction(CmsFormula.FUNC_IIFGRP)
    End Sub

    Private Sub btnFSignAllotDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignAllotDate.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_ALLOTDATE)
    End Sub

    Private Sub btnFSignCrtTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCrtTime.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_CRTTIME)
    End Sub

    Private Sub btnFSignAllotComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignAllotComp.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_ALLOTCOMP)
    End Sub

    Private Sub btnFSignNONZERO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignNONZERO.Click
        AddFunction(CmsFormula.FUNC_NONZERO)
    End Sub

    Private Sub btnFSignUSERID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignUSERID.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_USERID)
    End Sub

    Private Sub btnFSignUSERNAME_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignUSERNAME.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_USERNAME)
    End Sub

    Private Sub btnFSignUABS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignUABS.Click
        AddFunction(CmsFormula.FUNC_UABS)
    End Sub

    Private Sub btnFSignUPLONG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignUPLONG.Click
        AddFunction(CmsFormula.FUNC_UPLONG)
    End Sub

    Private Sub btnFSignCLONG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCLONG.Click
        AddFunction(CmsFormula.FUNC_CLONG)
    End Sub

    Private Sub btnFSignSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSQL.Click
        AddFunction(CmsFormula.FUNC_SQL)
    End Sub

    Private Sub btnFSignCUST_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCUST.Click
        AddFunction(CmsFormula.FUNC_CUSTOMIZE)
    End Sub

    Private Sub btnFSignCUST_ID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCUST_ID.Click
        Dim strCustFmlID As String = CStr(TimeId.CurrentMilliseconds())
        txtFormulaRight.Text = txtFormulaRight.Text & strCustFmlID
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & strCustFmlID
    End Sub

    Private Sub btnFSignComma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignComma.Click
        txtFormulaRight.Text = txtFormulaRight.Text & ","
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & ","
    End Sub

    Private Sub btnSemicolon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSemicolon.Click
        txtFormulaRight.Text = txtFormulaRight.Text & ";"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & ";"
    End Sub

    Private Sub btnFSignSpace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSpace.Click
        txtFormulaRight.Text = txtFormulaRight.Text & " "
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & " "
    End Sub

    Private Sub btnFSignEqual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignEqual.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "="
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "="
    End Sub

    Private Sub btnFSignGreater1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignGreater1.Click
        txtFormulaRight.Text = txtFormulaRight.Text & ">="
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & ">="
    End Sub

    Private Sub btnFSignGreater2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignGreater2.Click
        txtFormulaRight.Text = txtFormulaRight.Text & ">"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & ">"
    End Sub

    Private Sub btnFSignSmaller1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSmaller1.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "<="
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "<="
    End Sub

    Private Sub btnFSignSmaller2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignSmaller2.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "<"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "<"
    End Sub

    Private Sub btnFSignNotEqual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignNotEqual.Click
        txtFormulaRight.Text = txtFormulaRight.Text & "!="
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "!="
    End Sub

    Private Sub btnDateNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDateNow.Click
        AddFunctionWithCloseBracket(CmsFormula.FUNC_NOW)
    End Sub

    Private Sub btnFSignYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignYear.Click
        AddFunction(CmsFormula.FUNC_YEAR)
    End Sub

    Private Sub btnFSignMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignMonth.Click
        AddFunction(CmsFormula.FUNC_MONTH)
    End Sub

    Private Sub btnFSignDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignDate.Click
        AddFunction(CmsFormula.FUNC_DATE)
    End Sub

    Private Sub btnFSignToday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignToday.Click
        AddFunction(CmsFormula.FUNC_TODAY)
    End Sub

    Private Sub btnTOMORROW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTOMORROW.Click
        AddFunction(CmsFormula.FUNC_TOMORROW)
    End Sub

    Private Sub btnFSignNowTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignNowTime.Click
        AddFunction(CmsFormula.FUNC_NOWTIME)
    End Sub

    Private Sub btnFSignWeekday_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignWeekday.Click
        AddFunction(CmsFormula.FUNC_WEEKDAY)
    End Sub

    Private Sub btnFSignUpRmb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignUpRmb.Click
        AddFunction(CmsFormula.FUNC_UPRMB)
    End Sub

    Private Sub btnFSignDIFFYEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignDIFFYEAR.Click
        AddFunction(CmsFormula.FUNC_DIFFYEAR)
    End Sub

    Private Sub btnFSignDIFFMONTH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignDIFFMONTH.Click
        AddFunction(CmsFormula.FUNC_DIFFMONTH)
    End Sub

    Private Sub btnFSignDIFFDAY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignDIFFDAY.Click
        AddFunction(CmsFormula.FUNC_DIFFDAY)
    End Sub

    Private Sub btnDIFFHOUR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDIFFHOUR.Click
        AddFunction(CmsFormula.FUNC_DIFFHOUR)
    End Sub

    Private Sub btnDIFFMIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDIFFMIN.Click
        AddFunction(CmsFormula.FUNC_DIFFMIN)
    End Sub

    Private Sub btnDIFFSEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDIFFSEC.Click
        AddFunction(CmsFormula.FUNC_DIFFSEC)
    End Sub

    Private Sub btnFSignCurYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCurYear.Click
        AddFunction(CmsFormula.FUNC_CURYEAR)
    End Sub

    Private Sub btnFSignCurMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCurMonth.Click
        AddFunction(CmsFormula.FUNC_CURMONTH)
    End Sub

    Private Sub btnFSignCurDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCurDay.Click
        AddFunction(CmsFormula.FUNC_CURDAY)
    End Sub

    Private Sub btnCURHOUR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCURHOUR.Click
        AddFunction(CmsFormula.FUNC_CURHOUR)
    End Sub

    Private Sub btnCURMIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCURMIN.Click
        AddFunction(CmsFormula.FUNC_CURMIN)
    End Sub

    Private Sub btnCURSEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCURSEC.Click
        AddFunction(CmsFormula.FUNC_CURSEC)
    End Sub

    Private Sub btnWEEK2DATE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWEEK2DATE.Click
        AddFunction(CmsFormula.FUNC_WEEK2DATE)
    End Sub

    Private Sub btnPREVWK_MON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPREVWK_MON.Click
        AddFunction(CmsFormula.FUNC_PREVWK_MON)
    End Sub

    Private Sub btnPREVWK_SAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPREVWK_SAT.Click
        AddFunction(CmsFormula.FUNC_PREVWK_SAT)
    End Sub

    Private Sub btnTHISWK_MON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTHISWK_MON.Click
        AddFunction(CmsFormula.FUNC_THISWK_MON)
    End Sub

    Private Sub btnTHISWK_SAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTHISWK_SAT.Click
        AddFunction(CmsFormula.FUNC_THISWK_SAT)
    End Sub

    Private Sub btnNEXTWK_MON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEXTWK_MON.Click
        AddFunction(CmsFormula.FUNC_NEXTWK_MON)
    End Sub

    Private Sub btnNEXTWK_SAT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEXTWK_SAT.Click
        AddFunction(CmsFormula.FUNC_NEXTWK_SAT)
    End Sub

    Private Sub btnNNEXTWK_MON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNNEXTWK_MON.Click
        AddFunction(CmsFormula.FUNC_NNEXTWK_MON)
    End Sub

    Private Sub btnFSignPREVMONTH_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignPREVMONTH_FD.Click
        AddFunction(CmsFormula.FUNC_PREVMONTH_FD)
    End Sub

    Private Sub btnFSignCURMONTH_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCURMONTH_FD.Click
        AddFunction(CmsFormula.FUNC_CURMONTH_FD)
    End Sub

    Private Sub btnFSignNEXTMONTH_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignNEXTMONTH_FD.Click
        AddFunction(CmsFormula.FUNC_NEXTMONTH_FD)
    End Sub

    Private Sub btnNNEXTMONTH_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNNEXTMONTH_FD.Click
        AddFunction(CmsFormula.FUNC_NNEXTMONTH_FD)
    End Sub

    Private Sub btnFSignPREVQTR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignPREVQTR_FD.Click
        AddFunction(CmsFormula.FUNC_PREVQTR_FD)
    End Sub

    Private Sub btnFSignCURQTR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignCURQTR_FD.Click
        AddFunction(CmsFormula.FUNC_CURQTR_FD)
    End Sub

    Private Sub btnFSignNEXTQTR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignNEXTQTR_FD.Click
        AddFunction(CmsFormula.FUNC_NEXTQTR_FD)
    End Sub

    Private Sub btnFSignBIRTHDAY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignBIRTHDAY.Click
        AddFunction(CmsFormula.FUNC_BIRTHDAY)
    End Sub

    Private Sub btnFSignEXTRACT_YEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignEXTRACT_YEAR.Click
        AddFunction(CmsFormula.FUNC_EXTRACT_YEAR)
    End Sub

    Private Sub btnFSignEXTRACT_MONTH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignEXTRACT_MONTH.Click
        AddFunction(CmsFormula.FUNC_EXTRACT_MONTH)
    End Sub

    Private Sub btnFSignEXTRACT_DAY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignEXTRACT_DAY.Click
        AddFunction(CmsFormula.FUNC_EXTRACT_DAY)
    End Sub

    Private Sub btnPREVYEAR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPREVYEAR_FD.Click
        AddFunction(CmsFormula.FUNC_PREVYEAR_FD)
    End Sub

    Private Sub btnTHISYEAR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTHISYEAR_FD.Click
        AddFunction(CmsFormula.FUNC_THISYEAR_FD)
    End Sub

    'Private Sub btnYEAR_LASTDAY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYEAR_LASTDAY.Click
    '    AddFunction(CmsFormula.FUNC_YEAR_LASTDAY)
    'End Sub

    Private Sub btnNEXTYEAR_FD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNEXTYEAR_FD.Click
        AddFunction(CmsFormula.FUNC_NEXTYEAR_FD)
    End Sub

    Private Sub btnFSignAND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignAND.Click
        AddFunction(CmsFormula.FUNC_AND)
    End Sub

    Private Sub btnFSignOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignOR.Click
        AddFunction(CmsFormula.FUNC_OR)
    End Sub

    Private Sub btnISNUM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnISNUM.Click
        AddFunction(CmsFormula.FUNC_ISNUM)
    End Sub

    Private Sub btnFSignINTTIMES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSignINTTIMES.Click
        AddFunction(CmsFormula.FUNC_INTTIMES)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtFormulaRight.Text = ""
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = ""
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub LoadResColumns(ByVal lngResID As Long, ByRef lstCols As ListBox)
        lstCols.Items.Clear() '清空列表

        If lngResID = 0 Then Return
        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, False, True)
        Dim datCol As DataTableColumn
        For Each datCol In alistColumns
            lstCols.Items.Add(New ListItem(datCol.ColDispName & " (" & datCol.ColTypeDispName & ")", datCol.ColName))
        Next
        alistColumns.Clear()
        alistColumns = Nothing
    End Sub

    Private Sub PrepareFuncShow()
        '检查表间计算公式是否支持
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_FORMULA_OUTERTABLE")
        btnFSignSum.Enabled = bln
        btnFSignAvg.Enabled = bln
        btnFSignMax.Enabled = bln
        btnFSignMin.Enabled = bln
        btnFSignCount.Enabled = bln
        btnFSignOne.Enabled = bln
        btnFSignPOne.Enabled = bln
        'btnFSignSUMN.Enabled = bln
        'btnFSignAVGN.Enabled = bln
        'btnFSignMAXN.Enabled = bln
        'btnFSignMINN.Enabled = bln
        'btnFSignEACHAND.Enabled = bln

        '检查日期计算公式是否支持
        bln = CmsFunc.IsEnable("FUNC_FORMULA_DATE")
        btnFSignYear.Enabled = bln
        btnFSignMonth.Enabled = bln
        btnFSignDate.Enabled = bln
        btnFSignToday.Enabled = bln
        btnFSignNowTime.Enabled = bln
        btnFSignWeekday.Enabled = bln
        btnFSignUpRmb.Enabled = bln
        btnFSignDIFFYEAR.Enabled = bln
        btnFSignDIFFMONTH.Enabled = bln
        btnFSignDIFFDAY.Enabled = bln
        btnFSignCURMONTH_FD.Enabled = bln
        'btnFSignCURMONTH_LD.Enabled = bln
        btnFSignNEXTMONTH_FD.Enabled = bln


        '检查高级计算公式是否支持
        bln = CmsFunc.IsEnable("FUNC_FORMULA_ADVANCE")
        btnFSignMax2.Enabled = bln
        btnFSignMIN2.Enabled = bln
        btnFSignUPLONG.Enabled = bln
        btnFSignNONZERO.Enabled = bln
        btnFSignUSERID.Enabled = bln
        btnFSignUSERNAME.Enabled = bln
        btnFSignUABS.Enabled = bln
        btnFSignIIF.Enabled = bln
        'btnFSignIIF2.Enabled = bln
        'btnFSignIIF2OR.Enabled = bln
        btnFSignIIFOR.Enabled = bln
        btnFSignIIF3.Enabled = bln
        btnFSignAllotDate.Enabled = bln
        btnFSignCrtTime.Enabled = bln
        btnFSignAllotComp.Enabled = bln

        '检查SQL计算公式是否支持
        bln = CmsFunc.IsEnable("FUNC_FORMULA_SQL")
        btnFSignSQL.Enabled = bln

        '检查定制计算公式是否支持
        bln = CmsFunc.IsEnable("FUNC_FORMULA_CUST")
        btnFSignCUST.Enabled = bln
        btnFSignCUST_ID.Enabled = bln

        '检查计算公式－逻辑函数 是否支持
        bln = CmsFunc.IsEnable("FUNC_FORMULA_LOGIC")
        btnFSignAND.Enabled = bln
        btnFSignOR.Enabled = bln

        bln = CmsFunc.IsEnable("FUNC_FORMULA_ANYTABLE")
        lbtnChooseOtherRes.Enabled = bln
    End Sub

    '-----------------------------------------------------------------
    '显示当前资源的关联子资源列表
    '-----------------------------------------------------------------
    Private Sub ShowSubRelTables(ByRef pst As CmsPassport, ByVal lngNewResID As Long)
        ddlSubTables.Items.Clear()

        Dim blnNewResIDCreated As Boolean = False
        Dim alistHostRes As ArrayList
        If Session("PAGE_CMSCAL_RESOURCES") Is Nothing Then
            alistHostRes = CmsTableRelation.GetSubRelatedResources(CmsPass, VLng("PAGE_RESID"), False, False)
            alistHostRes.Insert(0, CmsPass.GetDataRes(VLng("PAGE_RESID"))) '添加本资源为第一个资源
        Else
            alistHostRes = CType(Session("PAGE_CMSCAL_RESOURCES"), ArrayList)
        End If

        Dim datRes As DataResource
        For Each datRes In alistHostRes
            ddlSubTables.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
            If lngNewResID = datRes.ResID Then blnNewResIDCreated = True
        Next

        If blnNewResIDCreated = False And lngNewResID <> 0 Then
            datRes = pst.GetDataRes(lngNewResID)
            ddlSubTables.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
            alistHostRes.Add(datRes)
        End If
        Session("PAGE_CMSCAL_RESOURCES") = alistHostRes

        If lngNewResID <> 0 Then ddlSubTables.SelectedValue = CStr(lngNewResID)
    End Sub

    Private Sub ddlSubTables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlSubTables.SelectedIndexChanged
        Dim strResID As String = ddlSubTables.Items(ddlSubTables.SelectedIndex).Value
        LoadResColumns(CLng(strResID), ListBox1)
        PrepareFuncShow() 'Enable或Disable功能键
    End Sub

    Private Sub lbtnChooseOtherRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnChooseOtherRes.Click
        Session("CMSPAGE_FML_VERIFYNAME") = txtVerifyName.Text
        Session("CMSPAGE_FML_VERIFYTIP") = txtVerifyTip.Text
        Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = chkIncludeArithmetic.Checked
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME") & "&urlfmltype=" & VLng("PAGE_FMLTYPE") & "&urlfmlaiid=" & VLng("PAGE_AIID")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub AddFunction(ByVal strFuncName As String)
        txtFormulaRight.Text = txtFormulaRight.Text & strFuncName & "{"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & strFuncName & "{"
    End Sub

    Private Sub AddFunctionWithCloseBracket(ByVal strFuncName As String)
        txtFormulaRight.Text = txtFormulaRight.Text & strFuncName & "{}"
        Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
        Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & strFuncName & "{}"
    End Sub
End Class
End Namespace
