﻿using AppDesk.Serviço;
using AppDesk.Tools;
using Modelo.Classes.Manutencao;
using System;
using System.Windows;

namespace AppDesk.Windows.Estoque
{
    /// <summary>
    /// Lógica interna para FormDetalhesAlterarPeca.xaml
    /// </summary>
    public partial class FormDetalhesAlterarPeca : Window
    {
        private Peca _peca = null;

        private FormDetalhesAlterarPeca()
        {
            InitializeComponent();
        }

        public FormDetalhesAlterarPeca(Peca peca) : this()
        {
            _peca = peca;
            this.DataContext = _peca;
            QuantidadeUC.Value = _peca.Quantidade;
            CNPJFornecedorUC.Text = _peca.Fornecedor.CNPJ;
            EmailFornecedorUC.Text = _peca.Fornecedor.Email;
            TelefoneFornecedorUC.Text = _peca.Fornecedor.Telefone;
            NomeFornecedorUC.Text = _peca.Fornecedor.Razao_Social;


            if (!DesktopLoginControlService._Usuario.Permissoes.Manutencoes.Alterar)
            {
                SalvarAlteracoesBtn.IsEnabled = false;
            }
            if (!DesktopLoginControlService._Usuario.Permissoes.Manutencoes.Remover)
            {
                RemoverBtn.IsEnabled = false;
            }
        }

        private void SalvarAlteracoesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StandardMessageBoxes.ConfirmarAlteracaoMessageBox("Peça") == MessageBoxResult.Yes)
                {
                    _peca.Quantidade = QuantidadeUC.Value;
                    _peca.Descricao = DescricaoTextBox.Text;
                    ServicoDados.ServicoDadosPeca.GravarPeca(_peca);
                    StandardMessageBoxes.MensagemSucesso("Peça alterada com sucesso!", "Alteração");
                    MainWindowUpdater.UpdateDataGrids();
                }
            }
            catch (FieldException ex)
            {
                StandardMessageBoxes.MensagemDeErroCampoFormulario(ex.Message);
            }
            catch (Exception ex)
            {
                StandardMessageBoxes.MensagemDeErro(ex.Message);
            }
        }

        private void RemoverBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StandardMessageBoxes.ConfirmarRemocaoMessageBox("Peça") == MessageBoxResult.Yes)
            {
                ServicoDados.ServicoDadosPeca.RemoverPecaPorId(_peca.PecaId);
                StandardMessageBoxes.MensagemSucesso("Peca removida com sucesso!", "Remoção");
                MainWindowUpdater.UpdateDataGrids();
                this.Close();
            }
        }
        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
