-------------------------------------------------------------
--É¾³ýÁ÷³ÌÊµÀý
--CHENYU 2010-10-28
-------------------------------------------------------------
Create Procedure SP_WorkflowInstanceDelete
	@WorkflowInstKey NVARCHAR(50)
AS
	DELETE FROM WF_USERTASK WHERE [TASKID] IN (SELECT [ID] FROM WF_TASK WHERE WF_INSTANCE_ID=@WorkflowInstKey)
	DELETE FROM WF_TASK WHERE WF_INSTANCE_ID=@WorkflowInstKey
	DELETE FROM WF_INSTANCE WHERE [ID]=@WorkflowInstKey



