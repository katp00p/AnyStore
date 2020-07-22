using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmProducts : Form
    {
        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();

        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            DataTable categoriesDT = cdal.Select();
            cmbCategory.DataSource = categoriesDT;
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDecription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            p.added_by = usr.id;

            bool success = pdal.Insert(p);

            if (success)
            {
                clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to add new product.");
            }
        }

        public void clear()
        {
            txtProductID.Text = "";
            txtName.Text = "";
            cmbCategory.Text = "";
            txtDecription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDecription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);
            
            p.id = int.Parse(txtProductID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDecription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
            p.added_by = usr.id;

            bool success = pdal.Update(p);

            if (success)
            {
                clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to update product.");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            p.id = int.Parse(txtProductID.Text);

            bool success = pdal.Delete(p);

            if (success)
            {
                clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
                MessageBox.Show("Product has been sucessfully deleted.");
            }
            else
            {
                MessageBox.Show("Failed to delete product.");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
