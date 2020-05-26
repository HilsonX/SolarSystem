Imports System.Data.OleDb
Imports System.Math

Public Class Form1
    Dim _id As Int16
    Dim _m As Double
    Dim _r As Double
    Dim _h As Double
    Dim _v1 As Double
    Dim _v2 As Double
    Dim conn As OleDbConnection

    Const G As Double = 0.00000000006672

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.ShowDialog()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New OleDbConnection
        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\evgenus\Desktop\SolarSystem\Application\SolarSystem\planets.accdb;Persist Security Info=False;"
        conn.Open()
        'TODO: данная строка кода позволяет загрузить данные в таблицу "PlanetsDataSet1.planets". При необходимости она может быть перемещена или удалена.
        Me.PlanetsTableAdapter.Fill(Me.PlanetsDataSet1.planets)

    End Sub
    Private Sub Velocity()
        _v1 = Sqrt((G * _m) / (_r + _h))
        _v2 = Sqrt(2) * _v1
    End Sub
    Private Function GetPlanet() As Int16
        Try
            _h = Double.Parse(TextBox3.Text)
            _id = Int16.Parse(ComboBox1.SelectedValue)
        Catch ex As Exception
            MsgBox("дай паспацъ")

        End Try
        Dim row = PlanetsTableAdapter.GetData().Item(_id - 1)
        _r = row.R
        _m = row.M

    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GetPlanet()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Velocity()
        TextBox1.Text = _v1.ToString()
        TextBox2.Text = _v2.ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'в разработке
        Dim commrtt As New OleDbCommand("select * from planets b", conn)
        commrtt.CommandType = CommandType.Text
        Dim da As New OleDbDataAdapter(commrtt)
        Dim ds As New DataSet
        da.Fill(ds, "planets")

        ComboBox1.DataSource = ds.Tables("planets")
        ComboBox1.DisplayMember = "PlanetName"
        ComboBox1.ValueMember = "ID"
    End Sub
End Class
