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

        private PauseMenuManager _pauseMenuManager;
        private PauseMenuManager PauseMenuManager
        {
            get
            {
                if (_pauseMenuManager == null)
                    _pauseMenuManager = Resources.FindObjectsOfTypeAll<PauseMenuManager>().First();
                return _pauseMenuManager;
            }
            set { _pauseMenuManager = value; }
        }

        private GamePause _gamePause;
        private GamePause GamePause
        {
            get
            {
                if (_gamePause == null)
                    _gamePause = Resources.FindObjectsOfTypeAll<GamePause>().First();
                return _gamePause;
            }
            set { _gamePause = value; }
        }



        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            if (instance != null)
                DestroyImmediate(this);
            instance = this;
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {
            if (IsGameScene && Input.GetKeyDown(KeyCode.Escape))
            {
                Plugin.log.Info("Forcing song exit.");
                GamePause.Pause();
                PauseMenuManager.MenuButtonPressed();
            }
        }

        /// <summary>
        /// Called when the script becomes enabled and active
        /// </summary>
        private void OnEnable()
        {

        }

        /// <summary>
        /// Called when the script becomes disabled or when it is being destroyed.
        /// </summary>
        private void OnDisable()
        {

        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            instance = null;
        }
        #endregion
    }
}
