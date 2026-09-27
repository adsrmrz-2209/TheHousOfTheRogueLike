using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public PlayerData player1Data {  get; private set; }
    private void Awake()
    {
        CreatePlayer1();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CreatePlayer1()
    {
        player1Data = new();
        player = Instantiate(player, player1Data.SpawnPosition, Quaternion.identity);
        player.InitData(player1Data);
    }
}