using System;
using System.Media;
using System.Timers;
using Reversi.GameEngine.Classes;

namespace Reversi.GameEngine
{
    public class Game
    {
        #region Variables
        //timer for compturer move
        private Timer _timer;

        //assurance of exit from timer
        public bool AlreadyExitFromTimer { get; private set; }

        public bool EnabledComputerMoves { get; private set; }

        //could be changed only in LoadFromFile method or in cb_change        
        private bool _enabledTips = true;
        public bool EnabledTips { get { return _enabledTips; } private set { _enabledTips = value; } }

        private int _currentMove;
        public int CurrentMove
        {
            get
            {
                return _currentMove;
            }
            set
            {
                if (value > 0)
                    _currentMove = value;
                else
                {
                    throw new ArgumentException("Неправильно введені дані");
                }
            }
        }

        private Field _field;
        public Field Field { get { return _field; } private set { _field = value; } }
        #endregion

        #region Events
        public event Action InitDrawHandler;
        public event Action UpdateScoreLabelsHandler;
        public event Action<string> ShomMessageHandler;
        public Action<int, bool> DrawHandler;
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
        private void OnInitializeField()
        {
            _field = new Field();
            OnUpdateScoreAndPlayerMove();
        }
        private void OnInitializeDraw()
        {
            if (InitDrawHandler != null)
            {
                InitDrawHandler();
            }
        }
        private void OnUpdateScoreAndPlayerMove()
        {
            if (_field.GameProcess)
            {
                _field.CalculatePlayersPoints();
                if (UpdateScoreLabelsHandler != null)
                {
                    UpdateScoreLabelsHandler();
                }
            }
        }
        private void OnUpdateGameFinish()
        {
            //condition to finish game
            if (!Field.GameProcess)
            {
                //pnl_Field.Enabled = false;
                if (Field.FirstPlayerPoints > Field.SecondPlayerPoints)
                    ShomMessageHandler(String.Format("{0} player win with score: {1}", "First", Field.FirstPlayerPoints));
                else
                    if (Field.FirstPlayerPoints < Field.SecondPlayerPoints)
                        ShomMessageHandler(String.Format("{0} player win with score: {1}", "Second(you)", Field.SecondPlayerPoints));
                    else
                        ShomMessageHandler(String.Format("Draw:  {0}-{1}", Field.FirstPlayerPoints, Field.SecondPlayerPoints));
            }
        }
        private void OnDraw(int player, bool enabledTips)
        {
            if (DrawHandler != null)
            {
                DrawHandler(player, enabledTips);
            }
        }
        public void CreateNewGame()
        {
            AlreadyExitFromTimer = true;
            EnabledComputerMoves = false;
            CurrentMove = 1;
            Initialize();
        }
        #endregion

        public bool FirstPlayerMove()
        {
            return (_currentMove) % 2 == 0;
        }

        public void ChangeComputerModeOn(bool state)
        {
            EnabledComputerMoves = state;
        }

        public void ChangeTips(bool state)
        {
            _enabledTips = state;
        }

        public void Initialize()
        {
            OnInitializeField();
            OnInitializeDraw();
            ReDraw();
        }

        public void ReDraw()
        {
            if (AlreadyExitFromTimer)
            {
                if (FirstPlayerMove())
                    OnDraw((int)Players.FirstPlayer, EnabledTips);
                else
                    OnDraw((int)Players.SecondPlayer, EnabledTips);
            }
        }

        public void Move(int x, int y)
        {
            DoPlayerMove(x, y);
            OnUpdateScoreAndPlayerMove();
            OnUpdateGameFinish();
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
                            OnDraw((int)Players.SecondPlayer, _enabledTips);
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
                        OnDraw((int)Players.FirstPlayer, _enabledTips);
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
                OnDraw((int)Players.SecondPlayer, _enabledTips);
                PlayGoodSound();
                //update scode and finish condition
                OnUpdateScoreAndPlayerMove();
                OnUpdateGameFinish();
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

        #region States
        public GameState GetState()
        {
            return new GameState() { Field = new Field(_field), CurrentMove = _currentMove, EnabledTips = _enabledTips, FirstMoveAI = _field.FirstMoveAI};
        }
        public void RestoreState(GameState state)
        {            
            this.Field = state.Field;
            this.CurrentMove = state.CurrentMove;
            this.EnabledTips = state.EnabledTips;
            this.Field.FirstMoveAI = state.FirstMoveAI;
            this.Field.FindEnabledMoves(FirstPlayerMove() ? (int)Players.FirstPlayer : (int)Players.SecondPlayer);
            OnUpdateScoreAndPlayerMove();
            OnInitializeDraw();
            ReDraw();
        }        
        #endregion
        #endregion
    }
}
