using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace VanAddrKeyPairUtility
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateKeyPair_Click(object sender, RoutedEventArgs e)
        {
            string output = "";
            StreamReader reader = null;
            Process process = new Process();
            process.StartInfo.FileName = "keyconv.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "-G";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            try
            {
                process.Start();

                // Synchronously read the standard output of the spawned process. 
                reader = process.StandardOutput;
                output = reader.ReadToEnd();

                //// Write the redirected output to this application's window.
                txtGeneratedPublicKey.Text = output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                txtGeneratedPrivateKey.Text = output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[3].Split(':')[1].Trim();

                process.WaitForExit();
                process.Close();
            }
            finally
            {
                if (reader != null)
                    reader = null;
                process.Close();
            }
        }

        private void btnCombine_Click(object sender, RoutedEventArgs e)
        {
            if (txtPrivateKey.Text != string.Empty && txtPrivateKeyPart.Text != string.Empty)
            {
                string output = "";
                StreamReader reader = null;
                Process process = new Process();
                process.StartInfo.FileName = "keyconv.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = "-c " + txtPrivateKey.Text + " " + txtPrivateKeyPart.Text;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();

                    // Synchronously read the standard output of the spawned process. 
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();

                    // Write the redirected output to this application's window.
                    
                    process.WaitForExit();

                    txtResult.Text = output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[output.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length -1].Split(':')[1].Trim();
                }
                finally
                {
                    if (reader != null)
                        reader = null;
                    process.Close();
                }
            }
        }
    }
}