Public Class FormLogin

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
        Else
            CheckBox1.Checked = False
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If txtUsername.Text = "admin" And txtPassword.Text = "admin" Then
            MsgBox("Selamat, Berhasil Login")
            Me.Hide()
            txtUsername.Text = ""
            txtPassword.Text = ""
            FormMenu.Show()
        Else
            txtUsername.Text = ""
            txtPassword.Text = ""
            MsgBox("Gagal Login, Cek Username dan Password")
        End If
    End Sub
End Class