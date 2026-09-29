using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private PlayerLook playerLook;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float swayClamp = 0.09f;
    [SerializeField] private float smooth = 1f;

    private Vector3 origin;
    

    private void Awake()
    {
        playerLook = GetComponent<PlayerLook>();

        origin = weapon.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null || playerLook == null)
            return;
        float mouseX = playerLook.LookInput.x;
        float mouseY = playerLook.LookInput.y;
        mouseX = Mathf.Clamp(mouseX, -swayClamp, swayClamp);
        mouseY = Mathf.Clamp(mouseY, -swayClamp, swayClamp);

        Vector3 target = new Vector3 (mouseX, mouseY, 0f);

        weapon.transform.localPosition = Vector3.Lerp(weapon.transform.localPosition, target + origin, Time.deltaTime * smooth);
    }

    public void ChangeWeapon(GameObject gameObject)
    {
        weapon = gameObject;
    }
}
