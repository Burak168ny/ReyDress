using UnityEngine;

public class ColorToken : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // Rengi değişecek olan obje
    [SerializeField] private GameObject parentObject; // Çocukları kontrol edeceğimiz obje
    [SerializeField] private string[] hexColors; // Hex renk kodları

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // SpriteRenderer bileşenini al
        if (targetObject != null)
        {
            spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
        }

        // Rengi güncelle
        UpdateColor();
    }

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (parentObject == null || spriteRenderer == null || hexColors.Length == 0)
            return;

        // Aktif olan ilk çocuğu bul
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            GameObject child = parentObject.transform.GetChild(i).gameObject;
            if (child.activeSelf && i < hexColors.Length)
            {
                spriteRenderer.color = HexToColor(hexColors[i]);
                return;
            }
        }
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.white; // Hata olursa beyaz renk ata
    }
}
