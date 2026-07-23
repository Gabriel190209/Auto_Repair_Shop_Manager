using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReviziCars
{
    public partial class Form2 : Form
    {
        dbReviziCarEntities db = new dbReviziCarEntities();
        public Form2()
        {
            InitializeComponent();
        }

        private void btnCad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                var b = db.Funcionarios.Where(i => i.NomeCompleto == txtName.Text).FirstOrDefault();

                if (b != null)
                {
                    "Usuário ja cadastrado".Alert();
                    return;
                }

                db.Funcionarios.Add(new Funcionario() { NomeCompleto = txtName.Text, Senha = txtSenha.Text,DataCadastro = DateTime.Now });
                db.SaveChanges();
                "Usuário cadastrado com sucesso".Alert();

            } else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Colors.Preto;
            btnCad.BackColor = Colors.AzulEsc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
