using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PCentar
{
    public partial class MainWindow : Window
    {
        #region Deklaracije

        private Svestenik s = new Svestenik();
        private SvestenikDal sDal = new SvestenikDal();
        private DomacinDal dDal = new DomacinDal();
        private int kolikoStrana = 0;
        private int DomacinId;
        private string mailovi = null;
        private List<Domacin> listaPretrage = new List<Domacin>();

        #endregion Deklaracije

        #region Moje Metode

        private void NapuniGrid()
        {
            dataGrid1.Items.Clear();

            foreach (Domacin d in listaPretrage)
            {
                dataGrid1.Items.Add(d);
            }
        }

        private void Resetovanje()
        {
            comboBoxBracnoStanje.SelectedIndex = -1;
            comboBoxParohijal.SelectedIndex = -1;
            comboBoxPrimaZaSlavu.SelectedIndex = -1;
            comboBoxPrimaZaUskrs.SelectedIndex = -1;
            comboBoxDaLiSlavi.SelectedIndex = -1;
            comboBoxOsvecujeZito.SelectedIndex = -1;

            textBoxZapazanja.Clear();
            textBoxEMail.Clear();
            textBoxTelefon1.Clear();
            textBoxTelefon2.Clear();
            textBoxDrzava.Clear();
            textBoxIme.Clear();
            textBoxPrezime.Clear();
            textBoxClanovi.Clear();
            textBoxAdresa.Clear();
            textBoxMesto.Clear();
            textBoxNazivSlave.Clear();
            textBoxUkupnoClanova.Clear();
            textBoxSumaParohijal.Clear();
            textBoxZastoNeSlavi.Clear();
        }

        private void ResetovanjeIspravka()
        {
            comboBoxBracnoStanjei.SelectedIndex = -1;
            comboBoxParohijali.SelectedIndex = -1;
            comboBoxPrimaZaSlavui.SelectedIndex = -1;
            comboBoxPrimaZaUskrsi.SelectedIndex = -1;
            comboBoxDaLiSlavii.SelectedIndex = -1;
            comboBoxOsvecujeZitoi.SelectedIndex = -1;

            textBoxZapazanjai.Clear();
            textBoxEMaili.Clear();
            textBoxTelefon1i.Clear();
            textBoxTelefon2i.Clear();
            textBoxDrzavai.Clear();
            textBoxImei.Clear();
            textBoxPrezimei.Clear();
            textBoxClanovii.Clear();
            textBoxAdresai.Clear();
            textBoxMestoi.Clear();
            textBoxNazivSlavei.Clear();
            textBoxUkupnoClanovai.Clear();
            textBoxSumaParohijali.Clear();
            textBoxZastoNeSlavii.Clear();
        }

        private void PostavkaJezika(string jezik)
        {
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo(jezik);
        }

        private void Poruka(string poruka)
        {
            MessageBox.Show(poruka, "Обавештење", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool Validacija()
        {
            if (string.IsNullOrWhiteSpace(textBoxIme.Text))
            {
                Poruka("Морате попунити поље име!");
                textBoxIme.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPrezime.Text))
            {
                Poruka("Морате попунити поље презиме!");
                textBoxPrezime.Focus();
                return false;
            }
            return true;
        }

        private bool ValidacijaIspravka()
        {
            if (string.IsNullOrWhiteSpace(textBoxImei.Text))
            {
                Poruka("Морате попунити поље име!");
                textBoxImei.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPrezimei.Text))
            {
                Poruka("Морате попунити поље презиме!");
                textBoxPrezimei.Focus();
                return false;
            }
            return true;
        }

        #endregion Moje Metode

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");

            s = sDal.VratiIDSvestenika(1);

            // Glavi prikaz
            txtSvestenik.Text = s.imePrezimeSvestenika;
            txtParohijaSvestenika.Text = s.nazivParohije;
            txtTelefonSvestenika.Text = s.telMobilni;

            // Informacije
            textBoxImePrezimeSvestenika.Text = s.imePrezimeSvestenika;
            textBoxParohija.Text = s.nazivParohije;
            textBoxFiksniTelefonSvestenika.Text = s.telFiksni;
            textBoxMobilniTelefonSvestenika.Text = s.telMobilni;
            textBoxEmailUserName.Text = s.emailUserName;
            textBoxEmailPassword.Password = s.emailPassword;
            textBoxUsername.Text = s.logUserName;
            textBoxPassword.Password = s.logPassword;

            comboBoxBracnoStanje.ItemsSource = dDal.BracnoStanje();
            comboBoxDaLiSlavi.ItemsSource = dDal.DaNe();
            comboBoxParohijal.ItemsSource = dDal.DaNe();
            comboBoxPrimaZaSlavu.ItemsSource = dDal.DaNe();
            comboBoxPrimaZaUskrs.ItemsSource = dDal.DaNe();
            comboBoxOsvecujeZito.ItemsSource = dDal.OsvecujeZito();

            // EDIT
            comboBoxBracnoStanjei.ItemsSource = dDal.BracnoStanje();
            comboBoxDaLiSlavii.ItemsSource = dDal.DaNe();
            comboBoxParohijali.ItemsSource = dDal.DaNe();
            comboBoxPrimaZaSlavui.ItemsSource = dDal.DaNe();
            comboBoxPrimaZaUskrsi.ItemsSource = dDal.DaNe();
            comboBoxOsvecujeZitoi.ItemsSource = dDal.OsvecujeZito();

            // Counti
            TextBlockUkupnoBaza.Text = dDal.UkupnoBaza();
            TextBlockUkupnoParohijana.Text = dDal.UkupnoParohijana();
            TextBlockUkupnoKojiSlave.Text = dDal.UkupnoKojiSlave();
            TextBlockUkupnoPrimanjeUskrs.Text = dDal.UkupnoPrimanjeVaskrs();

            try
            {
                Uri adresaSvestenik = new Uri(dDal.VratiPoslednjuPutanjuSlike(), UriKind.Absolute);
                BitmapImage slikaSvestenik = new BitmapImage(adresaSvestenik);
                imgSvestenik.Source = slikaSvestenik;

                Uri adresaCrkva = new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Slike/lincCrkva.jpg", UriKind.Absolute);
                BitmapImage slikaCrkva = new BitmapImage(adresaCrkva);
                imgCrkva.Source = slikaCrkva;
            }
            catch (Exception)
            {
                Uri adresa = new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Slike/temp-slika.png", UriKind.Absolute);
                BitmapImage slika = new BitmapImage(adresa);
                imgSvestenik.Source = slika;

                Uri adresaCrkva = new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Slike/lincCrkva.jpg", UriKind.Absolute);
                BitmapImage slikaCrkva = new BitmapImage(adresaCrkva);
                imgCrkva.Source = slikaCrkva;
            }
        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                Domacin d = new Domacin();
                d.datumUnosa = DateTime.Now.ToShortDateString();
                d.imeDomacina = textBoxIme.Text;
                d.prezimeDomacina = textBoxPrezime.Text;
                d.deca = textBoxClanovi.Text;
                d.adresa = textBoxAdresa.Text;
                d.mesto = textBoxMesto.Text;
                d.nazivSlave = textBoxNazivSlave.Text;

                #region If za Combo

                //Bracno stanje
                if (comboBoxBracnoStanje.SelectedIndex < 0)
                {
                    d.bracnoStanje = null;
                }
                else
                {
                    d.bracnoStanje = comboBoxBracnoStanje.SelectedValue.ToString();
                }

                //Parohijal
                if (comboBoxParohijal.SelectedIndex < 0)
                {
                    d.parohijal = null;
                }
                else
                {
                    d.parohijal = comboBoxParohijal.SelectedValue.ToString();
                }

                // Da li slavi
                if (comboBoxDaLiSlavi.SelectedIndex < 0)
                {
                    d.daLiSlavi = null;
                }
                else
                {
                    d.daLiSlavi = comboBoxDaLiSlavi.SelectedValue.ToString();
                }

                //Prima za slavu
                if (comboBoxPrimaZaSlavu.SelectedIndex < 0)
                {
                    d.primanjeZaSlavu = null;
                }
                else
                {
                    d.primanjeZaSlavu = comboBoxPrimaZaSlavu.SelectedValue.ToString();
                }

                //Prima za Uskrs
                if (comboBoxPrimaZaUskrs.SelectedIndex < 0)
                {
                    d.primanjeZaVaskrs = null;
                }
                else
                {
                    d.primanjeZaVaskrs = comboBoxPrimaZaUskrs.SelectedValue.ToString();
                }

                // Gde osvecuje zito
                if (comboBoxOsvecujeZito.SelectedIndex < 0)
                {
                    d.donosiZito = null;
                }
                else
                {
                    d.donosiZito = comboBoxOsvecujeZito.SelectedValue.ToString();
                }

                #endregion If za Combo

                int ukupnoClanova = 0;
                if (!int.TryParse(textBoxUkupnoClanova.Text, out ukupnoClanova))
                {
                    textBoxUkupnoClanova.Text = ukupnoClanova.ToString();
                }

                if (string.IsNullOrWhiteSpace(textBoxUkupnoClanova.Text))
                {
                    d.ukupnoClanova = 0;
                }
                else
                {
                    d.ukupnoClanova = Convert.ToInt32(textBoxUkupnoClanova.Text);
                }

                d.sumaParohijal = textBoxSumaParohijal.Text;
                d.zapazanja = textBoxZapazanja.Text;
                d.email = textBoxEMail.Text;
                d.telefon1 = textBoxTelefon1.Text;
                d.telefon2 = textBoxTelefon2.Text;
                d.poreklo = textBoxDrzava.Text;
                d.zastoNeSlavi = textBoxZastoNeSlavi.Text;

                if (dDal.PostojiDomacin(d))
                {
                    Poruka("Домаћин са именом и презименом кога сте уписали већ постоји у бази.");
                    return;
                }

                int rez = dDal.UbaciDomacina(d);

                if (rez == 1)
                {
                    Resetovanje();
                    Poruka("Податак је успешно убачен у базу.");
                }
                else
                {
                    Poruka("Дошло је до грешке. Податак није убачен у базу.");
                }
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedIndex < 0)
            {
                return;
            }

            Domacin d = dataGrid1.SelectedItem as Domacin;
            DomacinId = d.idDomacin;
            textBoxImei.Text = d.imeDomacina;
            textBoxPrezimei.Text = d.prezimeDomacina;
            textBoxClanovii.Text = d.deca;
            textBoxAdresai.Text = d.adresa;
            textBoxMestoi.Text = d.mesto;
            textBoxNazivSlavei.Text = d.nazivSlave;
            textBoxUkupnoClanovai.Text = d.ukupnoClanova.ToString();
            comboBoxBracnoStanjei.SelectedValue = d.bracnoStanje;
            comboBoxParohijali.SelectedValue = d.parohijal;
            textBoxSumaParohijali.Text = d.sumaParohijal;
            comboBoxPrimaZaSlavui.SelectedValue = d.primanjeZaSlavu;
            comboBoxPrimaZaUskrsi.SelectedValue = d.primanjeZaVaskrs;
            comboBoxDaLiSlavii.SelectedValue = d.daLiSlavi;
            textBoxZastoNeSlavii.Text = d.zastoNeSlavi;
            comboBoxOsvecujeZitoi.SelectedValue = d.donosiZito;
            textBoxZapazanjai.Text = d.zapazanja;
            textBoxEMaili.Text = d.email;
            textBoxTelefon1i.Text = d.telefon1;
            textBoxTelefon2i.Text = d.telefon2;
            textBoxDrzavai.Text = d.poreklo;
        }

        private void comboBoxOpcijePretrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = comboBoxOpcijePretrage.SelectedIndex;

            switch (index)
            {
                case 0:
                    listaPretrage = dDal.VratiDomacine();
                    NapuniGrid();
                    textBoxPretraga.IsEnabled = false;
                    break;

                case 1:
                    listaPretrage = dDal.VratiKojiSlave();
                    NapuniGrid();
                    textBoxPretraga.IsEnabled = false;
                    break;

                case 2:
                    listaPretrage = dDal.VratiKojiDajuParohijal();
                    NapuniGrid();
                    textBoxPretraga.IsEnabled = false;
                    break;

                case 3:
                    listaPretrage = dDal.VratiKojiPrimajuUskrs();
                    NapuniGrid();
                    textBoxPretraga.IsEnabled = false;
                    break;

                default:
                    textBoxPretraga.IsEnabled = true;
                    break;
            }
        }

        private void textBoxPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPretraga.Text))
            {
                dataGrid1.Items.Clear();
                return;
            }

            int index = comboBoxOpcijePretrage.SelectedIndex;
            switch (index)
            {
                case 4:
                    listaPretrage = dDal.Pretraga("nazivSlave", textBoxPretraga.Text);
                    NapuniGrid();
                    break;

                case 5:
                    listaPretrage = dDal.Pretraga("prezimeDomacina", textBoxPretraga.Text);
                    NapuniGrid();
                    break;

                case 6:
                    listaPretrage = dDal.Pretraga("mesto", textBoxPretraga.Text);
                    NapuniGrid();
                    break;
            }
        }

        private void buttonIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBoxCestitkePretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCestitkePretraga.Text))
            {
                listBoxCestitke.ItemsSource = null;
                listBoxCestitke.Items.Clear();
                buttonPosaljiCestitke.IsEnabled = false;
                buttonCopyClipBoard.IsEnabled = false;
                mailovi = null;
                return;
            }

            listBoxCestitke.ItemsSource = dDal.Mailovi(textBoxCestitkePretraga.Text, ref mailovi);

            if (mailovi != null)
            {
                buttonPosaljiCestitke.IsEnabled = true;
                buttonCopyClipBoard.IsEnabled = true;
            }
            else
            {
                buttonCopyClipBoard.IsEnabled = false;
                buttonPosaljiCestitke.IsEnabled = false;
            }
        }

        private void buttonPosaljiCestitke_Click(object sender, RoutedEventArgs e)
        {
            MailMessage msg = new MailMessage(s.emailUserName, mailovi, txtNaslov.Text, textBoxPoruka.Text);
            SmtpClient client = new SmtpClient("smtp.liwest.at");
            client.Port = 25;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential(s.emailUserName, s.emailPassword);
            try
            {
                buttonPosaljiCestitke.IsEnabled = false;
                Cursor = Cursors.Wait;
                client.Send(msg);
                Cursor = Cursors.Arrow;
                buttonPosaljiCestitke.IsEnabled = true;
                Poruka("Честитке су успешно послате!");
            }
            catch (Exception ex)
            {
                Poruka(ex.Message);
            }
            finally
            {
                buttonPosaljiCestitke.IsEnabled = true;
                Cursor = Cursors.Arrow;
                client.Dispose();
            }
        }

        private void tbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbCestitke.IsSelected)
            {
                textBoxCestitkePretraga.Focus();
            }
            if (tbPretragaStampa.IsSelected)
            {
            }
            if (tbNovo.IsSelected)
            {
                textBoxIme.Focus();
            }
        }

        private void buttonCopyClipBoard_Click(object sender, RoutedEventArgs e)
        {
            if (mailovi != null)
            {
                Clipboard.SetText(mailovi);
                MessageBox.Show("Адресе су успешно копиране у Вашу меморију!", "Mail",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void buttonStampa_Click(object sender, RoutedEventArgs e)
        {
            if (listaPretrage.Count <= 0)
            {
                Poruka("Листа за штампање је празна.");
                return;
            }

            var ppd = new System.Windows.Forms.PrintPreviewDialog();
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);
            pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);
            ppd.Document = pd;
            ppd.Document.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 1169);

            try
            {
                ppd.ShowDialog();
                //pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            #region Osnovni izgled

            Svestenik s = sDal.VratiIDSvestenika(1);
            Brush crnaBoja = Brushes.Black;
            Font myFont = new Font("Tahoma", 11, System.Drawing.FontStyle.Regular);
            Font naslovFont = new Font("Tahoma", 15, System.Drawing.FontStyle.Bold);

            System.Drawing.Image img = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"Slike\grbPatrijarsije-Print.png");

            // Slika grba
            e.Graphics.DrawImage(img, 25, 25, img.Width, img.Height);
            e.Graphics.DrawImage(img, 710, 25, img.Width, img.Height);

            // Naslov Stranice
            e.Graphics.DrawString("Парохијски центар", new Font("Tahoma", 30, System.Drawing.FontStyle.Regular), crnaBoja, new System.Drawing.Point(225, 40));

            //Info osnovni
            e.Graphics.DrawString("Свештеник: " + s.imePrezimeSvestenika, myFont, crnaBoja, new System.Drawing.Point(25, 150));
            e.Graphics.DrawString("Парохија: " + s.nazivParohije, myFont, crnaBoja, new System.Drawing.Point(25, 175));
            e.Graphics.DrawString("Телефон: " + s.telMobilni, myFont, crnaBoja, new System.Drawing.Point(25, 200));

            //Linija
            e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new System.Drawing.Point(25, 230), new System.Drawing.Point(810, 230));

            #endregion Osnovni izgled

            //Koja je Lista
            e.Graphics.DrawString("- " + textBoxPretraga.Text + " -", naslovFont, crnaBoja, new System.Drawing.Point(20, 250));
            int topMargina = 330;
            int levaStrana = e.MarginBounds.Right;

            if (listaPretrage.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < 28; i++)
            {
                Domacin d1 = new Domacin();
                d1 = listaPretrage[kolikoStrana];
                string red = string.Format("{0}. {1} {2}, {3}, {4}, tel1:{5}, tel2:{6}",
                kolikoStrana + 1, d1.imeDomacina, d1.prezimeDomacina, d1.adresa, d1.mesto, d1.telefon1, d1.telefon2);
                System.Drawing.Point start = new System.Drawing.Point(20, topMargina);
                e.Graphics.DrawString(red, myFont, crnaBoja, start);
                topMargina += 28;
                kolikoStrana++;

                // Treba da se odradi da prelazi u drugi red.
                //if (red.Length >= levaStrana)
                //{
                //}

                if (kolikoStrana >= listaPretrage.Count)
                {
                    kolikoStrana = 0;
                    return;
                }
            }

            e.HasMorePages = kolikoStrana < listaPretrage.Count;
        }

        #region Promena jezika u TextBoxovima

        private void textBoxEMail_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxEMail_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        private void textBoxAdresa_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxAdresa_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        private void textBoxMesto_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxMesto_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        private void textBoxAdresai_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxAdresai_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        private void textBoxMestoi_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxMestoi_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        private void textBoxEMaili_GotFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("en-us");
        }

        private void textBoxEMaili_LostFocus(object sender, RoutedEventArgs e)
        {
            PostavkaJezika("sr-Cyrl-RS");
        }

        #endregion Promena jezika u TextBoxovima

        private void buttonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (DomacinId <= 0)
            {
                return;
            }

            if (ValidacijaIspravka())
            {
                Domacin d = new Domacin();
                d.idDomacin = DomacinId;
                d.imeDomacina = textBoxImei.Text;
                d.prezimeDomacina = textBoxPrezimei.Text;
                d.deca = textBoxClanovii.Text;
                d.adresa = textBoxAdresai.Text;
                d.mesto = textBoxMestoi.Text;
                d.nazivSlave = textBoxNazivSlavei.Text;

                #region If za Combo

                //Bracno stanje
                if (comboBoxBracnoStanjei.SelectedIndex < 0)
                {
                    d.bracnoStanje = null;
                }
                else
                {
                    d.bracnoStanje = comboBoxBracnoStanjei.SelectedValue.ToString();
                }

                //Parohijal
                if (comboBoxParohijali.SelectedIndex < 0)
                {
                    d.parohijal = null;
                }
                else
                {
                    d.parohijal = comboBoxParohijali.SelectedValue.ToString();
                }

                // Da li slavi
                if (comboBoxDaLiSlavii.SelectedIndex < 0)
                {
                    d.daLiSlavi = null;
                }
                else
                {
                    d.daLiSlavi = comboBoxDaLiSlavii.SelectedValue.ToString();
                }

                //Prima za slavu
                if (comboBoxPrimaZaSlavui.SelectedIndex < 0)
                {
                    d.primanjeZaSlavu = null;
                }
                else
                {
                    d.primanjeZaSlavu = comboBoxPrimaZaSlavui.SelectedValue.ToString();
                }

                //Prima za Uskrs
                if (comboBoxPrimaZaUskrsi.SelectedIndex < 0)
                {
                    d.primanjeZaVaskrs = null;
                }
                else
                {
                    d.primanjeZaVaskrs = comboBoxPrimaZaUskrsi.SelectedValue.ToString();
                }

                // Gde osvecuje zito
                if (comboBoxOsvecujeZitoi.SelectedIndex < 0)
                {
                    d.donosiZito = null;
                }
                else
                {
                    d.donosiZito = comboBoxOsvecujeZitoi.SelectedValue.ToString();
                }

                #endregion If za Combo

                int ukupnoClanova = 0;
                if (!int.TryParse(textBoxUkupnoClanovai.Text, out ukupnoClanova))
                {
                    textBoxUkupnoClanovai.Text = ukupnoClanova.ToString();
                }

                if (string.IsNullOrWhiteSpace(textBoxUkupnoClanovai.Text))
                {
                    d.ukupnoClanova = 0;
                }
                else
                {
                    d.ukupnoClanova = Convert.ToInt32(textBoxUkupnoClanovai.Text);
                }

                d.sumaParohijal = textBoxSumaParohijali.Text;
                d.zapazanja = textBoxZapazanjai.Text;
                d.email = textBoxEMaili.Text;
                d.telefon1 = textBoxTelefon1i.Text;
                d.telefon2 = textBoxTelefon2i.Text;
                d.poreklo = textBoxDrzavai.Text;
                d.zastoNeSlavi = textBoxZastoNeSlavii.Text;

                int rez = dDal.IspraviDomacina(d);

                if (rez == 1)
                {
                    Poruka("Податак је успешно промењен.");
                }
                else
                {
                    Poruka("Дошло је до грешке. Податак није промењен.");
                }
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxImei.Text))
            {
                return;
            }

            Domacin d = new Domacin();
            d.idDomacin = DomacinId;

            MessageBoxResult rezultatBoxa = MessageBox.Show("Да ли сте сигурни да желите да обришете податак?",
               "Брисање", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultatBoxa == MessageBoxResult.Yes)
            {
                int rez = dDal.ObrisiDomacina(d);

                if (rez == 1)
                {
                    ResetovanjeIspravka();
                    Poruka("Податак је обрисан.");
                }
                else
                {
                    Poruka("Дошло је до грешке");
                }
            }
        }

        private void buttonObrisiCeluBazu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rezultatBoxa = MessageBox.Show("Да ли сте сигурни да желите да обришете целу базу података?!",
               "Брисање", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rezultatBoxa == MessageBoxResult.Yes)
            {
                int rez = dDal.ObrisiBazu();
                if (rez == 1)
                {
                    Poruka("База је обрисана. Можете почети са новим уносима.");
                }
                else
                {
                    Poruka("Дошло је до грешке.");
                }
            }
        }

        private void buttonSacuvajSvestenika_Click(object sender, RoutedEventArgs e)
        {
            s.imePrezimeSvestenika = textBoxImePrezimeSvestenika.Text;
            s.nazivParohije = textBoxParohija.Text;
            s.telFiksni = textBoxFiksniTelefonSvestenika.Text;
            s.telMobilni = textBoxMobilniTelefonSvestenika.Text;
            s.emailUserName = textBoxEmailUserName.Text;
            s.emailPassword = textBoxEmailPassword.Password;
            s.logUserName = textBoxUsername.Text;
            s.logPassword = textBoxPassword.Password;

            int rez = sDal.SacuvajSvestenika(s);

            if (rez == 1)
            {
                Poruka("Подаци су сачувани.");
            }
            else
            {
                Poruka("Дошло је до грешке.");
            }
        }

        private void btnDialogSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (ofd.ShowDialog() == true)
            {
                Slika s = new Slika();
                s.Path = ofd.FileName;
                int rez = dDal.SacuvajPutanjuSlike(s);
                if (rez == -1)
                {
                    Poruka("Дошло је до грешке.");
                    return;
                }

                try
                {
                    Uri adresa = new Uri(ofd.FileName, UriKind.Absolute);
                    BitmapImage slika = new BitmapImage(adresa);
                    imgSvestenik.Source = slika;
                }
                catch (Exception ex)
                {
                    Poruka(ex.Message);
                }
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            tbMain.SelectedIndex = 2;
            e.Handled = true;
        }
    }
}