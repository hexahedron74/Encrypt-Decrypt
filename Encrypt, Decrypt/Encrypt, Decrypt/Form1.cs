using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encrypt__Decrypt
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        String hash = "hex@hedr0n";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(txtValue.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        txtEncrypt.Text = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "디코드 할 문자열을 다시 한번 확인해주시기 바랍니다.", "Error(ISD01)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = Convert.FromBase64String(txtEncrypt.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                            {
                                ICryptoTransform transform = tripDes.CreateDecryptor();
                                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                                txtDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                             }
                    }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "디코드 할 문자열을 다시 한번 확인해주시기 바랍니다.", "Error(ISD01)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
