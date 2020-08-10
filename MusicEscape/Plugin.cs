using IPA;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace MusicEscape
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger log { get; set; }

        [Init]
        public Plugin(IPALogger logger)
        {
            Instance = this;
            log = logger;
        }

        [OnEnable]
        public void OnEnable()
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        [OnDisable]
        public void OnDisable()
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            PauseManager pauseManager = PauseManager.instance;
            if (pauseManager)
                GameObject.Destroy(pauseManager);
        }

        private void SceneManager_activeSceneChanged(Scene current, Scene next)
        {
            if (next.name == "GameCore")
                new GameObject("MusicEscape_PauseManager").AddComponent<PauseManager>();
        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }

    }
}
