using System.Collections.Generic;
using UnityEngine;

public class Simple : MonoBehaviour
{
    public GameObject[] objects = new GameObject[6]; // 6 obje referansı
    public bool different = true; // Başlangıçta true olarak ayarla
    public bool sixStarsSound = false;

    void Start()
    {
        different = CheckChildren();
    }

    bool CheckChildren()
    {
        Dictionary<int, int> activeChildPositions = new Dictionary<int, int>(); // (Child Index, Hangi obje)

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null) continue;

            Transform parent = objects[i].transform;

            for (int j = 0; j < parent.childCount; j++)
            {
                if (parent.GetChild(j).gameObject.activeSelf)
                {

                    // Eğer zaten bu sırada bir aktif obje varsa, hata!
                    if (activeChildPositions.ContainsKey(j))
                    {
                        return false;
                    }

                    activeChildPositions[j] = i; // O indexteki aktif child'ı kaydet
                }
            }
        }
        
        sixStarsSound = true;
        return true;
    }

    void Update()
{
    if(!CheckChildren())
    {
        AudioManager.Instance.Play("SixStars");
        sixStarsSound = false;
    }
    different = CheckChildren();
}

}
