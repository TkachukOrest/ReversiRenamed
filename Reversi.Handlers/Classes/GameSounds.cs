using System.Media;


namespace Reversi.Handlers
{
    public sealed class GameSounds
    {
        #region Variables
        //music
        private SoundPlayer _sndBad;
        private SoundPlayer _sndGood;
        #endregion

        #region Constructors
        public GameSounds()
        {
            //music initializating
            _sndBad = new SoundPlayer(Properties.Resources.badMove1);
            _sndGood = new SoundPlayer(Properties.Resources.goodMove1);
            _sndBad.Load();
            _sndGood.Load();
        }
        #endregion

        #region Methods
        public void PlayGoodSound()
        {
            _sndGood.Play();
        }
        public void PlayBadSound()
        {
            _sndBad.Play();
        }
        #endregion
    }
}
