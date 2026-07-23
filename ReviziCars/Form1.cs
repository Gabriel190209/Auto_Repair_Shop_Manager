using ReviziCars.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReviziCars
{
    public partial class Form1 : Form
    {
        dbReviziCarEntities db = new dbReviziCarEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Colors.Preto;
            btnEnt.FillColor = Colors.AzulEsc;
        }


        private void btnEnt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtSenha.Text))
            {
                var b = db.Funcionarios.Where(x => x.NomeCompleto == txtName.Text && x.Senha == txtSenha.Text).FirstOrDefault();

                if (b == null)
                {
                    "Funcionário não encontrado".Alert();
                    return;
                }

                Settings.Default.Token = txtName.Text;
                Settings.Default.IsLogged = true;
                Settings.Default.Save();

                new Form3().Show();
                this.Hide();
            }
            else
            {
                "Não pode haver campos vazios".Alert();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
