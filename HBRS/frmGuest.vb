Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Public Class frmGuest
    Private client As New HttpClient()

    ' check the properties of your API project for api url
    Private apiUrl As String = "https://localhost:44312/api/guest"

    Private Sub frmGuest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGuestsAsync()
        TabControl1.SelectTab(0)
    End Sub

    Private Async Sub LoadGuestsAsync()
        Await display_guest()
    End Sub

    Private Async Sub bttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bttnSave.Click
        Dim guestData As New Dictionary(Of String, String) From {
            {"FName", Trim(txtFName.Text)},
            {"MName", Trim(txtMName.Text)},
            {"LName", Trim(txtLName.Text)},
            {"Address", Trim(txtAddress.Text)},
            {"Contact", Trim(txtNumber.Text)},
            {"Email", Trim(txtEmail.Text)},
            {"Gender", cboGender.Text}
        }

        Dim json As String = JsonConvert.SerializeObject(guestData)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Try
            Dim response = Await client.PostAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                MsgBox("Guest Added Successfully!", vbInformation, "Success")
                clear_text()
                Await display_guest()
            Else
                MsgBox("Server Error: " & response.ReasonPhrase, vbExclamation, "Failed")
            End If
        Catch ex As Exception
            MsgBox("Could not connect to API: " & ex.Message)
        End Try
    End Sub

    Private Async Function display_guest() As Task
        Try
            Dim jsonResponse As String = Await client.GetStringAsync(apiUrl)

            Dim dt As DataTable = JsonConvert.DeserializeObject(Of DataTable)(jsonResponse)

            ' 3. Show in ListView
            lvGuest.Items.Clear()
            For Each row As DataRow In dt.Rows
                Dim lv As New ListViewItem
                lv.Text = row("ID").ToString()
                lv.SubItems.Add(row("GuestFName").ToString())
                lv.SubItems.Add(row("GuestMName").ToString())
                lv.SubItems.Add(row("GuestLName").ToString())
                lv.SubItems.Add(row("GuestAddress").ToString())
                lv.SubItems.Add(row("GuestContactNumber").ToString())
                lv.SubItems.Add(row("Status").ToString())
                lvGuest.Items.Add(lv)
            Next
        Catch ex As Exception
            ' If API is down, this catches it
            MsgBox("Error fetching list: " & ex.Message)
        End Try
    End Function

    Private Sub bttnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bttnCancel.Click
        clear_text()
    End Sub

    Private Sub clear_text()
        txtFName.Clear()
        txtMName.Clear()
        txtLName.Clear()
        txtAddress.Clear()
        txtNumber.Clear()
        txtEmail.Clear()
    End Sub

    Private Sub txtFName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFName.KeyPress
        ' Allow Letters, Backspace, and Space (WhiteSpace)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLName.KeyPress
        ' Allow Letters, Backspace, and Space
        If Not Char.IsLetter(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMName.KeyPress
        ' Allow Letters, Backspace, and Space
        If Not Char.IsLetter(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddress.KeyPress
        ' Address usually needs Numbers too, but adhering to your previous logic:
        ' Allow Letters, Backspace, and Space
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso Not e.KeyChar = ","c AndAlso Not e.KeyChar = "."c AndAlso Not e.KeyChar = "-"c Then
            ' I updated this slightly to allow Digits, Commas, and Periods which are common in addresses
            ' If you want strictly letters only, use the same logic as Name.
            e.Handled = True
        End If
    End Sub

    Private Sub txtNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        ' Allow only numeric key presses and the Backspace key
        If (Not Char.IsDigit(e.KeyChar)) AndAlso (e.KeyChar <> ControlChars.Back) Then
            e.Handled = True
        End If
    End Sub

End Class