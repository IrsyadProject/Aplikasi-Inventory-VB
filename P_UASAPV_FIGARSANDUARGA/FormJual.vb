Imports System.Data.OleDb
Public Class FormJual
    Dim conn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim LokasiDB As String
    Sub Koneksi()
        LokasiDB = "Provider = Microsoft.Jet.OleDB.4.0;Data Source=dbbarang.mdb"
        conn = New OleDbConnection(LokasiDB)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub
    Sub Tombol(ByVal Nilai As Boolean)
        btnBaru.Enabled = Nilai
        btnSimpan.Enabled = Not Nilai
        btnEdit.Enabled = Nilai
        btnHapus.Enabled = Nilai
        btnBatal.Enabled = Not Nilai
        btnTutup.Enabled = Nilai
    End Sub
    Sub Matikan(ByVal Nilai As Boolean)
        txtNoNota.Enabled = Nilai
        cmbKodeBrg.Enabled = Nilai
        txtHargaJual.Enabled = Nilai
        txtJumlahJual.Enabled = Nilai
        txtTotal.Enabled = Nilai
    End Sub
    Sub Kosongkan()
        txtNoNota.Text = ""
        cmbKodeBrg.Text = ""
        txtHargaJual.Text = 0
        txtJumlahJual.Text = 0
        txtTotal.Text = 0
    End Sub

    Sub IsiCombo()
        Koneksi()
        Try
            'combobox barang
            cmd = New OleDb.OleDbCommand("select kodebrg from barang order by kodebrg", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbKodeBrg.Items.Add(dr("KodeBrg"))
            End While
        Catch ex As Exception

        End Try
    End Sub

    Sub TampilData()
        Koneksi()
        da = New OleDbDataAdapter("select * from jual", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "jual")
        DataGridView1.DataSource = (ds.Tables("jual"))
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 80
    End Sub

    Private Sub FormJual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
        Call IsiCombo()
    End Sub

    Private Sub cmbKodeBrg_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKodeBrg.LostFocus
        Call Koneksi()
        cmd = New OleDb.OleDbCommand("select * from barang where KodeBrg='" & _
                                     cmbKodeBrg.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            MsgBox("Maaf Kode Barang Tidak Ada")
        Else
            lblNamaBrg.Text = dr!NamaBrg
            txtHargaJual.Text = dr!HargaJual
        End If
    End Sub

    Private Sub txtJumlahJual_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtJumlahJual.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtJumlahJual_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJumlahJual.LostFocus
        txtTotal.Text = txtHargaJual.Text * txtJumlahJual.Text
    End Sub

    Private Sub btnBaru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Call Matikan(True)
        txtNoNota.Focus()
        Call Tombol(False)
        Call Kosongkan()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub btnTutup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Koneksi()
        Dim strHapus As String
        strHapus = "delete from jual where NoNota ='" & txtNoNota.Text & "'"
        cmd = New OleDb.OleDbCommand(strHapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Penjualan Berhasil dihapus")
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtNoNota.Text = "" Then
            MsgBox("No Nota masih kosong boss", vbExclamation, "Kesalahan")
            txtNoNota.Focus()
        ElseIf cmbKodeBrg.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeBrg.Focus()
        ElseIf txtHargaJual.Text = "" Then
            MsgBox("Harga Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaJual.Focus()
        ElseIf txtJumlahJual.Text = "" Then
            MsgBox("Jumlah Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtJumlahJual.Focus()
        Else
            Koneksi()
            Dim strUbah As String
            strUbah = "update jual set " & _
                    "KodeBrg = '" & cmbKodeBrg.Text & "', " & _
                    "HargaJual = " & txtHargaJual.Text & ", " & _
                    "JumlahJual = " & txtJumlahJual.Text & " " & _
                    "where NoNota ='" & txtNoNota.Text & "'"
            cmd = New OleDb.OleDbCommand(strUbah, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Penjualan Berhasil diubah")
            Call TampilData()
            Call Matikan(False)
            Call Kosongkan()
            Call Tombol(True)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtNoNota.Text = "" Then
            MsgBox("No Nota masih kosong boss", vbExclamation, "Kesalahan")
            txtNoNota.Focus()
        ElseIf cmbKodeBrg.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeBrg.Focus()
        ElseIf txtHargaJual.Text = "" Then
            MsgBox("Harga Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaJual.Focus()
        ElseIf txtJumlahJual.Text = "" Then
            MsgBox("Jumlah Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtJumlahJual.Focus()
        Else
            Call Koneksi()
            cmd = New OleDb.OleDbCommand("select * from jual where NoNota='" & _
                                         txtNoNota.Text & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                Koneksi()
                Dim simpan As String
                simpan = "insert into jual values('" & txtNoNota.Text & "','" & _
                    cmbKodeBrg.Text & "'," & txtHargaJual.Text & "," & _
                    txtJumlahJual.Text & ")"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                'Update stok barang
                simpan = "update barang set Stok = Stok - " & txtJumlahJual.Text & " where KodeBrg = '" & cmbKodeBrg.Text & "'"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Penjualan Berhasil disimpan, dan Stok Barang dikurangi")
                Call TampilData()
                Call Matikan(False)
                Call Kosongkan()
                Call Tombol(True)
            Else
                MsgBox("Data Penjualan sudah ada")
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtNoNota.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        cmbKodeBrg.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtHargaJual.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtJumlahJual.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtTotal.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value * DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Matikan(True)
        txtNoNota.Enabled = False
        cmbKodeBrg.Focus()
        btnBatal.Enabled = True
    End Sub
End Class