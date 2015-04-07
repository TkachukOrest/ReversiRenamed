using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Reversi.GameEngine
{
    public class Game
    {
        #region Variables      
        //music
        System.Media.SoundPlayer sndBad;
        System.Media.SoundPlayer sndGood;

        //timer for compturer move
        private System.Timers.Timer timer;

        //assurance of exit from timer
        public bool AlreadyExitFromTimer { get; set; }

        //could be changed only in LoadFromFile method or in cb_change
        private bool enabledTips = true;
        public bool EnabledTips { get { return enabledTips; } set { enabledTips = value; } }

        private bool enabledComputerMoves = false;
        public bool EnabledComputerMoves { get { return enabledComputerMoves; } set { enabledComputerMoves = value; } }

        private int currentMove = 1;
        public int CurrentMove { get { return currentMove; } set { currentMove = value; } }

        private Field field;
        public Field Field { get { return field; } set { field = value; } }
        #endregion

        #region Events
        public event Action InitPnlFieldHandler;
        public event Action InitDrawHandler;
        public event Action UpdateScoreLabelsHandler;
        public event Action<string> ShomMessageHandler;
        public Action<int, bool> DrawHandler;//draw from Console|Form
        #endregion

        #region Constructors
        public Game()
        {
            //music initializating
            sndBad = new System.Media.SoundPlayer(Reversi.GameEngine.Properties.Resources.badMove1);
            sndGood = new System.Media.SoundPlayer(Reversi.GameEngine.Properties.Resources.goodMove1);
            sndBad.Load();
            sndGood.Load();

            //variables initializating
            AlreadyExitFromTimer = true;
            EnabledTips = true;
            EnabledComputerMoves = false;
            CurrentMove = 1;
            timer = new System.Timers.Timer();
        }
        #endregion

        #region Methods
        #region Methods which using events
        public void InitializeField()
        {
            currentMove = 1;
            field = new Field();
            InitPnlFieldHandler();
            UpdateScoreAndPlayerMove();
        }
        public void InitializeDraw()
        {
            InitDrawHandler();
        }
        public void UpdateScoreAndPlayerMove()
        {
            field.CalculatePlayersPoints();
            UpdateScoreLabelsHandler();
        }
        public void UpdateGameFinish()
        {
            //condition to finish game
            if (!Field.GameProcess)
            {
                //pnl_Field.Enabled = false;
                if (Field.FirstPlayerPoints > Field.SecondPlayerPoints)
                    ShomMessageHandler(String.Format("{0} player win with score: {1}", "Red", Field.FirstPlayerPoints));
                else
                    if (Field.FirstPlayerPoints < Field.SecondPlayerPoints)
                        ShomMessageHandler(String.Format("{0} player win with score: {1}", "Blue", Field.SecondPlayerPoints));
                    else
                        ShomMessageHandler(String.Format("Draw:  {0}-{1}", Field.FirstPlayerPoints, Field.SecondPlayerPoints));
            }
        }
        public void Draw(int player, bool enabledTips)
        {
            DrawHandler(player, enabledTips);
        }
        public void CreateNewGame()
        {
            AlreadyExitFromTimer = true;
            enabledComputerMoves = false;
            currentMove = 1;
            InitializeField();
            InitializeDraw();
            Draw((int)Players.SecondPlayer, enabledTips);
        }
        #endregion

        public void Move(int x, int y)
        {
            DoPlayerMove(x, y);
            UpdateScoreAndPlayerMove();
            UpdateGameFinish();
            DoCompMoveIfNeed();
        }

        private void DoPlayerMove(int x, int y)
        {
            if (field.GameProcess)
            {
                if ((currentMove) % 2 == 0)
                {
                    if (!enabledComputerMoves)
                    {
                        if (field.IsMovePoint(x, y, (int)Players.FirstPlayer))
                        {
                            field[x, y] = (int)Players.FirstPlayer;
                            currentMove++;
                            //draw new field for second player with tips which dependent from "bool enabledTips"                    
                            Draw((int)Players.SecondPlayer, enabledTips);
                            sndGood.Play();
                        }
                        else
                            sndBad.Play();
                    }
                }
                else
                {
                    if (field.IsMovePoint(x, y, (int)Players.SecondPlayer))
                    {
                        field[x, y] = (int)Players.SecondPlayer;
                        currentMove++;
                        //draw new field for first player with tips which dependent from "bool enabledTips"
                        Draw((int)Players.FirstPlayer, enabledTips);
                        sndGood.Play();
                    }
                    else
                        sndBad.Play();
                }
            }
        }

        private void DoCompMoveIfNeed()
        {
            if (enabledComputerMoves && field.GameProcess && ((currentMove % 2) == 0))
            {
                if (AlreadyExitFromTimer)
                {
                    AlreadyExitFromTimer = false;
                    timer.Elapsed += new ElapsedEventHandler(TimerTick);
                    timer.Interval = 1000;
                    timer.Enabled = true;
                }
            }
        }

        private void TimerTick(Object myObject, ElapsedEventArgs myEventArgs)
        {
            if ((currentMove) % 2 == 0 && enabledComputerMoves && field.GameProcess)
            {
                timer.Stop();
                timer.Enabled = false;
                //do computer move
                field.DoComputerMove((int)Players.FirstPlayer);
                //drawcomputermove
                Draw((int)Players.SecondPlayer, enabledTips);
                sndGood.Play();
                //update scode and finish condition
                UpdateScoreAndPlayerMove();
                UpdateGameFinish();
                currentMove++;
                AlreadyExitFromTimer = true;
            }
        }

        #region XML
        public void LoadFromXML(string fileName)
        {
            if (!XmlSerializator.ReadFromXML(ref field, ref enabledTips, ref currentMove, fileName))
            {
                throw new Exception("Виникла помилка при зчитуванні з файлу");
            }
            else
            {
                UpdateScoreAndPlayerMove();
                if ((currentMove) % 2 == 0)
                    Draw((int)Players.FirstPlayer, enabledTips);
                else
                    Draw((int)Players.SecondPlayer, enabledTips);
            }
        }
        #endregion
        #endregion
    }
}
