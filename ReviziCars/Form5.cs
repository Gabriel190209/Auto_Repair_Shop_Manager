using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReviziCars
{
    public partial class Form5 : Form
    {
        dbReviziCarEntities db = new dbReviziCarEntities();
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            numericUpDown1.Increment = 0.1M;
            numericUpDown1.DecimalPlaces = 1;
            this.BackColor = Colors.Preto;

            GroupBox[] gps = { groupBox1, groupBox2, groupBox3, groupBox5 };

            foreach (var i in gps)
            {
                i.Hide();
            }

            Guna2Button[] btns = { btnAt, btnMa, btnSe, btnC };

            foreach (var i in btns)
            {
                i.FillColor = Colors.AzulEsc;
            }

            CarregarDados();
        }

        private void CarregarDados()
        {
            dtgA.Columns.Clear();
            dtgA.Rows.Clear();

            dtgA.Columns.Add("nome", "Nome");
            dtgA.Columns.Add("acao", "");

            var a = db.Funcionarios.Select(i => new
            {
                Nome = i.NomeCompleto
            }).ToList();

            foreach (var item in a)
            {
                dtgA.Rows.Add(item.Nome, "❌");
            }

            dtgM.Columns.Clear();
            dtgM.Rows.Clear();

            dtgM.Columns.Add("nome", "Nome");
            dtgM.Columns.Add("acao", "");

            var b = db.Marcas.Select(i => new { Nome = i.Nome }).ToList();

            foreach (var item in b)
            {
                dtgM.Rows.Add(item.Nome, "❌");
            }

            dtgS.Columns.Clear();
            dtgS.Rows.Clear();

            dtgS.Columns.Add("nome", "Nome");
            dtgS.Columns.Add("acao", "");

            var c = db.Servicoes.Select(i => new { Nome = i.Nome }).ToList();

            foreach (var item in c)
            {
                dtgS.Rows.Add(item.Nome, "❌");
            }

            dtg.Columns.Clear();
            dtg.Rows.Clear();

            dtg.Columns.Add("nome", "Nome");
            dtg.Columns.Add("taxa", "Taxa");
            dtg.Columns.Add("acao", "");

            var e = db.FormaPagamentoes.Select(i => new { Nome = i.Nome, Taxa = i.TaxaDescontoPercent }).ToList();

            foreach(var i in e)
            {
                dtg.Rows.Add(i.Nome, $"{i.Taxa}%", "❌");
            }

            dtgC.Columns.Clear();
            dtgC.Rows.Clear();

            dtgC.Columns.Add("nome", "Nome");
            dtgC.Columns.Add("cpf", "CPF");
            dtgC.Columns.Add("cnpj", "CNPJ");
            dtgC.Columns.Add("email", "Email");
            dtgC.Columns.Add("tel", "Telefone");
            dtgC.Columns.Add("data", "Data");
            dtgC.Columns.Add("acao", "");

            var f = db.Clientes.Select(i => new {Nome = i.Nome, CPF = i.CPF, CNPJ = i.CNPJ, Email =  i.Email, Tel = i.Telefone, Data = i.DataCadastro}).ToList();

            foreach (var i in f)
            {
                dtgC.Rows.Add(i.Nome, i.CPF, i.CNPJ, i.Email, i.Tel, i.Data, "❌");
            }
        }

        private void btnAddA_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAtendente.Text))
            {
                Funcionario f = new Funcionario();
                f.NomeCompleto = txtAtendente.Text;
                db.Funcionarios.Add(f);
                db.SaveChanges();
                CarregarDados();
                txtAtendente.Text = "";
            }
            else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void btnAddM_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMarca.Text))
            {
                Marca m = new Marca();
                m.Nome = txtMarca.Text;
                db.Marcas.Add(m);
                db.SaveChanges();
                CarregarDados();
                txtMarca.Text = "";
            }
            else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void btnAddS_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtServico.Text))
            {
                Servico s = new Servico();
                s.Nome = txtServico.Text;
                db.Servicoes.Add(s);
                db.SaveChanges();
                CarregarDados();
                txtServico.Text = "";
            }
            else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void dtgA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                var linha = dtgA.Rows[e.RowIndex];

                if (linha.Cells["acao"].Value == "❌")
                {
                    var nome = linha.Cells["nome"].Value.ToString();

                    var b = db.Funcionarios.Where(i => i.NomeCompleto == nome).FirstOrDefault();

                    db.Funcionarios.Remove(b);
                    db.SaveChanges();
                    CarregarDados();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                "Não é possivel excluir Forma de Pagamento havendo vendas cadastradas com ela".Alert();
            }
        }

        private void dtgM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                var linha = dtgM.Rows[e.RowIndex];

                if (linha.Cells["acao"].Value == "❌")
                {
                    var nome = linha.Cells["nome"].Value.ToString();

                    var b = db.Marcas.Where(i => i.Nome == nome).FirstOrDefault();

                    db.Marcas.Remove(b);
                    db.SaveChanges();
                    CarregarDados();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                "Não é possivel excluir Forma de Pagamento havendo vendas cadastradas com ela".Alert();
            }
        }

        private void dtgS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                var linha = dtgS.Rows[e.RowIndex];

                if (linha.Cells["acao"].Value == "❌")
                {
                    var nome = linha.Cells["nome"].Value.ToString();

                    var b = db.Servicoes.Where(i => i.Nome == nome).FirstOrDefault();

                    db.Servicoes.Remove(b);
                    db.SaveChanges();
                    CarregarDados();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                "Não é possivel excluir Forma de Pagamento havendo vendas cadastradas com ela".Alert();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNome.Text))
            {
                
                try
                {
                    db.FormaPagamentoes.Add(new FormaPagamento() { Nome = txtNome.Text, TaxaDescontoPercent = numericUpDown1.Value });
                    db.SaveChanges();
                    CarregarDados();
                    numericUpDown1.Value = 0;
                    txtNome.Text = "";
                }
                catch (Exception ex)
                {
                    var inner = ex.InnerException?.InnerException?.Message
                                ?? ex.InnerException?.Message
                                ?? ex.Message;
                    MessageBox.Show("Erro detalhado: " + inner);
                }
            }
            else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void dtg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var linha = dtg.Rows[e.RowIndex];

                if (linha.Cells["acao"].Value == "❌")
                {
                    var nome = linha.Cells["nome"].Value.ToString();

                    var b = db.FormaPagamentoes.Where(i => i.Nome == nome).FirstOrDefault();

                    db.FormaPagamentoes.Remove(b);
                    db.SaveChanges();
                    CarregarDados();
                }
                else
                {
                    return;
                }
            } catch
            {
                "Não é possivel excluir Forma de Pagamento havendo vendas cadastradas com ela".Alert();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            groupBox5.Show();
        }

        private void btnSe_Click(object sender, EventArgs e)
        {
            groupBox3.Show();
        }

        private void btnMa_Click(object sender, EventArgs e)
        {
            groupBox2.Show();
        }

        private void btnAt_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCliente.Text) && (!string.IsNullOrEmpty(txtCpf.Text) || !string.IsNullOrEmpty(txtCnpj.Text)) && !string.IsNullOrEmpty(txtTel.Text))
            {
                Cliente c = new Cliente()
                {
                    Nome = txtCliente.Text,
                    CPF = string.IsNullOrEmpty(txtCpf.Text) ? null : txtCpf.Text,
                    CNPJ = string.IsNullOrEmpty(txtCnpj.Text) ? null : txtCnpj.Text,
                    Email = txtEmail.Text,
                    Telefone = txtTel.Text,
                    DataCadastro = DateTime.Now
                };
                db.Clientes.Add(c);
                db.SaveChanges();
                CarregarDados();
            }
            else
            {
                "Não pode haver campo vazio".Alert();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox5.Hide();
        }

        private void dtgC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var linha = dtgC.Rows[e.RowIndex];

                if (linha.Cells["acao"].Value == "❌")
                {
                    var nome = linha.Cells["nome"].Value.ToString();

                    var b = db.Clientes.Where(i => i.Nome == nome).FirstOrDefault();

                    db.Clientes.Remove(b);
                    db.SaveChanges();
                    CarregarDados();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                "Não é possivel excluir Forma de Pagamento havendo vendas cadastradas com ela".Alert();
            }
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
