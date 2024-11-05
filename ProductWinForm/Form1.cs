using productlib;
using RestClientLib;

namespace ProductWinForm
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new();
        public Form1()
        {
            InitializeComponent();
            bs.DataSource = new List<ProductResponse>();
            dgvProducts.DataSource = bs;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridView.CheckForIllegalCrossThreadCalls = false;

            btnRefresh.Click += DoClickRefresh;
            btnDelete.Click += DoClickDelete;
            btnNew.Click += DoClickNew;
            btnEdit.Click += DoClickEdit;
        }

        private async void DoClickRefresh(object? sender, EventArgs e)
        {
            try
            {
                using RestClient client = new(Program.Configuration.BaseUri);
                string endpoint = $"{Program.Configuration.Route}";
                var reqResult = await client.GetAsync<Result<List<ProductResponse>>>(endpoint);
                if(reqResult != null && reqResult.Succeded)
                {
                    bs.DataSource = reqResult.Data;
                    bs.ResetBindings(false);
                }
                else
                {
                    MessageBox.Show($"Error>{reqResult!.Message}", "Retrieving", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception ex)
            {
                MessageBox.Show($"Error>{ex.Message}", "Retrieving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DoClickDelete(object? sender, EventArgs e)
        {
            if (bs.Current == null) return;
            var result = MessageBox.Show("Are you sure to delete the current product?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                ProductResponse prd = (bs.Current as ProductResponse)!;
                string endpoint = $"{Program.Configuration.Route}/{prd.Id}";
                using RestClient client = new RestClient(Program.Configuration.BaseUri);
                var reqResult = await client.DeleteAsync<Result<bool>>(endpoint);
                if(reqResult != null)
                {
                    if(reqResult.Succeded)
                    {
                        bs.Remove(prd);
                        bs.ResetBindings(false);
                    }else
                    {
                        MessageBox.Show($"Error>{reqResult!.Message}", "Deleting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Error>{ex.Message}", "Deleting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DoClickNew(object? sender, EventArgs e)
        {
            var frm = new Form2();
            frm.ProductCreated += DoClickProductCreated;
            frm.Show();
        }

        private void DoClickProductCreated(object? sender, string e)
        {
            Task.Run(async () =>
            {
                string endpoint = $"{Program.Configuration.Route}/{e}";
                try
                {
                    using RestClient client = new RestClient(Program.Configuration.BaseUri);
                    var result = await client.GetAsync<Result<ProductResponse?>>(endpoint);
                    if(result != null)
                    {
                        var created = result.Data;
                        if(created != null)
                        {
                            bs.Add(created);
                            bs.ResetBindings(false);
                        }
                    }
                }catch (Exception)
                {   
                }
            });
        }

        private void DoClickEdit(object? sender, EventArgs e)
        {
            var prd = bs.Current as ProductResponse;
            var frm = new Form3(prd);
            frm.ProductUpdated += DoClickProductUpdated;
            frm.Show();
        }

        private void DoClickProductUpdated(object? sender, string e)
        {
            Task.Run(async () =>
            {
                string endpoint = $"{Program.Configuration.Route}/{e}";
                try
                {
                    using RestClient client = new RestClient(Program.Configuration.BaseUri);
                    var result = await client.GetAsync<Result<ProductResponse?>>(endpoint);
                    if (result != null)
                    {
                        var updated = result.Data;
                        var found = (bs.DataSource as List<ProductResponse>)!
                                    .FirstOrDefault(x => x.Id == updated!.Id);
                        if (found != null)
                        {
                            found.Name = updated!.Name;
                            found.Category = updated!.Category;
                            bs.ResetBindings(false);
                        }
                    }
                }
                catch (Exception)
                {
                }
            });
        }
    }
}
