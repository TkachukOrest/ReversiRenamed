using System;
using System.Timers;

namespace Reversi.GameEngine
{
    public class Game
    {
        #region Variables
        //timer for compturer move
        private Timer _timer;

        public bool AlreadyExitFromTimer { get; private set; }
        public bool EnabledComputerMoves { get; private set; }
        public bool EnabledTips { get; private set; }
        public Field Field { get; private set; }

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
        #region Methods which used events
        private void OnInitializeField()
        {
            Field = new Field();
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
            Field.CalculatePlayersPoints();
            if (UpdateScoreLabelsHandler != null)
            {
                UpdateScoreLabelsHandler();
            }
        }
        private void OnUpdateGameFinish()
        {
            if (!Field.GameProcess)
            {
                if (ShomMessageHandler != null)
                {
                    if (Field.FirstPlayerPoints > Field.SecondPlayerPoints)
                        ShomMessageHandler(String.Format("{0} player win with score: {1}", "First", Field.FirstPlayerPoints));
                    else
                        if (Field.FirstPlayerPoints < Field.SecondPlayerPoints)
                            ShomMessageHandler(String.Format("{0} player win with score: {1}", "Second(you)", Field.SecondPlayerPoints));
                        else
                            ShomMessageHandler(String.Format("Draw:  {0}-{1}", Field.FirstPlayerPoints, Field.SecondPlayerPoints));
                }
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
            EnabledTips = state;
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
                {
                    Field.FindEnabledMoves((int)Players.FirstPlayer);
                    OnDraw((int)Players.FirstPlayer, EnabledTips);
                }
                else
                {
                    Field.FindEnabledMoves((int)Players.SecondPlayer);
                    OnDraw((int)Players.SecondPlayer, EnabledTips);
                }
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
            if (Field.GameProcess)
            {
                if (FirstPlayerMove())
                {
                    if (!EnabledComputerMoves)
                    {
                        if (Field.IsMovePoint(x, y, (int)Players.FirstPlayer))
                        {
                            Field[x, y] = (int)Players.FirstPlayer;
                            _currentMove++;
                            Field.FindEnabledMoves((int)Players.SecondPlayer);
                            //draw new field for second player with tips which dependent from "bool enabledTips"                    
                            OnDraw((int)Players.SecondPlayer, EnabledTips);
                            PlayGoodSound();
                        }
                        else
                            PlayBadSound();
                    }
                }
                else
                {
                    if (Field.IsMovePoint(x, y, (int)Players.SecondPlayer))
                    {
                        Field[x, y] = (int)Players.SecondPlayer;
                        _currentMove++;
                        Field.FindEnabledMoves((int)Players.FirstPlayer);
                        //draw new field for first player with tips which dependent from "bool enabledTips"
                        OnDraw((int)Players.FirstPlayer, EnabledTips);
                        PlayGoodSound();
                    }
                    else
                        PlayBadSound();
                }
            }
        }

        private void DoCompMoveIfNeed()
        {
            if (EnabledComputerMoves && Field.GameProcess && FirstPlayerMove())
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
            if (FirstPlayerMove() && EnabledComputerMoves && Field.GameProcess)
            {
                _timer.Stop();
                _timer.Enabled = false;
                //do computer move
                Field.DoComputerMove((int)Players.FirstPlayer);

                //drawcomputermove with tips for second player
                Field.FindEnabledMoves((int)Players.SecondPlayer);
                OnDraw((int)Players.SecondPlayer, EnabledTips);
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
            return new GameState() { Field = new Field(this.Field), CurrentMove = _currentMove, EnabledTips = this.EnabledTips, FirstMoveAI = this.Field.FirstMoveAI };
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
