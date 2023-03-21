using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private int Score;

    private Blade Blade;
    private Spawner Spawner;

    public Image FadeImage;
    private void Awake()
    {
        Blade = FindObjectOfType<Blade>();
        Spawner = FindObjectOfType<Spawner>();
    }
    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
        Time.timeScale = 1f;
        Blade.enabled = true;
        Spawner.enabled = true;

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    public void IncreaseScore()
    {
        Score++;
        ScoreText.text = Score.ToString();
    }

    public void Explode()
    {
        Blade.enabled = false;
        Spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float Elapsed = 0f;
        float Duration = .5f;

        while (Elapsed < Duration)
        {
            float t = Mathf.Clamp01(Elapsed / Duration);
            FadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            Elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        Elapsed = 0f;

        while (Elapsed < Duration)
        {
            float t = Mathf.Clamp01(Elapsed / Duration);
            FadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            
            Elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
