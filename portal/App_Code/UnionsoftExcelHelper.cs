using NetReusables;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebServices;

/// <summary>
/// UnionsoftExcelHelper 的摘要说明
/// </summary>
public static class UnionsoftExcelHelper
{

    static Services Resource = new Services();
        
    
    private static Hashtable GetNewHst(bool CreateNewID, string RESID, string UserID)
    {
        Hashtable hst = new Hashtable();
        if (CreateNewID)
            hst.Add("ID", TimeId.CurrentMilliseconds(30).ToString());
        hst.Add("RESID", RESID);
        hst.Add("RELID", 0);
        hst.Add("CRTID", UserID);
        hst.Add("CRTTIME", DateTime.Now);
        hst.Add("EDTID", UserID);
        hst.Add("EDTTIME", DateTime.Now);
        return hst;
    }

    /// <summary>
    /// 获取保存表的Hashtable列表（JSON数据）
    /// </summary>
    /// <param name="JArray"></param>
    /// <param name="SaveTableResid"></param>
    /// <param name="SaveTableName"></param>
    /// <param name="UserID"></param>
    /// <param name="UseFieldID"></param>
    /// <param name="SaveSameTable"></param>
    /// <param name="CreateNewHst"></param>
    /// <param name="CreateNewID"></param>
    /// <returns></returns>
    public static List<Hashtable> CommonGetHstListByJArray(string JArray, string SaveTableResid, string SaveTableName, string UserID, bool UseFieldID = true, bool SaveSameTable = true, bool CreateNewHst = true, bool CreateNewID = true)
    {
        List<Hashtable> hstList = new List<Hashtable>();
        Newtonsoft.Json.Linq.JArray jsonVal = Newtonsoft.Json.Linq.JArray.Parse(JArray) as Newtonsoft.Json.Linq.JArray;
        if (jsonVal.Count == 0) return hstList;
        List<ReadDataColumnSet> AllReadDataColumnSet = new List<ReadDataColumnSet>();
        bool IsResidTable = false;
        if (!string.IsNullOrWhiteSpace(SaveTableResid))
            IsResidTable = true;

        for (int i = 0; i < jsonVal.Count; i++)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonVal[i]);
            DataTable jsonDT = JsonConvert.DeserializeObject<DataTable>(json);

            Hashtable hst = new Hashtable();
            if (IsResidTable && CreateNewHst)
                hst = GetNewHst(CreateNewID, SaveTableResid, UserID);

            for (int j = 0; j < jsonDT.Columns.Count; j++)
            {
                if (AllReadDataColumnSet.Count == 0 || SaveSameTable)
                {
                    if (j == 0)
                    {
                        List<Field> _ResIDField = new List<Field>();

                        if (IsResidTable)
                        {
                            _ResIDField = Resource.GetFieldList(SaveTableResid).ToList();
                        }
                        else
                        {
                            _ResIDField = ReadDataColumnSet.GetTableFieldByTableName(SaveTableName);
                        }

                        AllReadDataColumnSet = new List<ReadDataColumnSet>();
                        if (_ResIDField.Count == 0) return null;
                        string[] noNeedColumn = { "RESID", "RELID", "CRTID", "CRTTIME", "EDTID", "EDTTIME" };

                        for (int c = 0; c < jsonDT.Columns.Count; c++)
                        {
                            ReadDataColumnSet ColumnSet = new ReadDataColumnSet();
                            ColumnSet.FieldName = jsonDT.Columns[c].ColumnName;
                            ColumnSet.ReadColumnName = jsonDT.Columns[c].ColumnName;
                            Field f = _ResIDField.FirstOrDefault(p => (p.Name.Equals(ColumnSet.FieldName) && !UseFieldID) || (UseFieldID && p.Description.Equals(ColumnSet.FieldName)));

                            if (f != null && !noNeedColumn.Contains(f.Name.ToUpper()))
                            {
                                ColumnSet.FieldId = f.Name;
                                if (AllReadDataColumnSet.Find(p => p.FieldId.Equals(f.Name)) == null)
                                    AllReadDataColumnSet.Add(ColumnSet);
                            }
                        }
                    }
                }
            }
            for (int c = 0; c < jsonDT.Columns.Count; c++)
            {
                ReadDataColumnSet ColumnSet = AllReadDataColumnSet.FirstOrDefault(p => p.ReadColumnName.Equals(jsonDT.Columns[c].ColumnName));
                if (ColumnSet != null)
                    hst.Add(ColumnSet.FieldId, jsonDT.Rows[0][c]);
            }

            hstList.Add(hst);
        }
        return hstList;
    }

    /// <summary>
    /// 保存表的Hashtable列表（JSON数据）
    /// </summary>
    /// <param name="JArrayHst"></param>
    /// <param name="SaveTableResid"></param>
    /// <param name="SaveTableName"></param>
    /// <param name="UserID"></param>
    /// <param name="UpdateCon"></param>
    /// <returns></returns>
    public static int CommonSaveHstList(List<Hashtable> JArrayHst, string SaveTableResid, string SaveTableName, string UserID, string UpdateCon = "")
    {
        if (string.IsNullOrWhiteSpace(UpdateCon))
        {
            return SaveNewHstByName(JArrayHst, SaveTableName);
        }
        else
        {
            return UpdateHstByName(JArrayHst, SaveTableName, UpdateCon);
        }
    }


    private static int SaveNewHstByName(Hashtable hst, string Name, string ConnectionName = "", string UpdateCon = "")
    {
        return AddORUpdateByHashtable(hst, Name, "");
    }

    public static int SaveNewHstByName(List<Hashtable> hstList, string Name, string ConnectionName = "", string UpdateCon = "")
    {
        int count = 0;
        foreach (Hashtable h in hstList)
        {
            count += SaveNewHstByName(h, Name, "");
        }
        return count;
    }

    private static int UpdateHstByName(Hashtable hst, string Name, string condition, string ConnectionName = "")
    {
        if (string.IsNullOrWhiteSpace(condition) || string.IsNullOrWhiteSpace(Name)) return 0;
        if (hst.Contains("ID")) hst.Remove("ID");
        return AddORUpdateByHashtable(hst, Name, condition);
    }

    private static int UpdateHstByName(List<Hashtable> hstList, string Name, string condition)
    {
        int num = 0;
        int count = 0;
        foreach (Hashtable h in hstList)
        {
            try
            {
                num = UpdateHstByName(h, Name, condition);
                if (num == 0)
                    count += 1;
                else
                    count += num;
            }
            catch
            {
            }
        }
        return count;
    }

    public static int AddORUpdateByHashtable(Hashtable hst, string TableName, string UpdateCon)
    {
        string str = "";
        string sql = "";
        bool IsUpdate = false;

        if (!string.IsNullOrWhiteSpace(UpdateCon))
            IsUpdate = true;

        List<Field> FieldList = ReadDataColumnSet.GetTableFieldByTableName(TableName);


        if (FieldList.Count == 0)
        {
            return -1;
        }

        StringBuilder sbField = new StringBuilder();
        StringBuilder sbValue = new StringBuilder();
        foreach (DictionaryEntry de in hst)
        {
            string field = de.Key.ToString();
            if (string.IsNullOrWhiteSpace(field))
                continue;

            string valueStr = "";
            Field f = FieldList.FirstOrDefault(p => p.Name.Equals(field));

            if (f == null)
                continue;

            int t = ReadDataColumnSet.getType(f.DataType);
            if (de.Value == null)
            {
                valueStr = " null ";
            }
            else
            {
                valueStr = de.Value.ToString();
                if (t == 0 || t == -1 || string.IsNullOrWhiteSpace(valueStr))
                {
                    valueStr = "'" + valueStr + "'";
                }
            }

            if (IsUpdate)
            {
                if (sbValue.Length == 0)
                    sbValue.Append(" SET ");
                else
                    sbValue.Append(",");
                sbValue.Append(field + "=" + valueStr);
            }
            else
            {
                if (sbField.Length == 0)
                    sbField.Append(" ( ");
                else
                    sbField.Append(",");
                sbField.Append(field);

                if (sbValue.Length == 0)
                    sbValue.Append("VALUES (");
                else
                    sbValue.Append(",");
                sbValue.Append(valueStr);
            }
        }

        if (IsUpdate)
        {
            str = " UPDATE  " + TableName;
            sql = str + sbValue.ToString() + " " + " where " + UpdateCon;
        }
        else
        {
            str = " INSERT INTO " + TableName;
            sql = str + sbField.ToString() + " ) " + sbValue.ToString() + " ) ";
        }
        string[] changePassWord = Common.getChangePassWord();
        return Resource.ExecuteSql(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
    }

}

public class SaveInfoByReadDataColumn
{
    /// <summary>
    /// 查询出来的数据
    /// </summary>
    public DataTable DT { get; set; }
    /// <summary>
    /// 表资源ID
    /// </summary>
    public string SaveTableResid { get; set; }
    public string SaveTableName { get; set; }

    public List<ReadDataColumnSet> ReadDataColumnSet { get; set; }
    public bool UseFieldID { get; set; }
    public bool CreateNewID { get; set; }
    public string UserID { get; set; }
    public bool UseSaveDT { get; set; }

}

public class ReadDataColumnSet
{
    /// <summary>
    /// 读取Excel表头
    /// </summary>
    public string ReadColumnName { get; set; }
    public string FieldName { get; set; }
    public Type DataBaseFieldType { get; set; }
    public string FieldId { get; set; }
    public int ColumnsIndex { get; set; }
    public int FieldType { get; set; }

     
    public static List<ReadDataColumnSet> GetList(string MatchingFieldArrStr)
    {
        List<ReadDataColumnSet> list = new List<ReadDataColumnSet>();
        if (string.IsNullOrWhiteSpace(MatchingFieldArrStr)) return list;

        string[] MatchingFieldArr = MatchingFieldArrStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < MatchingFieldArr.Length; i++)
        {
            var arrstr = MatchingFieldArr[i];
            if (string.IsNullOrWhiteSpace(arrstr) || !arrstr.Contains("#")) continue;

            string[] FieldArr = arrstr.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

            if (string.IsNullOrWhiteSpace(FieldArr[0]) || string.IsNullOrWhiteSpace(FieldArr[1])) continue;

            list.Add(new ReadDataColumnSet()
            {
                ReadColumnName = FieldArr[0],
                FieldName = FieldArr[1]
            });
        }
        return list;
    }

    public static List<ReadDataColumnSet> GetTableColumns(DataTable argDT)
    {
        List<ReadDataColumnSet> TableColumnList = new List<ReadDataColumnSet>();

        for (int j = 0; j < argDT.Columns.Count; j++)
        {
            TableColumnList.Add(new ReadDataColumnSet()
            {
                ReadColumnName = argDT.Columns[j].ColumnName,
                ColumnsIndex = j
            });
        }
        return TableColumnList;
    }

    public static List<Field> GetTableFieldByTableName(string TableName)
    {
        List<Field> FieldList = new List<Field>();

        if (string.IsNullOrEmpty(TableName)) return FieldList;
        string sql = "select * from Funciton_GetDataBaseColumn('" + TableName + "')";
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        DataSet ds = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow vDr in ds.Tables[0].Rows)
                {
                    FieldList.Add(new Field()
                    {
                        Name = vDr["ColumnName"].ToString(),
                        Description = vDr["ColumnName"].ToString(),
                        DataType = vDr["SystemTypeName"].ToString()
                        //SystemTypeName = vDr["SystemTypeName"].ToString(),
                        //ColumnType = Type.GetType(ChangeToCSharpType(vDr["SystemTypeName"].ToString())),
                        //IsNullable = Convert.ToBoolean(vDr["IsNullable"])
                    });
                }
            }
        }
        return FieldList;
    }

    /// <summary>
    /// 数据库中与C#中的数据类型对照
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string ChangeToCSharpType(string type)
    {
        string reval = string.Empty;
        switch (type.ToLower())
        {
            case "int":
                reval = "System.Int32";
                break;
            case "text":
                reval = "System.String";
                break;
            case "bigint":
                reval = "System.Int64";
                break;
            case "binary":
                reval = "System.Byte[]";
                break;
            case "bit":
                reval = "System.Boolean";
                break;
            case "char":
                reval = "System.String";
                break;
            case "datetime":
                reval = "System.DateTime";
                break;
            case "decimal":
                reval = "System.Decimal";
                break;
            case "float":
                reval = "System.Double";
                break;
            case "image":
                reval = "System.Byte[]";
                break;
            case "money":
                reval = "System.Decimal";
                break;
            case "nchar":
                reval = "System.String";
                break;
            case "ntext":
                reval = "System.String";
                break;
            case "numeric":
                reval = "System.Decimal";
                break;
            case "nvarchar":
                reval = "System.String";
                break;
            case "real":
                reval = "System.Single";
                break;
            case "smalldatetime":
                reval = "System.DateTime";
                break;
            case "smallint":
                reval = "System.Int16";
                break;
            case "smallmoney":
                reval = "System.Decimal";
                break;
            case "timestamp":
                reval = "System.DateTime";
                break;
            case "tinyint":
                reval = "System.Byte";
                break;
            case "uniqueidentifier":
                reval = "System.Guid";
                break;
            case "varbinary":
                reval = "System.Byte[]";
                break;
            case "varchar":
                reval = "System.String";
                break;
            case "Variant":
                reval = "Object";
                break;
            default:
                reval = "System.String";
                break;
        }
        return reval;
    }

    public static int getType(string type)
    {
        int dataType = -1;
        switch (type.ToLower())
        {
            case "int":
                dataType = 2;
                break;
            case "text":
                dataType = 0;
                break;
            case "bigint":
                dataType = 2;
                break;
            case "binary":
                dataType = 0;
                break;
            case "bit":
                dataType = 2;
                break;
            case "char":
                dataType = 0;
                break;
            case "datetime":
                dataType = 0;
                break;
            case "decimal":
                dataType = 3;
                break;
            case "float":
                dataType = 3;
                break;
            case "image":
                dataType = 0;
                break;
            case "money":
                dataType = 3;
                break;
            case "nchar":
                dataType = 0;
                break;
            case "ntext":
                dataType = 0;
                break;
            case "numeric":
                dataType = 3;
                break;
            case "nvarchar":
                dataType = 0;
                break;
            case "real":
                dataType = 0;
                break;
            case "smalldatetime":
                dataType = 0;
                break;
            case "smallint":
                dataType = 2;
                break;
            case "smallmoney":
                dataType = 3;
                break;
            case "timestamp":
                dataType = 0;
                break;
            case "tinyint":
                dataType = 2;
                break;
            case "uniqueidentifier":
                dataType = 0;
                break;
            case "varbinary":
                dataType = 0;
                break;
            case "varchar":
                dataType = 0;
                break;
            case "Variant":
                dataType = 0;
                break;
            default:
                dataType = 0;
                break;
        }
        return dataType;        
    }
}

