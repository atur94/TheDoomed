using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    void Awake()
    {
        _textMesh = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(Vector3 position, int damage, bool isCriticalStrike)
    {
        if (isCriticalStrike)
        {
            _textMesh.rectTransform.localScale = new Vector3(1.5f,1.5f);
            _textMesh.color = Color.red;
        }
        _textMesh.rectTransform.position = position + Vector3.up * 90f;
        _textMesh.SetText(damage.ToString());
        disappearTimer = DISSAPEAR_TIME;
    }

    private readonly float DISSAPEAR_TIME = 0.7f;
    private float disappearTimer;
    Vector3 moveVector = new Vector3(50,50);

    void Update()
    {
        _textMesh.rectTransform.position += moveVector * Time.deltaTime;
        if (moveVector.y > 0) moveVector -= new Vector3(16f, 16f) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer > DISSAPEAR_TIME * .5f)
        {
            _textMesh.rectTransform.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            if (_textMesh.rectTransform.localScale.y > 0.3f && _textMesh.rectTransform.localScale.x > 0.3f)
                _textMesh.rectTransform.localScale -= 1f * Time.deltaTime * Vector3.one;
            

        }
        if (disappearTimer < 0)
        {
            Color color = _textMesh.color;
            color.a -= Time.deltaTime;
            _textMesh.color = color;
            if(_textMesh.color.a < 0) Destroy(gameObject);
        }
    }

    public static DamagePopup Create(Vector3 position, int damageAmmount, bool isCriticalStrike = false)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponentInChildren<DamagePopup>();
        damagePopup.Setup(position, damageAmmount, isCriticalStrike);
        return damagePopup;

    }
}
