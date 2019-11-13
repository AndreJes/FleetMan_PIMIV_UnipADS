﻿using AppDesk.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Validacao;

namespace AppDesk.UserControls.Campos.Masked
{
    /// <summary>
    /// Interação lógica para CNPJFieldUC.xam
    /// </summary>
    public partial class CNPJFieldUC : UserControl
    {
        private bool validado = false;

        private string _text;

        public string Text
        {
            get
            {
                if (validado)
                {
                    return _text;
                }
                else
                {
                    throw new FieldException("CNPJ");
                }
            }
            set
            {
                CNPJTextBox.Text = value;
                validado = true;
            }
        }

        public CNPJFieldUC()
        {
            InitializeComponent();
        }

        async void Validar()
        {
            validado = await Validador.ValidarCPFCNPJAsync(CNPJTextBox.Text.Replace(".", "").Replace("-", "").Replace("/", ""));
            if (validado)
            {
                _text = CNPJTextBox.Text.Replace(".", "").Replace("-", "").Replace("/", "");
                CNPJTextBox.BorderBrush = HexaColorPicker.TextBoxValidoColor;
            }
            else
            {
                CNPJTextBox.BorderBrush = HexaColorPicker.TextBoxInvalidoColor;
            }
        }

        private void CNPJTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validar();
        }
    }
}