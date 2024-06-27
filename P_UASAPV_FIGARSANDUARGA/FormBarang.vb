Imports System.Data.OleDb
Public Class FormBarang
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
        txtKodeBrg.Enabled = Nilai
        txtNamaBrg.Enabled = Nilai
        txtHargaBeli.Enabled = Nilai
        txtHargaJual.Enabled = Nilai
        txtStok.Enabled = Nilai
    End Sub
    Sub Kosongkan()
        txtKodeBrg.Text = ""
        txtNamaBrg.Text = ""
        txtHargaBeli.Text = 0
        txtHargaJual.Text = 0
        txtStok.Text = 0
    End Sub
    Sub TampilData()
        Koneksi()
        da = New OleDbDataAdapter("select * from barang", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "barang")
        DataGridView1.DataSource = (ds.Tables("barang"))
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 120
        DataGridView1.Columns(4).Width = 80
        'Format Mata Uang
        DataGridView1.Columns(2).DefaultCellStyle.Format = "Rp ###,###"
        DataGridView1.Columns(3).DefaultCellStyle.Format = "Rp ###,###"
    End Sub

    Private Sub FormBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        txtKodeBrg.Focus()
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
        If txtKodeBrg.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            txtKodeBrg.Focus()
        ElseIf txtNamaBrg.Text = "" Then
            MsgBox("Nama Barang masih kosong boss", vbExclamation, "Kesalahan")
            txtNamaBrg.Focus()
        ElseIf txtHargaBeli.Text = "" Then
            MsgBox("Harga Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaBeli.Focus()
        ElseIf txtHargaJual.Text = "" Then
            MsgBox("Harga Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaJual.Focus()
        ElseIf txtStok.Text = "" Then
            MsgBox("Stok masih kosong boss", vbExclamation, "Kesalahan")
            txtStok.Focus()
        Else
            Call Koneksi()
            cmd = New OleDb.OleDbCommand("select * from barang where KodeBrg='" & _
                                         txtKodeBrg.Text & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                Koneksi()
                Dim simpan As String
                simpan = "insert into barang values('" & txtKodeBrg.Text & "','" & _
                    txtNamaBrg.Text & "'," & txtHargaBeli.Text & "," & _
                    txtHargaJual.Text & "," & txtStok.Text & ")"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil disimpan")
                Call TampilData()
                Call Matikan(False)
                Call Kosongkan()
                Call Tombol(True)
            Else
                MsgBox("Data sudah ada")
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtKodeBrg.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            txtKodeBrg.Focus()
        ElseIf txtNamaBrg.Text = "" Then
            MsgBox("Nama Barang masih kosong boss", vbExclamation, "Kesalahan")
            txtNamaBrg.Focus()
        ElseIf txtHargaBeli.Text = "" Then
            MsgBox("Harga Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaBeli.Focus()
        ElseIf txtHargaJual.Text = "" Then
            MsgBox("Harga Jual masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaJual.Focus()
        ElseIf txtStok.Text = "" Then
            MsgBox("Stok masih kosong boss", vbExclamation, "Kesalahan")
            txtStok.Focus()
        Else
            Koneksi()
            Dim strUbah As String
            strUbah = "update barang set " & _
                    "NamaBrg = '" & txtNamaBrg.Text & "', " & _
                    "HargaBeli = " & txtHargaBeli.Text & ", " & _
                    "HargaJual = " & txtHargaJual.Text & ", " & _
                    "Stok = " & txtStok.Text & " " & _
                    "where KodeBrg ='" & txtKodeBrg.Text & "'"
            cmd = New OleDb.OleDbCommand(strUbah, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Berhasil diubah")
            Call TampilData()
            Call Matikan(False)
            Call Kosongkan()
            Call Tombol(True)
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtKodeBrg.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        txtNamaBrg.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        txtHargaBeli.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtHargaJual.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtStok.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
    End Sub


    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Koneksi()
        Dim strHapus As String
        strHapus = "delete from barang where KodeBrg ='" & txtKodeBrg.Text & "'"
        cmd = New OleDb.OleDbCommand(strHapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Berhasil dihapus")
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Matikan(True)
        txtKodeBrg.Enabled = False
        txtNamaBrg.Focus()
        btnBatal.Enabled = True
    End Sub

    Private Sub txtHargaBeli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHargaBeli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtHargaJual_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHargaJual.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtStok_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStok.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub
End Class
