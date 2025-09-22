using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    public Button toggleButton; // Çocukları değiştiren buton
    public Button updateTextButton; // Yazıyı güncelleyen buton
    public Transform parentObject; // Çocukları kontrol edeceğimiz obje
    public TextMeshProUGUI timerText; // TMP Text referansı
    public bool isTextUpdated = true; // Buton kilitleme kontrolü

    private int currentIndex = 0;
    private readonly int[] timeValues = { 60, 120, 240, 480 }; // Metin değerleri

    void Start()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleChildren);

        if (updateTextButton != null)
            updateTextButton.onClick.AddListener(() => UpdateTimerText(currentIndex));

        SetActiveChild(0);
    }

    void ToggleChildren()
    {
        currentIndex = (currentIndex + 1) % parentObject.childCount;
        SetActiveChild(currentIndex);
    }

    void SetActiveChild(int activeIndex)
    {
        for (int i = 0; i < parentObject.childCount; i++)
        {
            parentObject.GetChild(i).gameObject.SetActive(i == activeIndex);
        }
    }

    void UpdateTimerText(int activeIndex)
    {
        if (!isTextUpdated) return;

        if (timerText != null && activeIndex < timeValues.Length)
        {
            timerText.text = timeValues[activeIndex].ToString();
            isTextUpdated = false; // İşlem sonunda false yap
            toggleButton.interactable = false; // Butonu devre dışı bırak

            
        }
    }
}
