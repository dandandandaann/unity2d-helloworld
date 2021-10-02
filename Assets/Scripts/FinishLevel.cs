using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private AudioSource finishAudio;

    [SerializeField]
    private AudioSource BackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        finishAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.IsPlayer())
        {
            BackgroundMusic.Stop();
            finishAudio.Play();

            Invoke("ChangeLevel", 2f);
            //ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
