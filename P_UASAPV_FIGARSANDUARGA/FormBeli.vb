Imports System.Data.OleDb
Public Class FormBeli
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
        txtNoFaktur.Enabled = Nilai
        cmbKodeSupp.Enabled = Nilai
        cmbKodeBarang.Enabled = Nilai
        txtHargaBeli.Enabled = Nilai
        txtJumlahBeli.Enabled = Nilai
        txtTotal.Enabled = Nilai
    End Sub
    Sub Kosongkan()
        txtNoFaktur.Text = ""
        cmbKodeSupp.Text = ""
        cmbKodeBarang.Text = ""
        txtHargaBeli.Text = 0
        txtJumlahBeli.Text = 0
        txtTotal.Text = 0
    End Sub

    Sub IsiCombo()
        Koneksi()
        Try
            cmd = New OleDb.OleDbCommand("select kodesupp from supplier order by kodesupp", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbKodeSupp.Items.Add(dr("Kodesupp"))
            End While
            'combobox barang
            cmd = New OleDb.OleDbCommand("select kodebrg from barang order by kodebrg", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbKodeBarang.Items.Add(dr("KodeBrg"))
            End While
        Catch ex As Exception

        End Try
    End Sub

    Sub TampilData()
        Koneksi()
        da = New OleDbDataAdapter("select * from beli", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "beli")
        DataGridView1.DataSource = (ds.Tables("beli"))
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
        DataGridView1.Columns(3).Width = 120
        DataGridView1.Columns(4).Width = 100
    End Sub

    Private Sub FormBeli_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
        Call IsiCombo()
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Call Matikan(True)
        txtNoFaktur.Focus()
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
        If txtNoFaktur.Text = "" Then
            MsgBox("No Faktur masih kosong boss", vbExclamation, "Kesalahan")
            txtNoFaktur.Focus()
        ElseIf txtHargaBeli.Text = "" Then
            MsgBox("Harga Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaBeli.Focus()
        ElseIf txtJumlahBeli.Text = "" Then
            MsgBox("Jumlah Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtJumlahBeli.Focus()
        ElseIf txtTotal.Text = "" Then
            MsgBox("Total masih kosong boss", vbExclamation, "Kesalahan")
            txtTotal.Focus()
        ElseIf cmbKodeSupp.Text = "" Then
            MsgBox("Kode Supplier masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeSupp.Focus()
        ElseIf cmbKodeBarang.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeBarang.Focus()
        Else
            Call Koneksi()
            cmd = New OleDb.OleDbCommand("select * from beli where NoFaktur='" & _
                                         txtNoFaktur.Text & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If Not dr.HasRows Then
                Koneksi()
                Dim simpan As String
                simpan = "insert into beli values('" & txtNoFaktur.Text & "','" & _
                    cmbKodeSupp.Text & "','" & cmbKodeBarang.Text & "','" & _
                    txtHargaBeli.Text & "','" & txtJumlahBeli.Text & "')"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                'Update stok barang
                simpan = "update barang set Stok = Stok + " & txtJumlahBeli.Text & " where KodeBrg = '" & cmbKodeBarang.Text & "'"
                cmd = New OleDb.OleDbCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Pembelian Berhasil disimpan, dan Stok Barang bertambah")
                Call TampilData()
                Call Matikan(False)
                Call Kosongkan()
                Call Tombol(True)
            Else
                MsgBox("Data Pembelian sudah ada")
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If txtNoFaktur.Text = "" Then
            MsgBox("No Faktur masih kosong boss", vbExclamation, "Kesalahan")
            txtNoFaktur.Focus()
        ElseIf txtHargaBeli.Text = "" Then
            MsgBox("Harga Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtHargaBeli.Focus()
        ElseIf txtJumlahBeli.Text = "" Then
            MsgBox("Jumlah Beli masih kosong boss", vbExclamation, "Kesalahan")
            txtJumlahBeli.Focus()
        ElseIf txtTotal.Text = "" Then
            MsgBox("Total masih kosong boss", vbExclamation, "Kesalahan")
            txtTotal.Focus()
        ElseIf cmbKodeSupp.Text = "" Then
            MsgBox("Kode Supplier masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeSupp.Focus()
        ElseIf cmbKodeBarang.Text = "" Then
            MsgBox("Kode Barang masih kosong boss", vbExclamation, "Kesalahan")
            cmbKodeBarang.Focus()
        Else
            Koneksi()
            Dim strUbah As String
            strUbah = "update beli set " & _
                    "KodeSupp = '" & cmbKodeSupp.Text & "', " & _
                    "KodeBrg = '" & cmbKodeBarang.Text & "', " & _
                    "HargaBeli = " & txtHargaBeli.Text & ", " & _
                    "JumlahBeli = " & txtJumlahBeli.Text & " " & _
                    "where NoFaktur ='" & txtNoFaktur.Text & "'"
            cmd = New OleDb.OleDbCommand(strUbah, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Data Pembelian Berhasil diubah")
            Call TampilData()
            Call Matikan(False)
            Call Kosongkan()
            Call Tombol(True)
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtNoFaktur.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        cmbKodeSupp.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        cmbKodeBarang.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        txtHargaBeli.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        txtJumlahBeli.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        txtTotal.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value * DataGridView1.Rows(e.RowIndex).Cells(4).Value
    End Sub


    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Koneksi()
        Dim strHapus As String
        strHapus = "delete from beli where NoFaktur ='" & txtNoFaktur.Text & "'"
        cmd = New OleDb.OleDbCommand(strHapus, conn)
        cmd.ExecuteNonQuery()
        'pengurangan jumlah barang
        strHapus = "update barang set Stok = Stok - " & txtJumlahBeli.Text & " where KodeBrg = '" & cmbKodeBarang.Text & "'"
        cmd = New OleDb.OleDbCommand(strHapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Pembelian Berhasil dihapus, dan pengurangan stok barang")
        Call TampilData()
        Call Matikan(False)
        Call Kosongkan()
        Call Tombol(True)
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Matikan(True)
        txtNoFaktur.Enabled = False
        cmbKodeSupp.Focus()
        btnBatal.Enabled = True
    End Sub

    Private Sub cmbKodeSupp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKodeSupp.LostFocus
        Call Koneksi()
        cmd = New OleDb.OleDbCommand("select * from supplier where KodeSupp='" & _
                                     cmbKodeSupp.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            MsgBox("Maaf Kode Supplier Tidak Ada")
        Else
            lblNamaSupp.Text = dr!NamaSupp
        End If
    End Sub

    Private Sub cmbKodeBarang_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKodeBarang.LostFocus
        Call Koneksi()
        cmd = New OleDb.OleDbCommand("select * from barang where KodeBrg='" & _
                                     cmbKodeBarang.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            MsgBox("Maaf Kode Barang Tidak Ada")
        Else
            lblNamaBrg.Text = dr!NamaBrg
            txtHargaBeli.Text = dr!HargaBeli
        End If
    End Sub

    Private Sub txtJumlahBeli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtJumlahBeli.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub txtJumlahBeli_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJumlahBeli.LostFocus
        txtTotal.Text = txtHargaBeli.Text * txtJumlahBeli.Text
    End Sub
End Class