<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingRoom.aspx.cs" Inherits="QLKS.BookingRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div ng-app="QLKS" ng-controller="BookingRoomCtrl">
        <div class="panel panel-primary">
            <div class="panel-heading">
                Đặt phòng
            </div>
            <div class="panel-body">
                <div class="col-md-4 form-group">
                    <input type="text" id="EmployeeID" placeholder="Mã nhân viên" class="form-control" />
                </div>
                <div class="col-md-4 form-group">
                    <input type="text" id="EmployeeName" placeholder="Tên nhân viên" class="form-control" />
                </div>
                <div class="col-md-12 form-button">
                    <button class="btn btn-info" type="button" ng-click="searchEmp()">
                        <i class="ace-icon fa fa-search"></i>Tìm kiếm
                    </button>
                    <button class="btn" type="button" ng-click="showModal()">
                        <i class="ace-icon fa fa-plus"></i>Thêm mới
                    </button>
                </div>
            </div>
        </div>
    </div>
    <script src="App/BookingRoom.js"></script>
</asp:Content>