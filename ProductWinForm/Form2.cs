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
    public partial class Form2 : Form
    {
        public event EventHandler<string>? ProductCreated = null;
        public Form2()
        {
            InitializeComponent();
            cboCat.DataSource = Enum.GetNames<Category>()
                                    .Where(x => x!=Enum.GetName<Category>(Category.None))
                                    .ToList();
            cboCat.SelectedIndex = -1;
            btnClear.Click += DoClickClear;
            btnSubmit.Click += DoClickSubmit;
        }

        private void DoClickClear(object? sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();
            cboCat.SelectedIndex = -1;
        }

        private async void DoClickSubmit(object? sender, EventArgs e)
        {
            var code = txtCode.Text.Trim();
            if (string.IsNullOrEmpty(code)){
                MessageBox.Show("Code is required", "Submitting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            ProductCreateReq req = new()
            {
                Code = code,
                Name = txtName.Text,
                Category = cboCat.SelectedItem?.ToString()
            };
            try
            {
                string endpoint = $"{Program.Configuration.Route}";
                using RestClient client = new(Program.Configuration.BaseUri);
                var reqResult = await client.PostAsync<ProductCreateReq, Result<string?>>(endpoint, req);
                if(reqResult != null)
                {
                    if (reqResult.Succeded)
                    {
                        ProductCreated?.Invoke(this, reqResult.Data!);
                        MessageBox.Show($"Successfully created", "Creating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error>{reqResult!.Message}", "Creating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Error>{ex.Message}", "Retrieving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
