using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private PlayerLook playerLook;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float smooth = 1f;
    [SerializeField] private float swayMultiplier = 1.5f;

    private void Awake()
    {
        playerLook = GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null || playerLook == null)
            return;
        float mouseX = playerLook.LookInput.x * swayMultiplier;
        float mouseY = playerLook.LookInput.y * swayMultiplier;
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        Transform weaponTransform = weapon.transform;
        weaponTransform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    public void ChangeWeapon(GameObject gameObject)
    {
        weapon = gameObject;
    }
}
