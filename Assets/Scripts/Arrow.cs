using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    public int currentIndex = 0;

    private void Start()
    {
        // Çocukları listeye ekle
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }

        // Aktif olan çocuğun indexini bul
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].activeSelf)
            {
                currentIndex = i;
                break;
            }
        }

        // Güncellemeyi uygula
        UpdateActiveChild();
    }

    public void ToggleLeft()
    {
        AudioManager.Instance.Play("Change");
        currentIndex = (currentIndex == 0) ? children.Count - 1 : currentIndex - 1;
        UpdateActiveChild();
    }

    public void ToggleRight()
    {
        AudioManager.Instance.Play("Change");
        currentIndex = (currentIndex == children.Count - 1) ? 0 : currentIndex + 1;
        UpdateActiveChild();
    }

    private void UpdateActiveChild()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetActive(i == currentIndex);
        }
    }
}
