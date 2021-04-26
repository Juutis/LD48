using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : UIPopup
{

    private WaterEffect waterEffect;
    private PlayerControls playerControls;
    [SerializeField]
    private UISwitch waterEffectSwitch;

    [SerializeField]
    private UISwitch verticalAxisSwitch;

    public override void Init(Transform container, UIPopupManager manager)
    {
        base.Init(container, manager);
        playerControls = FindObjectOfType<PlayerControls>();
        waterEffect = GameObject.FindObjectOfType<WaterEffect>();
        waterEffectSwitch.Init(waterEffect.Status);
        verticalAxisSwitch.Init(playerControls.YAxisInverted);
    }

    public void ToggleWaterEffect() {
        waterEffect.Toggle();
    }

    public void ToggleYAxis() {
        playerControls.ToggleYAxis();
    }

}
