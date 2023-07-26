using BLL.Attendants;
using BLL.WorkResults;
using Mutosi_Web.App_Code;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mutosi_Web.pages.BillResult
{
    public partial class Default : PagePermisstion
    {
        private string _title = "Kết quả vòng quay may mắn";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((SiteAuditMaster)Master).SetFormTitle(_title);
                txtToDate.Text = txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                bindOption();
            }
            ScriptManager script = ScriptManager.GetCurrent(Page);
            script.Dispose();
            script.RegisterPostBackControl(btnExport);
        }
        void bindOption()
        {
            // ddlAuditor.Items.Insert(0, new ListItem("-Tất cả-", "-1"));

            //Pf.bindEmployeeDropDown(2, null, null, ref ddlSup);
            //if (Employee.TypeId == 2)
            //{
            //    ddlSup.SelectedValue = Employee.EmployeeId.ToString();
            //    ddlSup.Enabled = false;
            //}
            //Pf.bindEmployeeDropDown(4, null, null, ref ddlMDO);
            //Pf.bindEmployeeDropDown(3, null, Employee.TypeId == 2 ? Employee.EmployeeId : null, ref ddlAuditor);
            //Thread.Sleep(1000);

            Pf.bindAreaDropDown(Employee.EmployeeId.Value, ref ddlArea);
            //ddlProvince.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            Pf.bindAddressDropDown(Employee.EmployeeId.Value, -1, null, null, null, "ProvinceId", "ProvinceName", ref ddlProvince);
            ddlDistrict.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            ddlTown.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            ViewState["PageNumber"] = 1;
        }
        void Filter(int PageNumber = 1)
        {
            try
            {
                if (!DateTime.TryParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime FromDate))
                {
                    Toastr.ErrorToast("Từ ngày không đúng định dạng dd/MM/yyyy");
                    return;
                }
                if (!DateTime.TryParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ToDate))
                {
                    Toastr.ErrorToast("Đến ngày không đúng định dạng dd/MM/yyyy");
                    return;
                }


                //int SupId = Convert.ToInt32(ddlSup.SelectedValue);
                //int AuditorId = Convert.ToInt32(ddlAuditor.SelectedValue);
                int AreaId = Convert.ToInt32(ddlArea.SelectedValue);
                int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
                int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
                int TownId = Convert.ToInt32(ddlTown.SelectedValue);
                //int MVOId = Convert.ToInt32(ddlMDO.SelectedValue);

              
                using (DataTable data = new WorkResultController().BillResultGetList(Employee.EmployeeId.Value, FromDate, ToDate,
                    AreaId, ProvinceId, DistrictId, TownId, null, txtShopCode.Text, txtMobile.Text, txtProduct.Text, txtBillSeri.Text,
                    PageNumber, 20))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        workresult.Visible = true;
                        plPaging.Visible = true;
                        rpWorkResult.DataSource = data;
                        rpWorkResult.DataBind();

                        LoadPaging(data, PageNumber);


                        if (data.Rows.Count > 0)
                        {
                            int totalItem = (int)data.Rows[0]["TotalRows"];
                            int totalPage = (int)Math.Ceiling((decimal)totalItem / 20);
                            //lblTotalRow.Text = totalItem.ToString();
                            ViewState["TotalPage"] = totalPage;
                            DataTable dtPage = new DataTable();
                            dtPage.Columns.Add("Page", typeof(int));
                            dtPage.Columns.Add("Class", typeof(string));

                            rpt_pagination.Visible = true;
                            int currentPage = ViewState["PageNumber"] == null ? 1 : Int16.Parse(ViewState["PageNumber"].ToString());
                            int start = ((int)Math.Ceiling((decimal)currentPage / 20) - 1) * 10 + 1;
                            int end = start + (20 - 1);
                            end = end > totalPage ? totalPage : end;
                            for (int i = start; i <= end; i++)
                            {
                                DataRow dr = dtPage.NewRow();
                                dr["Page"] = i;
                                dr["Class"] = i == PageNumber ? "paginate_button page-item active" : "paginate_button page-item";
                                dtPage.Rows.Add(dr);
                            }
                            rpt_pagination.DataSource = dtPage;
                            rpt_pagination.DataBind();
                        }
                        else
                        {
                            rpt_pagination.Visible = false;
                        }

                    }
                    else
                    {
                        plPaging.Visible = true;
                        workresult.Visible = false;
                        rpWorkResult.DataSource = new DataTable();
                        rpWorkResult.DataBind();
                        Toastr.WarningToast("Không có dữ liệu.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Toastr.ErrorToast(ex.Message);
            }
        }
        private void LoadPaging(DataTable data, int PageNumber = 1, int RowNumber = 20)
        {
            ViewState["PageNumber"] = PageNumber;
            DataTable data_pag = new DataTable();
            data_pag.Columns.Add("PageNumber", typeof(int));
            data_pag.Columns.Add("Active", typeof(string));

            int TotalRows = Convert.ToInt32(data.Rows[0]["TotalRows"]);
            lblFrom.Text = (((PageNumber - 1) * RowNumber) + 1).ToString();
            lblTo.Text = TotalRows > RowNumber ? (PageNumber * RowNumber).ToString() : (RowNumber - (RowNumber - TotalRows)).ToString();
            if (Convert.ToInt32(lblTo.Text) > TotalRows)
                lblTo.Text = TotalRows.ToString();
            lblTotalRows.Text = TotalRows.ToString();

            if (TotalRows > RowNumber)
            {
                int TotalPages = Convert.ToInt32(TotalRows / RowNumber) + 1;
                ViewState["TotalPages"] = TotalPages;
                for (int i = 1; i <= TotalPages; i++)
                {
                    if (i == PageNumber)
                        _ = data_pag.Rows.Add(i, "paginate_button page-item active");
                    else
                        data_pag.Rows.Add(i, "paginate_button page-item");
                }
            }
            else
                _ = data_pag.Rows.Add(1, "paginate_button page-item active");

            ViewState["DataPaging"] = data_pag;
            rptPaging.DataSource = data_pag;
            rptPaging.DataBind();

        }

        protected void lnkPage_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            RepeaterItem item = lnk.NamingContainer as RepeaterItem;
            LinkButton lnkPage = item.FindControl("lnkPage") as LinkButton;

            int PageNumber = Convert.ToInt32(lnkPage.Text.Trim());
            int TotalPages = (int)ViewState["TotalPages"];
            DataTable data_pag = ViewState["DataPaging"] as DataTable;
            ViewState["PageNumber"] = PageNumber;
            lnkPrevious.Enabled = lnkNext.Enabled = true;
            if (PageNumber == 1)
                lnkPrevious.Enabled = !lnkPrevious.Enabled;
            else if (PageNumber == TotalPages)
                lnkNext.Enabled = !lnkNext.Enabled;

            Filter(PageNumber);
            //LoadPaging(data_pag, PageNumber);
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            int PageNumber = (int)ViewState["PageNumber"];
            int Previous = PageNumber == 1 ? 1 : PageNumber - 1;
            lnkPrevious.Enabled = lnkNext.Enabled = true;
            DataTable data_pag = ViewState["DataPaging"] as DataTable;
            if (PageNumber == 1)
                lnkPrevious.Enabled = false;
            Filter(Previous);
            //LoadPaging(data_pag, Previous);
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            int TotalPages = (int)ViewState["TotalPages"];
            int PageNumber = (int)ViewState["PageNumber"];
            int Next = PageNumber == TotalPages ? TotalPages : PageNumber + 1;
            lnkPrevious.Enabled = lnkNext.Enabled = true;
            DataTable data_pag = ViewState["DataPaging"] as DataTable;
            if (PageNumber == TotalPages)
                lnkNext.Enabled = false;
            Filter(Next);
            //LoadPaging(data_pag, Next);
        }


        protected void rpt_pagination_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
            Filter(Convert.ToInt32(e.CommandArgument));
        }

        protected void lbn_next_Click(object sender, EventArgs e)
        {
            int? pageNumber = Int16.Parse(ViewState["PageNumber"].ToString());
            int? totalPage = Int16.Parse(ViewState["TotalPage"].ToString());
            if (pageNumber < totalPage)
            {
                pageNumber++;
                ViewState["PageNumber"] = pageNumber;
                Filter(pageNumber.Value);
            }
        }

        protected void lbn_prev_Click(object sender, EventArgs e)
        {
            int? pageNumber = Int16.Parse(ViewState["PageNumber"].ToString());
            if (pageNumber > 1)
            {
                pageNumber--;
                ViewState["PageNumber"] = pageNumber;
                Filter(pageNumber.Value);
            }

        }
        protected void lbn_first_Click(object sender, EventArgs e)
        {
            ViewState["PageNumber"] = 1;
            Filter(1);
        }

        protected void lbn_end_Click(object sender, EventArgs e)
        {
            ViewState["PageNumber"] = ViewState["TotalPage"];
            Filter(Convert.ToInt32(ViewState["TotalPage"]));
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
            if (ProvinceId < 0)
            {
                ddlDistrict.Items.Clear();
                ddlTown.Items.Clear();
                ddlDistrict.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
                ddlTown.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            }
            else
                Pf.bindAddressDropDown(Employee.EmployeeId.Value, null, ProvinceId, null, null, "DistrictId", "DistrictName", ref ddlDistrict);
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            int AreaId = Convert.ToInt32(ddlArea.SelectedValue);
            if (AreaId < 0)
            {
                ddlProvince.Items.Clear();
                ddlDistrict.Items.Clear();
                ddlTown.Items.Clear();
                ddlProvince.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
                ddlDistrict.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
                ddlTown.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            }
            else
                Pf.bindAddressDropDown(Employee.EmployeeId.Value, AreaId, null, null, null, "ProvinceId", "ProvinceName", ref ddlProvince);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            if (DistrictId < 0)
            {
                ddlTown.Items.Clear();
                ddlTown.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
            }
            else
                Pf.bindAddressDropDown(Employee.EmployeeId.Value, null, null, DistrictId, null, "TownId", "TownName", ref ddlTown);
        }
        protected void img_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton image = sender as ImageButton;
            RepeaterItem rpt = (RepeaterItem)image.NamingContainer;
            Panel pnGrid = rpt.FindControl("pnGrid") as Panel;
            PlaceHolder pndetail = rpt.FindControl("pndetail") as PlaceHolder;

            if (image.CommandName == "Show")
            {
                image.ImageUrl = "~/images/minus.png";
                pnGrid.Visible = pndetail.Visible = true;
                image.CommandName = "Hide";
            }
            else if (image.CommandName == "Hide")
            {
                image.CommandName = "Show";
                pnGrid.Visible = pndetail.Visible = false;
                image.ImageUrl = "~/images/plus.png";
            }
        }
        private void BindTabAttendant(RepeaterItem item, Panel pbb)
        {
            try
            {
                Repeater rptAttendant = item.FindControl("rptAttendant") as Repeater;
                Label lblAuditDate = (Label)item.FindControl("lblAuditDate");
                Label lblShopId = (Label)item.FindControl("lblShopId");
                Label lblEmployeeId = (Label)item.FindControl("lblEmployeeId");

                Label lblWorkId = (Label)item.FindControl("lblWorkId");

                Button btn = (Button)item.FindControl("btnChangeImage");
                btn.OnClientClick = $"popWindow_Custom('/Popups/ImportImageAdmin.aspx?WorkId=" + lblWorkId.Text + "&KPIId=0', 700, 520); return false;";

                int EmployeeId = Convert.ToInt32(lblEmployeeId.Text.Trim());
                int ShopId = Convert.ToInt32(lblShopId.Text.Trim());
                int AuditDate = Convert.ToInt32(lblAuditDate.Text.Trim());
                DataTable data = new AttendantController().AttendantGetList(EmployeeId, ShopId, AuditDate);
                if (data != null && data.Rows.Count > 0)
                {
                    rptAttendant.DataSource = data;
                    rptAttendant.DataBind();
                }
                else
                    pbb.Visible = false;
            }
            catch (Exception ex)
            {
                pbb.Visible = false;
                Toastr.ErrorToast(ex.Message);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

            if (!DateTime.TryParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime FromDate))
            {
                Toastr.ErrorToast("Từ ngày không đúng định dạng dd/MM/yyyy");
                return;
            }
            if (!DateTime.TryParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ToDate))
            {
                Toastr.ErrorToast("Đến ngày không đúng định dạng dd/MM/yyyy");
                return;
            }
            int AreaId = Convert.ToInt32(ddlArea.SelectedValue);
            int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
            int DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            int TownId = Convert.ToInt32(ddlTown.SelectedValue);
            using (DataTable data = new WorkResultController().BillResultExport(Employee.EmployeeId.Value, FromDate, ToDate,
                    AreaId, ProvinceId, DistrictId, TownId, null, txtShopCode.Text, txtMobile.Text, txtProduct.Text, txtBillSeri.Text))
            {
                if (data != null && data.Rows.Count > 0)
                {
                    string Filename = $"Report_BillResult_{DateTime.Now:yyyyMMddHHmmss}";
                    Pf.Excel(data, Filename);
                }
                else
                {
                    Toastr.WarningToast("Không có dữ liệu.");
                    return;
                }
            }
        }
        //protected void ddlSup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlAuditor.Items.Clear();
        //    string selected = ddlSup.SelectedValue;
        //    if (!string.IsNullOrEmpty(selected) && selected != "-1")
        //        Pf.bindEmployeeDropDown(null, null, Convert.ToInt32(selected), ref ddlAuditor);
        //    else
        //        ddlAuditor.Items.Insert(0, new ListItem("-Tất cả-", "-1"));
        //}
    }
}