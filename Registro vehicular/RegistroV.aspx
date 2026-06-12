<%-- Matricula: 223112212, Nombre completo: Alvarado Gonzalez Erick Jesus, Materia: Programacion cliente-servidor, Grupo: SFTW_05_01 y Carrera: Ingeniería en software --%>


<%@ Page Title="Registro Vehicular" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RegistroV.aspx.vb" Inherits="EXAMEN_2P.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        
        body {
            background-color: papayawhip; /* Color de fondo de la página */
        }

        

          .table {
            background-color: whitesmoke; /* Color de fondo de la tabla */
            border-collapse: collapse; /* Colapso de bordes */
            width: 100%; /* Ancho de la tabla */
        }

        .table th, .table td {
            border: 1px solid #ddd; /* Borde de las celdas */
            padding: 8px; /* Espaciado interno de las celdas */
            text-align: left; /* Alineación del texto a la izquierda */
        }

        .table th {
            background-color: white; /* Color de fondo de la fila encabezado */
            color: white; /* Color del texto en la fila encabezado */
        }

        

    input[type=radio] {
        background-color: white;
    }

    .gridview-header {
        color: black; 
        font-weight: bold;
    }

    .table th {
        color: black;
    }
    .label-text {
        background-color: black;
        color: white;
        padding: 5px 10px;
        border-radius: 5px;
        display: inline-block; 
    }

    </style>

    <h1>Registro Vehicular</h1>

     <div class="row">
     <div class="col-md-3">
        <span class="label-text">Placa</span>

         <asp:TextBox ID="txtPlaca" runat="server" placeholder="Placa"></asp:TextBox>
     </div>
         </div>

    <div class="row">
        <div class="col-md-4">
                    <span class="label-text">Marca</span>

            <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceMarca" DataTextField="marca" DataValueField="cve_marca"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceMarca" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_marca, marca FROM EJAG_marca"></asp:SqlDataSource>
        </div>

        <div class="col-md-4">
                    <span class="label-text">Submarca</span>

            <asp:DropDownList ID="ddlSubmarca" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceSubmarca" DataTextField="submarca" DataValueField="cve_subm"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceSubmarca" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT cve_subm, submarca FROM EJAG_sub WHERE (cve_marca = @Cve_marca) ORDER BY submarca">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlMarca" PropertyName="SelectedValue" Name="cve_marca"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <div class="col-md-3">
                    <span class="label-text">Modelo</span>

            <<asp:DropDownList ID="ddlAnos" runat="server"></asp:DropDownList>
        </div>

        <div class="col-md-3">
                    <span class="label-text">Color</span>

            <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceColor" DataTextField="Color" DataValueField="Cve_color"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceColor" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_color, color FROM EJAG_color"></asp:SqlDataSource>
        </div>

    </div>

    <div class="row">
        <div class="col-md-3">
                    <span class="label-text">NumeroSerie</span>

            <asp:TextBox ID="txtNumS" runat="server" placeholder="NUmeroSerie"></asp:TextBox>
        </div>

         <div class="col-md-3">
                     <span class="label-text">NumeroMotor</span>

     <asp:TextBox ID="txtMotor" runat="server" placeholder="Num Motor"></asp:TextBox>
 </div>

        
        <div class="col-md-3">
                    <span class="label-text">Combustible</span>

            <asp:DropDownList ID="ddlCombustible" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceCombustible" DataTextField="Combustible" DataValueField="Cve_combustible"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceCombustible" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_combustible, combustible FROM EJAG_combustible"></asp:SqlDataSource>
        </div>

        <div class="col-md-1">
                    <span class="label-text">Estado</span>

            <asp:DropDownList ID="ddlEstados" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceEstados" DataTextField="Estado" DataValueField="Cve_estado"></asp:DropDownList>
<asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_estado, Estado FROM EJAG_estados"></asp:SqlDataSource>

                    <span class="label-text">Municipio</span>

              <asp:DropDownList ID="ddlMunicipios" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceMunicipios" DataTextField="Municipio" DataValueField="Cve_municipio"></asp:DropDownList>
  <asp:SqlDataSource runat="server" ID="SqlDataSourceMunicipios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_municipio, Municipio FROM EJAG_municipios WHERE (Cve_estado = @Cve_estado) ORDER BY Municipio">
      <SelectParameters>
          <asp:ControlParameter ControlID="ddlEstados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
      </SelectParameters>
  </asp:SqlDataSource>

                    <span class="label-text">Localidad</span>

            <asp:DropDownList ID="ddlLocalidades" runat="server" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="Cve_localidad" AutoPostBack="True"></asp:DropDownList>
 <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_localidad, localidad, latitud_decimal, longitud_decimal, altitud FROM EJAG_localidades WHERE (Cve_estado = @Cve_estado) AND (Cve_municipio = @Cve_municipio) ORDER BY localidad">
     <SelectParameters>
         <asp:ControlParameter ControlID="ddlEstados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
         <asp:ControlParameter ControlID="ddlMunicipios" PropertyName="SelectedValue" Name="Cve_municipio"></asp:ControlParameter>
     </SelectParameters>
 </asp:SqlDataSource>
        </div>

    </div>

    <div class="row">
                <span class="label-text">Matricula/nomina</span>

 <asp:TextBox ID="txtMatricula" runat="server" placeholder="Matricula o nomina"></asp:TextBox>
    </div>

    <div class="row">
    <div class="col-md-3">
                <span class="label-text">Dueno</span>

        <asp:RadioButton ID="rdrDuenoac" runat="server" Text="Dueño actual" GroupName="grupoDueno" />
        <asp:RadioButton ID="rdrDuenoan" runat="server" Text="Dueño anterior" GroupName="grupoDueno" />
    </div>

        <div class="col-md-4">
<asp:Button ID="btnInsertarVehiculo" runat="server" Text="Insertar Vehículo" OnClick="InsertarVehiculo" />
<asp:Button ID="btnModificarVehiculo" runat="server" Text="Modificar Vehículo" OnClick="ModificarVehiculo" />
<asp:Button ID="btnEliminar" runat="server" Text="Eliminar Vehículo" OnClick="EliminarVehiculo" />
<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />



        </div>
</div>

      <div class="row">
    <div class="col-md-12">
        <asp:GridView ID="GridViewPersonas" runat="server" AutoGenerateColumns="False" DataKeyNames="Matricula" CssClass="table" OnRowCommand="GridViewPersonas_RowCommand">
            <Columns>
                <asp:BoundField DataField="Matricula" HeaderText="Matricula" SortExpression="Matricula" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Paterno" HeaderText="Paterno" SortExpression="Paterno" />
                <asp:BoundField DataField="Materno" HeaderText="Materno" SortExpression="Materno" />
                <asp:BoundField DataField="Nombre_estado" HeaderText="Estado" SortExpression="Nombre_estado" />
                <asp:BoundField DataField="Nombre_municipio" HeaderText="Municipio" SortExpression="Nombre_municipio" />
                <asp:BoundField DataField="Nombre_localidad" HeaderText="Localidad" SortExpression="Nombre_localidad" />
                <asp:BoundField DataField="Latitud" HeaderText="Latitud" SortExpression="Latitud" />
                <asp:BoundField DataField="Longitud" HeaderText="Longitud" SortExpression="Longitud" />
                <asp:BoundField DataField="Tipo" HeaderText="Propietario Actual" SortExpression="Tipo" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnDetalle" runat="server" CommandName="MostrarDetalle" CommandArgument="<%# Container.DataItemIndex %>" Text="Ver Detalle" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblDetalle" runat="server" Text=""></asp:Label>

    </div>

</div>

  <div class="row">
        <div class="col-md-12">
<asp:GridView ID="GridViewVehiculos" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table" OnRowCommand="GridViewVehiculos_RowCommand">
    <Columns>
        <asp:BoundField DataField="Placa" HeaderText="Placa" />
        <asp:BoundField DataField="Marca" HeaderText="Marca" />
        <asp:BoundField DataField="Submarca" HeaderText="Submarca" />
        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
        <asp:BoundField DataField="Color" HeaderText="Color" />
        <asp:BoundField DataField="NumeroSerie" HeaderText="Número de Serie" />
        <asp:BoundField DataField="NumeroMotor" HeaderText="Número de Motor" />
        <asp:BoundField DataField="Combustible" HeaderText="Combustible" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
        <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
        <asp:BoundField DataField="Dueno" HeaderText="Dueño" />
          <asp:BoundField DataField="Latitud" HeaderText="Latitud" Visible="false" />
    <asp:BoundField DataField="Longitud" HeaderText="Longitud" Visible="false" />

        <asp:TemplateField HeaderText="Seleccionar">
            <ItemTemplate>
                <asp:Button ID="btnSeleccionar" runat="server" CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' Text="Seleccionar" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

        </div>

    </div>

       <div class="row">
           <div class="col-md-4">
               <div>
</div>
               <map:GoogleMap ID="googleMap1" runat="server" MapType="HYBRID" Zoom="16" Latitude="19.9798047" Longitude="-98.6853093" CssClass="map" DefaultAdress="Universidad Politecnica de Pachuca" Width="100%"></map:GoogleMap>

           </div>

       </div>


</asp:Content>
