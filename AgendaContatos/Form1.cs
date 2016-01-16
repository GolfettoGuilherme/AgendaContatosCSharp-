using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaContatos
{
    public partial class frmAgenda : Form
    {
        //variavel para definir o comportamento do botão de ação
        private OperacaoEnum acao;

        public frmAgenda()
        {
            InitializeComponent();
        }

        //AÇÕES DE EXECUÇÃO INDEPENDENTE
        private void frmAgenda_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            CarregarListaContatos();
            AlterarCampos(false);
        }



        //BOTÕES
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesIncluirAlterarExcluir(false);
            AlterarCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int indiceExcluido = lbxContatos.SelectedIndex;
            if (indiceExcluido == 0)
            {
                if (MessageBox.Show("Tem certeza?", "Pergunta!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //limpa toda a tela
                    lbxContatos.Items.Clear();
                    List<Contato> contatosList = new List<Contato>();
                    ManipuladorDeArquivos.EscreverArquivo(contatosList);
                    CarregarListaContatos();
                    LimparCampos();
                }

            }
            else if (indiceExcluido > 0)
            {

                if (MessageBox.Show("Tem certeza?", "Pergunta!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lbxContatos.SelectedIndex = 0;
                    lbxContatos.Items.RemoveAt(indiceExcluido);
                    List<Contato> contatosList = new List<Contato>();
                    foreach (Contato c in lbxContatos.Items)
                    {
                        contatosList.Add(c);
                    }
                    ManipuladorDeArquivos.EscreverArquivo(contatosList);
                    CarregarListaContatos();
                    LimparCampos();
                }

            }
            else
            {
                MessageBox.Show("Não há lista para deletar", "Erro");
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato
            {
                Nome = txbNome.Text,
                Email = txbEmail.Text,
                NumeroTelefone = txbNumero.Text
            };
            List<Contato> contatosList = new List<Contato>();
            foreach (Contato contatoDaLista in lbxContatos.Items)
            {
                contatosList.Add(contatoDaLista);
            }

            if (acao == OperacaoEnum.INCLUIR)
            {
                contatosList.Add(contato);
            }
            else
            {
                //remover o contato selecionado
                int index = lbxContatos.SelectedIndex;
                contatosList.RemoveAt(index);
                //insere com o index correto
                contatosList.Insert(index, contato);
            }
            ManipuladorDeArquivos.EscreverArquivo(contatosList);
            CarregarListaContatos();
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            LimparCampos();
            AlterarCampos(false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesIncluirAlterarExcluir(true);
            AlterarCampos(false);
        }



        //METODOS PRÓPRIOS AXILIARES
        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorDeArquivos.LerArquivo().ToArray());
        }

        private void LimparCampos()
        {
            txbNome.Text = "";
            txbEmail.Text = "";
            txbNumero.Text = "";
        }

        private void AlterarCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbNumero.Enabled = estado;
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            //quando eu seleciono um elemento da lista
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesIncluirAlterarExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contatoSelecionado = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contatoSelecionado.Nome;
            txbEmail.Text = contatoSelecionado.Email;
            txbNumero.Text = contatoSelecionado.NumeroTelefone;
        }
    }
}
