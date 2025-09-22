using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonAnimator : MonoBehaviour
{
    public GameObject againButton; // Inspector'dan atanacak
    public GameObject againText; // Inspector'dan atanacak
    public GameObject curtainQ;
    public GameObject curtainA;
    public Animator CloseClodeQAnimator;
    public Animator CloseClodeAAnimator;

    private const string Q_CLOSE_ANIM_NAME = "CloudQClose";
    private const string A_CLOSE_ANIM_NAME = "CloudClose";
    private const float ANIMATION_DURATION = 1f; // 1 saniye bekleme süresi

    public List<GameObject> sixStarObjects = new List<GameObject>(); // Yeni liste


    public void AgainButtonQ()
    {
        AudioManager.Instance.Stop("Firework");
        AudioManager.Instance.Stop("Firework2");

        // Yeni liste içindeki objeleri pasif hale getir
        foreach (var obj in sixStarObjects)
        {
            obj.SetActive(false);
        }
        AudioManager.Instance.Play("Again");

        againButton.SetActive(false);
        againText.SetActive(false);
        curtainQ.SetActive(true);

        CloseClodeQAnimator.Play(Q_CLOSE_ANIM_NAME);
        StartCoroutine(DisableAfterDelay(curtainQ, CloseClodeQAnimator));
    }

    public void AgainButtonA()
    {
        curtainA.SetActive(true);
        CloseClodeAAnimator.Play(A_CLOSE_ANIM_NAME);
        StartCoroutine(DisableAfterDelay(curtainA, CloseClodeAAnimator));
    }

    private IEnumerator DisableAfterDelay(GameObject curtain, Animator animator)
    {
        yield return new WaitForSeconds(ANIMATION_DURATION);

        // Eğer animasyonun tamamen bittiğinden emin olmak istersen AnimatorStateInfo kullanabilirsin.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}
