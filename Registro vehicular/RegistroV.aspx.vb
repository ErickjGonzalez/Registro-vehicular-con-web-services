Imports System.Data.SqlClient
Imports GoogleMaps
Imports MySql.Data.MySqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
        
            Dim inicio As Integer = 1950
            Dim fin As Integer = 2025

            ddlAnos.Items.Clear()

            For i As Integer = inicio To fin
                ddlAnos.Items.Add(i.ToString())
            Next

            LlenarGridView()

            LlenarGridViewVehiculos()
        End If
    End Sub

    Private Sub LlenarGridViewVehiculos()
        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

        Dim query As String = "SELECT * FROM EJAG_RegistroVehiculos"

        Dim dt As New DataTable()

        Using connection As New SqlConnection(connectionString)
            Using adapter As New SqlDataAdapter(query, connection)
                adapter.Fill(dt)
            End Using
        End Using

        GridViewVehiculos.DataSource = dt
        GridViewVehiculos.DataBind()
    End Sub




    Private Sub LlenarGridView()
        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"
        Dim query As String = "
        SELECT 
            Matricula AS Matricula,
            Nombre,
            Paterno,
            Materno,
            NULL AS Nomina,
            NULL AS Sexo,
            estados.estado AS Nombre_estado,
            municipios.municipio AS Nombre_municipio,
            localidades.localidad AS Nombre_localidad,
            localidades.latitud_decimal AS Latitud,
            localidades.longitud_decimal AS Longitud,
            'Alumno' AS Tipo
        FROM     
            dbo.EJAG_alumnos
            INNER JOIN dbo.EJAG_localidades AS localidades ON dbo.EJAG_alumnos.Cve_estado = localidades.Cve_estado 
                AND dbo.EJAG_alumnos.Cve_municipio = localidades.Cve_municipio 
                AND dbo.EJAG_alumnos.Cve_localidad = localidades.Cve_localidad
            INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado 
                AND localidades.Cve_municipio = municipios.Cve_municipio
            INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado

        UNION ALL

        SELECT 
            Nomina AS Matricula,
            Nombre,
            Paterno,
            Materno,
            NULL AS Matricula,
            Sexo,
            estados.estado AS Nombre_estado,
            municipios.municipio AS Nombre_municipio,
            localidades.localidad AS Nombre_localidad,
            localidades.latitud_decimal AS Latitud,
            localidades.longitud_decimal AS Longitud,
            'Maestro' AS Tipo
        FROM     
            dbo.EJAG_maestros
            INNER JOIN dbo.EJAG_localidades AS localidades ON dbo.EJAG_maestros.Cve_estado = localidades.Cve_estado 
                AND dbo.EJAG_maestros.Cve_municipio = localidades.Cve_municipio 
                AND dbo.EJAG_maestros.Cve_localidad = localidades.Cve_localidad
            INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado 
                AND localidades.Cve_municipio = municipios.Cve_municipio
            INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado

        UNION ALL

        SELECT 
            Nomina AS Matricula,
            Nombre,
            Paterno,
            Materno,
            NULL AS Matricula,
            Sexo,
            estados.estado AS Nombre_estado,
            municipios.municipio AS Nombre_municipio,
            localidades.localidad AS Nombre_localidad,
            localidades.latitud_decimal AS Latitud,
            localidades.longitud_decimal AS Longitud,
            'Directivo' AS Tipo
        FROM     
            dbo.EJAG_directivos
            INNER JOIN dbo.EJAG_localidades AS localidades ON dbo.EJAG_directivos.Cve_estado = localidades.Cve_estado 
                AND dbo.EJAG_directivos.Cve_municipio = localidades.Cve_municipio 
                AND dbo.EJAG_directivos.Cve_localidad = localidades.Cve_localidad
            INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado 
                AND localidades.Cve_municipio = municipios.Cve_municipio
            INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado
    "

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    GridViewPersonas.DataSource = reader
                    GridViewPersonas.DataBind()
                End Using
            End Using
        End Using
    End Sub



    Protected Sub GridViewPersonas_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewPersonas.RowCommand
        If e.CommandName = "MostrarDetalle" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridViewPersonas.Rows(index)

            Dim latitud As Double = Double.Parse(row.Cells(7).Text)
            Dim longitud As Double = Double.Parse(row.Cells(8).Text)

            googleMap1.Latitude = latitud
            googleMap1.Longitude = longitud

            Dim matricula As String = GridViewPersonas.DataKeys(index).Value.ToString()

            Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"
            Dim query As String = "SELECT Usuarios.Nombre, Usuarios.Paterno, Usuarios.Materno, Usuarios.Nombre_estado, Usuarios.Nombre_municipio, Usuarios.Nombre_localidad, Usuarios.CURP, Usuarios.RFC " &
                  "FROM ( " &
                      "SELECT Alumnos.Nombre, Alumnos.Paterno, Alumnos.Materno, estados.estado AS Nombre_estado, municipios.municipio AS Nombre_municipio, localidades.localidad AS Nombre_localidad, Alumnos.CURP, Alumnos.RFC " &
                      "FROM dbo.EJAG_alumnos AS Alumnos " &
                      "INNER JOIN dbo.EJAG_localidades AS localidades ON Alumnos.Cve_estado = localidades.Cve_estado AND Alumnos.Cve_municipio = localidades.Cve_municipio AND Alumnos.Cve_localidad = localidades.Cve_localidad " &
                      "INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado AND localidades.Cve_municipio = municipios.Cve_municipio " &
                      "INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado " &
                      "WHERE Alumnos.Matricula = @Matricula " &
                      "UNION ALL " &
                      "SELECT Maestros.Nombre, Maestros.Paterno, Maestros.Materno, estados.estado AS Nombre_estado, municipios.municipio AS Nombre_municipio, localidades.localidad AS Nombre_localidad, Maestros.CURP, Maestros.RFC " &
                      "FROM dbo.EJAG_maestros AS Maestros " &
                      "INNER JOIN dbo.EJAG_localidades AS localidades ON Maestros.Cve_estado = localidades.Cve_estado AND Maestros.Cve_municipio = localidades.Cve_municipio AND Maestros.Cve_localidad = localidades.Cve_localidad " &
                      "INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado AND localidades.Cve_municipio = municipios.Cve_municipio " &
                      "INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado " &
                      "WHERE Maestros.Nomina = @Matricula " &
                      "UNION ALL " &
                      "SELECT Directivos.Nombre, Directivos.Paterno, Directivos.Materno, estados.estado AS Nombre_estado, municipios.municipio AS Nombre_municipio, localidades.localidad AS Nombre_localidad, Directivos.CURP, Directivos.RFC " &
                      "FROM dbo.EJAG_directivos AS Directivos " &
                      "INNER JOIN dbo.EJAG_localidades AS localidades ON Directivos.Cve_estado = localidades.Cve_estado AND Directivos.Cve_municipio = localidades.Cve_municipio AND Directivos.Cve_localidad = localidades.Cve_localidad " &
                      "INNER JOIN dbo.EJAG_municipios AS municipios ON localidades.Cve_estado = municipios.Cve_estado AND localidades.Cve_municipio = municipios.Cve_municipio " &
                      "INNER JOIN dbo.EJAG_estados AS estados ON localidades.Cve_estado = estados.Cve_estado " &
                      "WHERE Directivos.Nomina = @Matricula " &
                  ") AS Usuarios"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Matricula", matricula)
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Dim detalle As String = "Nombre: " & reader("Nombre") & " " & reader("Paterno") & " " & reader("Materno") & "<br />" &
                                                "Estado: " & reader("Nombre_estado") & "<br />" &
                                                "Municipio: " & reader("Nombre_municipio") & "<br />" &
                                                "Localidad: " & reader("Nombre_localidad") & "<br />" &
                                                "CURP: " & reader("CURP") & "<br />" &
                                                "RFC: " & reader("RFC")

                            lblDetalle.Text = detalle
                        End If
                    End Using
                End Using
            End Using
        End If
    End Sub


    Protected Sub GridViewVehiculos_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewVehiculos.RowCommand
        If e.CommandName = "Seleccionar" AndAlso e.CommandArgument IsNot Nothing Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim placa As String = GridViewVehiculos.Rows(index).Cells(0).Text 
            CargarDetallesPorPlaca(placa)
        End If


    End Sub

    Private Sub CargarDetallesPorPlaca(ByVal placa As String)
        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

        Dim query As String = "SELECT * FROM EJAG_RegistroVehiculos WHERE Placa = @Placa"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Placa", placa)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader.HasRows Then
                    reader.Read()
                    txtPlaca.Text = reader("Placa").ToString()
                    txtMatricula.Text = reader("Matricula").ToString()
                    txtMotor.Text = reader("NumeroMotor").ToString()
                    txtNumS.Text = reader("NumeroSerie").ToString()



                    ' Marca
                    Dim valorMarca As String = reader("Marca").ToString()
                    If ddlMarca.Items.FindByText(valorMarca) IsNot Nothing Then
                        ddlMarca.SelectedValue = ddlMarca.Items.FindByText(valorMarca).Value
                    Else
                    End If

                    ' Submarca
                    Dim valorSubmarca As String = reader("Submarca").ToString()
                    If ddlSubmarca.Items.FindByText(valorSubmarca) IsNot Nothing Then
                        ddlSubmarca.SelectedValue = ddlSubmarca.Items.FindByText(valorSubmarca).Value
                    Else
                    End If

                    ' Color
                    Dim valorColor As String = reader("Color").ToString()
                    If ddlColor.Items.FindByText(valorColor) IsNot Nothing Then
                        ddlColor.SelectedValue = ddlColor.Items.FindByText(valorColor).Value
                    Else
                    End If

                    ' Combustible
                    Dim valorCombustible As String = reader("Combustible").ToString()
                    If ddlCombustible.Items.FindByText(valorCombustible) IsNot Nothing Then
                        ddlCombustible.SelectedValue = ddlCombustible.Items.FindByText(valorCombustible).Value
                    Else
                    End If

                    ' Estado
                    Dim valorEstado As String = reader("Estado").ToString().Trim()
                    If ddlEstados.Items.FindByText(valorEstado) IsNot Nothing Then
                        ddlEstados.SelectedValue = ddlEstados.Items.FindByText(valorEstado).Value
                    Else
                        For Each item As ListItem In ddlEstados.Items
                            If item.Text.Trim().Equals(valorEstado, StringComparison.OrdinalIgnoreCase) Then
                                ddlEstados.SelectedValue = item.Value
                                Exit For
                            End If
                        Next
                    End If

                    ' Municipio
                    Dim valorMunicipio As String = reader("Municipio").ToString().Trim()
                    If ddlMunicipios.Items.FindByText(valorMunicipio) IsNot Nothing Then
                        ddlMunicipios.SelectedValue = ddlMunicipios.Items.FindByText(valorMunicipio).Value
                    Else
                        For Each item As ListItem In ddlMunicipios.Items
                            If item.Text.Trim().Equals(valorMunicipio, StringComparison.OrdinalIgnoreCase) Then
                                ddlMunicipios.SelectedValue = item.Value
                                Exit For
                            End If
                        Next
                    End If

                    ' Localidad
                    Dim valorLocalidad As String = reader("Localidad").ToString().Trim()
                    If ddlLocalidades.Items.FindByText(valorLocalidad) IsNot Nothing Then
                        ddlLocalidades.SelectedValue = ddlLocalidades.Items.FindByText(valorLocalidad).Value
                    Else
                        For Each item As ListItem In ddlLocalidades.Items
                            If item.Text.Trim().Equals(valorLocalidad, StringComparison.OrdinalIgnoreCase) Then
                                ddlLocalidades.SelectedValue = item.Value
                                Exit For
                            End If
                        Next
                    End If

                    reader.Close()


                    Dim modeloQuery As String = "SELECT Modelo FROM EJAG_RegistroVehiculos WHERE Placa = @Placa"

                    Using modeloCommand As New SqlCommand(modeloQuery, connection)
                        modeloCommand.Parameters.AddWithValue("@Placa", placa)
                        Dim modeloReader As SqlDataReader = modeloCommand.ExecuteReader()
                        If modeloReader.HasRows Then
                            modeloReader.Read()
                            Dim modelo As String = modeloReader("Modelo").ToString()
                            ddlAnos.Items.Clear()
                            ddlAnos.Items.Add(modelo)
                            modeloReader.Close()
                        End If
                    End Using

                Else
                End If
                connection.Close()
            End Using
        End Using
    End Sub




    Protected Sub InsertarVehiculo(sender As Object, e As EventArgs)
        Dim placa As String = txtPlaca.Text
        Dim marca As String = ddlMarca.SelectedItem.Text
        Dim submarca As String = ddlSubmarca.SelectedItem.Text
        Dim año As Integer = Integer.Parse(ddlAnos.SelectedItem.Text)
        Dim color As String = ddlColor.SelectedItem.Text
        Dim numSerie As String = txtNumS.Text
        Dim numMotor As String = txtMotor.Text
        Dim combustible As String = ddlCombustible.SelectedItem.Text
        Dim estado As String = ddlEstados.SelectedItem.Text
        Dim municipio As String = ddlMunicipios.SelectedItem.Text
        Dim localidad As String = ddlLocalidades.SelectedItem.Text
        Dim matricula As String = txtMatricula.Text
        Dim dueño As String = If(rdrDuenoac.Checked, "Dueño actual", "Dueño anterior")

        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

        Dim query As String = "INSERT INTO EJAG_RegistroVehiculos (Placa, Marca, Submarca, Modelo, Color, NumeroSerie, NumeroMotor, Combustible, Estado, Municipio, Localidad, Matricula, Dueno) VALUES (@placa, @marca, @submarca, @año, @color, @numSerie, @numMotor, @combustible, @estado, @municipio, @localidad, @matricula, @dueño)"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@placa", placa)
                command.Parameters.AddWithValue("@marca", marca)
                command.Parameters.AddWithValue("@submarca", submarca)
                command.Parameters.AddWithValue("@año", año)
                command.Parameters.AddWithValue("@color", color)
                command.Parameters.AddWithValue("@numSerie", numSerie)
                command.Parameters.AddWithValue("@numMotor", numMotor)
                command.Parameters.AddWithValue("@combustible", combustible)
                command.Parameters.AddWithValue("@estado", estado)
                command.Parameters.AddWithValue("@municipio", municipio)
                command.Parameters.AddWithValue("@localidad", localidad)
                command.Parameters.AddWithValue("@matricula", matricula)
                command.Parameters.AddWithValue("@dueño", dueño)

                Try
                    ' Abrir la conexión y ejecutar la consulta
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
=                        Response.Write("<script>alert('Vehículo insertado correctamente.');</script>")
                    Else
                        Response.Write("<script>alert('Error al insertar el vehículo.');</script>")
                    End If
                Catch ex As Exception
                    Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                End Try
            End Using
        End Using
    End Sub




    Protected Sub ModificarVehiculo(sender As Object, e As EventArgs) Handles btnModificarVehiculo.Click
        Dim placa As String = txtPlaca.Text
        Dim marca As String = ddlMarca.SelectedItem.Text
        Dim submarca As String = ddlSubmarca.SelectedItem.Text
        Dim año As Integer = Integer.Parse(ddlAnos.SelectedItem.Text)
        Dim color As String = ddlColor.SelectedItem.Text
        Dim numSerie As String = txtNumS.Text
        Dim numMotor As String = txtMotor.Text
        Dim combustible As String = ddlCombustible.SelectedItem.Text
        Dim estado As String = ddlEstados.SelectedItem.Text
        Dim municipio As String = ddlMunicipios.SelectedItem.Text
        Dim localidad As String = ddlLocalidades.SelectedItem.Text
        Dim matricula As String = txtMatricula.Text
        Dim dueño As String = If(rdrDuenoac.Checked, "Dueño actual", "Dueño anterior")

        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

        Dim query As String = "UPDATE EJAG_RegistroVehiculos SET Marca = @marca, Submarca = @submarca, Modelo = @año, Color = @color, NumeroSerie = @numSerie, NumeroMotor = @numMotor, Combustible = @combustible, Estado = @estado, Municipio = @municipio, Localidad = @localidad, Matricula = @matricula, Dueno = @dueño WHERE Placa = @placa"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                ' Asignar los parámetros de la consulta
                command.Parameters.AddWithValue("@placa", placa)
                command.Parameters.AddWithValue("@marca", marca)
                command.Parameters.AddWithValue("@submarca", submarca)
                command.Parameters.AddWithValue("@año", año)
                command.Parameters.AddWithValue("@color", color)
                command.Parameters.AddWithValue("@numSerie", numSerie)
                command.Parameters.AddWithValue("@numMotor", numMotor)
                command.Parameters.AddWithValue("@combustible", combustible)
                command.Parameters.AddWithValue("@estado", estado)
                command.Parameters.AddWithValue("@municipio", municipio)
                command.Parameters.AddWithValue("@localidad", localidad)
                command.Parameters.AddWithValue("@matricula", matricula)
                command.Parameters.AddWithValue("@dueño", dueño)

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        Response.Write("<script>alert('Vehículo modificado correctamente.');</script>")
                    Else
                        Response.Write("<script>alert('Error al modificar el vehículo.');</script>")
                    End If
                Catch ex As Exception
                    Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                End Try
            End Using
        End Using
    End Sub

    Protected Sub EliminarVehiculo(sender As Object, e As EventArgs)
        Dim placa As String = txtPlaca.Text

        Dim connectionString As String = "Data Source=LEAH\SQLEXPRESS;Initial Catalog=2231122112;User ID=sa;Password=aaa;"

        Dim query As String = "DELETE FROM EJAG_RegistroVehiculos WHERE Placa = @placa"

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@placa", placa)

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        Response.Write("<script>alert('Vehículo eliminado correctamente.');</script>")
                    Else
                        Response.Write("<script>alert('No se encontró ningún vehículo con la placa especificada.');</script>")
                    End If
                Catch ex As Exception
                    Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                End Try
            End Using
        End Using
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Dim matricula As String = txtMatricula.Text.Trim().ToUpper()

        BuscarMatriculaEnGridView(GridViewVehiculos, 11, matricula)

        BuscarMatriculaEnGridView(GridViewPersonas, 0, matricula)
    End Sub

    Private Sub BuscarMatriculaEnGridView(gridView As GridView, matriculaColumnIndex As Integer, matricula As String)
        For Each row As GridViewRow In gridView.Rows
            Dim matriculaRow As String = row.Cells(matriculaColumnIndex).Text.Trim().ToUpper()

            If matriculaRow = matricula Then
                row.BackColor = System.Drawing.Color.Yellow
                row.Visible = True ' Mostrar la fila resaltada
            Else
                row.Visible = False
            End If
        Next
    End Sub







End Class
