using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli kütüphane


public class TikTak : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // TMP Text referansı
    [SerializeField] private Button startButton; // Buton referansı
    [SerializeField] private GameObject restartPanel;
    private int currentTime; // Şu anki zaman değeri
    private Coroutine countdownCoroutine;

    public bool win = true; // Yeni boolean değişken


    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText atanmadı! Lütfen bir TMP Text ekleyin.");
            return;
        }

        if (startButton == null)
        {
            Debug.LogError("Start Button atanmadı! Lütfen buton referansını ekleyin.");
            return;
        }

        startButton.onClick.AddListener(() => StartCoroutine(DelayedStart())); // Butona tıklanınca gecikmeli başlat
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f); // Butona basıldıktan 0.5 saniye sonra başlasın
        StartTimer();
    }

    public void StartTimer()
    {
        win = true;
        // Text'teki değeri alıp integer'a çeviriyoruz
        if (!int.TryParse(timerText.text, out currentTime))
        {
            Debug.LogError("TimerText içeriği geçerli bir sayı değil!");
            return;
        }

        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine); // Sayaç çalışıyorsa önce durdur.
        }

        countdownCoroutine = StartCoroutine(TimerCountdown());
    }

private IEnumerator TimerCountdown()
{
    yield return new WaitForSeconds(1f); // Sayacın başlaması 1 saniye aralıklarla olur

    while (currentTime > 0 && win) // win değişkenini de kontrol et
    {
        yield return new WaitForSeconds(1f);

        if (!win) // Eğer kazanıldıysa sayaç durmalı
        {
            yield break; // Coroutine'i durdur
        }

        if (currentTime == 9)
        {
            AudioManager.Instance.Play("ClockOver"); // 8 saniye kaldığında özel sesi çal
        }
        else if (currentTime > 8)
        {
            AudioManager.Instance.Play("TickClock"); // Normal geri sayım sesi
        }

        currentTime--;
        timerText.text = currentTime.ToString();
    }

    if (currentTime == 0)
    {
        restartPanel.SetActive(true);
        AudioManager.Instance.Play("Lose"); 
    }
}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
