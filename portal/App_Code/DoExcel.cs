using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Aspose.Cells;
/// <summary>
/// Excel导出及导入操作类
/// </summary>
public class DoExcel
{
    
    /// <summary>
    /// 将DatTable导出Excel，动态创建表头和列
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="SheetName">工作薄sheet名称</param>
    /// <returns>Excel文件流</returns>
    public byte[] ExportExcel(DataTable dt, string SheetName)
    {
        //创建Excel工作薄
        Workbook workbook = new Workbook();

        //创建Sheet
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = SheetName.Trim().Equals("") ? "Sheet1" : SheetName;
        worksheet.IsSelected = true;
        
        Cells cells = worksheet.Cells;

        //将数据填充到Excel中[yyyy-MM-dd 为日期格式]
        cells.ImportDataTable(dt, true, 0, 0, dt.Rows.Count, dt.Columns.Count, false, "yyyy-MM-dd",false);

        // 为表头单元格添加样式
        Aspose.Cells.Style style = workbook.CreateStyle();
        style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;  //设置居中
        style.Font.Size = 10;//文字大小       
        style.IsTextWrapped = true;//自动换行 
        style.Font.IsBold = true;
        style.HorizontalAlignment = TextAlignmentType.Center;//文字居中
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            worksheet.Cells[0, i].SetStyle(style);
            worksheet.Cells.SetColumnWidth(i, 15);//列固定宽
            //worksheet.AutoFitColumn(i);//列自动宽
        }

        worksheet.Cells.SetRowHeight(0, 20);
        //冻结行和列
        worksheet.FreezePanes(1, 0, 1, 0);
        //设置当前显示的工作表
        workbook.Worksheets.ActiveSheetIndex = 0;

        //将填充的数据，转为流
        MemoryStream ms = new MemoryStream();
        workbook.Save(ms, new OoxmlSaveOptions(SaveFormat.Xlsx));
        byte[] bt = ms.ToArray();
        ms.Close();

        return bt;
    }

    ///<summary>
    /// 将DatTable导出Excel
    /// </summary>
    /// <param name="dt">数据源（导出一个sheet）</param>
    /// <param name="TemplateExcelPath">Excel模板地址【根据模板中的格式、表头、列导出数据，Excel模板版本需为：2007及以上】</param>
    /// <param name="dic">Excel中固定列的键值集合</param>
    /// <returns>Excel文件流</returns>
    public byte[] ExportExcel(DataTable dt, string TemplateExcelPath,  Dictionary<string, string> dic)
    {
        WorkbookDesigner designer = new WorkbookDesigner();
        designer.Workbook = new Aspose.Cells.Workbook(TemplateExcelPath);

        if (dic != null)
        {
            //键值对取值  
            foreach (var k in dic.Keys)
            {
                //给Excel模板中固定的cell赋值
                designer.SetDataSource(k, dic[k]);
            }
        }        
       
        //绑定数据
        designer.SetDataSource(dt);
        designer.Process();
        //触发Excel中的计算公式
        designer.CalculateFormula = true;
        
        MemoryStream ms = new MemoryStream();
        designer.Workbook.Save(ms, Aspose.Cells.SaveFormat.Xlsx);
        byte[] bt = ms.ToArray();
        ms.Close();
        return bt;
    }

    ///<summary>
    /// 将DatTable导出Excel
    /// </summary>
    /// <param name="ds">数据源（导出多个sheet）</param>
    /// <param name="TemplateExcelPath">Excel模板地址【根据模板中的格式、表头、列导出数据，Excel模板版本需为：2007及以上】</param>
    /// <param name="dic">Excel中固定列的键值集合</param>
    /// <returns>Excel文件流</returns>
    public byte[] ExportExcel(DataSet ds, string TemplateExcelPath, Dictionary<string, string> dic)
    {
        WorkbookDesigner designer = new WorkbookDesigner();
        designer.Workbook = new Aspose.Cells.Workbook(TemplateExcelPath);

        if (dic != null)
        {
            //键值对取值  
            foreach (var k in dic.Keys)
            {
                //给Excel模板中固定的cell赋值               
                designer.SetDataSource(k.ToUpper(), dic[k]);            
            }
        }

        int j = 1;
        foreach (DataTable dt in ds.Tables)
        {
            //指定数据要填充的sheet名称
            dt.TableName = "sheet" + j;
            //绑定数据
            designer.SetDataSource(dt);
            j++;
        }
      
        designer.Process();
        //触发Excel中的计算公式
        designer.CalculateFormula = true;
        
        MemoryStream ms = new MemoryStream();
        designer.Workbook.Save(ms, Aspose.Cells.SaveFormat.Xlsx);//Excel版本
        byte[] bt = ms.ToArray();
        ms.Close();
        return bt;
    }

	
    /// <summary>
    /// 导入Excel，将Excel数据转为DataTable
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <returns>结果集</returns>
    public DataTable ImportExcel(System.IO.Stream stream)
    {
        //获取上传的文件，并转为文件流
        /*HttpPostedFile postedFile = Request.Files["PrjFilePath"];
        Stream stream = postedFile.InputStream;
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);*/

        /*Workbook book = new Workbook();
        book.Open(fullFilename);
        Worksheet sheet = book.Worksheets[0];
        Cells cells = sheet.Cells;
        //获取excel中的数据保存到一个datatable中
        DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);        
        return dt_Import;*/

        Workbook book = new Workbook(stream);
        Worksheet sheet = book.Worksheets[0];
        Cells cells = sheet.Cells;
        DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow+1 , cells.MaxDataColumn + 1, true);
        return dt;
    }

}



public class GetExcelHelperOptionModel
{
    /// <summary>
    /// 关键字
    /// </summary>
    public string KeyType { get; set; }
    /// <summary>
    /// 查询语句
    /// </summary>
    public string querySql { get; set; }
    /// <summary>
    /// 查询出来的数据
    /// </summary>
    public DataSet _DataSet { get; set; }
    /// <summary>
    /// 是否包含标题
    /// </summary>
    public bool HasTitle { get; set; }
    /// <summary>
    /// 是否包含序号列
    /// </summary>
    public bool HasSerialNumber { get; set; }
    /// <summary>
    /// 是否包含汇总列
    /// </summary>
    public bool HasSummary { get; set; }
    /// <summary>
    /// 报表标题
    /// </summary>
    public string ReportTitle { get; set; }
    /// <summary>
    /// 报表副标题
    /// </summary>
    public string ReportSubtitle { get; set; }
    /// <summary>
    /// 读取的工作表名字符串
    /// </summary>
    public string ReadSheetStr { get; set; }
    /// <summary>
    /// 开始行（从1开始，包括标题行，不包括报表标题行）
    /// </summary>
    public int StartRowNum { get; set; }
    /// <summary>
    /// 开始列（从1开始，不包括序号列）
    /// </summary>
    public int StartColumnNum { get; set; }
    /// <summary>
    /// 文件物理路径
    /// </summary>
    public string FilePath { get; set; }
    /// <summary>
    /// 请求类型
    /// 0：读取Excel
    /// 1：标准写入Excel
    /// </summary>
    public int GetAjaxType { get; set; }
    /// <summary>
    /// 是否复制读取的文件
    /// </summary>
    public bool IsCopyReadExcel { get; set; }
    /// <summary>
    /// 复制读取文件的路径
    /// </summary>
    public string CopyReadExcelPath { get; set; }
    /// <summary>
    /// 是否为Port路径
    /// </summary>
    public bool IsPortalPath { get; set; }
    /// <summary>
    /// 如果存在此工作簿，是否替换工作簿
    /// </summary>
    public bool IsReplaceWorkBook { get; set; }
    /// <summary>
    /// 是否操作原表
    /// </summary>
    public bool IsUseOldSheet { get; set; }
    /// <summary>
    /// 是否自动调整列宽
    /// </summary>
    public bool IsSetColumnWidth { get; set; }
    /// <summary>
    /// 替换字典集
    /// </summary>
    public string ReplaceDictionaryJson { get; set; }
    /// <summary>
    /// 单元格赋值
    /// </summary>
    public List<CellInfos> CellInfos { get; set; }
}


/// <summary>
/// 多条数据
/// </summary>
public class CellInfos
{
    /// <summary>
    /// 序号
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public List<CellInfo> CellInfo { get; set; }
}



public class CellInfo
{
    public CellInfo()
    {

    }

    public CellInfo(int RowIndex, int ColumnIndex, string ColumnName)
    {
        this.RowIndex = RowIndex;
        this.ColumnIndex = ColumnIndex;
        this.ColumnName = ColumnName;
    }

    /// <summary>
    /// 行位置
    /// </summary>
    public int RowIndex { get; set; }
    /// <summary>
    /// 列位置
    /// </summary>
    public int ColumnIndex { get; set; }
    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 单元格值
    /// </summary>
    public object Value { get; set; }

}
public class GetWordHelperOptionModel
{
    /// <summary>
    /// 关键字
    /// </summary>
    public string KeyType { get; set; }
    /// <summary>
    /// 查询语句
    /// </summary>
    public string querySql { get; set; }
    /// <summary>
    /// 查询出来的数据
    /// </summary>
    public DataSet _DataSet { get; set; }
    /// <summary>
    /// 打开文件物理路径
    /// </summary>
    public string OpenFilePath { get; set; }
    /// <summary>
    /// 保存文件物理路径
    /// </summary>
    public string SaveFilePath { get; set; }
    /// <summary>
    /// 如果存在此文档，是否替换文档
    /// </summary>
    public bool IsReplaceWord { get; set; }
    /// <summary>
    /// 替换字典集
    /// </summary>
    public string ReplaceDictionaryJson { get; set; }

}

