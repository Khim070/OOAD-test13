using productlib;
using RestClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductWinForm
{
    public partial class Form3 : Form
    {
        public event EventHandler<string>? ProductUpdated = null;
        private ProductResponse? curProduct = null;
        public Form3(ProductResponse? Product = null)
        {
            InitializeComponent();
            curProduct = Product;
            cboCat.DataSource = Enum.GetNames<Category>()
                                    .Where(x => x != Enum.GetName<Category>(Category.None))
                                    .ToList();
            cboCat.SelectedIndex = -1;

            ShowCurrentProduct();

            btnCancel.Click += (sender, e) => { ShowCurrentProduct();  };
            btnSubmit.Click += DoClickSubmit;
            txtKey.Leave += DoKeyLeave;
        }

        private async void DoKeyLeave(object? sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();
            if (key.Equals(curProduct?.Id) || key.Equals(curProduct?.Code)) return;

            string endpoint = $"{Program.Configuration.Route}/{key}";
            try
            {
                using RestClient client = new RestClient(Program.Configuration.BaseUri);
                var reqResult = await client.GetAsync<Result<ProductResponse?>>(endpoint);
                if(reqResult != null)
                {
                    curProduct = reqResult.Data;
                    ShowCurrentProduct();
                    txtKey.Text = key;
                }
            }
            catch (Exception) { }
        }

        private async void DoClickSubmit(object? sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();
            string? name = txtName.Text.Trim();
            string? cat = cboCat.SelectedItem?.ToString();
            if (
                (key.Equals(curProduct?.Name, StringComparison.OrdinalIgnoreCase) ||
                 key.Equals(curProduct?.Code, StringComparison.OrdinalIgnoreCase)) &&
                (name?.Equals(curProduct?.Name) ?? false) &&
                (cat?.Equals(curProduct?.Category) ?? false))
                {
                MessageBox.Show("No modification to be updated", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;    
            }

            ProductUpdateReq req = new ProductUpdateReq()
            {
                Key = txtKey.Text.Trim(),
                Name = name,
                Category = cat,
            };

            try
            {
                string endpoint = $"{Program.Configuration.Route}";
                using RestClient client = new RestClient(Program.Configuration.BaseUri);
                var reqResult = await client.PutAsync<ProductUpdateReq, Result<bool>>(endpoint, req);
                if(reqResult != null)
                {
                    if (reqResult.Succeded)
                    {
                        ProductUpdated?.Invoke(this, req.Key);
                        MessageBox.Show($"Successfully updated", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error>{reqResult!.Message}", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show($"Error>{ex.Message}", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCurrentProduct()
        {
            txtKey.Text = curProduct?.Id;
            txtName.Text = curProduct?.Name;
            cboCat.SelectedItem = curProduct?.Category;
        }
    }
}
