<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QLKS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Dashboard - Ace Admin</title>
    <meta name="description" content="overview &amp; stats" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link rel="stylesheet" href="Scripts/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Scripts/assets/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="Scripts/assets/css/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="Scripts/assets/css/ace.min.css" class="ace-main-stylesheet" />
    <link rel="stylesheet" href="Scripts/assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="Scripts/assets/css/ace-rtl.min.css" />

    <script src="Scripts/assets/js/jquery-2.1.4.min.js"></script>

    <%--Angular--%>
    <script src="Scripts/assets/js/angular.min.js"></script>
    <script src="Scripts/assets/js/angular-select2.js"></script>
    <script src="Scripts/assets/js/angular-filter.min.js"></script>
    <script src="Scripts/assets/js/angular-datatables.min.js"></script>
</head>
<body class="login-layout blur-login">
    <div class="main-container">
        <div class="main-content">
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="login-container">
                        <div class="center">
                            <h1>
                                <i class="ace-icon fa fa-leaf green"></i>
                                <span class="white" id="id-text2">Đăng nhập</span>
                            </h1>
                        </div>
                        <div class="space-6"></div>
                        <div class="position-relative">
                            <div id="login-box" class="login-box visible widget-box no-border">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <h4 class="header blue lighter bigger">
                                            <i class="ace-icon fa fa-coffee green"></i>
                                            Vui lòng nhập thông tin
                                        </h4>
                                        <div class="space-6"></div>
                                        <form>
                                            <fieldset>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="text" class="form-control" placeholder="Username" />
                                                        <i class="ace-icon fa fa-user"></i>
                                                    </span>
                                                </label>
                                                <label class="block clearfix">
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="password" class="form-control" placeholder="Password" />
                                                        <i class="ace-icon fa fa-lock"></i>
                                                    </span>
                                                </label>
                                                <div class="space"></div>
                                                <div class="clearfix">
                                                    <button type="button" class="width-35 pull-right btn btn-sm btn-primary">
                                                        <i class="ace-icon fa fa-key"></i>
                                                        <span class="bigger-110">Login</span>
                                                    </button>
                                                </div>
                                                <div class="space-4"></div>
                                            </fieldset>
                                        </form>
                                    </div>
                                    <!-- /.widget-main -->
                                </div>
                                <!-- /.widget-body -->
                            </div>
                            <!-- /.login-box -->
                        </div>
                        <!-- /.position-relative -->
                    </div>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.main-content -->
    </div>
    <!-- /.main-container -->
</body>
</html>