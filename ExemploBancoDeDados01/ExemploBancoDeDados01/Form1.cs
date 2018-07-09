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

namespace ExemploBancoDeDados01
{
    public partial class Form1 : Form
    {
        private string caminhoConexao = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\104968\Documents\ExemploBancoDeDados01.mdf;Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(caminhoConexao);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "INSERT INTO cores VALUES (@NOME)";

            string corDigitada = txtCor.Text;
            command.Parameters.AddWithValue("@NOME", corDigitada);
            command.ExecuteNonQuery();
            MessageBox.Show("Cadastrado");

            sqlConnection.Close();
        }

        private void btnCadastrar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCadastrar.PerformClick();
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();
            SqlCommand comando = new SqlCommand("SELECT nome FROM cores;");
            comando.Connection = conexao;

            // Cria em memória uma tabela 
            DataTable dataTable = new DataTable();
            // ExecuteReader - Executa o comando e retorna um SQLDataReader
            // Load - define para a tabela em memória as colunas
            // e registros(retornados do select)
            // para poder trabalhar com elas depois.
            dataTable.Load(comando.ExecuteReader());

            StringBuilder sb = new StringBuilder();
            // Percorre os registros da tabela em memória
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string cor = dataTable.Rows[i][0].ToString();
                sb.AppendLine(cor);
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(sb.ToString());
            conexao.Close();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(caminhoConexao);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM cores WHERE nome = @NOMEDACOR";
            string corParaApagar = cbCorApagar.SelectedItem.ToString();
            command.Parameters.AddWithValue("@NOMEDACOR", corParaApagar);
            command.ExecuteNonQuery();
            cbCorApagar.SelectedIndex = -1;
            connection.Close();
        }

        private void cbCorApagar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCorApagar.SelectedIndex != -1)
            {
                txtNovaCor.Text = cbCorApagar.SelectedItem.ToString();
            }

        }

        private void cbCorApagar_DropDown(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(caminhoConexao);
            conexao.Open();

            SqlCommand command = new SqlCommand("SELECT nome FROM cores ORDER BY nome", conexao);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            cbCorApagar.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbCorApagar.Items.Add(table.Rows[i][0].ToString());
            }


            conexao.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(caminhoConexao);
            conexao.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "UPDATE cores SET nome = @NOVONOME WHERE nome = @ANTIGONOME";


            string novoNome = txtNovaCor.Text;
            string antigoNome = cbCorApagar.SelectedItem.ToString();

            comando.Parameters.AddWithValue("@NOVONOME", novoNome);
            comando.Parameters.AddWithValue("@ANTIGONOME", antigoNome);
            comando.ExecuteNonQuery();

            cbCorApagar.SelectedIndex = -1;
            txtNovaCor.Clear();
            conexao.Close();

        }
    }
}
