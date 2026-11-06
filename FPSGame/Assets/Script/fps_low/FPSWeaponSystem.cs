using UnityEngine;
using UnityEngine.InputSystem;

public enum WeapomType
{
    Pistol,
    Rifle,
    Shotgun,
}
public class FPSWeaponSystem : MonoBehaviour
{
   
    public WeapomType currentWeapon = WeapomType.Pistol;

    public int[] maxAmmoCounts = { 10, 30, 8 };
    public int[] currentAmmoCounts = { 10, 30, 8 };

    private void start()
    {
        currentWeapon = WeapomType.Rifle;
    }
   

    // Update is called once per frame
    void Update()
    {

        if(Keyboard.current.digit1Key.wasPressedThisFrame) currentWeapon = WeapomType.Pistol;
        if(Keyboard.current.digit2Key.wasPressedThisFrame) currentWeapon = WeapomType.Rifle;
        if(Keyboard.current.digit3Key.wasPressedThisFrame) currentWeapon = WeapomType.Shotgun;

        float scrollY = Mouse.current.scroll.y.ReadValue();

        if (scrollY != 0f)
        {
            int totalWeapons = 3;
            int currentIndex = (int)currentWeapon;

            if (scrollY > 0f)
            {
                currentIndex = (currentIndex + 1) % totalWeapons;
            }
            else if (scrollY < 0f)
            {
                currentIndex = (currentIndex - 1 + totalWeapons) % totalWeapons;
            }

            currentWeapon = (WeapomType)currentIndex;
            Debug.Log($"[무기 교체] 현재 무기: {currentWeapon}");

        }

        if (Mouse.current.leftButton.wasPressedThisFrame) FireWeapon();
        if (Keyboard.current.rKey.wasPressedThisFrame) ReloadAllWeapons();
    }

    void FireWeapon()
    {
        int weaponIndex = (int)currentWeapon;
        if (currentAmmoCounts[weaponIndex] <= 0)
        {
            Debug.Log($"{currentWeapon}의 탄약이 없습니다! R키를 누르세요.");
            return;
        }

        currentAmmoCounts[weaponIndex]--;
        switch (currentWeapon)
        {
            case WeapomType.Pistol:
                Debug.Log($"[권총] 탕! (남은 탄약: {currentAmmoCounts[weaponIndex]}발");
                break;
            case WeapomType.Rifle:
                Debug.Log($"[소총] 탕! (남은 탄약: {currentAmmoCounts[weaponIndex]}발");
                break;
            case WeapomType.Shotgun:
                Debug.Log($"[샷건] 탕! (남은 탄약: {currentAmmoCounts[weaponIndex]}발");
                break;
        }


    }

    void ReloadAllWeapons()
    {
        Debug.Log("전채 무기 재장전 시작");
        for (int i = 0; i < currentAmmoCounts.Length; i++)
        {
            currentAmmoCounts[i] = maxAmmoCounts[i];

        }
        Debug.Log("모든 무기 재장전 완료");
    }
}
