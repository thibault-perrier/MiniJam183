#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScene : MonoBehaviour
{
    [SerializeField, HideInInspector] private string sceneName;

#if UNITY_EDITOR
    [SerializeField] private SceneAsset _sceneAsset;
    [SerializeField] private TextMeshProUGUI _levelNumber; 

    [ContextMenu("Update Linked Scene")]
    public void SetSceneName()
    {
        sceneName = _sceneAsset.name;
        _levelNumber.text = sceneName;
        gameObject.name = sceneName;
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
#endif

    public void LaunchScene()
    {
        SceneManager.LoadScene(sceneName);
    }


}
