﻿using AppDesk.Serviço;
using AppDesk.Tools;
using AppDesk.Windows.Clientes;
using AppDesk.Windows.Veiculos;
using Modelo.Classes.Clientes;
using Modelo.Classes.Web;
using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppDesk.Windows.Locacoes
{
    /// <summary>
    /// Lógica interna para FormDetalhesAlterarAluguel.xaml
    /// </summary>
    public partial class FormDetalhesAlterarAluguel : Window
    {
        private Aluguel _aluguel = null;

        private FormDetalhesAlterarAluguel()
        {
            InitializeComponent();
        }

        public FormDetalhesAlterarAluguel(Aluguel aluguel) : this()
        {
            _aluguel = aluguel;
            PreencherCampos();

            if (!DesktopLoginControlService._Usuario.Permissoes.Locacoes.Alterar)
            {
                CancelarLocacaoBtn.IsEnabled = false;
                SalvarPagamentoBtn.IsEnabled = false;
            }
            if (!DesktopLoginControlService._Usuario.Permissoes.Locacoes.Remover)
            {
                RemoverBtn.IsEnabled = false;
            }
        }

        private void PreencherCampos()
        {
            switch (_aluguel.EstadoDoPagamento)
            {
                case EstadosDePagamento.PAGO:
                    PagoRadioBtn.IsChecked = true;
                    break;
                case EstadosDePagamento.VENCIDO:
                    VencidoRadioBtn.IsChecked = true;
                    break;
                case EstadosDePagamento.AGUARDANDO:
                    AguardandoPagamentoRadioBtn.IsChecked = true;
                    break;
                default:
                    break;
            }

            if (_aluguel.Cliente is ClientePF)
            {
                CPFTextBox.Text = (_aluguel.Cliente as ClientePF).CPFTxt;
                CPFTextBox.Visibility = Visibility.Visible;
            }
            else if (_aluguel.Cliente is ClientePJ)
            {
                CNPJTextBox.Text = (_aluguel.Cliente as ClientePJ).CNPJTxt;
                CNPJTextBox.Visibility = Visibility.Visible;
            }

            DataContratacaoUC.Date = _aluguel.DataContratacao;
            DataVencimentoUC.Date = _aluguel.DataVencimento;

            PlacaVeiculoTextBox.Text = _aluguel.Veiculo.Placa;

            this.DataContext = _aluguel;
        }

        private void DetalhesVeiculoBtn_Click(object sender, RoutedEventArgs e)
        {
            FormDetalhesVeiculo formDetalhesVeiculo = new FormDetalhesVeiculo(ServicoDados.ServicoDadosVeiculos.ObterVeiculoPorId(_aluguel.VeiculoId));
            formDetalhesVeiculo.Show();
        }

        private void DetalhesClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            FormAlterarClientes formDetalhesCliente = null;
            if (_aluguel.Cliente is ClientePF)
            {
                formDetalhesCliente = new FormAlterarClientes(ServicoDados.ServicoDadosClientes.ObterClientePFPorId(_aluguel.ClienteId));
            }
            else if (_aluguel.Cliente is ClientePJ)
            {
                formDetalhesCliente = new FormAlterarClientes(ServicoDados.ServicoDadosClientes.ObterClientePJPorId(_aluguel.ClienteId));
            }
            formDetalhesCliente.Show();
        }

        private void CancelarLocacaoBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Cancelar Locação?" + Environment.NewLine + "Atenção está ação não poderá ser desfeita!", "Cancelar locação", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _aluguel.EstadoDoAluguel = EstadosAluguel.CANCELADO;
                ServicoDados.ServicoDadosAluguel.GravarAluguel(_aluguel);
                MessageBox.Show("Locação cancelado com sucesso!");
            }
        }

        private void RemoverBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirmar remoção da locação?", "Confirmar remoção", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ServicoDados.ServicoDadosAluguel.RemoverAluguelPorId(_aluguel.AluguelId);
                MessageBox.Show("Locação removida com sucesso!", "Remoção bem-sucedida", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindowUpdater.UpdateDataGrids();
                this.Close();
            }
        }

        private void SalvarPagamentoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AguardandoPagamentoRadioBtn.IsChecked == true)
            {
                _aluguel.EstadoDoPagamento = EstadosDePagamento.AGUARDANDO;
            }
            else if (PagoRadioBtn.IsChecked == true)
            {
                _aluguel.EstadoDoPagamento = EstadosDePagamento.PAGO;
            }
            else if (VencidoRadioBtn.IsChecked == true)
            {
                _aluguel.EstadoDoPagamento = EstadosDePagamento.VENCIDO;
            }
            ServicoDados.ServicoDadosAluguel.GravarAluguel(_aluguel);
        }

        private void PagamentoCheckChanged_Event(object sender, RoutedEventArgs e)
        {
            if (DesktopLoginControlService._Usuario.Permissoes.Locacoes.Alterar)
            {
                if (AguardandoPagamentoRadioBtn.IsChecked == true && _aluguel.EstadoDoPagamento != EstadosDePagamento.AGUARDANDO)
                {
                    SalvarPagamentoBtn.IsEnabled = true;
                }
                else if (PagoRadioBtn.IsChecked == true && _aluguel.EstadoDoPagamento != EstadosDePagamento.PAGO)
                {
                    SalvarPagamentoBtn.IsEnabled = true;
                }
                else if (VencidoRadioBtn.IsChecked == true && _aluguel.EstadoDoPagamento != EstadosDePagamento.VENCIDO)
                {
                    SalvarPagamentoBtn.IsEnabled = true;
                }
                else
                {
                    SalvarPagamentoBtn.IsEnabled = false;
                }
            }
        }
    }
}
