using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SceneChanger : MonoBehaviour
{
    [Dropdown(nameof(SceneValues))]
    public string sceneName;

    private List<string> SceneValues
    {
        get
        {
            return new List<string>() { "SampleScene", "MovementTestScene" };
        }
    }
    [Button]
    private void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
            return;

        if (SceneManager.GetActiveScene().name == sceneName)
            return;

        SceneManager.LoadScene(sceneName);
    }
}
