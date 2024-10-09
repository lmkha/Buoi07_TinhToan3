using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";
            radCong.Checked = true;             //đầu tiên chọn phép cộng
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            string so1 = txtSo1.Text;
            string so2 = txtSo2.Text;
            string kq = "0";
            if (radCong.Checked)
                kq = CongSoLon(so1, so2);
            else if (radTru.Checked)
                kq = TruSoLon(so1, so2);
            //else if (radNhan.Checked) 
            //    kq = so1 * so2;
            //else if (radChia.Checked && so2 != 0) 
            //    kq = so1 / so2;
            if (txtSo2.Text == "0" && radChia.Checked)
            {
                MessageBox.Show("Không thể chia cho số 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSo2.Focus();
                return;
            }
            txtKq.Text = kq.ToString();
        }


        private void txtSo2_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo2.SelectAll();
        }
        private void txtSo1_MouseClick(object sender, MouseEventArgs e)
        {
            txtSo1.SelectAll();
        }

        private string CongSoLon(string soThuNhat, string soThuHai)
        {
            bool laSoAm1 = soThuNhat.StartsWith("-");
            bool laSoAm2 = soThuHai.StartsWith("-");

            if (laSoAm1 && laSoAm2)
            {
                return "-" + CongSoLon(soThuNhat.Substring(1), soThuHai.Substring(1));
            }
            else if (laSoAm1)
            {
                return TruSoLon(soThuHai, soThuNhat.Substring(1));
            }
            else if (laSoAm2)
            {
                return TruSoLon(soThuNhat, soThuHai.Substring(1));
            }

            while (soThuNhat.Length < soThuHai.Length)
                soThuNhat = "0" + soThuNhat;
            while (soThuHai.Length < soThuNhat.Length)
                soThuHai = "0" + soThuHai;

            string ketQua = "";
            int nho = 0;

            for (int i = soThuNhat.Length - 1; i >= 0; i--)
            {
                int chuSo1 = soThuNhat[i] - '0';
                int chuSo2 = soThuHai[i] - '0';
                int tong = chuSo1 + chuSo2 + nho;

                ketQua = (tong % 10).ToString() + ketQua;
                nho = tong / 10;
            }

            if (nho > 0)
                ketQua = nho.ToString() + ketQua;

            return ketQua.TrimStart('0');
        }


        private string TruSoLon(string soThuNhat, string soThuHai)
        {
            bool laSoAm = false;

            if (SoSanhLonHon(soThuHai, soThuNhat))
            {
                string temp = soThuNhat;
                soThuNhat = soThuHai;
                soThuHai = temp;
                laSoAm = true;
            }

            while (soThuNhat.Length < soThuHai.Length)
                soThuNhat = "0" + soThuNhat;
            while (soThuHai.Length < soThuNhat.Length)
                soThuHai = "0" + soThuHai;

            string ketQua = "";
            int muon = 0;

            for (int i = soThuNhat.Length - 1; i >= 0; i--)
            {
                int chuSo1 = soThuNhat[i] - '0';
                int chuSo2 = soThuHai[i] - '0';

                int hieu = chuSo1 - chuSo2 - muon;
                if (hieu < 0)
                {
                    hieu += 10;
                    muon = 1;
                }
                else
                {
                    muon = 0;
                }

                ketQua = hieu.ToString() + ketQua;
            }

            ketQua = ketQua.TrimStart('0');

            if (ketQua == "")
                ketQua = "0";

            if (laSoAm)
                ketQua = "-" + ketQua;

            return ketQua;
        }

        private bool SoSanhLonHon(string soThuNhat, string soThuHai)
        {
            if (soThuNhat.Length > soThuHai.Length) return true;
            if (soThuNhat.Length < soThuHai.Length) return false;
            return string.Compare(soThuNhat, soThuHai) > 0;
        }
 
    }
}
