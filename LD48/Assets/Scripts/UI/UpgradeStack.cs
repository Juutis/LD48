using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStack : MonoBehaviour
{
    [SerializeField]
    UpgradeConfig config;
    [SerializeField]
    UpgradeType type;
    [SerializeField]
    GameObject upgradeTicketPrefab;

    private List<Upgrade> upgrades;
    private Stack<UpgradeTicket> tickets = new Stack<UpgradeTicket>();
    private float minOffset = 2f;
    private float maxOffset = 4f;
    private float minAngle = -10f;
    private float maxAngle = 10f;
    private UpgradeUI upgradeUI;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case UpgradeType.damage:
                upgrades = config.damage;
                break;
            case UpgradeType.attackSpeed:
                upgrades = config.attackSpeed;
                break;
            case UpgradeType.movementSpeed:
                upgrades = config.movementSpeed;
                break;
            case UpgradeType.lights:
                upgrades = config.lights;
                break;
            case UpgradeType.health:
                upgrades = config.health;
                break;
        }

        float x = transform.position.x;
        float y = transform.position.y;

        foreach (Upgrade u in upgrades.AsEnumerable().Reverse())
        {
            GameObject upgradeTicket = Instantiate(upgradeTicketPrefab);
            upgradeTicket.transform.parent = transform;

            upgradeTicket.transform.position = new Vector3(x, y, 0);
            // set offset for the next ticket
            x = x + Random.Range(minOffset, maxOffset);
            y = y - Random.Range(minOffset, maxOffset);

            float randomAngle = Random.Range(minAngle, maxAngle);
            upgradeTicket.transform.Rotate(Vector3.back, randomAngle);

            UpgradeTicket ticket = upgradeTicket.GetComponent<UpgradeTicket>();
            ticket.Initialize(u.description, u.loreText, u.price, u.value);
            tickets.Push(ticket);

            Button ticketButton = upgradeTicket.GetComponent<Button>();
            ticketButton.onClick.AddListener(delegate { BuyTopTicket(); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTopTicket()
    {
        if (upgradeUI == null)
        {
            Debug.Log("UpgradeStack " + transform.name + " doesn't have UpgradeUI reference! Can't buy!");
            return;
        }

        UpgradeTicket ticket = tickets.Peek();
        if (upgradeUI.GetMoney() >= ticket.GetPrice())
        {
            upgradeUI.Upgrade(ticket.GetValue(), type);
            tickets.Pop();
            Destroy(ticket.gameObject);
            upgradeUI.ReduceMoney(ticket.GetPrice());
        }
        else
        {
            // TODO: indicate, can't buy!
        }
    }

    public UpgradeType GetUpgradeType()
    {
        return type;
    }

    public void SetUpgradeUI(UpgradeUI ui)
    {
        Debug.Log(this);
        upgradeUI = ui;
    }
}

public enum UpgradeType
{
    damage,
    attackSpeed,
    movementSpeed,
    lights,
    health
}
