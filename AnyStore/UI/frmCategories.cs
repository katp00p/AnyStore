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
    public partial class frmCategories : Form
    {
        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();

        public frmCategories()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            
            c.title = txtTitle.Text;
            c.description = txtDecription.Text;
            c.added_date = DateTime.Now;
            c.added_by = usr.id;

            bool success = dal.Insert(c);

            if (success)
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to insert new category");
            }
        }

        public void Clear()
        {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDecription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[rowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[rowIndex].Cells[1].Value.ToString();
            txtDecription.Text = dgvCategories.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDecription.Text;
            c.added_date = DateTime.Now;
            c.added_by = usr.id;

            bool success = dal.Update(c);

            if (success)
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update category");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            c.id = int.Parse(txtCategoryID.Text);

            bool success = dal.Delete(c);

            if (success)
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete category");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
