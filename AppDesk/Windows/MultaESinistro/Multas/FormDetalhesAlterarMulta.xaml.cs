﻿using AppDesk.Serviço;
using AppDesk.Tools;
using AppDesk.Windows.Motoristas;
using AppDesk.Windows.Veiculos;
using Modelo.Classes.Desk;
using Modelo.Enums;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AppDesk.Windows.MultaESinistro.Multas
{
    /// <summary>
    /// Lógica interna para FormDetalhesAlterarMulta.xaml
    /// </summary>
    public partial class FormDetalhesAlterarMulta : Window
    {
        private Multa _multa = null;

        private FormDetalhesAlterarMulta()
        {
            InitializeComponent();
        }

        public FormDetalhesAlterarMulta(Multa multa) : this()
        {
            _multa = multa;
            PopularComboBox();
            EstadoPagamentoInfracaoComboBox.SelectedItem = _multa.EstadoDoPagamento.ToString("G");
            this.DataContext = _multa;
            CPFUC.Text = _multa.Motorista.CPF;
            PlacaUC.Text = _multa.Veiculo.Placa;
            DataMultaUC.Date = _multa.DataDaMulta;
            DataVencimentoUC.Date = _multa.DataVencimento;
            ValorMultaUC.Valor = _multa.Valor;

            if (!DesktopLoginControlService._Usuario.Permissoes.MultasSinistros.Alterar)
            {
                SalvarAlteracaoPagamentoBtn.IsEnabled = false;
            }
            if (!DesktopLoginControlService._Usuario.Permissoes.MultasSinistros.Remover)
            {
                RemoverMultaBtn.IsEnabled = false;
            }
        }

        private void PopularComboBox()
        {
            var lista = Enum.GetNames(typeof(EstadosDePagamento));
            for (int i = 0; i < lista.Length; i++)
            {
                lista[i] = lista[i].Replace('_', ' ');
            }
            EstadoPagamentoInfracaoComboBox.ItemsSource = lista;
        }

        private void RemoverMultaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StandardMessageBoxes.ConfirmarRemocaoMessageBox("Multa") == MessageBoxResult.Yes)
            {
                ServicoDados.ServicoDadosMulta.RemoverMultaPorId(_multa.MultaId);
                StandardMessageBoxes.MensagemSucesso("Multa removida com sucesso!", "Remoção");
                MainWindowUpdater.UpdateDataGrids();
                this.Close();
            }
        }

        private void EstadoPagamentoInfracaoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstadoPagamentoInfracaoComboBox.SelectedItem.ToString() != _multa.EstadoDoPagamento.ToString("G") && DesktopLoginControlService._Usuario.Permissoes.MultasSinistros.Alterar)
            {
                SalvarAlteracaoPagamentoBtn.IsEnabled = true;
            }
        }

        private void SalvarAlteracaoPagamentoBtn_Click(object sender, RoutedEventArgs e)
        {
            _multa.EstadoDoPagamento = (EstadosDePagamento)Enum.Parse(typeof(EstadosDePagamento), EstadoPagamentoInfracaoComboBox.SelectedItem.ToString());
            ServicoDados.ServicoDadosMulta.GravarMulta(_multa);
            MainWindowUpdater.UpdateDataGrids();
        }

        private void DetalhesDoMotoristaBtn_Click(object sender, RoutedEventArgs e)
        {
            FormDetalhesMotorista formDetalhesMotorista = new FormDetalhesMotorista(ServicoDados.ServicoDadosMotorista.ObterMotoristaPorId(_multa.MotoristaId));
            formDetalhesMotorista.Show();
        }

        private void DetalhesDoVeiculoBtn_Click(object sender, RoutedEventArgs e)
        {
            FormDetalhesVeiculo formDetalhesVeiculo = new FormDetalhesVeiculo(ServicoDados.ServicoDadosVeiculos.ObterVeiculoPorId(_multa.VeiculoId));
            formDetalhesVeiculo.Show();
        }
    }
}
