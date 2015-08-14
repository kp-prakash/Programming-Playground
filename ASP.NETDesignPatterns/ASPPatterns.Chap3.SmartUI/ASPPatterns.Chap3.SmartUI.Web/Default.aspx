<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ASPPatterns.Chap3.SmartUI.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Sales Application!
    </h2>
    <br/>
    <asp:DropDownList ID="ddlDiscountType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscountType_SelectedIndexChanged">
        <asp:ListItem Value="0">No Discount</asp:ListItem>
        <asp:ListItem Value="1">Trade Discount</asp:ListItem>
    </asp:DropDownList>
    <br/>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
        EmptyDataText="There are no data records to display." OnRowDataBound="GridView1_OnRowDataBound">
        <Columns>
            <asp:BoundField DataField="ProductId" HeaderText="ProductId" ReadOnly="True" SortExpression="ProductId" />
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
            <asp:BoundField DataField="RRP" HeaderText="RRP" SortExpression="RRP" />
            <asp:TemplateField HeaderText="SellingPrice" SortExpression="SellingPrice">
                <ItemTemplate>
                    <asp:Label ID="lblSellingPrice" runat="server" Text='’<%# Bind("SellingPrice") %>’'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SellingPrice" HeaderText="SellingPrice" SortExpression="SellingPrice" />
            <asp:TemplateField HeaderText="”Discount”">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDiscount"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="”Savings”">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSavings"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ShopConnectionString1 %>"
        ProviderName="<%$ ConnectionStrings:ShopConnectionString1.ProviderName %>" SelectCommand="SELECT [ProductId], [ProductName], [RRP], [SellingPrice] FROM [Products]">
    </asp:SqlDataSource>
</asp:Content>
