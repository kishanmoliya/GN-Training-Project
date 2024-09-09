<%@ Page Title="" Language="C#" MasterPageFile="~/Default/MasterPage.master" AutoEventWireup="true" CodeFile="IncomeDataTree.aspx.cs" Inherits="AdminPanel_DataTree_HospitalDataTree_IncomeDataTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageHeader" runat="Server">
    <asp:Label ID="lblPageHeader_XXXXX" runat="server" Text="Hospital Tree"></asp:Label>
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
        <asp:Label ID="lblBreadCrumbLast" runat="server" Text="Hospital Tree"></asp:Label>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageContent" runat="Server">
    <!--Help Text-->
    <ucHelp:ShowHelp ID="ucHelp" runat="server" />
    <!--Help Text End-->
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>

    <%-- Search --%>
    <asp:UpdatePanel ID="upApplicationFeature" runat="server">
        <ContentTemplate>
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label SkinID="lblSearchHeaderIcon" runat="server"></asp:Label>
                        <asp:Label ID="lblSearchHeader" SkinID="lblSearchHeaderText" runat="server"></asp:Label>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse pull-right"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div role="form">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-search"></i>
                                            </span>
                                            <asp:TextBox ID="txtHospital" CssClass="First form-control" runat="server" PlaceHolder="Enter Hospital"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="fa fa-search"></i>
                                            </span>
                                            <asp:TextBox ID="txtPrintName" CssClass="form-control" runat="server" PlaceHolder="Enter Print Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Button ID="btnSearch" SkinID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClear" runat="server" SkinID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- End Search --%>

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
                                <asp:Label SkinID="lblSearchResultHeaderIcon" runat="server"></asp:Label>
                                <asp:Label ID="lblSearchResultHeader" SkinID="lblSearchResultHeaderText" runat="server"></asp:Label>
                                <label class="control-label">&nbsp;</label>
                            </div>
                            <div class="tools">
                                <div>
                                    <asp:HyperLink SkinID="hlAddNew" ID="hlAddNew" NavigateUrl="~/AdminPanel/Master/MST_Hospital/MST_HospitalAddEdit.aspx" runat="server"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row" runat="server" id="Div_SearchResult" visible="false">
                                <div class="col-md-12">
                                    <div id="TableContent">
                                        <table class="table table-bordered table-advanced table-striped table-hover" id="sample_1">
                                            <%-- Table Header --%>
                                            <thead>
                                                <tr class="TRDark">
                                                    <th class="text-center">
                                                        <asp:Label ID="lbhAdd" runat="server" Text="#"></asp:Label>
                                                    </th>
                                                    <th class="text-left">
                                                        <asp:Label ID="lbhHospital" runat="server" Text="Hospital"></asp:Label>
                                                    </th>
                                                    <th class="text-left">
                                                        <asp:Label ID="lbhPrintName" runat="server" Text="Print Name"></asp:Label>
                                                    </th>
                                                    <th class="text-left">
                                                        <asp:Label ID="lbhPrintLine1" runat="server" Text="Print Line1"></asp:Label>
                                                    </th>
                                                    <th class="text-left">
                                                        <asp:Label ID="lbhRemarks" runat="server" Text="Remarks"></asp:Label>
                                                    </th>
                                                    <th class="nosortsearch text-nowrap text-center">
                                                        <asp:Label ID="lbhAction" runat="server" Text="Action"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <%-- END Table Header --%>

                                            <tbody>
                                                <asp:Repeater ID="rpData" runat="server">
                                                    <ItemTemplate>
                                                        <%-- Table Rows --%>
                                                        <tr class="odd gradeX text-center">
                                                            <td class="text-center">
                                                                <i
                                                                    class="collapse-icon fa fa-plus-circle text-info"
                                                                    data-toggle="collapse"
                                                                    data-target="#subGroup<%# Container.ItemIndex %>"
                                                                    aria-expanded="false"></i>

                                                            </td>
                                                            <td class="text-left">
                                                                <asp:HyperLink ID="hlViewHospitalID" NavigateUrl='<%# "~/AdminPanel/Master/MST_Hospital/MST_HospitalView.aspx?HospitalID=" + GNForm3C.CommonFunctions.EncryptBase64(Eval("HospitalID").ToString()) %>' data-target="#viewiFrameReg" CssClass="modalButton" data-toggle="modal" runat="server"><%#Eval("Hospital") %></asp:HyperLink>
                                                            </td>
                                                            <td class="text-left">
                                                                <%#Eval("PrintName") %>
                                                            </td>
                                                            <td class="text-left">
                                                                <%#Eval("PrintLine1") %>
                                                            </td>
                                                            <td class="text-left">
                                                                <%#Eval("Remarks") %>
                                                            </td>text-nowrap text-center">
                                                                <asp:HyperLink ID="hlView" SkinID="View" NavigateUrl='<%# "~/AdminPanel/Master/MST_Hospital/MST_HospitalView.aspx?HospitalID=" + GNForm3C.CommonFunctions.EncryptBase64(Eval("HospitalID").ToString()) %>' data-target="#viewiFrameReg" data-toggle="modal" runat="server"></asp:HyperLink>
                                                                <asp:HyperLink ID="hlEdit" SkinID="Edit" NavigateUrl='<%# "~/AdminPanel/Master/MST_Hospital/MST_HospitalAddEdit.aspx?HospitalID=" + GNForm3C.CommonFunctions.EncryptBase64(Eval("HospitalID").ToString()) %>' runat="server"></asp:HyperLink>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                    SkinID="Delete"
                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete record ? ');"
                                                                    CommandName="DeleteRecord"
                                                                    CommandArgument='<%#Eval("HospitalID") %>'>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <%-- END Table Rows --%>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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



