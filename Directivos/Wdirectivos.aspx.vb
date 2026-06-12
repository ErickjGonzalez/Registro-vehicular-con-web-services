Imports System.Data.SqlClient
Imports EXAMEN_2P.Wdirectivos

Public Class Wdirectivos
    Inherits System.Web.UI.Page

    Public Class Directivo
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarDirectivosEnGridView()
        End If
    End Sub

    Protected Sub CargarDirectivosEnGridView()
        Dim directivos As List(Of Directivo) = GestorDBDirectivos.ObtenerDirectivos()

        If directivos.Count > 0 Then
            GridViewDirectivos.DataSource = directivos
            GridViewDirectivos.DataBind()
        Else
           
        End If
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

        GestorDBDirectivos.InsertarDirectivo(nomina, nombre, paterno, materno, rfc, curp, estado, municipio, localidad)

        LimpiarTextBox()
        CargarDirectivosEnGridView()
    End Sub

    Protected Sub Modificar_Click(sender As Object, e As EventArgs) Handles Modificar.Click
        Dim nomina As String = txtNomina.Text
        Dim nombre As String = txtNombre.Text
        Dim paterno As String = txtPaterno.Text
        Dim materno As String = txtMaterno.Text
        Dim rfc As String = txtRFC.Text
        Dim curp As String = txtCURP.Text
        Dim estado As String = Estados.SelectedValue
        Dim municipio As String = Municipios.SelectedValue
        Dim localidad As String = Localidades.SelectedValue

        GestorDBDirectivos.ModificarDirectivo(nomina, nombre, paterno, materno, rfc, curp, estado, municipio, localidad)

        LimpiarTextBox()
        CargarDirectivosEnGridView()
    End Sub

    Protected Sub Eliminar_Click(sender As Object, e As EventArgs) Handles Eliminar.Click
        Dim nomina As String = txtNomina.Text

        GestorDBDirectivos.EliminarDirectivo(nomina)

        LimpiarTextBox()
        CargarDirectivosEnGridView()
    End Sub

    Private Sub LimpiarTextBox()
        txtNomina.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtRFC.Text = ""
        txtCURP.Text = ""
      
    End Sub

    Protected Sub GridViewDirectivos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewDirectivos.SelectedIndexChanged
        
    End Sub

End Class

Public Class GestorDBDirectivos
    Private Shared connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

    Public Shared Function ObtenerDirectivos() As List(Of Directivo)
        Dim directivos As New List(Of Directivo)()

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT Nomina, Nombre, Paterno, Materno, RFC, CURP, Sexo, Cve_estado, Cve_municipio, Cve_localidad FROM EJAG_directivos"

                Using cmd As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim directivo As New Directivo()
                            directivo.Nomina = reader("Nomina").ToString()
                            directivo.Nombre = reader("Nombre").ToString()
                            directivo.Paterno = reader("Paterno").ToString()
                            directivo.Materno = reader("Materno").ToString()
                            directivo.RFC = reader("RFC").ToString()
                            directivo.CURP = reader("CURP").ToString()
                            directivo.Sexo = reader("Sexo").ToString() 
                            directivo.Cve_estado = reader("Cve_estado").ToString()
                            directivo.Cve_municipio = reader("Cve_municipio").ToString()
                            directivo.Cve_localidad = reader("Cve_localidad").ToString()

                            directivos.Add(directivo)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener la lista de directivos desde la base de datos.", ex)
        End Try

        Return directivos
    End Function

    Public Shared Sub InsertarDirectivo(nomina As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO EJAG_directivos (Nomina, Nombre, Paterno, Materno, RFC, CURP, Cve_estado, Cve_municipio, Cve_localidad) VALUES (@nomina, @nombre, @paterno, @materno, @rfc, @curp, @estado, @municipio, @localidad)"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@paterno", paterno)
                    cmd.Parameters.AddWithValue("@materno", materno)
                    cmd.Parameters.AddWithValue("@rfc", rfc)
                    cmd.Parameters.AddWithValue("@curp", curp)
                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado))
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio))
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad))

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al insertar directivo en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub ModificarDirectivo(nomina As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE EJAG_directivos SET Nombre = @nombre, Paterno = @paterno, Materno = @materno, RFC = @rfc, CURP = @curp, Cve_estado = @estado, Cve_municipio = @municipio, Cve_localidad = @localidad WHERE Nomina = @nomina"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@paterno", paterno)
                    cmd.Parameters.AddWithValue("@materno", materno)
                    cmd.Parameters.AddWithValue("@rfc", rfc)
                    cmd.Parameters.AddWithValue("@curp", curp)
                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado))
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio))
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad))

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al modificar directivo en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub EliminarDirectivo(nomina As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "DELETE FROM EJAG_directivos WHERE Nomina = @nomina"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@nomina", nomina)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar directivo de la base de datos.", ex)
        End Try
    End Sub
End Class
