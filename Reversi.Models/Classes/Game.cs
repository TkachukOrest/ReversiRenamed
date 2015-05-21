using System;
using System.Timers;
using Reversi.GameEngine.Classes;

namespace Reversi.GameEngine
{
    public class Game
    {
        #region Variables
        //timer for computer move
        private Timer _timer;

        public bool AlreadyExitFromTimer { get; private set; }
        public bool EnabledComputerMoves { get; private set; }
        public bool EnabledTips { get; private set; }
        public Field Field { get; private set; }
        private IArtificialIntelligence _computerIntelligence;

        private int _currentMove;
        public int CurrentMove
        {
            get
            {
                return _currentMove;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Неправильно введені дані");                    
                }
                else
                {
                    _currentMove = value;
                }
            }
        }
        #endregion

        #region Events
        //Peer:Необхідно дописати модифікатор event
        public EventHandler<DrawEventArgs> DrawHandler;
        public event EventHandler<ShowMessageEventArgs> ShomMessageHandler; 
        public event EventHandler InitDrawHandler;
        public event EventHandler UpdateScoreHandler;               
        public event EventHandler PlayGoodSoundHandler;
        public event EventHandler PlayBadSoundHandler;
        #endregion

        #region Constructors
        public Game(IArtificialIntelligence computerIntelligence)
        {
            //variables initializating
            AlreadyExitFromTimer = true;
            EnabledTips = true;
            EnabledComputerMoves = false;
            CurrentMove = 1;
            _timer = new Timer();
            _computerIntelligence = computerIntelligence;
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
                InitDrawHandler(this, EventArgs.Empty);
            }
        }
        private void OnPlayGoodSound()
        {
            if (PlayGoodSoundHandler != null)
            {
                PlayGoodSoundHandler(this, EventArgs.Empty);
            }
        }
        private void OnPlayBadSound()
        {
            if (PlayBadSoundHandler != null)
            {
                PlayBadSoundHandler(this, EventArgs.Empty);
            }
        }
        private void OnUpdateScoreAndPlayerMove()
        {
            Field.CalculatePlayersPoints();
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler(this, EventArgs.Empty);
            }
        }
        private void OnUpdateGameFinish()
        {
            if (!Field.GameProcess)
            {
                if (ShomMessageHandler != null)
                {
                    if (Field.FirstPlayerPoints > Field.SecondPlayerPoints)
                    {
                        ShomMessageHandler(this, new ShowMessageEventArgs(String.Format("{0} player win with score: {1}", "First", Field.FirstPlayerPoints)));
                    }
                    else
                    { 
                        if (Field.FirstPlayerPoints < Field.SecondPlayerPoints)
                        {
                            ShomMessageHandler(this, new ShowMessageEventArgs(String.Format("{0} player win with score: {1}", "Second(you)", Field.SecondPlayerPoints)));
                        }
                        else
                        { 
                            ShomMessageHandler(this, new ShowMessageEventArgs(String.Format("Draw:  {0}-{1}", Field.FirstPlayerPoints, Field.SecondPlayerPoints)));
                        }
                    }
                }
            }
        }
        private void OnDraw(int player, bool enabledTips)
        {
            if (DrawHandler != null)
            {
                DrawHandler(this, new DrawEventArgs(player, enabledTips));
            }
        }       
        private void Initialize()
        {
            OnInitializeField();
            OnInitializeDraw();
            ReDraw();
        }
        public void CreateNewGame()
        {
            AlreadyExitFromTimer = true;
            EnabledComputerMoves = false;
            CurrentMove = 1;
            Initialize();
        }
        #endregion

        public bool IsFirstPlayerMove()
        {
            return (_currentMove) % 2 == 0;
        }

        public void EnableComputerMode(bool state)
        {
            EnabledComputerMoves = state;
        }

        public void EnableTips(bool state)
        {
            EnabledTips = state;
        }

        public void ReDraw()
        {
            if (AlreadyExitFromTimer)
            {
                if (IsFirstPlayerMove())
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

        public void MoveTo(int x, int y)
        {            
            DoPlayerMove(x, y);
            OnUpdateScoreAndPlayerMove();
            OnUpdateGameFinish();
            DoComputerMove();            
        }

        private void DoPlayerMove(int x, int y)
        {
            if (Field.GameProcess)
            {
                if (IsFirstPlayerMove())
                {
                    TryFirstPlayerMove(x, y);
                }
                else
                {
                    TrySecondPlayerMove(x, y);
                }
            }
        }

        private void TryFirstPlayerMove(int x, int y)
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
                    OnPlayGoodSound();
                }
                else
                {
                    OnPlayBadSound();
                }
            }
        }

        private void TrySecondPlayerMove(int x, int y)
        {
            if (Field.IsMovePoint(x, y, (int)Players.SecondPlayer))
            {
                Field[x, y] = (int)Players.SecondPlayer;
                _currentMove++;
                Field.FindEnabledMoves((int)Players.FirstPlayer);
                //draw new field for first player with tips which dependent from "bool enabledTips"
                OnDraw((int)Players.FirstPlayer, EnabledTips);
                OnPlayGoodSound();
            }
            else
            {
                OnPlayBadSound();
            }
        }

        private void DoComputerMove()
        {
            if (ComputerMoveCondition())
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
            if (ComputerMoveCondition())
            {
                _timer.Enabled = false;                
                _computerIntelligence.DoComputerMove(this.Field,(int)Players.FirstPlayer);

                //drawcomputermove with tips for second player
                Field.FindEnabledMoves((int)Players.SecondPlayer);
                OnDraw((int)Players.SecondPlayer, EnabledTips);
                OnPlayGoodSound();
                
                OnUpdateScoreAndPlayerMove();
                OnUpdateGameFinish();
                CurrentMove++;
                AlreadyExitFromTimer = true;                
            }
        }

        private bool ComputerMoveCondition()
        {
            return EnabledComputerMoves && Field.GameProcess && IsFirstPlayerMove();
        }

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
            this.Field.FindEnabledMoves(IsFirstPlayerMove() ? (int)Players.FirstPlayer : (int)Players.SecondPlayer);
            OnUpdateScoreAndPlayerMove();
            OnInitializeDraw();
            ReDraw();
        }
        #endregion
        #endregion
    }
}
