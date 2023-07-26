<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAudit.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mutosi_Web.pages.BillResult.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openNewImage(file, WorkId, KPIId) {
            popWindow_ImageAudit("../../Popups/ImagesAuditDetail.aspx?src1=" + file + "&WorkId=" + WorkId + "&KPIId=" + KPIId, 820, 635);
        }
        function popWindow_ImageAudit(link, width, height) {
            var w = width, h = height;
            var left = (screen.width / 2) - (w / 2);
            var top = 10;
            window.open(link, 'popup', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            <div class="card card-info">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Từ ngày :</label>
                                <div class="input-group date" id="reservationdate" data-target-input="nearest">
                                    <%--<input type="text" class="form-control datetimepicker-input" data-target="#reservationdate" />--%>
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control datetimepicker-input" data-target="#reservationdate"></asp:TextBox>
                                    <div class="input-group-append" data-target="#reservationdate" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Đến ngày :</label>
                                <div class="input-group date" id="reservationdate1" data-target-input="nearest">
                                    <%--<input type="text" class="form-control datetimepicker-input" data-target="#reservationdate" />--%>
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control datetimepicker-input" data-target="#reservationdate1"></asp:TextBox>
                                    <div class="input-group-append" data-target="#reservationdate1" data-toggle="datetimepicker">
                                        <div class="input-group-text"><i class="fa fa-calendar"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Khu vực:</label>
                                <asp:DropDownList runat="server" ID="ddlArea" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Tỉnh/Thành phố:</label>
                                <asp:DropDownList runat="server" ID="ddlProvince" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quận/Huyện:</label>
                                <asp:DropDownList runat="server" ID="ddlDistrict" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Phường/Xã:</label>
                                <asp:DropDownList runat="server" ID="ddlTown" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%--<div class="col-md-2">
                            <div class="form-group">
                                <label>MDO:</label>
                                <asp:DropDownList runat="server" ID="ddlMDO" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Supervisor:</label>
                                <asp:DropDownList runat="server" ID="ddlSup" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;" AutoPostBack="true" OnSelectedIndexChanged="ddlSup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Auditor:</label>
                                <asp:DropDownList runat="server" ID="ddlAuditor" class="form-control select2 select2-hidden-accessible" Style="width: 100%; height: 40px;">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ID Store:</label>
                                <asp:TextBox runat="server" ID="txtShopCode" CssClass="form-control" placeholder="ID Store cách nhau bởi khoảng trắng" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ID Product:</label>
                                <asp:TextBox runat="server" ID="txtProduct" CssClass="form-control" placeholder="ID Product cách nhau bởi khoảng trắng" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile:</label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Mobile cách nhau bởi khoảng trắng" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Bill Seri:</label>
                                <asp:TextBox runat="server" ID="txtBillSeri" CssClass="form-control" placeholder="Bill Seri cách nhau bởi khoảng trắng" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button runat="server" class="btn btn-primary" Text="Tìm kiếm" ID="btnFilter" OnClick="btnFilter_Click" />
                            <asp:Button runat="server" class="btn btn-danger" Text="Xuất báo cáo" ID="btnExport" OnClick="btnExport_Click" UseSubmitBehavior="false" OnClientClick="this.value='Đang xuất file...'; this.disabled='true'" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <section class="content" id="workresult" runat="server" visible="false">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="card">
                       <%-- <div class="card-header">
                            <h3 class="card-title">Kết quả làm việc</h3>

                        </div>--%>
                        <div class="card-body" style="padding: 0 !important;">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap4">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>#</th>
                                                    <th>BillId</th>
                                                    <th>Cửa hàng</th>
                                                    <th>Địa chỉ</th>
                                                    <th>Nhân viên</th>
                                                    <th>Mã HĐ</th>
                                                    <th>Khách hàng</th>
                                                    <th>Đ/c KH</th>
                                                    <th>Ngày mua hàng</th>
                                                    <th>Thành tiền</th>
                                                    <th>Ngày tạo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rpWorkResult">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="img" ImageUrl="~/images/plus.png" runat="server" OnClick="img_Click" CommandName="Show" Width="13" />
                                                            </td>
                                                            <td><%# Eval("RN") %></td>
                                                            <td>
                                                                <asp:Label Text='<%# Eval("BillId") %>' runat="server" ID="lblBillId" />
                                                            </td>
                                                            <td><%# Eval("ShopId") %> - <%# Eval("ShopName") %></td>
                                                            <td><%# Eval("AddressLine") %></td>
                                                            <td><%# Eval("EmployeeId") %> - <%# Eval("EmployeeName") %></td>
                                                            <td><%# Eval("BillSeri") %></td>
                                                            <td><%# Eval("CusName") %></td>
                                                            <td><%# Eval("CusAddress") %></td>
                                                            <td><%# Eval("BillDateFormat") %></td>
                                                            <td><%# !string.IsNullOrEmpty(Convert.ToString(Eval("Amount"))) ? Convert.ToDouble(Eval("Amount")).ToString("#0,##") : "" %></td>
                                                            <td><%# String.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("CreateDate")) %></td>
                                                        </tr>
                                                        <asp:PlaceHolder ID="pndetail" Visible="false" runat="server">
                                                            <tr style="height: 40%">
                                                                <td colspan="100%" style="padding: 0px !important;">
                                                                    <asp:Panel runat="server" ID="pnGrid" Visible="false">
                                                                        <%-- Tab Attendant --%>
                                                                        <asp:Panel runat="server" ID="plLucky">
                                                                            <div class="col-sm-11" style="height: 480px; overflow-y: scroll;">
                                                                                <table class="table table-bordered">
                                                                                    <tr style="text-align: left; vertical-align: top">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th class="text-center">Thông tin vòng quay</th>
                                                                                                <th class="text-center">Hình ảnh hoá đơn</th>
                                                                                                <th class="text-center">Hình ảnh khách hàng nhận quà</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <br />
                                                                                                    <table style="width:100%">
                                                                                                        <tr>
                                                                                                            <td><strong>Pay Method</strong></td>
                                                                                                            <td><%# Eval("PayMethod") %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>Product</strong></td>
                                                                                                            <td><%# Eval("ProductName") %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>LuckyDraw</strong> </td>
                                                                                                            <td><%# Eval("LuckyDraw") %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>LuckyDrawCode</strong> </td>
                                                                                                            <td><%# Eval("LuckyDrawCode") %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>Gift</strong> </td>
                                                                                                            <td><%# Eval("LuckyDrawGiftId") %> -  <%# Eval("GiftName") %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>LuckyDraw Time</strong> </td>
                                                                                                            <td><%# String.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("LuckyDrawTime")) %></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td><strong>Comment</strong> </td>
                                                                                                            <td><%# Eval("MiniGameComment") %></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td style="text-align: center">
                                                                                                    <br />
                                                                                                    <a onclick="return openNewImage('<%# Eval("ImageBill") %>','<%# Eval("ShopId") %>', '0')">
                                                                                                        <img src='<%# Eval("ImageBill") %>' style="width: 235px; height: 300px;" /></a>
                                                                                                </td>
                                                                                                <td style="text-align: center">
                                                                                                    <br />
                                                                                                    <a onclick="return openNewImage('<%# Eval("ImageCusReGift") %>','<%# Eval("ShopId") %>', '0')">
                                                                                                        <img src='<%# Eval("ImageCusReGift") %>' style="width: 235px; height: 300px;" /></a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </asp:Panel>

                                                                </td>
                                                            </tr>
                                                        </asp:PlaceHolder>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <asp:Panel runat="server" ID="plPaging" Visible="false">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-5">
                                                    <div class="dataTables_info" id="example2_info" role="status" aria-live="polite">
                                                        Hiển thị
                                                        <asp:Label ID="lblFrom" runat="server" />
                                                        đến
                                                        <asp:Label ID="lblTo" runat="server" />
                                                        của 
                                                        <asp:Label ID="lblTotalRows" runat="server" />
                                                        dữ liệu
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-7" style="display: none;">
                                                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                                        <ul class="pagination">
                                                            <li class="paginate_button page-item previous" id="example2_previous">
                                                                <asp:LinkButton Text="Previous" runat="server" CssClass="page-link" ID="lnkPrevious" CommandName="Previous" OnClick="lnkPrevious_Click" />
                                                            </li>
                                                            <asp:Repeater runat="server" ID="rptPaging">
                                                                <ItemTemplate>
                                                                    <li class='<%# Eval("Active") %>'>
                                                                        <asp:LinkButton ID="lnkPage" Text='<%# Eval("PageNumber") %>' CommandName="Page" runat="server" CssClass="page-link" OnClick="lnkPage_Click" />
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <li class="paginate_button page-item next" id="example2_next">
                                                                <asp:LinkButton Text="Next" runat="server" CssClass="page-link" ID="lnkNext" CommandName="Next" OnClick="lnkNext_Click" />
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-7">
                                                    <div class="dataTables_paginate paging_simple_numbers">
                                                        <ul class="pagination">
                                                            <li class="paginate_button page-item previous">
                                                                <asp:LinkButton
                                                                    CssClass="page-link" runat="server" ID="lbn_first" OnClick="lbn_first_Click"><i class="fa fa-angle-double-left"></i>
                                                                </asp:LinkButton>
                                                            </li>
                                                            <li class="paginate_button page-item previous" id="prv_button">
                                                                <asp:LinkButton
                                                                    CssClass="page-link" runat="server" ID="lbn_prev" OnClick="lbn_prev_Click"><i class="fa fa-angle-left"></i>
                                                                </asp:LinkButton>
                                                            </li>
                                                            <asp:Repeater ID="rpt_pagination" runat="server" OnItemCommand="rpt_pagination_ItemCommand">
                                                                <ItemTemplate>
                                                                    <li class='<%#Eval("Class")%>'>
                                                                        <asp:LinkButton
                                                                            CssClass="page-link" CommandName="Page" CommandArgument='<%#Eval("Page")%>' runat="server" Font-Bold="True"><%#Eval("Page")%>  
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <li class="paginate_button page-item next">
                                                                <asp:LinkButton
                                                                    CssClass="page-link" runat="server" ID="lbn_next" OnClick="lbn_next_Click"><i class="fa fa-angle-right"></i>
                                                                </asp:LinkButton>
                                                            </li>
                                                            <li class="paginate_button page-item previous">
                                                                <asp:LinkButton
                                                                    CssClass="page-link" runat="server" ID="lbn_end" OnClick="lbn_end_Click"><i class="fa fa-angle-double-right"></i>
                                                                </asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
