<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingRoom.aspx.cs" Inherits="QLKS.BookingRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div ng-app="QLKS" ng-controller="BookingRoomCtrl">
        <div class="panel panel-primary">
            <div class="panel-heading">
                Đặt phòng
            </div>
            <div class="panel-body">
                <div class="infobox infobox-small infobox-dark booking " ng-class="{'infobox-green': item.TrangThai == 0, 'infobox-grey': item.TrangThai == 1}" ng-click="showModal(item)" ng-repeat="item in RoomList">
                    <div class="infobox-chart">
                        <span class="ace-icon fa fa-home" style="font-size: 30px; vertical-align: middle;"></span>
                    </div>
                    <div class="infobox-data">
                        <div class="infobox-content">{{item.TenPhong}}</div>
                        <div class="infobox-content">{{item.StrTrangThai}}</div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalBooking" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header no-padding">
                        <div class="table-header" id="titleheader">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                <span class="white">&times;</span>
                            </button>
                            Thêm mới
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="form-group col-md-6">
                            <label class="control-label">Mã phòng</label>
                            <input type="text" placeholder="Mã phòng" class="form-control" readonly="readonly" />
                        </div>
                        <div class="form-group col-md-5">
                            <label class="control-label">Khách hàng</label>
                            <select class="chosen-select form-control" data-placeholder="Chức vụ">
                                <option value=""></option>
                                <option value="QL">Quản Lý</option>
                                <option value="NV">Nhân Viên</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1">
                            <button type='button' class="btn btn-sm btn-primary pull-right" style="margin-top: 25px">
                                <i class="ace-icon fa fa-plus"></i>Thêm
                            </button>
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label">Từ ngày</label>
                            <div class="input-group">
                                <input class="form-control date-picker" type="text" data-date-format="dd-mm-yyyy" />
                                <span class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label">Đến ngày</label>
                            <div class="input-group">
                                <input class="form-control date-picker" type="text" data-date-format="dd-mm-yyyy" />
                                <span class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label">Đơn giá</label>
                            <input type="text" placeholder="Đơn giá" class="form-control" readonly="readonly" />
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label">Thành tiền</label>
                            <input type="text" placeholder="Thành tiền" class="form-control" readonly="readonly" />
                        </div>
                        <div class="form-group col-md-5">
                            <label class="control-label">Sản phẩm</label>
                            <select class="chosen-select form-control" data-placeholder="Chức vụ">
                                <option value=""></option>
                                <option value="QL">Quản Lý</option>
                                <option value="NV">Nhân Viên</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1">
                            <button type='button' class="btn btn-sm btn-primary pull-right" style="margin-top: 25px">
                                <i class="ace-icon fa fa-plus"></i>Chọn
                            </button>
                        </div>
                        <div class="form-group col-md-5">
                            <label class="control-label">Dịch vụ</label>
                            <select class="chosen-select form-control" data-placeholder="Chức vụ">
                                <option value=""></option>
                                <option value="QL">Quản Lý</option>
                                <option value="NV">Nhân Viên</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1">
                            <button type='button' class="btn btn-sm btn-primary pull-right" style="margin-top: 25px">
                                <i class="ace-icon fa fa-plus"></i>Chọn
                            </button>
                        </div>
                        <div class="form-group col-md-6 table-qlks">
                            <table class="table table-bordered  no-margin-bottom">
                                <thead>
                                    <tr>
                                        <th>Mã SP</th>
                                        <th>Tên SP</th>
                                        <th>Đơn giá</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>1</td>
                                        <td>1</td>
                                        <td class="text-center">
                                            <button type="button" class="btn btn-danger" title="Xóa khỏi danh sách">
                                                <i class="ace-icon fa fa-trash-o"></i>
                                            </button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="text-center">Tổng tiền</td>
                                        <td colspan="2" class="text-center">10</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-group col-md-6 table-qlks">
                            <table class="table table-bordered no-margin-bottom">
                                <thead>
                                    <tr>
                                        <th>Mã DV</th>
                                        <th>Tên DV</th>
                                        <th>Đơn giá</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>1</td>
                                        <td>1</td>
                                        <td class="text-center">
                                            <button type="button" class="btn btn-danger" title="Xóa khỏi danh sách">
                                                <i class="ace-icon fa fa-trash-o"></i>
                                            </button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="text-center">Tổng tiền</td>
                                        <td colspan="2" class="text-center">10</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type='button' class="btn btn-sm btn-primary pull-right" ng-click="UpdateEmp()">
                            <i class="ace-icon fa fa-save"></i>Đặt phòng
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="App/BookingRoom.js"></script>
</asp:Content>