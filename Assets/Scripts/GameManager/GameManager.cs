using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerBehavior player;
    public Player player1 {  get; private set; }
    private void Awake()
    {
        CreatePlayer1();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CreatePlayer1()
    {
        player1 = new();
        player = Instantiate(player, player1.SpawnPosition, Quaternion.identity);
        player.InitData(player1);
    }
}