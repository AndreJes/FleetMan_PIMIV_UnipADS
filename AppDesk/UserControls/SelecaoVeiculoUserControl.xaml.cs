﻿using AppDesk.Serviço;
using Modelo.Classes.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppDesk.UserControls
{
    /// <summary>
    /// Interação lógica para SelecaoVeiculoUserControl.xam
    /// </summary>
    public partial class SelecaoVeiculoUserControl : UserControl, INotifyPropertyChanged
    {
        private Veiculo _veiculo { get; set; }
        public Veiculo Veiculo { 
            get
            {
                return _veiculo;
            }
            set
            {
                if (value != _veiculo && value != null)
                {
                    _veiculo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public SelecaoVeiculoUserControl()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelecionarVeiculoBtn_Click(object sender, RoutedEventArgs e)
        {
            SelecionarVeiculo(VeiculosDataGrid.SelectedItem as Veiculo);
        }

        private void PesquisarPlacaVeiculo_Click(object sender, RoutedEventArgs e)
        {
            Veiculo veiculo = ServicoDados.ServicoDadosVeiculos.ObterVeiculoPorPlaca(PesquisarVeiculoTextBox.Text);
            if (veiculo != null)
            {
                MessageBoxResult result = MessageBox.Show("Veiculo encontrado. Deseja selecioná-lo agora?", "Veiculo encontrado", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SelecionarVeiculo(veiculo);
                }
            }
            else
            {
                MessageBox.Show("Veiculo não encontrado!");
            }
        }

        private void SelecionarVeiculo(Veiculo veiculo)
        {
            Veiculo = ServicoDados.ServicoDadosVeiculos.ObterVeiculoPorId(veiculo.VeiculoId);
            VeiculoSelecionadoTextBox.DataContext = Veiculo;
            MessageBox.Show("Veiculo selecionado!");
        }

    }
}