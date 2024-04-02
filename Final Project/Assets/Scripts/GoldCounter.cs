using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCounterText;
    public static GoldCounter singleton;
    private int goldCollected = 0;
    public ImageUpdater imageUpdater;


    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this.gameObject);
        }
        singleton = this;
    }

    public void RegisterLoot(int point = 1)
    {
        goldCollected += point;
        goldCounterText.text = goldCollected.ToString();

        imageUpdater.UpdateImage(goldCollected);
    }
}
