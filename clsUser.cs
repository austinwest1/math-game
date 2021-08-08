using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5___Math_Game
{
    class clsUser
    {
        #region Attributes
        /// <summary>
        /// Contains the age of the player.
        /// </summary>
        private static int iPlayerAge;
        /// <summary>
        /// Contains the name of the player.
        /// </summary>
        private static string sPlayerName;
        /// <summary>
        /// Gets and sets the players age
        /// </summary>
        private static int iPlayerScore;
        /// <summary>
        /// Contains the time score for the player.
        /// </summary>
        private static double dPlayerTime;
        #endregion

        #region Methods
        public static int PlayerAge
        {
            get { return iPlayerAge; }
            set { iPlayerAge = value; }
        }
        /// <summary>
        /// Gets and sets the players name.
        /// </summary>
        public static string PlayerName
        {
            get { return sPlayerName; }
            set { sPlayerName = value; }
        }
        //Gets and sets the players score.
        public static int PlayerScore
        {
            get { return iPlayerScore; }
            set { iPlayerScore = value; }
        }
        /// <summary>
        /// Gets and sets the players score.
        /// </summary>
        public static double PlayerTime { get; set; }
        #endregion
    }
}
