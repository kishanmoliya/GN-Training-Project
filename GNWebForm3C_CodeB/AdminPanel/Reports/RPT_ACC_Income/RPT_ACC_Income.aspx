<%@ Page Title="" Language="C#" MasterPageFile="~/Default/MasterPage.master" AutoEventWireup="true" CodeFile="RPT_ACC_Income.aspx.cs" Inherits="AdminPanel_Reports_RPT_ACC_Income_RPT_ACC_Income" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBreadcrumb" Runat="Server">
     <li>
        <i class="fa fa-home"></i>
        <asp:HyperLink ID="hlHome" runat="server" NavigateUrl="~/AdminPanel/Default.aspx" Text="Home"></asp:HyperLink>
        <i class="fa fa-angle-right"></i>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageContent" Runat="Server">
        <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="rvIncomeReport" runat="server" Width="100%" Height="600px">
        <LocalReport ReportPath="E:\GN-Training-Project\GNWebForm3C_CodeB\App_Code\RPT_DataTable\RPT_ACC_IncomeReport.rdlc"></LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScripts" Runat="Server">
</asp:Content>
