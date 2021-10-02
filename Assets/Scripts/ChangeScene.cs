using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private int NextSceneIndex = 0;

    public void Change()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}
