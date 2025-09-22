using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class True : MonoBehaviour
{
    [SerializeField] private GameObject curtainQ; 
    [SerializeField] private GameObject testButton; 
    [SerializeField] private GameObject testWarningButton; 
    [SerializeField] private Testing testingScript;  
    [SerializeField] private Simple simpleScript;  
    [SerializeField] private Animator curtainQAnimator; 

    [SerializeField] private GameObject sixStarsButton;
    [SerializeField] private GameObject sixStarsText;
    [SerializeField] private GameObject dogru;

    [SerializeField] private Animator sixStarAnim;

    [SerializeField] private Clock clockScript;
    [SerializeField] private TikTak tikTak;

    private bool hasPlayed = false; // Animasyonun oynayıp oynamadığını kontrol etmek için
    private bool previousAnswer = false; // answer değerinin önceki halini saklamak için

    public List<GameObject> cloudQList = new List<GameObject>(); // Yeni liste

    public List<GameObject> sixStarObjects = new List<GameObject>(); // Yeni liste


    private void Awake()
    {
        // cloudQList içine eklemek istediğin objeleri buraya ekle
        // Örnek olarak bazı objeler ekledim, ihtiyacına göre değiştir
        cloudQList.Add(testButton);
        cloudQList.Add(testWarningButton);
        cloudQList.Add(sixStarsButton);
        // cloudQList.Add(sixStarsText);
        clockScript = FindObjectOfType<Clock>(); // Eğer sahnede sadece bir Clock varsa
    }

    private void Update()
    {
        // Eğer answer değeri false'tan true'ya geçiş yaptıysa ve daha önce oynatılmadıysa
        if (testingScript.answer && !hasPlayed && !previousAnswer)
        {
            hasPlayed = true;
            StartCoroutine(CloseCurtainAfterAnimation());
        }

        // Eğer answer tekrar false olursa, tekrar oynatılabilir hale getir
        if (!testingScript.answer)
        {
            hasPlayed = false;
        }

        // previousAnswer değişkenini güncelle (answer'in değişimini takip etmek için)
        previousAnswer = testingScript.answer;

        // Eğer `different` true ise testButton açık, değilse kapalı
        testButton.SetActive(simpleScript.different);
        
        // testWarningButton, testButton'un tam tersi olacak
        testWarningButton.SetActive(!simpleScript.different);
    }

    private IEnumerator CloseCurtainAfterAnimation()
    {
        tikTak.win = false;
        AudioManager.Instance.Play("Applause");
        AudioManager.Instance.Play("Win");

        const string ANIM_NAME = "CloudsQ"; 

        curtainQAnimator.Play(ANIM_NAME); 

        float animationTime = curtainQAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationTime * 2f); 
        curtainQ.SetActive(false); 

        // Perdeden sonra cloudQList içindeki objeleri aktif et
        foreach (var obj in cloudQList)
        {
            obj.SetActive(true);
        }
        clockScript.toggleButton.interactable = true;
        clockScript.isTextUpdated = true; // İşlem sonunda false yap


        dogru.SetActive(false);

        sixStarsButton.SetActive(true);

        const string AGAIN_ANIM_NAME = "SixStar";
        sixStarAnim.Play(AGAIN_ANIM_NAME);

        AudioManager.Instance.Play("Firework");
        AudioManager.Instance.Play("Firework2");


        // Yeni liste içindeki objeleri aktif hale getir
        foreach (var obj in sixStarObjects)
        {
            obj.SetActive(true);
        }

        float animationSixTime = sixStarAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationSixTime * 2f); 
        sixStarsText.SetActive(true);
    }
}
