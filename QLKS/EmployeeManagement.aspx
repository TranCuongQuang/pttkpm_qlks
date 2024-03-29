﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeManagement.aspx.cs" Inherits="QLKS.EmployeeManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div ng-app="QLKS" ng-controller="EmployeeManagementCtrl">
        <div class="panel panel-primary">
            <div class="panel-heading">
                Tìm kiếm
            </div>
            <div class="panel-body form-horizontal">
                <div class="form-group col-sm-6">
                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Mã nhân viên</label>
                    <div class="col-md-8">
                        <input type="text" id="EmployeeID" placeholder="Mã nhân viên" class="form-control" />
                    </div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Tên nhân viên</label>
                    <div class="col-md-8">
                        <input type="text" id="EmployeeName" placeholder="Tên nhân viên" class="form-control" />
                    </div>
                </div>
                <div class="col-md-12 form-button">
                    <button class="btn btn-info" type="button" ng-click="searchEmp()">
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
                Danh sách nhân viên
            </div>
            <div class="panel-body table-qlks">
                <table id="dynamic-table" datatable="ng" dt-options="dtOptions" class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Mã NV</th>
                            <th>Tên NV</th>
                            <th>SĐT</th>
                            <th>Ngày sinh</th>
                            <th>Email</th>
                            <th>Địa chỉ</th>
                            <th>Chức vụ</th>
                            <th>Tên Đăng Nhập</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr ng-repeat="x in dataTable">
                            <td>{{x.MaNV}}</td>
                            <th>{{x.TenNV}}</th>
                            <th>{{x.SDT}}</th>
                            <th>{{x.NgaySinh}}</th>
                            <th>{{x.Email}}</th>
                            <th>{{x.DiaChi}}</th>
                            <th>{{x.ChucVu}}</th>
                            <th>{{x.TenDangNhap}}</th>
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
                            <div class="form-horizontal" role="form" style="height: auto;">
                                <div class="form-group col-sm-6" id="manv">
                                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1-1">Mã nhân viên </label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtMEmployeeID" placeholder="Mã nhân viên" class="form-control" readonly="readonly" />
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
                                        <select class="chosen-select form-control input-required" id="txtMRole" data-placeholder="Chức vụ">
                                            <option value=""></option>
                                            <option value="QL">Quản Lý</option>
                                            <option value="NV">Nhân Viên</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Tên đăng nhập</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtMUsername" placeholder="Tên đăng nhập" class="form-control input-required" />
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label class="col-sm-4 control-label no-padding-right" for="form-field-1">Mật khẩu</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtMPassword" placeholder="Mật khẩu" class="form-control input-required" />
                                    </div>
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

    <!-- inline scripts related to this page -->
    <script src="App/EmployeeManagement.js"></script>
    <%--<script type="text/javascript">
        jQuery(function ($) {
            //initiate dataTables plugin
            var myTable =
                $('#dynamic-table')
                    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                    .DataTable({
                        bAutoWidth: false,
                        "aoColumns": [
                            { "bSortable": false },
                            null, null, null, null, null,
                            { "bSortable": false }
                        ],
                        "aaSorting": [],

                        //"bProcessing": true,
                        //"bServerSide": true,
                        //"sAjaxSource": "http://127.0.0.1/table.php"	,

                        //,
                        //"sScrollY": "200px",
                        //"bPaginate": false,

                        //"sScrollX": "100%",
                        //"sScrollXInner": "120%",
                        //"bScrollCollapse": true,
                        //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                        //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                        //"iDisplayLength": 50

                        select: {
                            style: 'multi'
                        }
                    });

            $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';

            new $.fn.dataTable.Buttons(myTable, {
                buttons: [
                    {
                        "extend": "colvis",
                        "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                        "className": "btn btn-white btn-primary btn-bold",
                        columns: ':not(:first):not(:last)'
                    },
                    {
                        "extend": "copy",
                        "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                        "className": "btn btn-white btn-primary btn-bold"
                    },
                    {
                        "extend": "csv",
                        "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                        "className": "btn btn-white btn-primary btn-bold"
                    },
                    {
                        "extend": "excel",
                        "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                        "className": "btn btn-white btn-primary btn-bold"
                    },
                    {
                        "extend": "pdf",
                        "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                        "className": "btn btn-white btn-primary btn-bold"
                    },
                    {
                        "extend": "print",
                        "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                        "className": "btn btn-white btn-primary btn-bold",
                        autoPrint: false,
                        message: 'This print was produced using the Print button for DataTables'
                    }
                ]
            });
            myTable.buttons().container().appendTo($('.tableTools-container'));

            //style the message box
            var defaultCopyAction = myTable.button(1).action();
            myTable.button(1).action(function (e, dt, button, config) {
                defaultCopyAction(e, dt, button, config);
                $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
            });

            var defaultColvisAction = myTable.button(0).action();
            myTable.button(0).action(function (e, dt, button, config) {

                defaultColvisAction(e, dt, button, config);

                if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                    $('.dt-button-collection')
                        .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                        .find('a').attr('href', '#').wrap("<li />")
                }
                $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
            });

            ////

            setTimeout(function () {
                $($('.tableTools-container')).find('a.dt-button').each(function () {
                    var div = $(this).find(' > div').first();
                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                });
            }, 500);

            myTable.on('select', function (e, dt, type, index) {
                if (type === 'row') {
                    $(myTable.row(index).node()).find('input:checkbox').prop('checked', true);
                }
            });
            myTable.on('deselect', function (e, dt, type, index) {
                if (type === 'row') {
                    $(myTable.row(index).node()).find('input:checkbox').prop('checked', false);
                }
            });

            /////////////////////////////////
            //table checkboxes
            $('th input[type=checkbox], td input[type=checkbox]').prop('checked', false);

            //select/deselect all rows according to table header checkbox
            $('#dynamic-table > thead > tr > th input[type=checkbox], #dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
                var th_checked = this.checked;//checkbox inside "TH" table header

                $('#dynamic-table').find('tbody > tr').each(function () {
                    var row = this;
                    if (th_checked) myTable.row(row).select();
                    else myTable.row(row).deselect();
                });
            });

            //select/deselect a row when the checkbox is checked/unchecked
            $('#dynamic-table').on('click', 'td input[type=checkbox]', function () {
                var row = $(this).closest('tr').get(0);
                if (this.checked) myTable.row(row).deselect();
                else myTable.row(row).select();
            });

            $(document).on('click', '#dynamic-table .dropdown-toggle', function (e) {
                e.stopImmediatePropagation();
                e.stopPropagation();
                e.preventDefault();
            });

            //And for the first simple table, which doesn't have TableTools or dataTables
            //select/deselect all rows according to table header checkbox
            var active_class = 'active';
            $('#simple-table > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
                var th_checked = this.checked;//checkbox inside "TH" table header

                $(this).closest('table').find('tbody > tr').each(function () {
                    var row = this;
                    if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
                    else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
                });
            });

            //select/deselect a row when the checkbox is checked/unchecked
            $('#simple-table').on('click', 'td input[type=checkbox]', function () {
                var $row = $(this).closest('tr');
                if ($row.is('.detail-row ')) return;
                if (this.checked) $row.addClass(active_class);
                else $row.removeClass(active_class);
            });

            /********************************/
            //add tooltip for small view action buttons in dropdown menu
            $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

            //tooltip placement on right or left
            function tooltip_placement(context, source) {
                var $source = $(source);
                var $parent = $source.closest('table')
                var off1 = $parent.offset();
                var w1 = $parent.width();

                var off2 = $source.offset();
                //var w2 = $source.width();

                if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                return 'left';
            }

            /***************/
            $('.show-details-btn').on('click', function (e) {
                e.preventDefault();
                $(this).closest('tr').next().toggleClass('open');
                $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
            });
            /***************/

            /**
            //add horizontal scrollbars to a simple table
            $('#simple-table').css({'width':'2000px', 'max-width': 'none'}).wrap('<div style="width: 1000px;" />').parent().ace_scroll(
              {
                horizontal: true,
                styleClass: 'scroll-top scroll-dark scroll-visible',//show the scrollbars on top(default is bottom)
                size: 2000,
                mouseWheelLock: true
              }
            ).css('padding-top', '12px');
            */

        })
    </script>--%>
</asp:Content>