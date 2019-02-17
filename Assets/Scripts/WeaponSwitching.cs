using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public RifleScr rifle;
    public ShotgunScr shotgun;
    public int selectedWeapon = 0;
    public CanvasGroup gunDisplay;
    public GameObject muzzleFlash;
  

    // Use this for initialization
    void Start ()
    {
        SelectWeapon();
	}
	
	// Update is called once per frame
	void Update ()
    {

        int previousSelectedWeapon = selectedWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && shotgun.shotgunAvailable == true)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && rifle.rifleAvailable == true)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                gunDisplay.GetComponent<CanvasGroup>().alpha = 1;

             
            }
            else
            {
          
                weapon.gameObject.SetActive(false);
               

            }
            i++;
        }
    }
}
