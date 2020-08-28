using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MusicEscape
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager instance { get; private set; }

        public bool IsGameScene => SceneManager.GetActiveScene().name == "GameCore";

        private static PauseMenuManager _pauseMenuManager;
        private static PauseMenuManager PauseMenuManager
        {
            get
            {
                if (_pauseMenuManager == null)
                    _pauseMenuManager = Resources.FindObjectsOfTypeAll<PauseMenuManager>().First();
                return _pauseMenuManager;
            }
            set { _pauseMenuManager = value; }
        }

        private static GamePause _gamePause;
        private static GamePause GamePause
        {
            get
            {
                if (_gamePause == null)
                    _gamePause = Resources.FindObjectsOfTypeAll<GamePause>().First();
                return _gamePause;
            }
            set { _gamePause = value; }
        }

        private void SetEvents(GamePause gamePause)
        {
            if (gamePause == null) return;
            RemoveEvents(gamePause);
            GamePause.didPauseEvent += OnGamePaused;
            GamePause.didResumeEvent += OnGameResumed;
        }

        private void RemoveEvents(GamePause gamePause)
        {
            if (gamePause == null) return;
            GamePause.didPauseEvent -= OnGamePaused;
            GamePause.didResumeEvent -= OnGameResumed;
        }

        private void OnGameResumed()
        {
            IsPaused = false;
        }

        private void OnGamePaused()
        {
            IsPaused = true;
        }



        #region Monobehaviour Messages
        public bool IsPaused { get; private set; }
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            if (instance != null)
            {
                Plugin.log.Warn("Instance of PauseManager already exists.");
                DestroyImmediate(this);
            }
            instance = this;
        }

        private void Start()
        {
            Plugin.log.Debug("PauseManager active.");
            SetEvents(GamePause);
        }

        private void OnDisable()
        {
            Plugin.log.Debug("PauseManager disabled.");
        }

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPaused)
                    GamePause.Pause();

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PauseMenuManager.RestartButtonPressed();
                    Plugin.log.Info("Forcing song restart.");
                }
                else
                {
                    PauseMenuManager.MenuButtonPressed();
                    Plugin.log.Info("Forcing song exit.");
                }
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                if (!IsPaused)
                {
                    Plugin.log.Info("Pausing song.");
                    GamePause.Pause();
                    PauseMenuManager.ShowMenu();
                }
                else
                {
                    Plugin.log.Info("Unpausing song.");
                    PauseMenuManager.ContinueButtonPressed();
                }
            }
        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
#if DEBUG
            Plugin.log.Debug("Destroying PauseManager");
#endif
            instance = null;
        }
        #endregion
    }
}
