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
using System.Reflection;
using System.Media;

namespace Assignment_5___Math_Game
{
    /// <summary>
    /// Interaction logic for FinalScoreWindow.xaml
    /// </summary>
    public partial class FinalScoreWindow : Window
    {
        #region Methods

        /// <summary>
        /// Constructs the FinalScoreWindow. Sets background and other statistics.
        /// </summary>
        public FinalScoreWindow()
        {
            InitializeComponent();
            playerNameLbl.Content += " " + clsUser.PlayerName;
            playerAgeLbl.Content += " " + clsUser.PlayerAge;
            playerScoreLbl.Content += " " + clsUser.PlayerScore + "/10";
            playerTimeLbl.Content += " " + String.Format("{0:0.00}", clsUser.PlayerTime.ToString()) + " seconds";

            InitializeComponent();

            if (clsUser.PlayerScore < 5)
            {
                // bad score picture
                this.Background = new ImageBrush(new BitmapImage(new Uri(System.Environment.CurrentDirectory + "..\\..\\..\\bad.jpg")));

                statusLbl.Content = "Try again for a better score!";
            }
            else if (clsUser.PlayerScore > 4 && clsUser.PlayerScore < 8)
            {
                // average score picture
                this.Background = new ImageBrush(new BitmapImage(new Uri(System.Environment.CurrentDirectory + "..\\..\\..\\medium.jpg")));

                statusLbl.Content = "Not bad but I think you can do better!";
            }
            else if (clsUser.PlayerScore > 7 && clsUser.PlayerScore < 11)
            {
                // high score pciture
                this.Background = new ImageBrush(new BitmapImage(new Uri(System.Environment.CurrentDirectory + "..\\..\\..\\good2.jpg")));

                statusLbl.Content = "You are awesome!";
            }

        }
        /// <summary>
        /// Handles errors and displays a message when triggered.
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
                // could writre out to a file here
            }
        }

        /// <summary>
        /// Returns the user to the main menu when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundPlayer blip = new SoundPlayer("blip2.wav");
                blip.Play();

                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion
    }
}
