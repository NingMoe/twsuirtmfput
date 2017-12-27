copy /y ..\..\Resource\AddFlow3.ocx AddFlow3.ocx
copy /y ..\..\Resource\FlowDiagram.dll FlowDiagram.dll
copy /y ..\..\Resource\InterFlow.ocx InterFlow.ocx

copy /y ..\..\Resource\Interop*.*

%windir%\system32\regsvr32 AddFlow3.ocx
%windir%\system32\regsvr32 FlowDiagram.dll
%windir%\system32\regsvr32 InterFlow.ocx