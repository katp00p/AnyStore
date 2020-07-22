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
    public partial class frmInventory : Form
    {
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();

        public frmInventory()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            DataTable cDT = cdal.Select();
            cmbCategories.DataSource = cDT;
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";

            DataTable pDT = pdal.Select();
            dgvProducts.DataSource = pDT;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cmbCategories.Text;

            DataTable dt = pdal.DisplayProductByCategory(category);
            dgvProducts.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
        }
    }
}
