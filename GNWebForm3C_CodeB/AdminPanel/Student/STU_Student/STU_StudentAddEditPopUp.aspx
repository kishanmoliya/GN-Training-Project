<%@ Page Title="" Language="C#" MasterPageFile="~/Default/MasterPageView.master" AutoEventWireup="true" CodeFile="STU_StudentAddEditPopUp.aspx.cs" Inherits="AdminPanel_Student_STU_Student_STU_StudentAddEditPopUp" %>

<asp:Content ID="cnthead" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="cntPageContent" style="display: none"  ContentPlaceHolderID="cphPageContent" runat="Server">
    <!-- BEGIN SAMPLE FORM PORTLET-->
    <div class="portlet light">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label SkinID="lblFormHeaderIcon" ID="lblViewFormHeaderIcon" runat="server"></asp:Label>
                <span class="caption-subject font-green-sharp bold uppercase">Student Details Add/Edit</span>
            </div>
            <div class="tools">
                <asp:HyperLink ID="CloseButton" SkinID="hlClosemymodal" runat="server" ClientIDMode="Static"></asp:HyperLink>
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-horizontal" role="form">
                <asp:ScriptManager ID="sm" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="upEmp_EmployeeDetails" runat="server" EnableViewState="true" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </triggers>
                    <contenttemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <ucMessage:ShowMessage ID="ucMessage" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" SkinID="VS" runat="server" />
                            </div>
                        </div>

                        <div class="portlet light">
                            <div class="portlet-body form">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblStudentName_XXXXX" runat="server" Text="Student Name"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server" PlaceHolder="Enter Student Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvStudentName" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtStudentName" ErrorMessage="Enter Student Name"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblEnrollmentNo_XXXXX" runat="server" Text="Enrollment No."></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtEnrollmentNo" CssClass="form-control" runat="server" onkeypress="return IsPositiveInteger(event)" PlaceHolder="Enter EnrollmentNo"></asp:TextBox>
                                                <asp:CompareValidator ID="cvEnrollmentNo" runat="server" ControlToValidate="txtEnrollmentNo" ErrorMessage="Enter Valid EnrollmentNo" SetFocusOnError="True" Operator="DataTypeCheck" Display="Dynamic" Type="Double"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvEnrollmentNo" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtEnrollmentNo" ErrorMessage="Enter EnrollmentNo"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <asp:Label ID="lblRollNo_XXXXX" runat="server" Text="Roll No."></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtRollNo" CssClass="form-control" runat="server" onkeypress="return IsPositiveInteger(event)" PlaceHolder="Enter RollNo"></asp:TextBox>
                                                <asp:CompareValidator ID="cvRollNo" runat="server" ControlToValidate="txtRollNo" ErrorMessage="Enter Valid RollNo" SetFocusOnError="True" Operator="DataTypeCheck" Display="Dynamic" Type="Integer"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvRollNo" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtRollNo" ErrorMessage="Enter RollNo"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblCurrentSem_XXXXX" runat="server" Text="Current Sem"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlCurrentSem" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCurrentSem" SetFocusOnError="True" runat="server" Display="Dynamic" ControlToValidate="ddlCurrentSem" ErrorMessage="Select Current Semester" InitialValue="-99"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <asp:Label ID="lblEmailInstitute_XXXXX" runat="server" Text="Institute Email"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtEmailInstitute" CssClass="form-control" runat="server" PlaceHolder="Enter Institute Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmailInstitute" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtEmailInstitute" ErrorMessage="Enter Institute Email"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblEmailPersonal_XXXXX" runat="server" Text="Personal Email"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtEmailPersonal" CssClass="form-control" runat="server" PlaceHolder="Enter Personal Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmailPersonal" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtEmailPersonal" ErrorMessage="Enter Personal Email"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblBirthDate_XXXXX" runat="server" Text="Birth Date"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <div class="input-group input-medium date date-picker" data-date-format="yyyy-mm-dd">
                                                    <asp:TextBox ID="dtpBirthDate" CssClass="form-control" runat="server" placeholder="Birth Date"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                    </span>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ControlToValidate="dtpBirthDate" ErrorMessage="Enter Birth Date" Display="Dynamic" Type="Date"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblContactNo_XXXXX" runat="server" Text="Contact No."></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server" PlaceHolder="Enter Contact No."></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvContactNo" SetFocusOnError="True" Display="Dynamic" runat="server" ControlToValidate="txtContactNo" ErrorMessage="Enter Contact No."></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                <span class="required">*</span>
                                                <asp:Label ID="lblGender_XXXXX" runat="server" Text="Gender"></asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlGender" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvGender" SetFocusOnError="True" runat="server" Display="Dynamic" ControlToValidate="ddlGender" ErrorMessage="Select Gender" InitialValue="-99"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-5">
                                                    <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
                                                    <asp:HyperLink ID="hlCancel" runat="server" SkinID="hlCancel" NavigateUrl="~/AdminPanel/Student/STU_Student/STU_StudentList.aspx"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- END SAMPLE FORM PORTLET-->
     <%-- Loading  --%>
    <asp:UpdateProgress ID="upr" runat="server">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server" SkinID="UpdatePanelLoding" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- END Loading  --%>
</asp:Content>
<asp:Content ID="cntScripts" ContentPlaceHolderID="cphScripts" runat="Server">
    <script>
        $(document).keyup(function (e) {
            if (e.keyCode == 27) {
                ;
                $("#CloseButton").trigger("click");
            }
        });
    </script>
</asp:Content>

