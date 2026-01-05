using System;
using System.Collections;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private Enemy enemyMovement;
    [SerializeField]
    private Transform enemySpawn;

    [SerializeField]
    private GameObject playerPrefab;
    private PlayerMovement playerMovement;
    [SerializeField]
    private Transform playerSpawn;


    public event Action<int> ChangeCountdown;

    private void Start()
    {
        StartGame();
    }

    private void SpawnPlayers()
    {
        var player = Instantiate(playerPrefab, enemySpawn.position + Vector3.up, Quaternion.identity);
        var enemy = Instantiate(enemyPrefab, playerSpawn.position + Vector3.up, Quaternion.identity);

        playerMovement = player.GetComponent<PlayerMovement>();
        enemyMovement = enemy.GetComponent<Enemy>();

        enemyMovement.targetPlayer = player;

    }

    public void StartGame()
    {
        SpawnPlayers();
        StartCoroutine(countdownRoutine()); 
    }

    IEnumerator countdownRoutine()
    {
        for (int i = 0; i < 4; i++) {
            ChangeCountdown?.Invoke(i);
            yield return new WaitForSeconds(1f);
        }

        ChangeCountdown?.Invoke(-1);

        playerMovement.canMove = true;
        enemyMovement.canMove = true;
    }
}
