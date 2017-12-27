using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using WebServices;

public class ExcelOperate
{
    public static DataTable SetDaoRuAccess(DataTable  dt, string ResID)
    {
        string dtName = "";
        Services Resource = new Services();
        string repDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string[] changePassWord = Common.getChangePassWord();
        DataTable tbName = Resource.SelectData(" select id,PID,res_table from cms_resource where id =" + ResID, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        if (tbName.Rows.Count>0)
        {
            if (tbName.Rows[0]["res_table"].ToString().IndexOf("VIEW_") != -1)
            {
                tbName = Resource.SelectData(" select id,PID,res_table from cms_resource where id =" + tbName.Rows[0]["PID"].ToString(), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                if (tbName.Rows[0]["res_table"].ToString().IndexOf("VIEW_") != -1)
                {
                    tbName = Resource.SelectData(" select id,PID,res_table from cms_resource where id =" + tbName.Rows[0]["PID"].ToString(), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                    if (tbName.Rows[0]["res_table"].ToString().IndexOf("VIEW_") != -1)
                    {
                        tbName = Resource.SelectData(" select id,PID,res_table from cms_resource where id =" + tbName.Rows[0]["PID"].ToString(), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                        if (tbName.Rows[0]["res_table"].ToString().IndexOf("VIEW_") != -1)
                        {
                            tbName = Resource.SelectData(" select id,PID,res_table from cms_resource where id =" + tbName.Rows[0]["PID"].ToString(), changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
                        }
                    }
                }
            }
        }
        if (tbName.Rows.Count>0)
        {
            dtName = tbName.Rows[0]["res_table"].ToString();
            ResID = tbName.Rows[0]["id"].ToString();
        }
        DataTable ColDT = Resource.SelectData("SELECT cd_colname,cd_dispname FROM CMS_TABLE_DEFINE where CD_REsID=" + ResID, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        //CSS基础数据
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName != "ID" && dt.Columns[i].ColumnName != "RESID" && dt.Columns[i].ColumnName != "RELID" && dt.Columns[i].ColumnName != "EDTID" && dt.Columns[i].ColumnName != "EDTTIME" && dt.Columns[i].ColumnName != "CRTID" && dt.Columns[i].ColumnName != "CRTTIME")
            {
              //  dt.Columns[i].ColumnName = "U_" + dt.Columns[i].ColumnName;
                DataRow[] drs = ColDT.Select(" cd_dispname='" + dt.Columns[i].ColumnName.Trim()+ "'");
                if (drs.Length > 0)
                {
                    dt.Columns[i].ColumnName = drs[0]["cd_colname"].ToString();
                }
            }
        }
        dt.Columns.Add("ID");
        DataColumn dc = new DataColumn();
        dc.DefaultValue = ResID;
        dc.ColumnName = "RESID";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DefaultValue = 0;
        dc.ColumnName = "RELID";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DefaultValue = "test";
        dc.ColumnName = "CRTID";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DefaultValue = repDate;
        dc.ColumnName = "CRTTIME";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DefaultValue = "test";
        dc.ColumnName = "EDTID";
        dt.Columns.Add(dc);
        dc = new DataColumn();
        dc.DefaultValue = repDate;
        dc.ColumnName = "EDTTIME";
        dt.Columns.Add(dc);
        //'这个sql语句是用来查询数据库里面列的序列的SqlBulkCopy 是根据列的序列来插入数据的
        string sql = "select * from " + dtName + " where 1=0";
        DataTable newDt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        newDt.TableName = dtName;
        for (int i = 0; i < dt.Rows.Count ; i++)
		{
            newDt.Rows.Add();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Columns[j].ColumnName.ToUpper() == "ID"){
                    newDt.Rows[i]["ID"] = Resource.GetCurrentMilliseconds();
                }
                else
                {
                    string str = dt.Rows[i][dt.Columns[j].ColumnName].ToString().Replace("'", "").ToString();
                    for (int k = 0; k < newDt.Columns.Count; k++)
                    {
                        if (dt.Columns[j].ColumnName.ToString().Trim() == newDt.Columns[k].ColumnName.ToString().Trim())
                        {
                            if (str != "")
                            {
                                newDt.Rows[i][newDt.Columns[k].ColumnName] = str;
                            }
                        }
                    }
                }
            }
		}
       
        try
        {
            SqlBulkCopy sbc = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["StoreProcedureConnection"].ConnectionString);
            sbc.DestinationTableName = dtName;
            sbc.BatchSize = 500;
            sbc.BulkCopyTimeout = 5000;
            sbc.WriteToServer(newDt);
            sbc.Close();
        }
        catch (Exception ex)
        {
            dt = new DataTable();
            return dt;
        }
        return dt;
    }

}
