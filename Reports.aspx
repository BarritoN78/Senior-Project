<%@ Page Title="Reports" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Reports.aspx.vb" Inherits="Grp_5_Web_App.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="tbl_Filters" runat ="server" Width="97%">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel ID="pn_Tables" runat="server">
                    <asp:Label ID="lbl_Tables" runat="server" Text="Choose Table:"></asp:Label>
                    <asp:DropDownList ID="drp_Tables" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_Tables_SelectedIndexChanged">
                        <asp:ListItem>Checking</asp:ListItem>
                        <asp:ListItem>Assets</asp:ListItem>
                        <asp:ListItem>People</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel ID="pn_FilterBy" runat="server">
                    <asp:Label ID="lbl_FilterBy" runat="server" Text="Filter By:"></asp:Label>
                    <asp:DropDownList ID="drp_FilterBy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_FilterBy_SelectedIndexChanged">
                        <%--Fill with Column Names or needed reports based on Table--%>
                    </asp:DropDownList>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel ID="pn_DateFilter" runat="server">
                    <asp:Label ID="lbl_DateFilter" runat="server" Text="Date:"></asp:Label>
                    <asp:DropDownList ID="drp_MonthFilter" runat="server">
                        <%--Populate with code when made visible--%>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drp_DayFilter" runat="server">
                        <%--Populate with code when made visible--%>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drp_YearFilter" runat="server">
                        <%--Populate with code when made visible--%>
                    </asp:DropDownList>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel ID="pn_TextFilter" runat="server">
                    <asp:Label ID="lbl_TextFilter" runat="server" Text="Filter Text:"></asp:Label>
                    <asp:TextBox ID="txt_TextFilter" runat="server" AutoPostBack="true"></asp:TextBox>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btn_Filter" runat="server" Text="Filter" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>          
    <asp:GridView ID="grd_Data" runat="server" CssClass="grd_Data">
    </asp:GridView>
</asp:Content>
