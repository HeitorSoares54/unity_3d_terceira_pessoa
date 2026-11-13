using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] RawImage imgR;
    private Rect aux;
    [SerializeField] private int coin_counter = 0;
    [SerializeField] private TMP_Text txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aux = new Rect(imgR.uvRect);

    }

    private void Update()
    {
        aux.y += 0.1f * Time.deltaTime;
        imgR.uvRect = aux;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("coin"))
        {
            Destroy(col.gameObject);
            coin_counter += 10;
            txt.text = coin_counter.ToString();
        }
    }
}
