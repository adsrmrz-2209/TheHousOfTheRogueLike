using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrapper
{
    //private static readonly string bootstrapScene = "BootstrapScene";
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        //for(int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
        //{
        //    var scene = SceneManager.GetSceneAt(sceneIndex);

        //    if (scene.name == bootstrapScene)
        //        return;
        //}

        //SceneManager.LoadScene(bootstrapScene, LoadSceneMode.Additive);

        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
    }
}
