﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Default/MasterPage.master" AutoEventWireup="true" CodeFile="BR_BranchIntakeList.aspx.cs" Inherits="AdminPanel_BranchIntake_BR_BranchIntake_BR_BranchIntakeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageHeader" runat="Server">
    <asp:Label ID="lblPageHeader_XXXXX" runat="server" Text="Branch Intake"></asp:Label>
    <small>
        <asp:Label ID="lblPageHeaderInfo_XXXXX" runat="server" Text="Master"></asp:Label></small>
    <span class="pull-right">
        <small>
            <asp:HyperLink ID="hlShowHelp" SkinID="hlShowHelp" runat="server"></asp:HyperLink>
        </small>
    </span>
</asp:Content>
    
<asp:Content ID="Content3" ContentPlaceHolderID="cphBreadcrumb" runat="Server">
    <li>
        <i class="fa fa-home"></i>
        <asp:HyperLink ID="hlHome" runat="server" NavigateUrl="~/AdminPanel/Default.aspx" Text="Home"></asp:HyperLink>
        <i class="fa fa-angle-right"></i>
    </li>
    <li class="active">
        <asp:Label ID="lblBreadCrumbLast" runat="server" Text="Branch Intake"></asp:Label>
    </li>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphPageContent" runat="Server">
    <!--Help Text-->
    <ucHelp:ShowHelp ID="ucHelp" runat="server" />
    <!--Help Text End-->

    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hfHeaders" runat="server" />
    <%-- List --%>
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <ucMessage:ShowMessage ID="ucMessage" runat="server" ViewStateMode="Disabled" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Label SkinID="lblSearchHeaderIcon" runat="server"></asp:Label>
                                <asp:Label ID="lblSearchHeaderIcon" SkinID="" Text="Branch Intakes" runat="server"></asp:Label>
                                <label class="control-label">&nbsp;</label>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="TableContent">
                                        <table class="table table-bordered table-advanced table-striped " id="tblIncomeList">
                                            <%-- Table Header --%>
                                            <thead>
                                                <tr class="TRDark">
                                                    <asp:Repeater ID="rpAddmissionYearHead" runat="server">
                                                        <ItemTemplate>
                                                            <th class="text-center">
                                                                <asp:Label ID="lblMonth" runat="server" Text='<%#Container.DataItem %>'></asp:Label>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </tr>
                                            </thead>
                                            <%-- END Table Header --%>

                                            <tbody>
                                                <asp:Repeater ID="rpIntakeData" runat="server" OnItemDataBound="rpIntake_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("Branch") %>'></asp:Label>
                                                            </td>
                                                            <asp:Repeater ID="rpAddmissionYearBody" runat="server">
                                                                <ItemTemplate>
                                                                    <td class="text-right">
                                                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>' Visible="false"></asp:Label>
                                                                        <asp:TextBox ID="txtIntake" CssClass="form-control" runat="server" Text='<%# Eval("Intake") %>' />
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 text-right">
                                    <asp:Button ID="btnSave" runat="server" Text="save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnClear" runat="server" Text="clear" OnClick="btnClear_Click" CssClass="btn btn-secondary" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- END List --%>

    <%-- Loading  --%>
    <asp:UpdateProgress ID="upr" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server" Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server" SkinID="UpdatePanelLoding" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- END Loading  --%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="cphScripts" runat="Server">
</asp:Content>