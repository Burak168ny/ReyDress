using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obj3;
    [SerializeField] private GameObject obj4;
    [SerializeField] private GameObject obj5;
    [SerializeField] private GameObject obj6;

    [SerializeField] private Animator cloudCloseAnimation;
    [SerializeField] private GameObject curtain1; // İlk perde referansı
    [SerializeField] private GameObject curtain2; // İkinci perde referansı

    [SerializeField] private GameObject button1; // İlk buton referansı
    [SerializeField] private GameObject button2; // İkinci buton referansı
    [SerializeField] private GameObject button3; // Üçüncü buton referansı

    private List<List<GameObject>> childLists = new List<List<GameObject>>();
    public List<GameObject> cloudAList = new List<GameObject>(); // Yeni liste

    private void Awake()
    {
        AddChildrenToList(obj1);
        AddChildrenToList(obj2);
        AddChildrenToList(obj3);
        AddChildrenToList(obj4);
        AddChildrenToList(obj5);
        AddChildrenToList(obj6);

        // cloudAList içine eklemek istediğin objeleri buraya ekle
        cloudAList.Add(obj1);
        cloudAList.Add(obj2);
        cloudAList.Add(obj3);
        cloudAList.Add(obj4);
        cloudAList.Add(obj5);
        cloudAList.Add(obj6);
    }

    private void AddChildrenToList(GameObject parent)
    {
        List<GameObject> childList = new List<GameObject>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            childList.Add(parent.transform.GetChild(i).gameObject);
        }

        childLists.Add(childList);
    }

    public void RandomizeActiveChildren()
    {
        AudioManager.Instance.Play("Random");
        const string CLOUD_CLOSE = "Clouds";

        // Animasyonu başlat
        cloudCloseAnimation.Play(CLOUD_CLOSE);

        // Animasyon bitince perdeyi kapat
        StartCoroutine(DisableCurtainAfterAnimation());

        if (curtain2 != null) curtain2.SetActive(true);

        List<int> indices = new List<int> { 0, 1, 2, 3, 4, 5 };
        ShuffleList(indices);

        foreach (var childList in childLists)
        {
            foreach (var child in childList)
            {
                child.SetActive(false);
            }
        }

        for (int i = 0; i < childLists.Count; i++)
        {
            childLists[i][indices[i]].SetActive(true);
        }

        if (button1 != null) button1.SetActive(true);
        if (button2 != null) button2.SetActive(true);
        if (button3 != null) button3.SetActive(true);
    }

    private IEnumerator DisableCurtainAfterAnimation()
    {
        float animationTime = cloudCloseAnimation.GetCurrentAnimatorStateInfo(0).length; // Animasyon süresi
        yield return new WaitForSeconds(animationTime * 2f); // Animasyon bitene kadar bekle
        
        if (curtain1 != null) curtain1.SetActive(false); // Perdeyi kapat

        // Perdeden sonra cloudAList içindeki objeleri aktif et
        foreach (var obj in cloudAList)
        {
            obj.SetActive(true);
        }
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}
