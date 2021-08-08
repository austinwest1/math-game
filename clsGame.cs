using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Assignment_5___Math_Game
{
    class clsGame
    {
        #region Attributes
        /// <summary>
        /// Creates a new instance of random.
        /// </summary>
        Random rand = new Random();
        /// <summary>
        /// The current game type (Addition, subtraction, multiplication or division).
        /// </summary>
        private static string sGameType;
        /// <summary>
        /// Gets and sets the game type.
        /// </summary>
        #endregion

        #region Methods
        public static string GameType
        {
            get { return sGameType; }
            set { sGameType = value; }
        }
        /// <summary>
        /// Returns a random int between 0 and 10.
        /// </summary>
        /// <returns>int</returns>
        public int getRand()
        {
            try
            {
                int r = rand.Next(0, 10);
                return r;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Returns a random int that is not 0.
        /// </summary>
        /// <returns>int</returns>
        public int divisionGame()
        {
            try
            {
                int r;
                do
                {
                    r = getRand();

                } while (r == 0);

                return r;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Returns the factors of a number.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>int factors</returns>
        public List<int> getFactors(int num)
        {
            try
            {
                var factors = new List<int>();
                int max = (int)Math.Sqrt(num);

                for (int factor = 1; factor <= max; ++factor)
                {
                    if (num % factor == 0)
                    {
                        factors.Add(factor);
                        if (factor != num / factor)
                        {
                            factors.Add(num / factor);
                        }
                    }
                }
                return factors;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        /// <summary>
        /// Determines if a set of operations is correct.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <param name="ans"></param>
        /// <returns>Bool</returns>
        public bool isCorrect(string operation, int op1, int op2, int ans)
        {
            try
            {
                if (operation == "add")
                {
                    int icorrectAns = op1 + op2;
                    if (ans == icorrectAns)
                    {
                        return true;
                    }
                    else return false;
                }
                else if (operation == "sub")
                {
                    int icorrectAns = op1 - op2;
                    if (ans == icorrectAns)
                    {
                        return true;
                    }
                    else return false;
                }
                else if (operation == "mult")
                {
                    int icorrectAns = op1 * op2;
                    if (ans == icorrectAns)
                    {
                        return true;
                    }
                    else return false;
                }
                else if (operation == "div")
                {
                    int icorrectAns = op1 / op2;
                    if (ans == icorrectAns)
                    {
                        return true;
                    }
                    else return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
        #endregion
    }
}
