<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerManagement.aspx.cs" Inherits="QLKS.CustomerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<body class="no-skin">--%>
        <div ng-app="QLKS" ng-controller="CustomerManagementCtrl">
            <div class="main-container ace-save-state" id="main-container">
                <div class="main-content">
                    <div class="main-content-inner">
                        <div class="page-content">
                            <%--<div class="page-header">
                                <h1>Form Elements
				            <small>
                                <i class="ace-icon fa fa-angle-double-right"></i>
                                Common form elements and layouts
                            </small>
                                </h1>
                            </div>--%>
                            <!-- /.page-header -->

                            <div class="row">
                                <div class="col-xs-12">
                                    <!-- PAGE CONTENT BEGINS -->
                                    <div class="widget-box">
                                        <div class="widget-header widget-header-small">
                                            <h5 class="widget-title lighter">QL Nhân viên</h5>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div class="form-horizontal" role="form">
                                                    <div class="form-group col-sm-4">
                                                        <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Mã nhân viên </label>

                                                        <div class="col-sm-8">
                                                            <input type="text" id="EmployeeID" placeholder="Mã nhân viên" class="form-control" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-sm-4">
                                                        <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Tên nhân viên </label>

                                                        <div class="col-sm-8">
                                                            <input type="text" id="EmployeeName" placeholder="Tên nhân viên" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div class="space-4"></div>

                                                    <div class="clearfix form-actions">
                                                        <div class="col-md-offset-3 col-md-12">
                                                            <button class="btn btn-info" type="button" ng-click="searchEmp()">
                                                                <i class="ace-icon fa fa-check bigger-110"></i>
                                                                Tìm kiếm
                                                            </button>
                                                            &nbsp; &nbsp; &nbsp;
                                                                <a class="modal123" href="#modal-table" role="button" data-toggle="modal" data-id="Create" style="text-decoration: none; color: antiquewhite;">
                                                                    <button class="btn" type="button">
                                                                        <i class="ace-icon fa fa-plus bigger-110"></i>
                                                                        Thêm mới
                                                                    </button>
                                                                </a>
                                                        </div>
                                                    </div>

                                                    <div class="hr hr-18 dotted hr-double"></div>
                                                    <%--<div class="row">
							                                <div class="col-xs-12">--%>
                                                    <!-- PAGE CONTENT BEGINS -->

                                                    <div class="row">
                                                        <div class="col-xs-12">
                                                            <%--<h3 class="header smaller lighter blue">Danh sách nhân viên</h3>--%>

                                                            <div class="clearfix">
                                                                <div class="pull-right tableTools-container"></div>
                                                            </div>
                                                            <div class="table-header">Danh sách nhân viên</div>

                                                            <!-- div.table-responsive -->

                                                            <!-- div.dataTables_borderWrap -->
                                                            <div>
                                                                <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr >
                                                                            <%--<th class="center">
                                                                                <label class="pos-rel">
                                                                                    <input type="checkbox" class="ace" />
                                                                                    <span class="lbl"></span>
                                                                                </label>
                                                                            </th>--%>
                                                                            <th>Mã NV</th>
                                                                            <th>Tên NV</th>
                                                                            <th>SĐT</th>
                                                                            <th>Ngày sinh</th>
                                                                            <th>Email</th>
                                                                            <th>Địa chỉ</th>
                                                                            <th>Chức vụ</th>
                                                                            <%--<th>
                                                                                <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>
                                                                                Update
                                                                            </th>--%>
                                                                            <%--<th class="hidden-480">Status</th>--%>

                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <tr ng-repeat="x in dataTable">
                                                                            <%--<td class="center">
                                                                                <label class="pos-rel">
                                                                                    <input type="checkbox" class="ace" />
                                                                                    <span class="lbl"></span>
                                                                                </label>
                                                                            </td>--%>

                                                                            <td>{{x.MaNV}}</td>
                                                                            <th>{{x.TenNV}}</th>
                                                                            <th>{{x.SDT}}</th>
                                                                            <th>{{x.NgaySinh}}</th>
                                                                            <th>{{x.Email}}</th>
                                                                            <th>{{x.DiaChi}}</th>
                                                                            <th>{{x.ChucVu}}</th>

                                                                            <td>
                                                                                <div class="hidden-sm hidden-xs action-buttons">
                                                                                    <a class="modal123 blue" href="#modal-table" role="button" data-toggle="modal" data-id="Info" data-value="{{x.MaNV}}" style="text-decoration: none; color: antiquewhite;">
                                                                                        <i class="ace-icon fa fa-search-plus bigger-130"></i>
                                                                                    </a>

                                                                                    <a class="modal123 green" href="#modal-table" role="button" data-toggle="modal" data-id="Update" data-value="{{x.MaNV}}" style="text-decoration: none; color: antiquewhite;">
                                                                                        <i class="ace-icon fa fa-pencil bigger-130"></i>
                                                                                    </a>

                                                                                    <a class="red" href="#" id="DeleteEmp" data-value="{{x.MaNV}}">
                                                                                        <i class="ace-icon fa fa-trash-o bigger-130"></i>
                                                                                    </a>
                                                                                </div>

                                                                                <%--<div class="hidden-md hidden-lg">
                                                                                    <div class="inline pos-rel">
                                                                                        <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                                                                            <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                                                                        </button>

                                                                                        <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                                                                            <li>
                                                                                                <a href="#" class="tooltip-info" data-rel="tooltip" title="View">
                                                                                                    <span class="blue">
                                                                                                        <i class="ace-icon fa fa-search-plus bigger-120"></i>
                                                                                                    </span>
                                                                                                </a>
                                                                                            </li>

                                                                                            <li>
                                                                                                <a href="#" class="tooltip-success" data-rel="tooltip" title="Edit">
                                                                                                    <span class="green">
                                                                                                        <i class="ace-icon fa fa-pencil-square-o bigger-120"></i>
                                                                                                    </span>
                                                                                                </a>
                                                                                            </li>

                                                                                            <li>
                                                                                                <a href="#" class="tooltip-error" data-rel="tooltip" title="Delete">
                                                                                                    <span class="red">
                                                                                                        <i class="ace-icon fa fa-trash-o bigger-120"></i>
                                                                                                    </span>
                                                                                                </a>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>--%>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <!-- PAGE CONTENT ENDS -->
                                                    <%--</div><!-- /.col -->
						                                </div><!-- /.row -->--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                    </div>
                </div>
            </div>

            <div id="modal-table" class="modal fade" tabindex="-1">
                <div class="modal-dialog" style="width: 850px!important; height: auto;">
                    <div class="modal-content">
                        <div class="modal-header no-padding">
                            <div class="table-header" id="titleheader">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    <span class="white">&times;</span>
                                </button>
                                Thêm mới
                            </div>
                        </div>

                        <div class="modal-body no-padding">
                            <%--<div class="widget-box">--%>
                                <%--<div class="widget-header widget-header-small">
												<h5 class="widget-title lighter">Search Form</h5>
											</div>--%>
                                <%--<div class="widget-body">--%>
                                    <div class="widget-main">
                                        <div class="form-horizontal" role="form" style="height:200px;">
                                            <div class="form-group col-sm-6" id="manv">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Mã nhân viên </label>

                                                <div class="col-sm-8">
                                                    <input type="text" id="txtMEmployeeID" placeholder="Mã nhân viên" class="form-control" readonly="readonly"/>
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Tên nhân viên </label>

                                                <div class="col-sm-8">
                                                    <input type="text" id="txtMEmployeeName" placeholder="Tên nhân viên" class="form-control input-required" />
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Số điện thoại</label>

                                                <div class="col-sm-8">
                                                    <input type="text" id="txtMSDT" placeholder="Số điện thoại" class="form-control input-required" />
                                                </div>
                                            </div>
                                            <br />
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Email</label>

                                                <div class="col-sm-8">
                                                    <input type="text" id="txtMEmail" placeholder="Email" class="form-control input-required" />
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Địa chỉ</label>

                                                <div class="col-sm-8">
                                                    <input type="text" id="txtMAddress" placeholder="Địa chỉ" class="form-control input-required" />
                                                </div>
                                            </div>
                                            <br />
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Ngày sinh</label>

                                                <div class="col-sm-8">
                                                    <%--<input type="date" id="txtMBirthday" placeholder="Ngày sinh" class="form-control" />--%>
                                                    <div class="input-group">
														<input class="form-control date-picker input-required" id="txtMBirthday" type="text" data-date-format="dd-mm-yyyy" />
														<span class="input-group-addon">
															<i class="fa fa-calendar bigger-110"></i>
														</span>
													</div>
                                                </div>
                                            </div>
                                            <div class="form-group col-sm-6">
                                                <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Chức vụ</label>

                                                <div class="col-sm-8">
                                                    <%--<input type="text" id="txtMRole" placeholder="Chức vụ" class="form-control" />--%>
                                                    <select class="chosen-select form-control input-required" id="txtMRole" data-placeholder="Chức vụ">
														<option value="">  </option>
														<option value="QL">Quản Lý</option>
														<option value="NV">Nhân Viên</option>
													</select>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="hr hr-18 dotted hr-double"></div>
                                        </div>
                                    </div>
                                </div>
                            <%--</div>--%>
                        <%--</div>--%>
                        <!-- /.col -->
                    </div>

                    <div class="modal-footer no-margin-top">
                        <button class="btn btn-sm btn-danger pull-left" data-dismiss="modal">
                            <i class="ace-icon fa fa-times"></i>
                            Close
                        </button>
                        <button type='button' id="insert" class="btn btn-sm btn-success pull-right" ng-click="InsertEmp()">
                            <i class="ace-icon fa fa-save"></i>
                            Save
                        </button>
                        <button type='button' id="update" class="btn btn-sm btn-success pull-right" ng-click="UpdateEmp()">
                            <i class="ace-icon fa fa-save"></i>
                            Save
                        </button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    <%--</body>--%>

    <!-- inline scripts related to this page -->
    <script src="App/CustomerManagement.js"></script>
    
</asp:Content>