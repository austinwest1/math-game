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
using System.Media;
using System.Windows.Threading;
using System.Reflection;

namespace Assignment_5___Math_Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        #region Attributes
        /// <summary>
        /// The number of succesful answers completed.
        /// </summary>
        private int iNumWins;
        /// <summary>
        /// The number of rounds completed.
        /// </summary>
        private int iNumRounds;
        /// <summary>
        /// A new instance of the clsGame class
        /// </summary>
        clsGame game = new clsGame();

        /// <summary>
        /// An instance of the DispatchTimer
        /// </summary>
        DispatcherTimer MyTimer;

        /// <summary>
        /// Gets and sets the TimerStart DateTime
        /// </summary>
        private DateTime TimerStart { get; set; }
        /// <summary>
        /// Constucts the GameWindow. Sets the game type.
        /// </summary>
        #endregion

        #region Methods
        public GameWindow()
        {
            InitializeComponent();
            gameTitleLbl.Content = clsGame.GameType + " GAME";

            SoundPlayer blip = new SoundPlayer("blip.wav");
            blip.Play();

            if (clsGame.GameType == "ADDITION")
            {
                operationLbl.Content = "+";
            }
            else if (clsGame.GameType == "SUBTRACTION")
            {
                operationLbl.Content = "-";
            }
            else if (clsGame.GameType == "MULTIPLICATION")
            {
                operationLbl.Content = "*";
            }
            else if (clsGame.GameType == "DIVISION")
            {
                operationLbl.Content = "/";
            }

            MyTimer = new DispatcherTimer();
            MyTimer.Interval = TimeSpan.FromSeconds(1);
            MyTimer.Tick += MyTimer_Tick1;
        }
        /// <summary>
        /// Increments the on screen timer every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Tick1(object sender, EventArgs e)
        {
            try
            {
                var currentValue = DateTime.Now - this.TimerStart;

                this.timerLbl.Content = currentValue.Seconds.ToString();
                clsUser.PlayerTime = currentValue.TotalSeconds;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SoundPlayer blip = new SoundPlayer("blip2.wav");
                blip.Play();

                this.TimerStart = DateTime.Now;

                // enables the answer box so the game can be played
                answerBox.IsEnabled = true;

                MyTimer.Start();
                runGame();
            }
            catch(Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// Handles errors risen up through exeptions and displays a message.
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
                //could write to a file here
            }
        }
        /// <summary>
        /// Runs the game. Fills in number values.
        /// </summary>
        private void runGame()
        {
            try
            {
                if (clsGame.GameType == "ADDITION")
                {
                    operand1.Content = game.getRand();
                    operand2.Content = game.getRand();
                }
                else if (clsGame.GameType == "SUBTRACTION")
                {
                    int op1;
                    int op2;
                    do
                    {
                        op1 = game.getRand();
                        op2 = game.getRand();
                    } while (op2 > op1);

                    operand1.Content = op1;
                    operand2.Content = op2;
                }
                else if (clsGame.GameType == "MULTIPLICATION")
                {
                    operand1.Content = game.getRand();
                    operand2.Content = game.getRand();
                }
                else if (clsGame.GameType == "DIVISION")
                {
                    int op1;

                    op1 = game.divisionGame();
                    var factors = game.getFactors(op1);
                    int op2;

                    bool done = false;
                    do
                    {
                        op2 = game.divisionGame();

                        for (int i = 0; i < factors.Count; i++)
                        {
                            if (op2 == factors[i])
                            {
                                done = true;
                            }
                        }

                    } while (!done);

                    operand1.Content = op1;
                    operand2.Content = op2;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            
        }
        /// <summary>
        /// Submits users answers to be checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitAnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //need to add a check to make sure somethign is entered
                SoundPlayer errorSound = new SoundPlayer("error2.wav");
                SoundPlayer correctSound = new SoundPlayer("correct.wav");

                if (answerBox.Text == "")
                {
                    return;
                }

                if (clsGame.GameType == "ADDITION")
                {
                    if (game.isCorrect("add", (int)operand1.Content, (int)operand2.Content, Int32.Parse(answerBox.Text)))
                    {
                        correctLbl.Content = "Correct!";
                        iNumWins++;
                        iNumRounds++;

                        // play happy sound
                        correctSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                            //this.Show();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                    else
                    {
                        correctLbl.Content = "Incorrect!";
                        iNumRounds++;

                        //play sad sound
                        errorSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                }
                else if (clsGame.GameType == "SUBTRACTION")
                {
                    if (game.isCorrect("sub", (int)operand1.Content, (int)operand2.Content, Int32.Parse(answerBox.Text)))
                    {
                        correctLbl.Content = "Correct!";
                        iNumWins++;
                        iNumRounds++;

                        // play happy sound
                        correctSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                            //this.Show();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                    else
                    {
                        correctLbl.Content = "Incorrect!";
                        iNumRounds++;

                        //play sad sound
                        errorSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                            //this.Show();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                }
                else if (clsGame.GameType == "MULTIPLICATION")
                {
                    if (game.isCorrect("mult", (int)operand1.Content, (int)operand2.Content, Int32.Parse(answerBox.Text)))
                    {
                        correctLbl.Content = "Correct!";
                        iNumWins++;
                        iNumRounds++;

                        // play happy sound
                        correctSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                    else
                    {
                        correctLbl.Content = "Incorrect!";
                        iNumRounds++;

                        //play sad sound
                        errorSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                }
                else if (clsGame.GameType == "DIVISION")
                {
                    if (game.isCorrect("div", (int)operand1.Content, (int)operand2.Content, Int32.Parse(answerBox.Text)))
                    {
                        correctLbl.Content = "Correct!";
                        iNumWins++;
                        iNumRounds++;

                        // play happy sound
                        correctSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                    else
                    {
                        correctLbl.Content = "Incorrect!";
                        iNumRounds++;

                        //play sad sound
                        errorSound.Play();

                        if (iNumRounds == 10)
                        {
                            clsUser.PlayerScore = iNumWins;

                            FinalScoreWindow fsw = new FinalScoreWindow();
                            this.Close();

                            fsw.ShowDialog();
                        }

                        answerBox.Text = "";
                        runGame();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Prevents non number characters from being entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void answerBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Enter))
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
