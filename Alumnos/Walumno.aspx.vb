Imports System.Data.SqlClient

Public Class Walumno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GridView1.DataSourceID = "SqlDataSourceAlumnos"
        End If
    End Sub

    Protected Sub Insertar_Click(sender As Object, e As EventArgs) Handles Insertar.Click
        Dim matricula As String = txtMatricula.Text
        Dim nombre As String = txtNombre.Text
        Dim paterno As String = txtPaterno.Text
        Dim materno As String = txtMaterno.Text
        Dim rfc As String = txtRFC.Text
        Dim curp As String = txtCURP.Text
        Dim sexo As String = txtSexo.Text
        Dim estado As String = Estados.SelectedValue
        Dim municipio As String = Municipios.SelectedValue
        Dim localidad As String = Localidades.SelectedValue

        GestorDBAlumnos.InsertarAlumno(matricula, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        LimpiarTextBox()

    End Sub

    Private Sub LimpiarTextBox()
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtRFC.Text = ""
        txtCURP.Text = ""
        txtSexo.Text = ""
    End Sub

    Protected Sub Modificar_Click(sender As Object, e As EventArgs) Handles Modificar.Click
        Dim matricula As String = txtMatricula.Text
        Dim nombre As String = txtNombre.Text
        Dim paterno As String = txtPaterno.Text
        Dim materno As String = txtMaterno.Text
        Dim rfc As String = txtRFC.Text
        Dim curp As String = txtCURP.Text
        Dim sexo As String = txtSexo.Text
        Dim estado As String = Estados.SelectedValue
        Dim municipio As String = Municipios.SelectedValue
        Dim localidad As String = Localidades.SelectedValue

        GestorDBAlumnos.ModificarAlumno(matricula, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        LimpiarTextBox()

    End Sub

    Protected Sub Eliminar_Click(sender As Object, e As EventArgs) Handles Eliminar.Click
        Dim matricula As String = txtMatricula.Text

        GestorDBAlumnos.EliminarAlumno(matricula)

        LimpiarTextBox()

    End Sub

    Protected Sub SqlDataSource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting

    End Sub
End Class

Public Class GestorDBAlumnos
    Private Shared connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

    Public Shared Sub InsertarAlumno(matricula As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, sexo As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO EJAG_alumnos (Matricula, Nombre, Paterno, Materno, CURP, RFC, Cve_estado, Cve_municipio, Cve_localidad) VALUES (@matricula, @nombre, @paterno, @materno,@curp, @rfc,  @estado, @municipio, @localidad)"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@matricula", matricula)
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
            Throw New Exception("Error al insertar alumno en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub ModificarAlumno(matricula As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, sexo As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE EJAG_alumnos SET Nombre = @nombre, Paterno = @paterno, Materno = @materno, CURP = @curp, RFC = @rfc, Cve_estado = @estado, Cve_municipio = @municipio, Cve_localidad = @localidad WHERE Matricula = @matricula"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@matricula", matricula)
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
            Throw New Exception("Error al modificar alumno en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub EliminarAlumno(matricula As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "DELETE FROM EJAG_alumnos WHERE Matricula = @matricula"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@matricula", matricula)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar alumno de la base de datos.", ex)
        End Try
    End Sub
End Class
