ALTER   VIEW VIEW_WF_START
AS
Select  WF_USERTASK.ID AS USERTASKID,
	WF_USERTASK.CREATETIME,
	WF_USERTASK.DEALTIME,
	WF_USERTASK.EMPCODE,
	WF_USERTASK.EMPNAME,
	WF_TASK.ISEXIGENCE,
	WF_TASK.WF_INSTANCE_ID AS FLOWINSTID,
	(SELECT TOP 1 FLOWID FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS FLOWID, 
	(SELECT TOP 1 FLOWNAME FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS FLOWNAME,
	(SELECT TOP 1 TASKSTATUS FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS FLOWTASKSTATUS, 
	(SELECT TOP 1 TaskStatus FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS TASKSTATUS,
	(SELECT TOP 1 MAINFIELDVALUE FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS MAINFIELDVALUE,
	(SELECT ISUSELESS FROM WF_INSTANCE WHERE WF_INSTANCE.ID=WF_TASK.WF_INSTANCE_ID) AS ISUSELESS
FROM WF_TASK,WF_USERTASK 
WHERE WF_TASK.ID=WF_USERTASK.TASKID 
	AND NodeType=0
	--AND WF_USERTASK.SAVESTATUS=0