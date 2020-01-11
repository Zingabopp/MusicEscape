using IPA;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace MusicEscape
{
    public class Plugin : IBeatSaberPlugin, IDisablablePlugin
    {
        internal static string Name => "MusicEscape";
        internal static IPALogger log;

        internal PauseManager PauseManager;

        public void Init(IPALogger logger)
        {
            log = logger;
        }
        #region IDisablable

        /// <summary>
        /// Called when the plugin is enabled (including when the game starts if the plugin is enabled).
        /// </summary>
        public void OnEnable()
        {
            PauseManager = new GameObject("MusicEscapePauseManager").AddComponent<PauseManager>();
        }

        /// <summary>
        /// Called when the plugin is disabled. It is important to clean up any Harmony patches, GameObjects, and Monobehaviours here.
        /// The game should be left in a state as if the plugin was never started.
        /// </summary>
        public void OnDisable()
        {
            if (PauseManager != null)
                GameObject.Destroy(PauseManager);
        }
        #endregion

        /// <summary>
        /// Called when the active scene is changed.
        /// </summary>
        /// <param name="prevScene">The scene you are transitioning from.</param>
        /// <param name="nextScene">The scene you are transitioning to.</param>
        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {

        }

        /// <summary>
        /// Called when the a scene's assets are loaded.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="sceneMode"></param>
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {



        }


        public void OnApplicationQuit()
        {

        }

        /// <summary>
        /// Runs at a fixed intervalue, generally used for physics calculations. 
        /// </summary>
        public void OnFixedUpdate()
        {

        }

        /// <summary>
        /// This is called every frame.
        /// </summary>
        public void OnUpdate()
        {

        }


        public void OnSceneUnloaded(Scene scene)
        {

        }


        /// <summary>
        /// This should not be used with an IDisablable plugin. 
        /// It will not be called if the plugin starts disabled and is enabled while the game is running.
        /// </summary>
        public void OnApplicationStart()
        { }
    }
}
