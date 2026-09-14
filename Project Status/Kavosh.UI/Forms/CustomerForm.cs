using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Kavosh.Services;
using Kavosh.Services.DTOs;

namespace YourApp.UI.Forms
{
    public partial class CustomerForm : XtraForm
    {
        private readonly CustomerService _customerService;
        private int? _selectedCustomerId;

        public CustomerForm(CustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
        }

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            await LoadCustomersAsync();
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            //var customers = await _customerService.GetAllCustomersAsync();
            //gridControl1.DataSource = customers;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (gridView1.GetFocusedRow() is CustomerDto dto)
            //{
            //    _selectedCustomerId = dto.Id;
            //    txtFullName.Text = dto.FullName;
            //    txtPhone.Text = dto.PhoneNumber;
            //    txtEmail.Text = dto.Email;
            //}
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _selectedCustomerId = null;
            txtFullName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFullName.Focus();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var dto = new CustomerDto
            //    {
            //        Id = _selectedCustomerId ?? 0,
            //        FullName = txtFullName.Text,
            //        PhoneNumber = txtPhone.Text,
            //        Email = txtEmail.Text
            //    };

            //    if (_selectedCustomerId.HasValue)
            //        await _customerService.UpdateCustomerAsync(dto);
            //    else
            //        await _customerService.AddCustomerAsync(dto);

            //    await LoadCustomersAsync();
            //    XtraMessageBox.Show("ذخیره شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            //if (!_selectedCustomerId.HasValue)
            //    return;

            //var confirm = XtraMessageBox.Show("مشتری حذف شود؟", "تایید حذف",
            //    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (confirm == DialogResult.Yes)
            //{
            //    await _customerService.DeleteCustomerAsync(_selectedCustomerId.Value);
            //    await LoadCustomersAsync();
            //    btnNew_Click(sender, e);
            //}
        }
    }
}
