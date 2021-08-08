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
using System.Reflection;
using System.Media;

namespace Assignment_5___Math_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Attributes
        MediaPlayer player = new MediaPlayer();
        #endregion

        #region Methods
        /// <summary>
        /// Contructs the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // starts playing the music
            player.Open(new Uri(System.Environment.CurrentDirectory + "..\\..\\..\\resources\\music.mp3", UriKind.Relative));

            player.Play();
            // increases the volume slightly
            player.Volume = 0.75;

        }
        /// <summary>
        /// Submits player info and begins the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                playerAgeErrorLbl.Visibility = Visibility.Hidden;
                playerNameErrorLbl.Visibility = Visibility.Hidden;

                //create new user
                clsUser player = new clsUser();
                int iPlayerAge = 0;

                if (playerAgeTxtBox.Text != "")
                {
                    iPlayerAge = Int32.Parse(playerAgeTxtBox.Text);
                }
                else if (playerAgeTxtBox.Text == "")
                {
                    playerAgeErrorLbl.Visibility = Visibility.Visible;
                }

                string sPlayerName = playerNameTxtBox.Text;

                if (iPlayerAge > 10 || iPlayerAge < 3)
                {
                    playerAgeErrorLbl.Visibility = Visibility.Visible;
                }
                else if (sPlayerName == "")
                {
                    playerNameErrorLbl.Visibility = Visibility.Visible;
                }
                else
                {
                    //set player info
                    clsUser.PlayerAge = iPlayerAge;
                    clsUser.PlayerName = sPlayerName;

                    clsGame game = new clsGame();

                    //choose which game to play
                    if ((bool)additionRadio.IsChecked)
                    {
                        clsGame.GameType = "ADDITION";
                        this.Hide();

                        //start addition game
                        GameWindow gw = new GameWindow();
                        gw.ShowDialog();

                        this.Show();
                    }
                    else if ((bool)subtractionRadio.IsChecked)
                    {
                        clsGame.GameType = "SUBTRACTION";
                        this.Hide();

                        //start subtraction game
                        GameWindow gw = new GameWindow();
                        gw.ShowDialog();

                        this.Show();
                    }
                    else if ((bool)multiplicationRadio.IsChecked)
                    {
                        clsGame.GameType = "MULTIPLICATION";
                        this.Hide();

                        //start multiplication game
                        GameWindow gw = new GameWindow();
                        gw.ShowDialog();

                        this.Show();
                    }
                    else if ((bool)divisionRadio.IsChecked)
                    {
                        clsGame.GameType = "DIVISION";
                        this.Hide();

                        //start division game
                        GameWindow gw = new GameWindow();
                        gw.ShowDialog();

                        this.Show();
                    }
                    else
                    {
                        radioErrorLbl.Content = "Must choose a game type!";
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Displays a message box with the exeption that occurred.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                //could write to a file here.
            }
        }

        /// <summary>
        /// Allows only numbers to be entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerAgeTxtBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    if (!(e.Key == Key.Back || e.Key == Key.Delete))
                    {
                        e.Handled = true;

                    }
                }
                SoundPlayer blip = new SoundPlayer("blip2.wav");
                blip.Play();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion
        /// <summary>
        /// Plays a sound when typing in the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerNameTxtBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                SoundPlayer blip = new SoundPlayer("blip2.wav");
                blip.Play();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void additionRadio_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SoundPlayer blip = new SoundPlayer("blip2.wav");
                blip.Play();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
