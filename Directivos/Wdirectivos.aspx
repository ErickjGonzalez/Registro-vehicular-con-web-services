<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Wdirectivos.aspx.vb" Inherits="EXAMEN_2P.Wdirectivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <h1>Pagina directivos</h1>
    <div class="row">
        <div class="col-md-3">
            <asp:TextBox ID="txtNomina" runat="server" placeholder="Nomina"></asp:TextBox>
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

        <div class="col-md-4">
            <asp:DropDownList ID="Localidades" runat="server" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="Cve_localidad"></asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Cve_localidad, localidad, latitud_decimal, longitud_decimal, altitud FROM EJAG_localidades WHERE (Cve_estado = @Cve_estado) AND (Cve_municipio = @Cve_municipio) ORDER BY localidad">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Estados" PropertyName="SelectedValue" Name="Cve_estado"></asp:ControlParameter>
                    <asp:ControlParameter ControlID="Municipios" PropertyName="SelectedValue" Name="Cve_municipio"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="Insertar" runat="server" Text="Insertar" OnClick="Insertar_Click" />
            <asp:Button ID="Modificar" runat="server" Text="Modificar" OnClick="Modificar_Click" />
            <asp:Button ID="Eliminar" runat="server" Text="Eliminar" OnClick="Eliminar_Click" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="GridViewDirectivos" runat="server" AutoGenerateColumns="False" DataKeyNames="Nomina" OnSelectedIndexChanged="GridViewDirectivos_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Nomina" HeaderText="Nomina" SortExpression="Nomina" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Paterno" HeaderText="Paterno" SortExpression="Paterno" />
                    <asp:BoundField DataField="Materno" HeaderText="Materno" SortExpression="Materno" />
                    <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                    <asp:BoundField DataField="CURP" HeaderText="CURP" SortExpression="CURP" />
                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="Sexo" />
                    <asp:BoundField DataField="Cve_estado" HeaderText="Cve_estado" SortExpression="Cve_estado" />
                    <asp:BoundField DataField="Cve_municipio" HeaderText="Cve_municipio" SortExpression="Cve_municipio" />
                    <asp:BoundField DataField="Cve_localidad" HeaderText="Cve_localidad" SortExpression="Cve_localidad" />
                  
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
