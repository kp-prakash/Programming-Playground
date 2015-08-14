using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Utils;

namespace MEFCalculatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MefComposer MefComposer
        {
            get { return MefComposer.Instance; }
        }

        public MainWindow()
        {
            InitializeComponent();
            MefComposer.ComposeParts(this);
        }

        [Import("Shared.Constants")]
        private dynamic Constants { get; set; }

        [Import("Shared.IOperations")]
        private dynamic Operations { get; set; }

        private void TxtNumberKeyDown(object sender, KeyEventArgs e)
        {
            if (!Constants.IsNumberKey(e.Key))
            {
                e.Handled = true;
                return;
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = Operations.Add(int.Parse(txtNumber1.Text), int.Parse(txtNumber2.Text)).ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }

        private void BtnSubtractClick(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = Operations.Subtract(int.Parse(txtNumber1.Text), int.Parse(txtNumber2.Text)).ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }

        private void BtnMultiplyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = Operations.Multiply(int.Parse(txtNumber1.Text), int.Parse(txtNumber2.Text)).ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }

        private void BtnDivideClick(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = Operations.Divide(int.Parse(txtNumber1.Text), int.Parse(txtNumber2.Text)).ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.GetType().ToString());
            }
        }
    }
}