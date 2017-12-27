Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculation
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
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

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
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
        '������Tooltip˵����Ϣ

        '�������ӵ�ע����Ϣ
        lbtnChooseOtherRes.ToolTip = "��Ӽ��㹫ʽ������Ҫ�ֶ����ڵ���Դ��"

        '�������ܰ�ť
        btnAddColumn.ToolTip = "���ѡ�е��ֶ������㹫ʽ�С�"
        btnFSignConstant.ToolTip = "���������е��ַ��������㹫ʽ�С�"
        btnFSignLeftBigbracket.ToolTip = "�����еĲ������ֵ���ʼ���š�"
        btnFSignRightBigbracket.ToolTip = "�����еĲ������ֵĽ������š�"
        btnFSignLeftbracket.ToolTip = "���ڱ��ʽ�иı�������ȼ��ı��ʽ��ʼ���š�"
        btnFSignRightbracket.ToolTip = "���ڱ��ʽ�иı�������ȼ��ı��ʽ�������š�"
        btnFSignComma.ToolTip = "���㹫ʽϵͳ������Ԫ�صķָ����š�"
        btnSemicolon.ToolTip = "���㹫ʽ����֮��ķָ����š�"
        btnFSignSpace.ToolTip = "����Ϊ�Ű��ã��ڼ��㹫ʽ����ʵ�����á�"

        '�������㹫ʽ
        btnFSignBase.ToolTip = "����ָ�����ʽ��������������㲢���س������������к����Ĳ�����������ʽ�����߶��Ѿ�֧��������ʽ�������㺯��Ƕ�ף����Դ˺����Ѿ�������ʹ�ã���Ϊ֧����ʷ�汾�����ڡ���ʽ��	[BASE]{����}"
        btnFSignMax2.ToolTip = "����ָ��2������ֵ�е����ֵ�����������ֻ������ֱȽϴ�С����ʽ��	[MAX2]{����1, ����2}"
        btnFSignMIN2.ToolTip = "����ָ��2������ֵ�е���Сֵ�����������ֻ������ֱȽϴ�С����ʽ��	[MIN2]{ ����1, ����2}"
        btnFSignUPLONG.ToolTip = "����ȡ��������Ʃ��2.1ȡ��Ϊ3��5.89ȡ��Ϊ6����ʽ��	[UPLONG]{����1}"
        btnFSignCLONG.ToolTip = "��������ȡ��������Ʃ��2.1ȡ��Ϊ2��5.89ȡ��Ϊ6����ʽ��	[CLONG]{����1}"
        btnFSignNONZERO.ToolTip = "ȡ��һֵ������0�Ĳ���ֵ�������ͣ�����ʽ��	[NONZERO]{����1, ����2, ����3, ��}"
        btnFSignUSERID.ToolTip = "���ص�ǰ�û��ĵ�¼�ʺš���ʽ��	[USERID]{}"
        btnFSignUSERNAME.ToolTip = "���ص�ǰ�û�����������ʽ��	[USERNAME]{}"
        btnFSignIIF.ToolTip = "�������������򷵻�Trueֵ�����򷵻�Falseֵ��������������������Trueֵ��FalseֵҲ�����Ǳ��ʽ����ʽ��	[IIF]{����1, ����2, ��, Trueֵ, Falseֵ}"
        btnFSignIIFOR.ToolTip = "������������֮һ�򷵻�Trueֵ�������������������򷵻�Falseֵ��Trueֵ��FalseֵҲ�����Ǳ��ʽ����ʽ��	[IIFOR]{����1, ����2, ��, Trueֵ, Falseֵ}"
        btnFSignIIF3.ToolTip = "�������������򷵻�Part1ֵ�����κ�һ�����������򷵻�Part2ֵ�������������������򷵻�Part3ֵ��Part1ֵ��Part2ֵ��Part3ֵ�����Ǳ��ʽ����ʽ��	[IIF3]{����1, ����2, ��, Part1ֵ, Part2ֵ, Part3ֵ}"
        btnIIFGRP.ToolTip = "�������÷ֺŸ񿪡������һ��������11������1n���򷵻�ֵ1������ڶ���������21������2n���򷵻�ֵ2���Դ����ơ�������һ��������󣬺����������ֵ�������㣬������������鶼�����㣬�򷵻�Falseֵ����ʽ��	[IIFGRP](����11, ����12, ..., ����1n, ֵ1; ����21, ����22, ..., ����2n, ֵ2; ...; ����n1, ����n2, ..., ����nn, ֵn; Falseֵ)��������	[IIFGRP]{x>y, x>=z, 0; a=b, 1; c>=d, c<200, 2; 3}"
        btnFSignAllotDate.ToolTip = "���ر�ϵͳ�ķ���ϵͳ���������ڡ���ʽ��	[ALLOTDATE]{}"
        btnFSignCrtTime.ToolTip = "���ص�ǰ��¼�Ĵ���ʱ�����ڵ��ֶ��ڲ����ƣ�CRTTIME����ʽ��	[CRTTIME]{}"
        btnFSignAllotComp.ToolTip = "��ȡ����ϵͳ���������ڵ��ж�������һ�����ڱ�亯�������������С����ؾ����� CRTTIME>='2005-1-1' (ע��2005-1-1��ϵͳ�еķ���ϵͳ��������)����ʽ��	[ALLOTDATE]{}"
        btnFSignUABS.ToolTip = "����ȡԭֵ������ȡ0��Ʃ��UABS(-2.1)ȡ0��5.89ȡ��Ϊ5.89����ʽ��	[UABS]{������ʽ}"
        btnFSignUpRmb.ToolTip = "����ָ��������ֵ��Ӧ�����Ľ���д������ֵ�磺ҼǪ������ʰ��Ԫ������Ч�������ؿ�ֵ����ʽ��	[UABS]{����}"
        btnFSignSum.ToolTip = "����ָ���������б��ͳ�ƣ�����ͳ��ֵ����ʽ��	[SUM]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�"
        btnFSignAvg.ToolTip = "����ָ���������б��ͳ�ƣ�����ͳ��ֵ����ʽ��	[AVG]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�"
        btnFSignMax.ToolTip = "����ָ���������б��ͳ�ƣ�����ͳ��ֵ����ʽ��	[MAX]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�"
        btnFSignMin.ToolTip = "����ָ���������б��ͳ�ƣ�����ͳ��ֵ����ʽ��	[MIN]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�"
        btnFSignCount.ToolTip = "ͳ��ָ����������ָ�������ļ�¼��������ʽ��	[COUNT]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�����ֶΣ�����ȡ��һ������ֶΣ�ֻ�Ǵ�����ȡ�����ơ�"
        btnFSignOne.ToolTip = "����ָ����������ָ��������ָ���ֶε�ֵ����ʽ��	[ONE]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�����ֶΣ�����ȡ��һ������ֶΣ�ֻ�Ǵ�����ȡ�����ơ�"
        btnFSignPOne.ToolTip = "����������ָ���ֶε�ֵ����ʽ��	[PONE]{�����ֶ�}"
        btnFIRST.ToolTip = "��������������ָ�����ڵ����м�¼�еĵ�һ����¼��ָ���ֶε�ֵ����ʽ��	[FIRST]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�����ֶΣ�����ȡ��һ������ֶΣ�ֻ�Ǵ�����ȡ�����ơ�"
        btnLAST.ToolTip = "��������������ָ�����ڵ����м�¼�е����һ����¼��ָ���ֶε�ֵ����ʽ��	[LAST]{����ֶ�, ����1, ����2, ��}��˵����	�������ĸ�ʽ��A) ����ֶ�=����ֶΣ�B) ����ֶ�=������C) ����ֶ�=�����ֶΡ�����ֶΣ�����ȡ��һ������ֶΣ�ֻ�Ǵ�����ȡ�����ơ�"
        btnNORELCOND.ToolTip = "ָ����亯����SQL����в������������ֶ�������ʹ��ʱֻ����Ϊ��亯������SUM��AVG��MAX��MIN�ȱ����㺯���������һ����������ʽ��	[NORELCOND]{}"
        btnLENGTH.ToolTip = "���ص�һ��������Ϊ�ַ����ĳ��ȡ���ʽ��	[LENGTH]{����1}"
        btnFIRSTNUM.ToolTip = "���ص�һ�������еĵ�һ�����ֵ����֣�������С��������ʽ��	[FIRSTNUM]{����1}"


        'ʱ����㹫ʽ
        btnDateNow.ToolTip = "���ص�ǰʱ�䣬��ʽ�磺2005-1-20 9:30:50������ʱ�亯���е�Ψһһ���Ե�ǰʱ��Ϊ��׼��ʱ�亯������ʽ��	[NOW]{}"
        btnFSignToday.ToolTip = "���ص�ǰ���ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[TODAY]{}  [TODAY]{ʱ�����1}"
        btnFSignNowTime.ToolTip = "���ص�ǰʱ�䡣�Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NOWTIME]{}  [NOWTIME]{ʱ�����1}"
        btnTOMORROW.ToolTip = "�����������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[TOMORROW]{}  [TOMORROW]{ʱ�����1}"
        btnFSignYear.ToolTip = "����ָ�����ڣ���һ����������ָ��ʱ�������ڶ����������ļ���������ʱ�䡣��ʽ��	[YEAR]{���ڲ���1��ʱ����}"
        btnFSignMonth.ToolTip = "����ָ�����ڣ���һ����������ָ��ʱ�������ڶ����������ļ���������ʱ�䡣��ʽ��	[MONTH]{���ڲ���1��ʱ����}"
        btnFSignDate.ToolTip = "����ָ�����ڣ���һ����������ָ��ʱ�������ڶ����������ļ���������ʱ�䡣"
        btnFSignDIFFYEAR.ToolTip = "����ָ������1����һ����������ָ������2���ڶ�������������ݼ����������365��Ϊһ����λ����ʽ��	[DIFFYEAR]{���ڲ���1�����ڲ���2}"
        btnFSignDIFFMONTH.ToolTip = "����ָ������1����һ����������ָ������2���ڶ������������·ݼ�����¼����30��Ϊһ����λ����ʽ��	[DIFFMONTH]{���ڲ���1�����ڲ���2}"
        btnFSignDIFFDAY.ToolTip = "����ָ������1����һ����������ָ������2���ڶ����������������������ʽ��	[DIFFDAY]{���ڲ���1�����ڲ���2}"
        btnFSignBIRTHDAY.ToolTip = "����ָ���������ڣ���һ����������Ӧ�Ľ����������ڡ���ʽ��	[DIFFYEAR]{�������ڲ���1}"
        btnFSignWeekday.ToolTip = "����ָ�����ڣ���һ�������������ڼ����ƣ�����һ�����ڶ����������Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[WEEKDAY]{}   [WEEKDAY]{��׼���ڲ���}"
        btnWEEK2DATE.ToolTip = "�����Լ�¼����ʱ��Ϊ��׼������ָ�����ڼ������������ڵ���0��С�ڵ���7���ļ��������ڡ�������7���ڵ�ָ����������ڡ���ʽ��	[WEEK2DATE]{���ڼ������}"
        btnFSignCurYear.ToolTip = "���ص�ǰ����ݡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURYEAR]{}  [CURDAY]{ʱ�����1}"
        btnFSignCurMonth.ToolTip = "���ص�ǰ���·ݡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURMONTH]{}  [CURMONTH]{ʱ�����1}"
        btnFSignCurDay.ToolTip = "���ص�ǰ���죨�·��е��죬1��31�����Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURDAY]{}  [CURDAY]{ʱ�����1}"
        btnCURHOUR.ToolTip = "���ص�ǰ��Сʱ���Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURHOUR]{}  [CURHOUR]{ʱ�����1}"
        btnCURMIN.ToolTip = "���ص�ǰ�ķ��ӡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURMIN]{}  [CURMIN]{ʱ�����1}"
        btnCURSEC.ToolTip = "���ص�ǰ�����ӡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURSEC]{}  [CURSEC]{ʱ�����1}"
        btnPREVWK_MON.ToolTip = "������������һ�����ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[PREVWK_MON]{}  [PREVWK_MON]{ʱ�����1}"
        btnPREVWK_SAT.ToolTip = "�������������������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[PREVWK_SAT]{}  [PREVWK_SAT]{ʱ�����1}"
        btnTHISWK_MON.ToolTip = "���ر�������һ�����ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[THISWK_MON]{}  [THISWK_MON]{ʱ�����1}"
        btnTHISWK_SAT.ToolTip = "���ر��������������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[THISWK_SAT]{}  [THISWK_SAT]{ʱ�����1}"
        btnNEXTWK_MON.ToolTip = "������������һ�����ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NEXTWK_MON]{}  [NEXTWK_MON]{ʱ�����1}"
        btnNEXTWK_SAT.ToolTip = "�������������������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NEXTWK_SAT]{}  [NEXTWK_SAT]{ʱ�����1}"
        btnNNEXTWK_MON.ToolTip = "��������������һ�����ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NNEXTWK_MON]{}  [NNEXTWK_MON]{ʱ�����1}"
        btnFSignPREVMONTH_FD.ToolTip = "�������µ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[PREVMONTH_FD]{}  [PREVMONTH_FD]{ʱ�����1}"
        btnFSignCURMONTH_FD.ToolTip = "���ر��µ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURMONTH_FD]{}  [CURMONTH_FD]{ʱ�����1}"
        btnFSignNEXTMONTH_FD.ToolTip = "�������µ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NEXTMONTH_FD]{}  [NEXTMONTH_FD]{ʱ�����1}"
        btnNNEXTMONTH_FD.ToolTip = "���������µ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NNEXTMONTH_FD]{}  [NNEXTMONTH_FD]{ʱ�����1}"
        btnFSignPREVQTR_FD.ToolTip = "�����ϼ��ȵ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[PREVQTR_FD]{}  [PREVQTR_FD]{ʱ�����1}"
        btnFSignCURQTR_FD.ToolTip = "���ر����ȵ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[CURQTR_FD]{}  [CURQTR_FD]{ʱ�����1}"
        btnFSignNEXTQTR_FD.ToolTip = "�����¼��ȵ�һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NEXTQTR_FD]{}  [NEXTQTR_FD]{ʱ�����1}"
        btnPREVYEAR_FD.ToolTip = "���������һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[PREVYEAR_FD]{}  [PREVYEAR_FD]{ʱ�����1}"
        btnTHISYEAR_FD.ToolTip = "���ؽ����һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[THISYEAR_FD]{}  [THISYEAR_FD]{ʱ�����1}"
        btnNEXTYEAR_FD.ToolTip = "���������һ������ڡ��Ե�һ��������Ϊ��׼ʱ�䣬�޲������Լ�¼����ʱ����Ϊ��׼����һ������Ϊ����NOW���ʾ��ÿ�μ�¼����ʱ��Ϊ��׼����ʽ��	[NEXTYEAR_FD]{}  [NEXTYEAR_FD]{ʱ�����1}"
        btnFSignEXTRACT_YEAR.ToolTip = "���ص�һ��ʱ������е���ݡ���ʽ��	[EXTRACT_YEAR]{ʱ�����1}"
        btnFSignEXTRACT_MONTH.ToolTip = "���ص�һ��ʱ������е��·ݡ���ʽ��	[EXTRACT_MONTH]{ʱ�����1}"
        btnFSignEXTRACT_DAY.ToolTip = "���ص�һ��ʱ������е��죨�磺2005-3-28�򷵻�28������ʽ��	[EXTRACT_DAY]{ʱ�����1}"


        '�߼����㹫ʽ
        btnFSignAND.ToolTip = "�������������������������أ������׳��쳣��һ�����ڱ����¼ʱУ��������Ϣ����ʽ��	[AND]{����1, ����2, ��}"
        btnFSignOR.ToolTip = "�������������������������أ������׳��쳣��һ�����ڱ����¼ʱУ��������Ϣ����ʽ��	[AND]{����1, ����2, ��}"
        btnISNUM.ToolTip = "У��ָ���Ĳ����Ƿ������֣����򷵻�true�����򷵻�false��ʹ��ʱһ����ΪAND������OR������һ��������������ʽ��	[ISNUM]{����1}"
        btnFSignINTTIMES.ToolTip = "����У�鹫ʽ����һ���������Եڶ�������������Ϊ0�����������������أ������׳��쳣����ʽ��	[INTTIMES]{����1, ����2}"

        'SQL����
        btnFSignSQL.ToolTip = "֧�ּ��㹫ʽ����SQL��䡣SQL�������Ҫ�ⲿ����Ĳ�����||������||���ǵ�ǰ��Դ��ĳ�ֶε�ֵ����ʽ��	[SQL]{SQL���}��������	[SQL]{SELECT SUM(COL1) FROM TABLE1 WHERE COL2='����'}    [SQL]{SELECT SUM(COL1) FROM TABLE1 WHERE COL2='|COL101|' AND COL3 LIKE '%|COL102|%'}"
        '------------------------------------------------------------------------------------------------------
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim lngDictResID As Long
        If RStr("selresid") <> "" Then '�մ�ѡ����Դ�������
            lngDictResID = RLng("selresid")
        Else
            lngDictResID = VLng("PAGE_RESID")
            Session("PAGE_CMSCAL_RESOURCES") = Nothing '����
            Session("CMSPAGE_FML_VERIFYNAME") = Nothing
            Session("CMSPAGE_FML_VERIFYTIP") = Nothing
            Session("CMSPAGE_FMLDESC") = Nothing
            Session("CMSPAGE_FMLEXPR") = Nothing
            Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = Nothing
        End If

        '--------------------------------------------------------------------------
        '��ȡָ���ֶ���Դ���ֶεĹ�ʽ��Ϣ
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
        If VLng("PAGE_FMLTYPE") = FormulaType.IsVerify Then  '��У�鹫ʽ
            lblColDesc.Text = "У�鹫ʽ����"
            If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
                txtVerifyName.Text = SStr("CMSPAGE_FML_VERIFYNAME")
            Else
                txtVerifyName.Text = datFml.strCDJ_COLNAME
            End If
            lblVerifyDesc.Text = "У�鹫ʽ��ʾ��Ϣ"
            lblVerifyDesc.Visible = True
            If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
                txtVerifyTip.Text = SStr("CMSPAGE_FML_VERIFYTIP")
            Else
                txtVerifyTip.Text = datFml.strCDJ_VERIFY_TIP
            End If
            txtVerifyTip.Visible = True

            If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
                txtFormulaRight.Text = SStr("CMSPAGE_FMLDESC")
                chkIncludeArithmetic.Checked = CBool(Session("CMSPAGE_FML_INCLUDE_ARITHMETIC"))
            Else
                txtFormulaRight.Text = datFml.strCDJ_FORMULA_DESC
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = datFml.strCDJ_FMLRIGHT_EXPR

                chkIncludeArithmetic.Checked = CBool(IIf(datFml.intCDJ_NO_ARITHMETIC = 0, True, False))
                Session("CMSPAGE_FML_INCLUDE_ARITHMETIC") = chkIncludeArithmetic.Checked
            End If
        Else '�Ǽ��㹫ʽ
            lblColDesc.Text = "�ֶ�����"
            Dim strColDispName As String = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            Dim strColType As String = CTableStructure.GetColTypeDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            txtVerifyName.Text = strColDispName & " (" & strColType & ")"
            txtVerifyName.ReadOnly = True

            lblVerifyDesc.Text = ""
            lblVerifyDesc.Visible = False
            txtVerifyTip.Text = ""
            txtVerifyTip.Visible = False

            If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
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

        'If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
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

        ShowSubRelTables(CmsPass, lngDictResID) '��������Դ�б��ѡ��Ϊ��
        LoadResColumns(lngDictResID, ListBox1)
        PrepareFuncShow() 'Enable��Disable���ܼ�
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        PrepareFuncShow() 'Enable��Disable���ܼ�
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If txtFormulaRight.Text.Trim() = "" Then
                PromptMsg("��������Ч�Ĺ�ʽ!")
                Return
            End If

            If VLng("PAGE_FMLTYPE") = FormulaType.IsVerify Then '��У�鹫ʽ
                Dim strVerifyName As String = txtVerifyName.Text.Trim()
                If strVerifyName = "" Then
                    PromptMsg("У�鹫ʽ�����Ʋ���Ϊ��!")
                    Return
                End If
                Dim strVerifyTip As String = txtVerifyTip.Text.Trim()
                If strVerifyTip = "" Then
                    PromptMsg("У�鹫ʽ����ʾ��Ϣ����Ϊ��!")
                    Return
                End If

                Dim strFmlDesc As String = txtFormulaRight.Text.Trim()
                ViewState("PAGE_AIID") = CTableColCalculation.SaveFormula(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_AIID"), FormulaType.IsVerify, strVerifyName, SStr("CMSPAGE_FMLEXPR"), strFmlDesc, txtVerifyTip.Text.Trim(), FormulaOccasion.Unknown, chkIncludeArithmetic.Checked)
            Else '���㹫ʽ
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

            PromptMsg("�ɹ�ɾ���ֶεļ��㹫ʽ��")
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnBackFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackFormula.Click
        Try
            Dim strDesc As String = SStr("CMSPAGE_FMLDESC").Trim()
            Dim strExpr As String = SStr("CMSPAGE_FMLEXPR").Trim()
            If strDesc = "" Or strExpr = "" Then
                '�Ѿ�Ϊ�գ����ػع�
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
            PromptMsg("��ѡ����Ч���ֶ�")
            Return
        End If
        Try
            Dim strColName As String = ListBox1.SelectedItem.Text
            Dim intPos As Integer = strColName.IndexOf(" (")
            strColName = strColName.Substring(0, intPos).Trim()

            Dim strResID As String = ddlSubTables.Items(ddlSubTables.SelectedIndex).Value
            If VLng("PAGE_RESID") = CLng(strResID) Then '�Ǳ���Դ
                txtFormulaRight.Text = txtFormulaRight.Text & "[" & strColName & "]"
                Session("CMSPAGE_FMLDESC") = txtFormulaRight.Text
                Session("CMSPAGE_FMLEXPR") = SStr("CMSPAGE_FMLEXPR") & "[" & ListBox1.SelectedItem.Value & "]"
            Else '�����Դ���ֶΣ��������ӳɿؼ�����
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
    '������
    '---------------------------------------------------------------
    Private Sub LoadResColumns(ByVal lngResID As Long, ByRef lstCols As ListBox)
        lstCols.Items.Clear() '����б�

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
        '�������㹫ʽ�Ƿ�֧��
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

        '������ڼ��㹫ʽ�Ƿ�֧��
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


        '���߼����㹫ʽ�Ƿ�֧��
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

        '���SQL���㹫ʽ�Ƿ�֧��
        bln = CmsFunc.IsEnable("FUNC_FORMULA_SQL")
        btnFSignSQL.Enabled = bln

        '��鶨�Ƽ��㹫ʽ�Ƿ�֧��
        bln = CmsFunc.IsEnable("FUNC_FORMULA_CUST")
        btnFSignCUST.Enabled = bln
        btnFSignCUST_ID.Enabled = bln

        '�����㹫ʽ���߼����� �Ƿ�֧��
        bln = CmsFunc.IsEnable("FUNC_FORMULA_LOGIC")
        btnFSignAND.Enabled = bln
        btnFSignOR.Enabled = bln

        bln = CmsFunc.IsEnable("FUNC_FORMULA_ANYTABLE")
        lbtnChooseOtherRes.Enabled = bln
    End Sub

    '-----------------------------------------------------------------
    '��ʾ��ǰ��Դ�Ĺ�������Դ�б�
    '-----------------------------------------------------------------
    Private Sub ShowSubRelTables(ByRef pst As CmsPassport, ByVal lngNewResID As Long)
        ddlSubTables.Items.Clear()

        Dim blnNewResIDCreated As Boolean = False
        Dim alistHostRes As ArrayList
        If Session("PAGE_CMSCAL_RESOURCES") Is Nothing Then
            alistHostRes = CmsTableRelation.GetSubRelatedResources(CmsPass, VLng("PAGE_RESID"), False, False)
            alistHostRes.Insert(0, CmsPass.GetDataRes(VLng("PAGE_RESID"))) '��ӱ���ԴΪ��һ����Դ
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
        PrepareFuncShow() 'Enable��Disable���ܼ�
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
