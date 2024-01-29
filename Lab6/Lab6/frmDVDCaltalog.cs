using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class frmDVDCaltalog : Form
    {
        int codeNo;
        decimal price;
        string language;
        int subtitile;
        public frmDVDCaltalog()
        {
            InitializeComponent();
            ResetFields(false);
        }

        void ResetFields(bool status)
        {
            txtNo.Clear();
            txtTitle.Clear();
            cbLang.SelectedIndex = -1;
            updPrice.Value = updPrice.Minimum;
            radYes.Checked = true;
            radNo.Checked = false;
            btnSave.Enabled = status;
            btnCancel.Enabled = status;
            btnAdd.Enabled = !status;
            txtTitle.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsDatabase.OpenConnect();
                SqlCommand com = new SqlCommand("Select max(DVDCodeNo) from DVDLibrary", clsDatabase.con);
                codeNo = Convert.ToInt32(com.ExecuteScalar());
                clsDatabase.CloseConnect();
            }catch (Exception ex)
            {
                codeNo = 0;
            }

            codeNo++;
            ResetFields(true);
            txtNo.Text = codeNo.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFields(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                clsDatabase.OpenConnect();
                string cmd = "insert into DVDLibrary values(@no, @title, @lang, @subtitle, @price)";
                SqlCommand command = new SqlCommand(cmd, clsDatabase.con);

                SqlParameter no = new SqlParameter("@no", SqlDbType.Int);
                no.Value = codeNo;
                command.Parameters.Add(no);

                SqlParameter title = new SqlParameter("@title", SqlDbType.NVarChar);
                title.Value = txtTitle.Text;
                command.Parameters.Add(title);

                SqlParameter lang = new SqlParameter("@lang", SqlDbType.NVarChar);
                lang.Value = cbLang.SelectedIndex.ToString();
                command.Parameters.Add(lang);

                SqlParameter subtitle = new SqlParameter("@subtitle", SqlDbType.Bit);
                subtitle.Value = subtitile; 
                command.Parameters.Add(subtitle);

                SqlParameter dvdPrice = new SqlParameter("@price", SqlDbType.Money);
                dvdPrice.Value = price;
                command.Parameters.Add(dvdPrice);

                command.ExecuteNonQuery();
                MessageBox.Show("Insert successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetFields(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not insert");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Can not insert");
            }
            finally
            {
                clsDatabase.CloseConnect();
            }
        }

        private void updPrice_ValueChanged(object sender, EventArgs e)
        {
            price = updPrice.Value;
        }

        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLang.SelectedIndex == 0) { return; }
            language = cbLang.SelectedIndex.ToString();
        }

        private void radYes_CheckedChanged(object sender, EventArgs e)
        {
            subtitile = 1;
        }

        private void radNo_CheckedChanged(object sender, EventArgs e)
        {
            subtitile = 0;
        }
    }
}
