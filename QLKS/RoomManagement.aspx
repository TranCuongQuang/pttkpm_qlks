<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoomManagement.aspx.cs" Inherits="QLKS.RoomManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<body class="no-skin">--%>
        <div ng-app="QLKS" ng-controller="RoomManagementCtrl">

            <div class="panel panel-primary">
            <div class="panel-heading">
                Tìm kiếm
            </div>
            <div class="panel-body form-horizontal">
                <div class="form-group col-sm-4">
                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Mã phòng</label>
                    <div class="col-md-8">
                        <input type="text" id="RoomID" placeholder="Mã phòng" class="form-control" />
                    </div>
                </div>
                <div class="form-group col-sm-4">
                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Tên phòng</label>
                    <div class="col-md-8">
                        <input type="text" id="RoomName" placeholder="Tên phòng" class="form-control" />
                    </div>
                </div>
                <div class="form-group col-sm-4">
                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Trạng thái</label>
                    <div class="col-sm-8">
                        <select class="chosen-select form-control" id="Status" data-placeholder="Trạng thái">
                            <option value=""> </option>
                            <option value="0">Trống</option>
                            <option value="1">Đã đặt phòng</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-12 form-button">
                    <button class="btn btn-info" type="button" ng-click="searchRoom()">
                        <i class="ace-icon fa fa-search"></i>Tìm kiếm
                    </button>
                    <button class="btn btn-primary modal123" type="button" data-id="Create" ng-click="showModal()">
                        <i class="ace-icon fa fa-plus"></i>Thêm mới
                    </button>
                </div>
            </div>
        </div>

        <div class="panel panel-primary">
            <div class="panel-heading">
                Danh sách phòng
            </div>
            <div class="panel-body table-qlks">
                <table id="dynamic-table" datatable="ng" dt-options="dtOptions" class="table table-bordered">
                    <thead>
                        <tr >
                            <th>Mã phòng</th>
                            <th>Tên phòng</th>
                            <th>Đơn giá</th>
                            <th>Trạng thái</th>
                            <th></th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr ng-repeat="x in dataTable">
                            <td>{{x.MaPhong}}</td>
                            <th>{{x.TenPhong}}</th>
                            <th>{{x.DonGia}}</th>
                            <th>{{x.StrTrangThai}}</th>

                            <td>
                                <div class="hidden-sm hidden-xs action-buttons">
                                    <a class="modal123 blue" href="#modal-table" role="button" data-toggle="modal" data-id="Info" data-value="{{x.MaPhong}}" style="text-decoration: none; color: antiquewhite;">
                                        <i class="ace-icon fa fa-search-plus bigger-130"></i>
                                    </a>

                                    <a class="modal123 green" href="#modal-table" role="button" data-toggle="modal" data-id="Update" data-value="{{x.MaPhong}}" style="text-decoration: none; color: antiquewhite;">
                                        <i class="ace-icon fa fa-pencil bigger-130"></i>
                                    </a>

                                    <a class="red" href="#" id="DeleteRoom" data-value="{{x.MaPhong}}">
                                        <i class="ace-icon fa fa-trash-o bigger-130"></i>
                                    </a>
                                </div>

                            </td>
                        </tr>
                    </tbody>
                </table>
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
                        <div class="widget-main">
                            <div class="" role="form" style="height:auto;">
                                <div class="form-group col-md-6" id="manv">
                                    <label class="control-label" for="form-field-1-1">Mã phòng</label>
                                    <input type="text" id="txtMRoomID" placeholder="Mã phòng" class="form-control" readonly="readonly"/>
                                 </div>
                                <div class="form-group col-md-6">
                                    <label class="control-label" for="form-field-1-1">Tên phòng</label>
                                    <input type="text" id="txtMRoomName" placeholder="Tên phòng" class="form-control input-required" />
                                 </div>
                                <div class="form-group col-md-6">
                                    <label class="control-label" for="form-field-1">Đơn giá</label>
                                    <input type="text" id="txtMAmount" placeholder="Đơn giá" class="form-control input-required" />
                                    </div>
                                <br />
                                <div class="form-group col-md-6">
                                    <label class="control-label" for="form-field-1">Trạng thái</label>
                                    <select class="chosen-select form-control input-required" id="txtMStatus" data-placeholder="Trạng thái">
                                            <option value="0">Trống</option>
                                            <option value="1">Đã đặt phòng</option>
                                        </select>
                                 </div>
                                <br />
                                <div class="form-group col-md-6">
                                    <label class="control-label">Trang thiết bị</label>
                                    <select class="chosen-select form-control" ng-model="ddlEquipment" ui-select2 style="width: 100%">
                                        <option value="">Chọn thiết bị</option>
                                        <option ng-repeat="item in EquipmentList" value="{{item.MaThietBi}}">{{item.TenThietBi}}</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-2 no-margin no-padding-left">
                                    <label class="control-label">Số Lượng</label>
                                    <input type="number" class="form-control" ng-model="txtQuantityEquipment" />
                                </div>
                                <div class="form-group col-md-1">
                                    <button type='button' class="btn btn-sm btn-primary pull-right" style="margin-top: 25px" ng-click="ChooseEquipment()">
                                        <i class="ace-icon fa fa-plus"></i>Chọn
                                    </button>
                                </div>
                                <div class="form-group col-md-12 table-qlks">
                                    <table class="table table-bordered  no-margin-bottom">
                                        <thead>
                                            <tr>
                                                <th>Mã Thiết Bị</th>
                                                <th>Tên Thiết Bị</th>
                                                <th>Số lượng</th>
                                                <th>Đơn giá</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-repeat="item in ChooseEquipmentList">
                                                <td>{{item.MaThietBi}}</td>
                                                <td>{{item.TenThietBi}}</td>
                                                <td>{{item.SoLuong}}</td>
                                                <td>{{item.DonGia}}</td>
                                                <td class="text-center">
                                                    <button type="button" class="btn btn-danger" title="Xóa khỏi danh sách" ng-click="RemoveChooseEquipment(item)" <%--ng-show="!item.MaTTBP || item.MaTTBP == 0"--%>>
                                                        <i class="ace-icon fa fa-trash-o"></i>
                                                    </button>
                                                    <button type="button" class="btn btn-primary" title="Sửa" ng-click="EditChooseEquipment(item)" ng-show="item.MaTTBP && item.MaTTBP != 0">
                                                        <i class="ace-icon fa fa-pencil"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="text-center">Tổng tiền</td>
                                                <td colspan="2" class="text-center">{{lblTotalMoneyEquipment}}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <br />
                                <div class="hr hr-18 dotted hr-double"></div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer no-margin-top">
                    <button class="btn btn-sm btn-danger pull-left" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        Close
                    </button>
                    <button type='button' id="insert" class="btn btn-sm btn-success pull-right" ng-click="InsertRoom()">
                        <i class="ace-icon fa fa-save"></i>
                        Save
                    </button>
                    <button type='button' id="update" class="btn btn-sm btn-success pull-right" ng-click="UpdateRoom()">
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
    <script src="App/RoomManagement.js"></script>
    
</asp:Content>
