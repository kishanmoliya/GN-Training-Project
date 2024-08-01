<%@ Page Title="" Language="C#" MasterPageFile="~/Default/MasterPage.master" AutoEventWireup="true" CodeFile="MasterDashboard.aspx.cs" Inherits="AdminPanel_MasterDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .right-align {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageHeader" runat="Server">
    <asp:Label ID="lblPageHeader_XXXXX" runat="server" Text="Master Dashboard"></asp:Label>
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
        <asp:Label ID="lblBreadCrumbLast" runat="server" Text="MastDashboard"></asp:Label>
    </li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphPageContent" runat="Server">
    <!--Help Text-->
    <ucHelp:ShowHelp ID="ucHelp" runat="server" />
    <!--Help Text End-->
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upMST_DSB" runat="server" EnableViewState="true" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <asp:UpdatePanel ID="upMST_DSB2" runat="server" EnableViewState="true" UpdateMode="Conditional" ChildrenAsTriggers="false">

                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <ucMessage:ShowMessage ID="ucMessage" runat="server" />
                            <asp:ValidationSummary ID="ValidationSummary1" SkinID="VS" runat="server" />
                        </div>
                    </div>

                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Label SkinID="lblFormHeaderIcon" ID="lblFormHeaderIcon" runat="server"></asp:Label>
                                <span class="caption-subject font-green-sharp bold uppercase">
                                    <asp:Label ID="lblFormHeader" runat="server" Text=""></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal" role="form">
                                <div class="form-body">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">
                                            <span class="required">*</span>
                                            <asp:Label ID="lblHospitalID_XXXXX" runat="server" Text="Hospital"></asp:Label>
                                        </label>
                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlHospitalID" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvHospitalID" SetFocusOnError="True" runat="server" Display="Dynamic" ControlToValidate="ddlHospitalID" ErrorMessage="Select Hospital" InitialValue="-99"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnShow" runat="server" SkinID="btnShow" OnClick="btnShow_Click" OnClientClick="toggleUpdatePanel();" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </label>
		
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- BEGIN Main contant-->
    <asp:UpdatePanel ID="upCount" runat="server" EnableViewState="true" UpdateMode="Conditional" ChildrenAsTriggers="false" style="display: none">
        <ContentTemplate>
            <!-- BEGIN Dashboard Counts-->
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption font-green">
                        <i class="fa fa-line-chart font-green"></i>
                        <span class="caption-subject bold uppercase">Total Overview</span>
                    </div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal" role="form">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <a class="dashboard-stat dashboard-stat-v2 blue" href="Account/ACC_Income/ACC_IncomeList.aspx">
                                        <div class="visual">
                                            <i class="fa fa-comments"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <asp:Label runat="server" ID="lblIncomeCount"></asp:Label>
                                            </div>
                                            <div class="desc">Incomes </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <a class="dashboard-stat dashboard-stat-v2 red" href="Account/ACC_Expense/ACC_ExpenseList.aspx">
                                        <div class="visual">
                                            <i class="fa fa-list"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <asp:Label runat="server" ID="lblExpenseCount"></asp:Label>
                                            </div>
                                            <div class="desc">Expenses</div>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <a class="dashboard-stat dashboard-stat-v2 green" href="Master/MST_SubTreatment/MST_SubTreatmentList.aspx">
                                        <div class="visual">
                                            <i class="fa fa-list"></i>
                                        </div>
                                        <div class="details">
                                            <div class="number">
                                                <asp:Label runat="server" ID="lblSubTreatmentCount"></asp:Label>
                                            </div>
                                            <div class="desc">SubTreatment</div>
                                        </div>
                                    </a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Dashboard Counts-->

            <!-- BEGIN Income Details-->
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-bullhorn "></i>Current Year Incomes
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body" style="display: block;">
                    <div class="table-responsive">
                        <div>
                            <asp:Label ID="lblNoIncome" runat="server" Text="No Income Found" Visible="False" class="text-danger"></asp:Label>
                            <asp:GridView ID="grdIncome" runat="server" AutoGenerateColumns="True" OnRowDataBound="grdIncExp_RowDataBound">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Income Details-->

            <!-- BEGIN Expense Details-->
            <div class="portlet box red">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-bullhorn "></i>Current Year Expense
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body" style="display: block;">
                    <div class="table-responsive">
                        <div>
                             <asp:Label ID="lblNoExpense" runat="server" Text="No Expense Found" Visible="False" class="text-danger"></asp:Label>
                            <asp:GridView ID="grdExpense" runat="server" AutoGenerateColumns="True" OnRowDataBound="grdIncExp_RowDataBound">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END Income Details-->

            <!-- BEGIN Income Treatment Type Summary-->
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-bullhorn "></i>Treatment Summary
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body" style="display: block;">
                    <div class="table-responsive" id="TableContent">
                          <asp:Label ID="lblNoRecords" runat="server" Text="No Treatment Summary Found" Visible="False" class="text-danger"></asp:Label>
                         <asp:Panel ID="pnlRepeater" runat="server">
                        <table class="table table-bordered table-advanced table-striped table-hover" id="sample_1">
                            <%-- Table Header --%>
                            <thead>
                                <tr class="TRDark">
                                    <th class="text-center" style="width: 20px;">
                                        <asp:Label ID="lbhSerialNo" runat="server" Text="Sr."></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="lblTreatment" runat="server" Text="Treatment Type"></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="lblPatientCount" runat="server" Text="Patient Count"></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="lblIncomeAmount" runat="server" Text="Income Amount"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <%-- END Table Header --%>

                            <tbody>
                                <asp:Repeater ID="rpData" runat="server">
                                    <ItemTemplate>
                                        <%-- Table Rows --%>
                                        <tr class="odd gradeX">
                                            <td class="text-center">
                                                <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td class="text-center">
                                                <%#Eval("Treatment") %>
                                            </td>
                                            <td class="text-center">
                                                <%#Eval("PatientCount") %>
                                            </td>
                                            <td class="text-center">
                                                <%#Eval("IncomeAmount",GNForm3C.CV.DefaultCurrencyFormatWithDecimalPoint) %>
                                            </td>
                                        </tr>
                                        <%-- END Table Rows --%>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                     </asp:Panel>
                    </div>
                </div>
            </div
            
            <!-- END Income Treatment Type Summary-->
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- END Main contant-->


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScripts" runat="Server">
    <script type="text/javascript">
        function toggleUpdatePanel() {
            var dropdown = document.getElementById('<%= ddlHospitalID.ClientID %>');
            var upCount = document.getElementById('<%= upCount.ClientID %>');

            if (dropdown.value !== "-99") { // Assuming "0" is the value for "Select Hospital"
                upCount.style.display = 'block';
            }
        }
    </script>
</asp:Content>

