using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitEffectUI : MonoBehaviour
{
    public Image hitImage;
    public float flashTime = 0.15f; // thời gian nhấp
    public float maxAlpha = 0.4f;

    void Start()
    {
        if (hitImage == null)
            hitImage = GetComponent<Image>();

        gameObject.SetActive(false); // ✅ BAN ĐẦU TẮT
    }

    public void Flash()
    {
        StopAllCoroutines();          // ✅ tránh chồng hiệu ứng
        gameObject.SetActive(true);  // ✅ BẬT LÊN KHI BỊ ĐÁNH
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        Color c = hitImage.color;
        c.a = maxAlpha;
        hitImage.color = c;

        yield return new WaitForSeconds(flashTime); // giữ đỏ 1 chút

        c.a = 0f;
        hitImage.color = c;

        yield return new WaitForSeconds(0.05f);

        gameObject.SetActive(false); // ✅ TẮT LẠI
    }
}
