Imports System
Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography

Namespace BulletinBoard_OJT.Utility
    Public Class Utility
        Private Shared ReadOnly _md5 As MD5 = MD5.Create()
        Public Shared Function GetFormattedText(ByVal text As String) As String
            If String.IsNullOrEmpty(text) Then Return String.Empty
            Return Regex.Replace(HttpUtility.HtmlEncode(text), "\r\n|\n|\r", "<br/>")
        End Function

        Public Shared Function GetPreviewText(ByVal textObject As Object) As String
            Dim text As String = TryCast(textObject, String)
            If String.IsNullOrEmpty(text) Then Return String.Empty
            Dim extract As String = text.Substring(0, Math.Min(150, text.Length))
            If text.Length > 150 Then extract += "..."
            extract = Regex.Replace(HttpUtility.HtmlEncode(extract), "\r\n|\n|\r", "<br>")
            Return extract
        End Function


        Public Shared Function GetMd5Hash(source As String) As String

            Dim data = _md5.ComputeHash(Encoding.UTF8.GetBytes(source))
            Dim sb As New StringBuilder()
            sb.Append(data)

            Return sb.ToString()

        End Function

        Public Shared Function VerifyMd5Hash(source As String, hash As String) As Boolean

            Dim sourceHash = GetMd5Hash(source)
            Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
            Return If(comparer.Compare(sourceHash, hash) = 0, True, False)

        End Function

        Public Shared Function GetMd5HashBase64(source As String) As String

            Dim data = _md5.ComputeHash(Encoding.UTF8.GetBytes(source))
            Return Convert.ToBase64String(data)

        End Function

        Public Shared Function VerifyMd5HashBase64(source As String, hash As String) As Boolean

            Dim sourceHash = GetMd5HashBase64(source)
            Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
            Return If(comparer.Compare(sourceHash, hash) = 0, True, False)

        End Function
    End Class
End Namespace
