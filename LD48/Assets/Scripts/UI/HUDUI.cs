using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    public static HUDUI main;

    void Awake() {
        main = this;
    }

    [SerializeField]
    private GameObject hullPointer;
    [SerializeField]
    private List<GameObject> hullIndicators;
    [SerializeField]
    private GameObject pressurePointer;
    [SerializeField]
    private List<GameObject> pressureIndicators;
    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private UIWarningBlinker pressureBlinker;

    private GameManager gameManager;
    // 400 is the max depth the level should hit
    // added 5% for less stressful gameplay 8)
    // lol
    private readonly float maxDepth = 420f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartPressureWarning() {
        pressureBlinker.StartBlinking();
    }

    public void StopPressureWarning() {
        pressureBlinker.StopBlinking();
    }

    // Update is called once per frame
    void Update()
    {
        float hp = gameManager.GetHealth();
        float maxHp = gameManager.GetMaxHealth();
        float maxHpPool = 100;
        float eulerHPAngle = 180f * (hp / maxHpPool);

        RectTransform hullPointerTransform = hullPointer.GetComponent<RectTransform>();
        hullPointerTransform.localEulerAngles = new Vector3(0, 0, -eulerHPAngle);

        float depth = GameManager.main.PlayerDepth;
        float eulerPressure = 180f * (depth / maxDepth);

        RectTransform pressurePointerTransform = pressurePointer.GetComponent<RectTransform>();
        pressurePointerTransform.localEulerAngles = new Vector3(0, 0, -eulerPressure);

        float eulerMaxAngle = 180f * (maxHp / maxHpPool);

        for (int i = 0; i < hullIndicators.Count; i++)
        {
            GameObject indicator = hullIndicators[i];
            RectTransform indicatorTransform = indicator.GetComponent<RectTransform>();
            indicatorTransform.localEulerAngles = new Vector3(0, 0, -eulerMaxAngle);

            indicator = pressureIndicators[i];
            indicatorTransform = indicator.GetComponent<RectTransform>();
            indicatorTransform.localEulerAngles = new Vector3(0, 0, -eulerMaxAngle);
            eulerMaxAngle -= eulerMaxAngle / 3f;
        }

        moneyText.text = string.Format("{0:D6}", gameManager.GetMoney());
    }
}
