SELECT   
        col.colorder AS 序号 ,  
        col.name AS 列名 ,  
        ISNULL(ep.[value], '') AS 列说明 ,  
        t.name AS 数据类型 ,  
        '['+ rtrim(cast(col.prec as varchar)) +case when ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0)=0 then '' else ','+rtrim(cast(ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) as varchar))  end  +']'   AS 长度 ,   
        
        CASE WHEN EXISTS ( SELECT   1  
                           FROM     dbo.sysindexes si  
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id  
                                                              AND si.indid = sik.indid  
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id  
                                                              AND sc.colid = sik.colid  
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name  
                                                              AND so.xtype = 'PK'  
                           WHERE    sc.id = col.id  
                                    AND sc.colid = col.colid ) THEN '√'  
             ELSE ''  
        END AS 主键 ,  
        CASE WHEN col.isnullable = 1 THEN '√'  
             ELSE ''  
        END AS 允许空 ,  
        ISNULL(comm.text, '') AS 默认值  , CASE WHEN col.colorder = 1 THEN obj.name  
                  ELSE ''  
             END AS 表名
FROM    dbo.syscolumns col  
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype  
        inner JOIN dbo.sysobjects obj ON col.id = obj.id  
                                         AND obj.xtype = 'U'  
                                         AND obj.status >= 0  
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id  
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id  
                                                      AND col.colid = ep.minor_id  
                                                      AND ep.name = 'MS_Description'  
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id  
                                                         AND epTwo.minor_id = 0  
                                                         AND epTwo.name = 'MS_Description'  
WHERE   obj.name = 'EquipmentMaintenanceComments'--表名  
ORDER BY col.colorder ;  