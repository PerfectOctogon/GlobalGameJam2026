using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskUIManager : MonoBehaviour
{
    public Image speedMaskImage;
    public Image doubleJumpMaskImage;
    public Image ultimateMaskImage;

    public Color activateColor;
    public Color deactivateColor;

    public void EnableSpeedMask()
    {
        speedMaskImage.gameObject.SetActive(true);
    }

    public void EnableDoubleJumpMask()
    {
        doubleJumpMaskImage.gameObject.SetActive(true);
    }

    public void EnableUltimateMask()
    {
        ultimateMaskImage.gameObject.SetActive(true);
    }
    
    public void ActivateSpeedMask()
    {
        DeactivateAllMasks();
        
        speedMaskImage.color = activateColor;
    }

    public void ActivateDoubleJumpMask()
    {
        DeactivateAllMasks();
        
        doubleJumpMaskImage.color = activateColor;
    }

    public void ActivateUltimateMask()
    {
        DeactivateAllMasks();
        
        ultimateMaskImage.color = activateColor;
    }

    void DeactivateAllMasks()
    {
        Color color = new Color(72, 46, 46);
        color.a = 0;
        
        speedMaskImage.color = deactivateColor;
        doubleJumpMaskImage.color = deactivateColor;
        ultimateMaskImage.color = deactivateColor;
    }
}
