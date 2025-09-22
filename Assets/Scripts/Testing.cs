using System.Collections.Generic;
using UnityEngine;
using System;


public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obj3;
    [SerializeField] private GameObject obj4;
    [SerializeField] private GameObject obj5;
    [SerializeField] private GameObject obj6;

    [SerializeField] private GameObject obj7;
    [SerializeField] private GameObject obj8;
    [SerializeField] private GameObject obj9;
    [SerializeField] private GameObject obj10;
    [SerializeField] private GameObject obj11;
    [SerializeField] private GameObject obj12;

    [SerializeField] private List<GameObject> statusObjects; // Doğru sayısını gösterecek objeler listesi
    [SerializeField] private GameObject dogru;
    public int correctCount = 0;

    public bool answer = true; // Başlangıçta false

    public void CompareObjects()
    {
        correctCount = 0; // Her seferinde sıfırdan başlasın

        for (int i = 0; i < 6; i++)
        {
            GameObject firstObj = GetObjectByIndex(i);
            GameObject secondObj = GetObjectByIndex(i + 6);

            int firstActiveIndex = GetActiveChildIndex(firstObj);
            int secondActiveIndex = GetActiveChildIndex(secondObj);

            if (firstActiveIndex == secondActiveIndex)
            {
                correctCount++; // Doğru eşleşme sayısını artır
            }
        }

        // Eğer tüm eşleşmeler doğruysa answer true olsun
        answer = (correctCount == 6);

        // Doğruluk sayısına göre objeleri güncelle
        UpdateStatusObjects(correctCount);
    }

    private GameObject GetObjectByIndex(int index)
    {
        switch (index)
        {
            case 0: return obj1;
            case 1: return obj2;
            case 2: return obj3;
            case 3: return obj4;
            case 4: return obj5;
            case 5: return obj6;
            case 6: return obj7;
            case 7: return obj8;
            case 8: return obj9;
            case 9: return obj10;
            case 10: return obj11;
            case 11: return obj12;
            default: return null;
        }
    }

    private int GetActiveChildIndex(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1; // Eğer aktif child bulunamazsa -1 döndür
    }

    private void UpdateStatusObjects(int correctCount)
    {
        // Önce tüm status objelerini kapat
        for (int i = 0; i < statusObjects.Count; i++)
        {
            statusObjects[i].SetActive(false);
        }

        // Ses seviyesini correctCount değerine göre belirle
        float volume = 0f;
        switch (correctCount)
        {
            case 1: volume = 0.25f; break;
            case 2: volume = 0.35f; break;
            case 3: volume = 0.45f; break;
            case 4: volume = 0.6f; break;
        }

        // Eğer doğru eşleşme varsa, ona karşılık gelen objeyi aktif et
        if (correctCount > 0 && correctCount < statusObjects.Count)
        {
            Sound trueSound = Array.Find(AudioManager.Instance.sounds, sound => sound.name == "True");
            if (trueSound != null)
            {
                trueSound.audioSource.volume = volume; // Ses seviyesini güncelle
                AudioManager.Instance.Play("True");
            }

            statusObjects[correctCount].SetActive(true);
            dogru.SetActive(true);
        }
        else if (correctCount == 0)
        {
            AudioManager.Instance.Play("False");
            statusObjects[correctCount].SetActive(true);
            dogru.SetActive(false);
        }
    }

}
