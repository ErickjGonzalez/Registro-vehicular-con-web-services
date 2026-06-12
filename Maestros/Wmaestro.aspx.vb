Imports System.Data.SqlClient

Public Class Wmaestro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Insertar_Click(sender As Object, e As EventArgs) Handles Insertar.Click
        Dim nomina As String = txtNomina.Text
        Dim nombre As String = txtNombre.Text
        Dim paterno As String = txtPaterno.Text
        Dim materno As String = txtMaterno.Text
        Dim rfc As String = txtRFC.Text
        Dim curp As String = txtCURP.Text
        Dim sexo As String = txtSexo.Text
        Dim estado As String = Estados.SelectedValue
        Dim municipio As String = Municipios.SelectedValue
        Dim localidad As String = Localidades.SelectedValue

        GestorDB.InsertarMaestro(nomina, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        LimpiarTextBox()

        CargarMaestrosEnGridView()
    End Sub

    Protected Sub Modificar_Click(sender As Object, e As EventArgs) Handles Modificar.Click
        Dim nomina As String = txtNomina.Text
        Dim nombre As String = txtNombre.Text
        Dim paterno As String = txtPaterno.Text
        Dim materno As String = txtMaterno.Text
        Dim rfc As String = txtRFC.Text
        Dim curp As String = txtCURP.Text
        Dim sexo As String = txtSexo.Text
        Dim estado As String = Estados.SelectedValue
        Dim municipio As String = Municipios.SelectedValue
        Dim localidad As String = Localidades.SelectedValue

        GestorDB.ModificarMaestro(nomina, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        LimpiarTextBox()

        CargarMaestrosEnGridView()
    End Sub

    Protected Sub Eliminar_Click(sender As Object, e As EventArgs) Handles Eliminar.Click
        Dim nomina As String = txtNomina.Text

        GestorDB.EliminarMaestro(nomina)

        LimpiarTextBox()

        CargarMaestrosEnGridView()
    End Sub

    Private Sub LimpiarTextBox()
        txtNomina.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtRFC.Text = ""
        txtCURP.Text = ""
        txtSexo.Text = ""
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
    End Sub

    Protected Sub CargarMaestrosEnGridView()
        Dim maestros As List(Of Maestro) = GestorDB.ObtenerMaestros()

        If maestros.Count > 0 Then
            GridView1.DataSource = maestros
            GridView1.DataBind()
        Else
        End If
    End Sub

End Class

Public Class GestorDB
    Private Shared connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

    Public Shared Sub InsertarMaestro(nomina As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, sexo As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO EJAG_maestros (Nomina, Nombre, Paterno, Materno, RFC, CURP, Sexo, Cve_estado, Cve_municipio, Cve_localidad) VALUES (@nomina, @nombre, @paterno, @materno, @rfc, @curp, @sexo, @estado, @municipio, @localidad)"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@paterno", paterno)
                    cmd.Parameters.AddWithValue("@materno", materno)
                    cmd.Parameters.AddWithValue("@rfc", rfc)
                    cmd.Parameters.AddWithValue("@curp", curp)
                    cmd.Parameters.AddWithValue("@sexo", sexo)
                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado))
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio))
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad))

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al insertar maestro en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub ModificarMaestro(nomina As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, sexo As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE EJAG_maestros SET Nombre = @nombre, Paterno = @paterno, Materno = @materno, RFC = @rfc, CURP = @curp, Sexo = @sexo, Cve_estado = @estado, Cve_municipio = @municipio, Cve_localidad = @localidad WHERE Nomina = @nomina"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@paterno", paterno)
                    cmd.Parameters.AddWithValue("@materno", materno)
                    cmd.Parameters.AddWithValue("@rfc", rfc)
                    cmd.Parameters.AddWithValue("@curp", curp)
                    cmd.Parameters.AddWithValue("@sexo", sexo)
                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado))
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio))
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad))

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al modificar maestro en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub EliminarMaestro(nomina As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "DELETE FROM EJAG_maestros WHERE Nomina = @nomina"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar maestro de la base de datos.", ex)
        End Try
    End Sub

    Public Shared Function ObtenerMaestros() As List(Of Maestro)
        Dim maestros As New List(Of Maestro)

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM EJAG_maestros"
                Using cmd As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim maestro As New Maestro()
                            maestro.Nomina = reader("Nomina").ToString()
                            maestro.Nombre = reader("Nombre").ToString()
                            maestro.Paterno = reader("Paterno").ToString()
                            maestro.Materno = reader("Materno").ToString()
                            maestro.RFC = reader("RFC").ToString()
                            maestro.CURP = reader("CURP").ToString()
                            maestro.Sexo = reader("Sexo").ToString()
                            maestro.Cve_estado = reader("Cve_estado").ToString()
                            maestro.Cve_municipio = reader("Cve_municipio").ToString()
                            maestro.Cve_localidad = reader("Cve_localidad").ToString()

                            maestros.Add(maestro)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener maestros de la base de datos.", ex)
        End Try

        Return maestros
    End Function
End Class

Public Class Maestro
    Public Property Nomina As String
    Public Property Nombre As String
    Public Property Paterno As String
    Public Property Materno As String
    Public Property RFC As String
    Public Property CURP As String
    Public Property Sexo As String
    Public Property Cve_estado As String
    Public Property Cve_municipio As String
    Public Property Cve_localidad As String
End Class
