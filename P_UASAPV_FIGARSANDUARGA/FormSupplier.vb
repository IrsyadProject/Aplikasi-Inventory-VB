Imports System.Data.OleDb
Public Class FormSupplier
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
        txtKodeSupp.Enabled = Nilai
        txtNamaSupp.Enabled = Nilai
        txtAlamatSupp.Enabled = Nilai
        txtTelpSupp.Enabled = Nilai
    End Sub
    Sub Kosongkan()
        txtKodeSupp.Text = ""
        txtNamaSupp.Text = ""
        txtAlamatSupp.Text = ""
        txtTelpSupp.Text = 0
    End Sub
    Sub TampilData()
        Koneksi()
        da = New OleDbDataAdapter("select * from supplier", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "supplier")
        DataGridView1.DataSource = (ds.Tables("supplier"))
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 120
    End Sub

    Private Sub FormSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Call Matikan(True)
        txtKodeSupp.Focus()
        Call Tombol(False)
        Call Kosongkan()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtKodeSupp.Text = "" Then
            MsgBox("Kode Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtKodeSupp.Focus()
        ElseIf txtNamaSupp.Text = "" Then
            MsgBox("Nama Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtNamaSupp.Focus()
        ElseIf txtAlamatSupp.Text = "" Then
            MsgBox("Alamat Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtAlamatSupp.Focus()
        ElseIf txtTelpSupp.Text = "" Then
            MsgBox("Telp Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtTelpSupp.Focus()
        Else
            Call Koneksi()
            cmd = New OleDb.OleDbCommand("select * from supplier where KodeSupp='" & _
                                         txtKodeSupp.Text & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                Koneksi()
                Dim simpan As String
                simpan = "insert into supplier values('" & txtKodeSupp.Text & "','" & _
                    txtNamaSupp.Text & "','" & txtAlamatSupp.Text & "','" & _
                    txtTelpSupp.Text & "')"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Supplier Berhasil disimpan")
                Call TampilData()
                Call Matikan(False)
                Call Kosongkan()
                Call Tombol(True)
            Else
                MsgBox("Data Supplier sudah ada")
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtKodeSupp.Text = "" Then
            MsgBox("Kode Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtKodeSupp.Focus()
        ElseIf txtNamaSupp.Text = "" Then
            MsgBox("Nama Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtNamaSupp.Focus()
        ElseIf txtAlamatSupp.Text = "" Then
            MsgBox("Alamat Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtAlamatSupp.Focus()
        ElseIf txtTelpSupp.Text = "" Then
            MsgBox("Telp Supplier masih kosong boss", vbExclamation, "Kesalahan")
            txtTelpSupp.Focus()
        Else
            Koneksi()
            Dim strUbah As String
            strUbah = "update supplier set " & _
                    "NamaSupp = '" & txtNamaSupp.Text & "', " & _
                    "AlamatSupp = '" & txtAlamatSupp.Text & "', " & _
                    "TelpSupp = '" & txtTelpSupp.Text & "' " & _
                    "where KodeSupp ='" & txtKodeSupp.Text & "'"
            cmd = New OleDb.OleDbCommand(strUbah, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Supplier Berhasil diubah")
            Call TampilData()
            Call Matikan(False)
            Call Kosongkan()
            Call Tombol(True)
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtKodeSupp.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtNamaSupp.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtAlamatSupp.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtTelpSupp.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
    End Sub


    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Koneksi()
        Dim strHapus As String
        strHapus = "delete from supplier where KodeSupp ='" & txtKodeSupp.Text & "'"
        cmd = New OleDb.OleDbCommand(strHapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Supplier Berhasil dihapus")
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Matikan(True)
        txtKodeSupp.Enabled = False
        txtNamaSupp.Focus()
        btnBatal.Enabled = True
    End Sub

    Private Sub txtTelpSupp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelpSupp.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub
End Class