Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class frmLogin
    Private client As New HttpClient()
    ' Note: Check your port number!
    Private apiUrl As String = "https://localhost:44312/api/auth/login"

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Security Protocol Fix (Crucial for Localhost)
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
        System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(se, cert, chain, sslerror) True
    End Sub

    Private Async Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Trim(UsernameTextBox.Text) = "" Or Trim(PasswordTextBox.Text) = "" Then
            MsgBox("Please Enter Both Fields!", vbInformation, "Note")
            Exit Sub
        End If

        Dim loginData As New Dictionary(Of String, String) From {
            {"Username", UsernameTextBox.Text},
            {"Password", PasswordTextBox.Text}
        }

        Dim json As String = JsonConvert.SerializeObject(loginData)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Try
            Dim response = Await client.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                MsgBox("Login Successful!", vbInformation, "Note")

                ' Set Status in Main Form (Assuming frmMain is accessible)
                frmMain.status.Items(0).Text = "Login as : " & Trim(UsernameTextBox.Text)

                Me.Hide()
                frmMain.ShowDialog()
            Else
                MsgBox("Login Failed! Invalid Username or Password.", vbCritical, "Access Denied")
            End If
        Catch ex As Exception
            MsgBox("Server Connection Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class