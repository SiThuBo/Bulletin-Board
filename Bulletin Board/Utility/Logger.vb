Imports log4net
Imports System
Imports System.Reflection

Namespace BulletinBoard_OJT.Utility
    Public Class Logger
        Private Shared ReadOnly log As ILog = LogManager.GetLogger("LogFileAppender")

        Public Sub WriteLog(ByVal p_LogType As Logger.LogType, ByVal p_Module As String, ByVal p_FunctionName As String, ByVal p_LogMessage As String)
            Try

                Select Case p_LogType
                    Case Logger.LogType.Info
                        Logger.log.Info(CObj((p_Module & "," & p_FunctionName & "," & p_LogMessage)))
                    Case Logger.LogType.[Error]
                        Logger.log.[Error](CObj((p_Module & "," & p_FunctionName & "," & p_LogMessage)))
                    Case Logger.LogType.Debug
                        Logger.log.Debug(CObj((p_Module & "," & p_FunctionName & "," & p_LogMessage)))
                    Case Logger.LogType.Fatal
                        Logger.log.Fatal(CObj((p_Module & "," & p_FunctionName & "," & p_LogMessage)))
                    Case Else
                        Logger.log.Info(CObj((p_Module & "," & p_FunctionName & "," & p_LogMessage)))
                End Select

            Catch ex As Exception
                Logger.log.[Error](CObj((p_Module & "," & p_FunctionName & "," & ex.Message)))
            End Try
        End Sub

        Public Enum LogType
            Info = 1
            [Error] = 2
            Debug = 3
            Fatal = 4
        End Enum
    End Class
End Namespace