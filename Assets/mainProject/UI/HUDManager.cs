using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text bulletCountText;
    public Image reloadIndicator;

    private void OnEnable()
    {
        EventManager.StartListening(UIEvent.OnAmmoChanged, UpdateBulletCount);
        EventManager.StartListening(GameEvent.OnWeaponReload, ShowReloadIndicator);
    }

    private void OnDisable()
    {
        EventManager.StopListening(UIEvent.OnAmmoChanged, UpdateBulletCount);
        EventManager.StopListening(GameEvent.OnWeaponReload, ShowReloadIndicator);
    }

    private void UpdateBulletCount(object parameter)
    {
        int currentBullets = (int)parameter;
        bulletCountText.text = currentBullets.ToString();
    }

    private void ShowReloadIndicator(object parameter)
    {
        float reloadTime = (float)parameter;
        reloadIndicator.fillAmount = 0;
        StartCoroutine(ReloadIndicatorCoroutine(reloadTime));
    }

    private IEnumerator ReloadIndicatorCoroutine(float reloadTime)
    {
        float elapsed = 0;
        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            reloadIndicator.fillAmount = elapsed / reloadTime;
            yield return null;
        }
        reloadIndicator.fillAmount = 1;
    }
}
