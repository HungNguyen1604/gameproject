using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHub : MonoBehaviour
{
    public TMP_Text hb_lb;
    public RectTransform tranz_rect;
    public Image hp_progress;
    private RectTransform parent_rect;
    private Transform anchor;
    private Camera cam;
    private float time_show = 0;
    private Tweener tw;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameObject.SetActive(false);
    }
    public void Init(RectTransform parent, Transform anchor)
    {
        this.parent_rect = parent;
        transform.SetParent(parent, true);
        this.anchor = anchor;
    }

    public void UpdateDamage(int hp, int total_hp)
    {
        gameObject.SetActive(true);
        hb_lb.text = hp.ToString() + "/" + total_hp.ToString();
        float val = (float)hp / (float)total_hp;
        if (tw != null)
            tw.Kill();
        tw = DOTween.To(() => hp_progress.fillAmount, x => hp_progress.fillAmount = x, val, 0.5f);
        time_show = 0.5f;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        time_show -= Time.deltaTime;
        gameObject.SetActive(time_show > 0);

        Vector2 screen_pos = cam.WorldToScreenPoint(anchor.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent_rect, screen_pos, null, out var anchor2d);
        tranz_rect.anchoredPosition = anchor2d;

    }
}
