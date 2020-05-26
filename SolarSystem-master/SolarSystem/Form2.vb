Imports System.Data.OleDb
Public Class Form2
    Private conn As OleDbConnection

    Private Sub RefreshGrid()
        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "select * from planets"

        Dim ds As New DataSet
        Dim da As New OleDbDataAdapter(c)
        da.Fill(ds, "planets")
        Grid.DataSource = ds
        Grid.DataMember = "planets"

        Grid.Columns("ID").Visible = False
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New OleDbConnection
        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\evgenus\Desktop\SolarSystem\Application\SolarSystem\planets.accdb;Persist Security Info=False;"
        conn.Open()
        RefreshGrid()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult
        Form3.ShowDialog()
        Try
            s1 = Integer.Parse(Form3.TextBox1.Text)
            s2 = Double.Parse(Form3.TextBox2.Text)
            s3 = Double.Parse(Form3.TextBox3.Text)

        Catch ex As Exception
            MsgBox("дай паспацъ")
        End Try
        s1 = Form3.TextBox1.Text
        s2 = Form3.TextBox2.Text
        s3 = Form3.TextBox3.Text
        r = Form3.DialogResult
        Form3.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "insert into planets(PlanetName, M, R) values('" & s1 & "','" & s2 & "','" & s3 & "')"
        c.ExecuteNonQuery()

        RefreshGrid()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim k As Integer
        Dim c As New OleDbCommand
        c.Connection = conn
        k = Grid.CurrentRow.Cells("ID").Value
        c.CommandText = "delete from planets where ID = " & k
        c.ExecuteNonQuery()
        RefreshGrid()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim s1, s2, s3 As String
        Dim r As DialogResult
        Dim k As Integer
        k = Grid.CurrentRow.Cells("ID").Value
        Form3.TextBox1.Text = Grid.CurrentRow.Cells("PlanetName").Value
        Form3.TextBox2.Text = Grid.CurrentRow.Cells("M").Value
        Form3.TextBox3.Text = Grid.CurrentRow.Cells("R").Value

        Form3.ShowDialog()

        Try
            s1 = Integer.Parse(Form3.TextBox1.Text)
            s2 = Double.Parse(Form3.TextBox2.Text)
            s3 = Double.Parse(Form3.TextBox3.Text)

        Catch ex As Exception
            MsgBox("дай паспацъ")
        End Try

        s1 = Form3.TextBox1.Text
        s2 = Form3.TextBox2.Text
        s3 = Form3.TextBox3.Text
        r = Form3.DialogResult
        Form3.Close()

        If r <> DialogResult.OK Then
            Exit Sub
        End If

        Dim c As New OleDbCommand
        c.Connection = conn
        c.CommandText = "update planets set PlanetName='" & s1 & "',M='" & s2 & "', R='" & s3 & "' where ID= " & k
        c.ExecuteNonQuery()

        RefreshGrid()
    End Sub
End Class