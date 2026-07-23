using ReviziCars.Properties;
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
    public partial class Form4 : Form
    {
        dbReviziCarEntities db = new dbReviziCarEntities();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.BackColor = Colors.Preto;
            btnAdd.BackColor = Colors.AzulEsc;
            CarregarDados();

            ComboBox[] cmbs = { cmbCliente, cmbMarca, cmbModelo, cmbServico, cmbStatus, cmbFormaP, cmbPagamentoS};

            foreach (var i in cmbs)
            {
                i.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            NumericUpDown[] nums = { numVT, numValorP, numValorS };

            foreach(var i in nums)
            {
                i.Increment = 0.1M;
                i.DecimalPlaces = 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void CarregarDados()
        {
            cmbCliente.Items.Clear();

            var a = db.Clientes.Select(i => new
            {
                Nome = i.Nome
            }).ToList().OrderBy(i => i.Nome);

            foreach (var i in a)
            {
                cmbCliente.Items.Add(i.Nome);
            }

            cmbCliente.SelectedIndex = 0;

            cmbMarca.Items.Clear();

            var b = db.Marcas.Select(i => new {Nome = i.Nome}).ToList();

            foreach (var i in b)
            {
                cmbMarca.Items.Add(i.Nome);
            }

            cmbMarca.SelectedIndex = 0;

            cmbServico.Items.Clear();

            var c = db.Servicoes.Select(i => new { Nome = i.Nome }).ToList();

            foreach(var i in c)
            {
                cmbServico.Items.Add(i.Nome);
            }

            cmbServico.SelectedIndex = 0;

            cmbFormaP.Items.Clear();
            cmbPagamentoS.Items.Clear();

            var d = db.FormaPagamentoes.Select(i => new { Nome = i.Nome }).ToList();

            foreach ( var i in d)
            {
                cmbFormaP.Items.Add(i.Nome);
                cmbPagamentoS.Items.Add(i.Nome);
            }

            cmbStatus.Items.Clear();

            var e = db.StatusServicoes.Select(i => new { Nome = i.Nome }).ToList();

            foreach (var i in e)
            {
                cmbStatus.Items.Add(i.Nome);
            }

            cmbStatus.SelectedIndex = 0;
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbModelo.Items.Clear();

            var b = db.Modeloes.Where(i => i.Marca.Nome == cmbMarca.Text).ToList();

            foreach ( var i in b)
            {
                cmbModelo.Items.Add(i.Nome);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbModelo.Text) && !string.IsNullOrEmpty(txtPlaca.Text)
                 && !string.IsNullOrEmpty(cmbPagamentoS.Text))
                {

                    var a = db.Funcionarios.First(i => i.NomeCompleto == Settings.Default.Token);
                    var b = db.Clientes.First(i => i.Nome == cmbCliente.Text);
                    var c = db.Marcas.First(i => i.Nome == cmbMarca.Text);
                    var d = db.Modeloes.First(i => i.Nome == cmbModelo.Text);
                    var z = db.Servicoes.First(i => i.Nome == cmbServico.Text);
                    var f = db.StatusServicoes.First(i => i.Nome == cmbStatus.Text);
                    var g = db.FormaPagamentoes.FirstOrDefault(i => i.Nome == cmbFormaP.Text);
                    var h = db.FormaPagamentoes.First(i => i.Nome == cmbPagamentoS.Text);

                    var u = db.FormaPagamentoes.FirstOrDefault(j => j.Nome == cmbFormaP.Text);
                    var o = db.FormaPagamentoes.First(j => j.Nome == cmbPagamentoS.Text);

                    var v1 = (numValorP.Value * (u.TaxaDescontoPercent / 100)) + numValorP.Value;
                    var v2 = (numValorS.Value * (o.TaxaDescontoPercent / 100)) + numValorS.Value;

                    var soma = v1 + v2;

                    Venda v = new Venda()
                    {
                        IdAtendente = a.ID,
                        IdCliente = b.ID,
                        IdMarca = c.ID,
                        IdModelo = d.ID,
                        IdServico = z.ID,
                        IdStatus = f.ID,
                        Placa = txtPlaca.Text,
                        ValorTotal = numVT.Value,
                        Observacao = txtOBS.Text,
                        PrimeiroPagamento = g.ID,
                        ValorPrimeiroPagamento = numValorP.Value,
                        SegundoPagamento = h.ID,
                        ValorSegundoPagamento = numValorS.Value,
                        Data = DateTime.Now
                    };

                    db.Vendas.Add(v);
                    db.SaveChanges();
                    var falta = numVT.Value - numValorP.Value - numValorS.Value;
                    var response = MessageBox.Show($"Deseja adicionar?\n\nValor que falta pagar: {falta.ToString()}", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (response == DialogResult.OK)
                    {
                        "Adicionado com sucesso".Alert();
                        new Form3().Show();
                        this.Hide();
                    } else
                    {
                        return;
                    }
                    

                }
                else
                {
                    "Não pode haver campos vazios".Alert();
                }
            } catch (Exception ex)
            {
                $"{ex}".Alert();
                //$"{ex.Message}\nConfira se os campos estão com valores não adequados: Data\n\nSaia desta pagina e tente novamente".Alert();
            }
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
