
Imports System.Security.Principal
Imports BulletinBoard_OJT.BusinessLogic
Imports BulletinBoard_OJT.DB_Entity
Imports BulletinBoard_OJT.Utility

<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config", Watch:=True)>
Namespace Controllers
    Public Class LoginController

        Inherits Controller

        Private log As Logger = New Logger()

        Private db As New BulletinBoardEntities
        ' GET: Login
        Function Login() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <AllowAnonymous>
        Function Login(user As Users) As ActionResult

            Try


                Dim validateUserResult As Int32
                validateUserResult = Users_BLL.ValidateUser(user.Email, user.Password)
                Dim message As String
                message = String.Empty



                Select Case validateUserResult
                    Case 0

                        message = "Username and/or password is incorrect."
                    Case -2

                        message = "Account has not been activated."

                    Case Else

                        log.WriteLog(Logger.LogType.Info, "LoginController", "Login", "Login Successful")
                        message = "Login Success"
                        FormsAuthentication.SetAuthCookie(user.Email, True)
                        Return RedirectToAction("Index", "Posts")


                End Select
                ViewBag.Message = message
                Return View()
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "LoginController", "Login", detailErrorLog)

            End Try


        End Function

        Function Logout() As ActionResult
            FormsAuthentication.SignOut()
            Session.Abandon()
            ' Clear the authentication cookie
            Dim c As New HttpCookie(FormsAuthentication.FormsCookieName, "")
            c.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(c)
            ' Clear session cookie - shouldn't be needed, but hey-ho.
            c = New HttpCookie("ASP.NET_SessionId", "")
            c.Expires = DateTime.Now.AddYears(-1)
            Response.Cookies.Add(c)
            FormsAuthentication.RedirectToLoginPage()

        End Function

    End Class



End Namespace

