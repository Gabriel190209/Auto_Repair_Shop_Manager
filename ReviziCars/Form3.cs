using Guna.UI2.WinForms;
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
using System.Xml.Linq;

namespace ReviziCars
{
    public partial class Form3 : Form
    {
        dbReviziCarEntities db = new dbReviziCarEntities();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.BackColor = Colors.Preto;
            dtg.GridColor = Colors.AzulCla;
            dtg.BackgroundColor = Colors.AzulEsc;
            header.BackColor = Colors.AzulEsc;
            label1.BackColor = Colors.AzulEsc;
            label1.BackColor = Colors.Preto;
            label2.BackColor = Colors.AzulEsc;

            Guna2Button[] btns = { guna2Button1, guna2Button4 };

            foreach (var i in btns)
            {
                i.BackColor = Colors.AzulEsc;
                i.FillColor = Colors.AzulCla;
            }

            guna2Button3.BackColor = Colors.AzulEsc;
            guna2Button3.FillColor = Colors.Verde;

            guna2Button2.BackColor = Colors.AzulEsc;

            lblOla.Text = $"Olá, {Settings.Default.Token}";

            CarregarDados();
            groupBox1.Hide();
            groupBox2.BackColor = Colors.Preto;
            groupBox2.Hide();
            groupBox3.BackColor = Colors.Preto;
            groupBox3.Hide();

            var a = db.StatusServicoes.ToList();

            foreach (var i in a)
            {
                cmbStatus.Items.Add(i.Nome);
            }

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.SelectedIndex = 0;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Settings.Default.Token = "";
            Settings.Default.IsLogged = false;
            Settings.Default.Save();

            new Form1().Show();
            this.Hide();
        }

        private void CarregarDados()
        {
            if (string.IsNullOrEmpty(txtPesq.Text))
            {
                dtg.Columns.Clear();
                dtg.Rows.Clear();

                dtg.Columns.Add("ID", "ID");
                dtg.Columns.Add("data", "Data");
                dtg.Columns.Add("nomeAtendente", "Atendente");
                dtg.Columns.Add("nomeCliente", "Cliente");
                dtg.Columns.Add("nomeMarca", "Marca");
                dtg.Columns.Add("nomeModelo", "Modelo");
                dtg.Columns.Add("placa", "Placa");
                dtg.Columns.Add("valorTotal", "Valor Total");
                dtg.Columns.Add("serico", "Serviço");
                dtg.Columns.Add("primeiroPagamento", "Primeiro Pagamento");
                dtg.Columns.Add("valorP", "Valor");
                dtg.Columns.Add("recebidoP", "Valor Recebido PGTO 1");
                dtg.Columns.Add("segundoPagamento", "Segundo Pagamento");
                dtg.Columns.Add("valorS", "Valor");
                dtg.Columns.Add("recebidoS", "Valor Recebido PGTO 2");
                dtg.Columns.Add("totalRecebido", "Valor Total Recebido");
                dtg.Columns.Add("obs", "Observação");
                dtg.Columns.Add("status", "Status");

                var b = db.Vendas.Select(i => new
                {
                    ID = i.ID,
                    NomeAtendente = i.Funcionario.NomeCompleto,
                    NomeCliente = i.Cliente.Nome,
                    NomeMarca = i.Marca.Nome,
                    NomeModelo = i.Modelo.Nome,
                    NomeServico = i.Servico.Nome,
                    Placa = i.Placa,
                    ValorTotal = i.ValorTotal,
                    Obs = i.Observacao,
                    PrimeiroP = i.FormaPagamento.Nome,
                    ValorP = i.ValorPrimeiroPagamento
                ,
                    SegundoP = i.FormaPagamento1.Nome,
                    ValorS = i.ValorSegundoPagamento,
                    Status = i.StatusServico.Nome,
                    Date = i.Data
                })
                    .ToList();

                foreach (var i in b)
                {
                    var a = db.FormaPagamentoes.First(j => j.Nome == i.PrimeiroP);
                    var c = db.FormaPagamentoes.First(j => j.Nome == i.SegundoP);

                    var v1 = (i.ValorP * (a.TaxaDescontoPercent / 100)) - i.ValorP;
                    var v2 = (i.ValorS * (c.TaxaDescontoPercent / 100)) - i.ValorS;

                    var soma = v1 + v2;

                    dtg.Rows.Add(i.ID, i.Date.ToString("dd/MM/yyyy"), i.NomeAtendente, i.NomeCliente, i.NomeMarca,
                        i.NomeModelo, i.Placa, i.ValorTotal, i.NomeServico, i.PrimeiroP, Math.Round(Convert.ToDouble(i.ValorP), 2), 
                        Math.Round(Convert.ToDouble(Math.Abs(v1 ?? 0)), 2), i.SegundoP, Math.Round(Convert.ToDouble(i.ValorS), 2), 
                        Math.Round(Convert.ToDouble(Math.Abs(v2)), 2), Math.Round(Convert.ToDouble(Math.Abs(soma ?? 0)), 2), i.Obs, i.Status);
                }
            } else
            {
                dtg.Columns.Clear();
                dtg.Rows.Clear();

                dtg.Columns.Add("ID", "ID");
                dtg.Columns.Add("data", "Data");
                dtg.Columns.Add("nomeAtendente", "Atendente");
                dtg.Columns.Add("nomeCliente", "Cliente");
                dtg.Columns.Add("nomeMarca", "Marca");
                dtg.Columns.Add("nomeModelo", "Modelo");
                dtg.Columns.Add("placa", "Placa");
                dtg.Columns.Add("valorTotal", "Valor Total");
                dtg.Columns.Add("serico", "Serviço");
                dtg.Columns.Add("primeiroPagamento", "Primeiro Pagamento");
                dtg.Columns.Add("valorP", "Valor");
                dtg.Columns.Add("recebidoP", "Valor Recebido PGTO 1");
                dtg.Columns.Add("segundoPagamento", "Segundo Pagamento");
                dtg.Columns.Add("valorS", "Valor");
                dtg.Columns.Add("recebidoS", "Valor Recebido PGTO 2");
                dtg.Columns.Add("totalRecebido", "Valor Total Recebido");
                dtg.Columns.Add("obs", "Observação");
                dtg.Columns.Add("status", "Status");
                

                var b = db.Vendas.Select(i => new
                {
                    ID = i.ID,
                    NomeAtendente = i.Funcionario.NomeCompleto,
                    NomeCliente = i.Cliente.Nome,
                    NomeMarca = i.Marca.Nome,
                    NomeModelo = i.Modelo.Nome,
                    NomeServico = i.Servico.Nome,
                    Placa = i.Placa,
                    ValorTotal = i.ValorTotal,
                    Obs = i.Observacao,
                    PrimeiroP = i.FormaPagamento.Nome,
                    ValorP = i.ValorPrimeiroPagamento
                ,
                    SegundoP = i.FormaPagamento1.Nome,
                    ValorS = i.ValorSegundoPagamento,
                    Status = i.StatusServico.Nome,
                    Date = i.Data
                })
                    .Where(i => i.NomeCliente.StartsWith(txtPesq.Text)).ToList();

                foreach (var i in b)
                {
                    var a = db.FormaPagamentoes.First(j => j.Nome == i.PrimeiroP);
                    var c = db.FormaPagamentoes.First(j => j.Nome == i.SegundoP);

                    var v1 = (i.ValorP * (a.TaxaDescontoPercent / 100)) - i.ValorP;
                    var v2 = (i.ValorS * (c.TaxaDescontoPercent / 100)) - i.ValorS;

                    var soma = v1 + v2;

                    dtg.Rows.Add(i.ID, i.Date.ToString("dd/MM/yyyy"), i.NomeAtendente, i.NomeCliente, i.NomeMarca,
                        i.NomeModelo, i.Placa, i.ValorTotal, i.NomeServico, i.PrimeiroP, Math.Round(Convert.ToDouble(i.ValorP), 2),
                        Math.Round(Convert.ToDouble(Math.Abs(v1 ?? 0)), 2), i.SegundoP, Math.Round(Convert.ToDouble(i.ValorS), 2),
                        Math.Round(Convert.ToDouble(Math.Abs(v2)), 2), Math.Round(Convert.ToDouble(Math.Abs(soma ?? 0)), 2), i.Obs, i.Status);
                }
            }
            
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            new Form5().Show();
            this.Hide();
        }

        private void txtPesq_TextChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var b = db.Vendas.Where(i => i.ID == numericUpDown1.Value).FirstOrDefault();

            if (b == null)
            {
                "Não encontrado".Alert();
                return;
            }

            db.Vendas.Remove(b);
            db.SaveChanges();
            "Excluido com sucesso".Alert();
            groupBox1.Hide();
            CarregarDados();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            CarregarDados();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Add();
            var ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets[1];

            for (int i = 0; i < dtg.Columns.Count; i++)
                ws.Cells[1, i + 1] = dtg.Columns[i].HeaderText;

            for (int row = 0; row < dtg.Rows.Count; row++)
                for (int col = 0; col < dtg.Columns.Count; col++)
                    ws.Cells[row + 2, col + 1] = dtg.Rows[row].Cells[col].Value?.ToString();

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel|*.xlsx";
                sfd.FileName = "Vendas_" + DateTime.Now.ToString("dd-MM-yyyy");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    wb.SaveAs(sfd.FileName);
                    MessageBox.Show("Exportado com sucesso!", "Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            wb.Close();
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void dtg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var linha = dtg.Rows[e.RowIndex];
            var coluna = dtg.Columns[e.ColumnIndex];

            if (dtg.Columns[e.ColumnIndex].Name == "status")
            {
                int id = Convert.ToInt32(linha.Cells["ID"].Value);

                Settings.Default.IdStatus = id.ToString();
                groupBox3.Show();
            }

            if (dtg.Columns[e.ColumnIndex].Name == "nomeCliente")
            {
                string nome = linha.Cells["nomeCliente"].Value.ToString();

                var b = db.Clientes.Select(i => new { Nome = i.Nome, CPF = i.CPF, CNPJ = i.CNPJ, Tel = i.Telefone }).Where(i => i.Nome == nome).FirstOrDefault();

                label8.Text = $"Nome: {b.Nome}";
                label5.Text = $"CPF: {b.CPF}";
                label6.Text = $"CNPJ: {b.CNPJ}";
                label7.Text = $"Telefone: {b.Tel}";
                groupBox2.Show();
            }

            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            groupBox3.Hide();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(Settings.Default.IdStatus);

                var b = db.Vendas.Where(i => i.ID == id).FirstOrDefault();

                var a = db.StatusServicoes.First(i => i.Nome == cmbStatus.Text);

                b.StatusServico = a;
                db.SaveChanges();

                "Status atualizado com sucesso".Alert();
                groupBox3.Hide();
                CarregarDados();

            } catch (Exception ex)
            {
                ex.Message.Alert();
            }
        }
    }
}
