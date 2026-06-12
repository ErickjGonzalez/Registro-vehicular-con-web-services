<%-- Matricula: 223112212, Nombre completo: Alvarado Gonzalez Erick Jesus, Materia: Programacion cliente-servidor, Grupo: SFTW_05_01 y Carrera: Ingeniería en software --%>


<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Walumno.aspx.vb" Inherits="EXAMEN_2P.Walumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Pagina de alumnos</h1>

        <div class ="row">
        <div class="col-md-3">
            <asp:TextBox ID="txtMatricula" runat="server" placeholder="Matricula"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtPaterno" runat="server" placeholder="Paterno"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtMaterno" runat="server" placeholder="Materno"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtRFC" runat="server" placeholder="RFC"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtCURP" runat="server" placeholder="CURP"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtSexo" runat="server" placeholder="Sexo"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-4">
            <asp:DropDownList ID="Estados" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceEstados" DataTextField="Estado" DataValueField="Cve_estado"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_estado, Estado FROM EJAG_estados"></asp:SqlDataSource>
        </div>

        <div class="col-md-4">
            <asp:DropDownList ID="Municipios" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceMunicipios" DataTextField="Municipio" DataValueField="Cve_municipio"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceMunicipios" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_municipio, Municipio FROM EJAG_municipios WHERE (Cve_estado = @Cve_estado) ORDER BY Municipio">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Estados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

    </div>
        <div class="col-md-4">
    <asp:DropDownList ID="Localidades" runat="server" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="Cve_localidad"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_localidad, localidad, latitud_decimal, longitud_decimal, altitud FROM EJAG_localidades WHERE (Cve_estado = @Cve_estado) AND (Cve_municipio = @Cve_municipio) ORDER BY localidad">
        <SelectParameters>
            <asp:ControlParameter ControlID="Estados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
            <asp:ControlParameter ControlID="Municipios" PropertyName="SelectedValue" Name="Cve_municipio"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
</div>

    <asp:Button ID="Insertar" runat="server" Text="Insertar" />
    <asp:Button ID="Modificar" runat="server" Text="Modificar" />
    <asp:Button ID="Eliminar" runat="server" Text="Eliminar" />

   <asp:GridView ID="GridView1" runat="server"></asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_estado, Estado FROM EJAG_estados"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_municipio, Municipio FROM EJAG_municipios WHERE (Cve_estado = @Cve_estado) ORDER BY Municipio">
        <SelectParameters>
            <asp:ControlParameter ControlID="Estados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_localidad, localidad, latitud_decimal, longitud_decimal, altitud FROM EJAG_localidades WHERE (Cve_estado = @Cve_estado) AND (Cve_municipio = @Cve_municipio) ORDER BY localidad">
        <SelectParameters>
            <asp:ControlParameter ControlID="Estados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
            <asp:ControlParameter ControlID="Municipios" PropertyName="SelectedValue" Name="Cve_municipio"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSourceAlumnos" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Matricula, Nombre, Paterno, Materno, RFC, CURP, Cve_estado, Cve_municipio, Cve_localidad FROM EJAG_alumnos"></asp:SqlDataSource>
</asp:Content>