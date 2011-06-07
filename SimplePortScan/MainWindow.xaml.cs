using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimplePortScan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ISimplePortScanController _controller = new SimplePortScanController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Figure out why this is not rendered
            //textBoxResults.Text = string.Format("Testing ports {0} to {1} on the target '{2}', please wait...",
            //    textBoxStartingPort.Text, textBoxEndingPort.Text, textBoxTarget.Text);
            
            IList<ScannedPort> PortResults = null;
            try
            {
                PortResults = _controller.ScanPorts(textBoxTarget.Text, textBoxStartingPort.Text, textBoxEndingPort.Text).ToList();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            textBoxResults.Text = string.Empty;
            for (int i = 0; i < PortResults.Count(); i++)
            {
                textBoxResults.Text += String.Format("Port {0}: {1}{2}",
                    PortResults[i].Port, (PortResults[i].IsOpen ? "open" : "closed"), Environment.NewLine);
            }
        }

        private void ValidateControls()
        {
            buttonStart.IsEnabled = _controller.InputValuesAcceptable(textBoxTarget.Text, 
                textBoxStartingPort.Text, textBoxEndingPort.Text);
        }

        private void textBoxStartingPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateControls();
        }

        private void textBoxEndingPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateControls();
        }

        private void textBoxTarget_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateControls();
        }
    }
}
