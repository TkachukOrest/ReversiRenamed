using System;
using System.Media;
using System.Timers;

//TODO: write to xml here
namespace Reversi.GameEngine
{
    public class Game
    {
        #region Variables
        //timer for compturer move
        private Timer _timer;

        //assurance of exit from timer
        public bool AlreadyExitFromTimer { get; set; }

        public bool EnabledComputerMoves { get; set; }

        //could be changed only in LoadFromFile method or in cb_change
        private bool _enabledTips = true;
        public bool EnabledTips { get { return _enabledTips; } set { _enabledTips = value; } }

        private int _currentMove;
        public int CurrentMove { get { return _currentMove; } set { _currentMove = value; } }

        private Field _field;
        public Field Field { get { return _field; } set { _field = value; } }
        #endregion

        #region Events
        public event Action InitDrawHandler;
        public event Action UpdateScoreLabelsHandler;
        public event Action<string> ShomMessageHandler;
        public Action<int, bool> DrawHandler;//draw from Console|Form
        public event Action PlayGoodSoundHandler;
        public event Action PlayBadSoundHandler;
        #endregion

        #region Constructors
        public Game()
        {
            //variables initializating
            AlreadyExitFromTimer = true;
            EnabledTips = true;
            EnabledComputerMoves = false;
            CurrentMove = 1;
            _timer = new Timer();
        }
        #endregion

        #region Methods
        #region Methods which using events
        public void InitializeField()
        {
            _currentMove = 1;
            _field = new Field();
            UpdateScoreAndPlayerMove();
        }
        public void InitializeDraw()
        {
            if (InitDrawHandler != null)
                InitDrawHandler();
        }
        public void UpdateScoreAndPlayerMove()
        {
            _field.CalculatePlayersPoints();
            if (UpdateScoreLabelsHandler != null)
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
            if (DrawHandler != null)
                DrawHandler(player, enabledTips);
        }
        public void CreateNewGame()
        {
            AlreadyExitFromTimer = true;
            EnabledComputerMoves = false;
            _currentMove = 1;
            InitializeField();
            InitializeDraw();
            Draw((int)Players.SecondPlayer, _enabledTips);
        }
        #endregion

        public bool FirstPlayerMove()
        {
            return (_currentMove) % 2 == 0;
        }

        public void Move(int x, int y)
        {
            DoPlayerMove(x, y);
            UpdateScoreAndPlayerMove();
            UpdateGameFinish();
            DoCompMoveIfNeed();
        }

        private void DoPlayerMove(int x, int y)
        {
            if (_field.GameProcess)
            {
                if (FirstPlayerMove())
                {
                    if (!EnabledComputerMoves)
                    {
                        if (_field.IsMovePoint(x, y, (int)Players.FirstPlayer))
                        {
                            _field[x, y] = (int)Players.FirstPlayer;
                            _currentMove++;
                            //draw new field for second player with tips which dependent from "bool enabledTips"                    
                            Draw((int)Players.SecondPlayer, _enabledTips);
                            PlayGoodSound();
                        }
                        else
                            PlayBadSound();
                    }
                }
                else
                {
                    if (_field.IsMovePoint(x, y, (int)Players.SecondPlayer))
                    {
                        _field[x, y] = (int)Players.SecondPlayer;
                        _currentMove++;
                        //draw new field for first player with tips which dependent from "bool enabledTips"
                        Draw((int)Players.FirstPlayer, _enabledTips);
                        PlayGoodSound();
                    }
                    else
                        PlayBadSound();
                }
            }
        }

        private void DoCompMoveIfNeed()
        {
            if (EnabledComputerMoves && _field.GameProcess && FirstPlayerMove())
            {
                if (AlreadyExitFromTimer)
                {
                    AlreadyExitFromTimer = false;
                    _timer.Elapsed += new ElapsedEventHandler(TimerTick);
                    _timer.Interval = 1000;
                    _timer.Enabled = true;
                }
            }
        }

        private void TimerTick(Object myObject, ElapsedEventArgs myEventArgs)
        {
            if (FirstPlayerMove() && EnabledComputerMoves && _field.GameProcess)
            {
                _timer.Stop();
                _timer.Enabled = false;
                //do computer move
                _field.DoComputerMove((int)Players.FirstPlayer);
                //drawcomputermove with tips for second player
                Draw((int)Players.SecondPlayer, _enabledTips);
                PlayGoodSound();
                //update scode and finish condition
                UpdateScoreAndPlayerMove();
                UpdateGameFinish();
                _currentMove++;
                AlreadyExitFromTimer = true;
            }
        }

        #region Music
        private void PlayGoodSound()
        {
            if (PlayGoodSoundHandler != null)
                PlayGoodSoundHandler();
        }
        private void PlayBadSound()
        {
            if (PlayBadSoundHandler != null)
                PlayBadSoundHandler();
        }
        #endregion

        #region XML
        public void LoadFromXML(string fileName)
        {
            if (!XmlSerializator.ReadFromXML(ref _field, ref _enabledTips, ref _currentMove, fileName))
            {
                throw new Exception("Виникла помилка при зчитуванні з файлу");
            }
            else
            {
                UpdateScoreAndPlayerMove();
                if (FirstPlayerMove())
                    Draw((int)Players.FirstPlayer, _enabledTips);
                else
                    Draw((int)Players.SecondPlayer, _enabledTips);
            }
        }

        public void WriteToXML(string fileName)
        {
            if (!XmlSerializator.WriteToXML(this.Field, this.EnabledTips, this.CurrentMove, fileName))
            {
                throw new Exception("Виникла помилка при записуванні у файл");
            }
        }

        #endregion
        #endregion
    }
}
