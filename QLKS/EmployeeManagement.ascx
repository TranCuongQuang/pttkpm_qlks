<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeManagement.ascx.cs" Inherits="QLKS.EmployeeManagement" %>
<div class="col-sm-12 form_contact__left form_contact__research accounting_form_final">
    <div class="col-sm-12" style="padding: 10px; padding-top: 0">
        <div class="form_show">
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label class="col-sm-12 control-label">dfggsd</label>
                        <div class="col-sm-12">
                            <input id="txtTransactionDateFrom" class="form-control entry_required date-picker" placeholder="dd/mm/yyyy" autocomplete="off"/>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label class="col-sm-12 control-label">cxc</label>
                        <div class="col-sm-12">
                            <input id="txtTransactionDateTo" class="form-control entry_required date-picker" placeholder="dd/mm/yyyy" autocomplete="off"/>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label class="col-sm-12 control-label"> TRạng thái</label>
                        <div class="col-sm-12">
                            <select id="drpStatus" class="form-control EMSelect2" style="width: 100% !important;">
                                <option value="">Tất cả</option>
                                <option value="1">a</option>
                                <option value="0">b</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label class="col-sm-12 control-label">Review</label>
                        <div class="col-sm-12">
                            <select id="drpReviewed" class="form-control EMSelect2" style="width: 100% !important;">
                                <option value="">Tất cả</option>
                                <option value="1">a</option>
                                <option value="0">b</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group  ">
                        <label class="col-sm-12 control-label">Mã số</label>
                        <div class="col-sm-12">
                            <input type="text" id="txtTransactionNo" runat="server"/>
                        </div>
                    </div>
                </div>
                
                <div class="col-sm-3">
                    <div class="form-group  ">
                        <label class="col-sm-12 control-label">Diễn giải</label>
                        <div class="col-sm-12">
                            <input type="text" id="txtDescription" runat="server"/>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group  ">
                        <label class="col-sm-12 control-label">Cash</label>
                        <div class="col-sm-12 height_combobox">
                            <input id="cboCashBookID" class="easyui-combobox" name="cboCashBookID">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="hide_form">
        </div>
    </div>
    <div class="col-sm-12 n_button_save" style="margin-top: 0">
        <div class="col-md-12">
            <input id="btnSearch" type="button" class="fa btn_disable btn_search btn_bg_text" onclick="" value="Search" runat="server"/>
            <input id="btnCreateNew" type="button" class="btn_accounting_new btn_add_blue" value="Thêm" runat="server" onclick=""/>
            <input id="btnImport" type="button" class="btn_accounting_new btn_add_blue" value="Import" runat="server" onclick=""/>
            <input id="btnExportExcel" type="button" class="fa btn_disable btn_export" onclick="ExportExcelVoucher()" value="Xuất>" runat="server" />  
            <input id="btnDelete" type="button" class="btn_accounting_new nd_btn_remove" value="Xóa" onclick="" runat="server" />
            
        </div>
    </div>
</div>
<!-- table content -->
<div class="widget-box widget-color-blue2 n_dsnhacungcap">
    <div class="widget-body" style="padding: 10px;">
        <div class="n_table_content" style="padding: 0px;">
            <table id="tableVoucher" class="table table-striped table-bordered table-hover">
                <thead class="">
                <tr>
                    <th class="text-center"></th>
                    <th class="text-center" scope="col" style="width: 60px;">avvv</th>
                    <th class="text-center">bvv</th>
                    <th class="text-center">cvv</th>
                </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
</div>
<script src="MdlACCE/App/ACCE_frmVoucherPayslipEntryIndex.js"></script>