Imports System.Data.SqlClient

Public Class Walumno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Verifica si es la primera carga de la página
        If Not IsPostBack Then
            ' Asigna el origen de datos al GridView
            GridView1.DataSourceID = "SqlDataSourceAlumnos"
        End If
    End Sub
    ' ... Otros métodos y eventos

    Protected Sub Insertar_Click(sender As Object, e As EventArgs) Handles Insertar.Click
        ' Obtener los valores de los controles en la página
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

        ' Llamar al método para insertar el alumno en la base de datos
        GestorDBAlumnos.InsertarAlumno(matricula, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        ' Limpiar los TextBox después de la inserción
        LimpiarTextBox()

        ' Puedes agregar aquí cualquier otra lógica después de la inserción
    End Sub

    ' Método para limpiar los TextBox
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
        ' Obtener los valores de los controles en la página
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

        ' Llamar al método para modificar el alumno en la base de datos
        GestorDBAlumnos.ModificarAlumno(matricula, nombre, paterno, materno, rfc, curp, sexo, estado, municipio, localidad)

        ' Limpiar los TextBox después de la modificación
        LimpiarTextBox()

        ' Puedes agregar aquí cualquier otra lógica después de la modificación
    End Sub

    Protected Sub Eliminar_Click(sender As Object, e As EventArgs) Handles Eliminar.Click
        ' Obtener el valor de la matrícula a eliminar
        Dim matricula As String = txtMatricula.Text

        ' Llamar al método para eliminar el alumno en la base de datos
        GestorDBAlumnos.EliminarAlumno(matricula)

        ' Limpiar los TextBox después de la eliminación
        LimpiarTextBox()

        ' Puedes agregar aquí cualquier otra lógica después de la eliminación
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

                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado)) ' Cambié el tipo de dato a Int32
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio)) ' Cambié el tipo de dato a Int32
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad)) ' Cambié el tipo de dato a Int32

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ' Manejar la excepción (puedes loggearla o mostrar un mensaje de error)
            Throw New Exception("Error al insertar alumno en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub ModificarAlumno(matricula As String, nombre As String, paterno As String, materno As String, rfc As String, curp As String, sexo As String, estado As String, municipio As String, localidad As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' Modificar el query según tus necesidades
                Dim query As String = "UPDATE EJAG_alumnos SET Nombre = @nombre, Paterno = @paterno, Materno = @materno, CURP = @curp, RFC = @rfc, Cve_estado = @estado, Cve_municipio = @municipio, Cve_localidad = @localidad WHERE Matricula = @matricula"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@matricula", matricula)
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@paterno", paterno)
                    cmd.Parameters.AddWithValue("@materno", materno)
                    cmd.Parameters.AddWithValue("@rfc", rfc)
                    cmd.Parameters.AddWithValue("@curp", curp)

                    cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(estado)) ' Cambié el tipo de dato a Int32
                    cmd.Parameters.AddWithValue("@municipio", Convert.ToInt32(municipio)) ' Cambié el tipo de dato a Int32
                    cmd.Parameters.AddWithValue("@localidad", Convert.ToInt32(localidad)) ' Cambié el tipo de dato a Int32

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ' Manejar la excepción (puedes loggearla o mostrar un mensaje de error)
            Throw New Exception("Error al modificar alumno en la base de datos.", ex)
        End Try
    End Sub

    Public Shared Sub EliminarAlumno(matricula As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' Modificar el query según tus necesidades
                Dim query As String = "DELETE FROM EJAG_alumnos WHERE Matricula = @matricula"

                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@matricula", matricula)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ' Manejar la excepción (puedes loggearla o mostrar un mensaje de error)
            Throw New Exception("Error al eliminar alumno de la base de datos.", ex)
        End Try
    End Sub
End Class