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
using System.Runtime;
using System.IO;

namespace WarzoneTool_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string[] lines;
        int posicionnumero;
        int nucleos = Environment.ProcessorCount / 2;
        string nucleosusados;
        char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        string userprofile;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool backup = (bool)checkbox1.IsChecked;

            if (backup = true)
            {
                System.IO.File.Copy(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini", userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini.backup");
            }

            FileInfo fileInfo = new FileInfo(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");
            fileInfo.IsReadOnly = false;
            lines[3] = lines[3].Substring(0, posicionnumero);
            lines[3] = lines[3] + trackBar1.Value.ToString();
            File.WriteAllLines(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini", lines);
            fileInfo.IsReadOnly = true;

            //Console.WriteLine(lines[3]);
            label2.Content = "Tu procesador cuenta con " + nucleos + " núcleos.\nTienes asignado " + trackBar1.Value + " núcleos para el juego.";
            MessageBox.Show("Se guardaron los cambios correctamente.");

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            userprofile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            lines = File.ReadAllLines(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");
            //Console.WriteLine(lines[3]);
            posicionnumero = lines[3].IndexOfAny(numeros);
            nucleosusados = lines[3].Substring(posicionnumero);
            trackBar1.Maximum = nucleos;
            int nucleosusadosint = int.Parse(nucleosusados.ToString());
            trackBar1.Value = nucleosusadosint;
            label1.Content = "Núcleos asignados: " + trackBar1.Value;
            label2.Content = "Tu procesador cuenta con " + nucleos + " núcleos.\nTienes asignado " + nucleosusados + " núcleos para el juego.";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");
            fileInfo.IsReadOnly = false;

            fileInfo = new FileInfo(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini.backup");
            fileInfo.IsReadOnly = false;

            System.IO.File.Delete(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");
            System.IO.File.Move(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini.backup", userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");

            fileInfo = new FileInfo(userprofile + "\\Documents\\Call of Duty Modern Warfare\\players\\adv_options.ini");
            fileInfo.IsReadOnly = true;

            MessageBox.Show("Backup restaurado exitosamente.");
        }

        private void trackBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (trackBar1.Value < 1)
            {
                MessageBox.Show("No puede asignar menos de 1 nucleo al juego.");
                trackBar1.Value = 1;
            }
            
                label1.Content = "Núcleos asignados: " + trackBar1.Value;
            
        }
    }

    
}
