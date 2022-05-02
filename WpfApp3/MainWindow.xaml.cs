using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();

        //indirizzo percorso rest api
        Uri indirizzoLogin = new Uri("http://10.1.0.18:3000/login");
        Uri indirizzoMenu = new Uri("http://10.1.0.18:3000/bar/1/menu?token=");
       

        public MainWindow()
        {
            InitializeComponent();
            lblMenu.IsEnabled = false;
            gridElenco.IsEnabled = false;          
        }

        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Utente a = new Utente();
            a.username = username;
            a.password = password;
            string utenteJSON = JsonConvert.SerializeObject(a);
            StringContent payload = new StringContent(utenteJSON, Encoding.UTF8, "application/json");
            var risultato = client.PostAsync(indirizzoLogin, payload).Result.Content.ReadAsStringAsync().Result;
            if (risultato.ToString() != "{\"message\":\"authentication failed\"}")
            {
                
                lblMenu.IsEnabled = true;
                gridElenco.IsEnabled = true;
                MostraMenu(risultato);
            }
            else
            {
                MessageBox.Show("Utente non registrato");
            }
            
        }

        private void MostraMenu(string risultato)
        {
            Token token = JsonConvert.DeserializeObject<Token>(risultato);
            var risposta = client.GetStringAsync(new Uri(indirizzoMenu.AbsoluteUri + token.token)).Result;
            List<Menu> elenco = JsonConvert.DeserializeObject<List<Menu>>(risposta);
            gridElenco.ItemsSource = elenco;
        }

      
    }


    
}



/*        private void btnPrenotazioni_Click(object sender, RoutedEventArgs e)
        {
            var risposta2 = client.GetStringAsync(indirizzo2).Result;
            List<Prenotazioni> elenco2 = JsonConvert.DeserializeObject<List<Prenotazioni>>(risposta2);

            gridElenco.ItemsSource = elenco2;
        }

        private void btnPrenotati_Click(object sender, RoutedEventArgs e)
        {
            var risposta = client.GetStringAsync(indirizzo).Result;
            List<Prenotati> elenco = JsonConvert.DeserializeObject<List<Prenotati>>(risposta);

            gridElenco.ItemsSource = elenco;
        }

        private void btnSpettacoli_Click(object sender, RoutedEventArgs e)
        {
            var risposta3 = client.GetStringAsync(indirizzo3).Result;
            List<Spettacoli> elenco3 = JsonConvert.DeserializeObject<List<Spettacoli>>(risposta3);

            gridElenco.ItemsSource = elenco3;
        }

        private void btnZone_Click(object sender, RoutedEventArgs e)
        {
            var risposta4 = client.GetStringAsync(indirizzo4).Result;
            List<Zone> elenco4 = JsonConvert.DeserializeObject<List<Zone>>(risposta4);

            gridElenco.ItemsSource = elenco4;
        }

        private void btnTipologie_Click(object sender, RoutedEventArgs e)
        {
            var risposta5 = client.GetStringAsync(indirizzo5).Result;
            List<Zone> elenco5 = JsonConvert.DeserializeObject<List<Zone>>(risposta5);

            gridElenco.ItemsSource = elenco5;
        }
    }
    
}

var risposta = client.GetStringAsync(indirizzo).Result;
List<Prenotati> elenco = JsonConvert.DeserializeObject<List<Prenotati>>(risposta);

gridElenco.ItemsSource = elenco;
*/


public class Token
{
    public String token {get;set;}
}