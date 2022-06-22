using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class GameEndWindow : MonoBehaviour
{
    [SerializeField] int _requrePoints;
    [SerializeField] Text _text;
    [SerializeField] int _timeToRestart;

    public int RequrePoints => _requrePoints;

    public void EndGame(string winnerName)
    {
        gameObject.SetActive(true);
        _text.text = string.Format("{0} Won The Battle!", winnerName);
        Time.timeScale = 0;
        StartCoroutine(RestartIn(_timeToRestart));
    }

    private IEnumerator RestartIn(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        Time.timeScale = 1;
        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}
