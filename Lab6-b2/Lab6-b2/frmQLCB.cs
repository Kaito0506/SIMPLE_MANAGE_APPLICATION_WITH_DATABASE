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

namespace Lab6_b2
{
    public partial class frmQLCB : Form
    {
        int MSCB;
        DataSet DS = new DataSet();
        SqlDataAdapter adChucVu;
        public frmQLCB()
        {
            InitializeComponent();
            InitializeDatabase();
            Reset(false);


        }

        void InitializeDatabase()
        {
            try
            {
                Database.OpenConnect();
                SqlDataAdapter da = new SqlDataAdapter("Select * from ChucVu", Database.con);
                da.Fill(DS, "ChucVu");
                Database.CloseConnect();
                // complex biding
                cbCV.DataSource = DS.Tables["ChucVu"];
                cbCV.DisplayMember = "tenCV";
                cbCV.ValueMember = "maCV";

            }catch (Exception ex)
            {
                Console.WriteLine("Error access CV");
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Database.OpenConnect();
                SqlCommand com = new SqlCommand("select max(maCB) from CanBo", Database.con);
                MSCB = Convert.ToInt32(com.ExecuteScalar());
                Database.CloseConnect();


            }catch (Exception ex)
            {
                MSCB = 0;
            }
            MSCB++;
            Reset(true );
            txtMSCB.Text = MSCB.ToString();

        }

        void Reset( bool status)
        {
            txtName.Clear();
            txtHours.Clear();
            txtPrice.Clear();
            txtMSCB.Clear();
            btnAdd.Enabled = !status;
            btnSave.Enabled = status;
            
        }

        private bool IsNotNullOrEmpty(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNotNullOrEmpty(txtName.Text) || !IsNotNullOrEmpty(txtHours.Text) || !IsNotNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Please enter valid values for all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string cmd = "insert into CanBo values(@id, @name, @roleid, @hours, @price)";
                Database.OpenConnect();
                SqlCommand com = new SqlCommand(cmd, Database.con);

                com.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = MSCB;
                com.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 30)).Value = txtName.Text;
                com.Parameters.Add(new SqlParameter("@roleid", SqlDbType.Int)).Value = cbCV.SelectedValue;
                com.Parameters.Add(new SqlParameter("@hours", SqlDbType.Int)).Value = Convert.ToInt32(txtHours.Text);
                com.Parameters.Add(new SqlParameter("@price", SqlDbType.Money)).Value = Convert.ToInt32(txtPrice.Text);

                com.ExecuteNonQuery();
                Reset(false);
                MessageBox.Show("Insert successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }catch (Exception ex) {
                Console.WriteLine("INSERT ERROR: " + ex.ToString());
                MessageBox.Show("Error appears when inserting data \n");
            }
        }
    }
}
