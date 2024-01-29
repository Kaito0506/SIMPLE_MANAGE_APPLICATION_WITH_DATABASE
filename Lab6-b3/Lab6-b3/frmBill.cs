using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_b3
{
    public partial class frmBill : Form
    {
        int CustomerID;
        DataSet DS = new DataSet();
        public frmBill()
        {
            InitializeComponent();
            InitData();
            Reset(true);
        }

        void InitData()
        {
            try
            {
                Database.OpenConnect();
                SqlDataAdapter da = new SqlDataAdapter("Select * from room", Database.con);
                da.Fill(DS, "room");

                cbRoomNum.DataSource = DS.Tables["room"];
                cbRoomNum.DisplayMember = "roomName";
                cbRoomNum.ValueMember = "roomID";
                Database.CloseConnect();


            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Access data unsuccessfully");
            }
        }

        public void Reset(bool status)
        {
            txtBillID.Clear();
            txtCCCD.Clear();
            txtName.Clear();
            txtName.Clear();
            txtTotal.Clear();
            cbRoomNum.SelectedIndex = -1;
            btnAdd.Enabled = status;
            btnSave.Enabled = !status;
            DateTT.Value = DateTime.Now;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Database.OpenConnect();
                SqlCommand cmd = new SqlCommand("Select max(ID) from customer", Database.con);
                CustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                Database.CloseConnect();

            }catch (Exception ex)
            {
                CustomerID = 0;
                Console.Write("Can not access customer");
                Console.WriteLine(ex.Message);
            }
            CustomerID++;
            Reset(false);
            txtBillID.Text = CustomerID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Database.OpenConnect();
                SqlCommand cmd = new SqlCommand("INSERT_CUSTOMER", Database.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", CustomerID);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@cccd", txtCCCD.Text);
                cmd.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@room", cbRoomNum.SelectedValue);

                cmd.ExecuteNonQuery();
                Database.CloseConnect();
                Reset(true);
                MessageBox.Show("Insert successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }catch (Exception ex)
            {
                Console.WriteLine("INSERT ERROR: " + ex.Message);
                MessageBox.Show("Can not save information");
            }
        }
    }
}
