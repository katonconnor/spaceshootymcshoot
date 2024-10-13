using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

  public class GameManager : MonoBehaviour
    {
        public GameObject shieldUpgradePrefab; // Prefab for the shield upgrade
        public float upgradeSpawnTime = 10.0f; // Time interval between shield upgrades
        private float nextUpgradeSpawnTime = 0.0f; // Keeps track of when to spawn the next upgrade
        public GameObject[] enemyPrefabs; // Array to hold different enemy prefabs
        public float spawnTime = 1.5f;
        public float time = 0.0f;
        private float nextSpawnTime; // Keeps track of the next spawn time
        public float totalTime = 0.0f;
        public player player;
        public TextMeshProUGUI liveText;
        public TextMeshProUGUI shildText;
        public TextMeshProUGUI weaponText;
        public TextMeshProUGUI pointsText;
        public TextMeshProUGUI timeText;

        public int puntos = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabs = new GameObject[2];
        enemyPrefabs[0] = Resources.Load<GameObject>("asteroid");
        enemyPrefabs[1] = Resources.Load<GameObject>("fastroid");

        // Load the shield upgrade prefab
        shieldUpgradePrefab = Resources.Load<GameObject>("shieldup");
    }


    // Update is called once per frame
    void Update()
        {
            CreateEnemy();
            CreateShieldUpgrade(); // Add this to spawn shield upgrades
            UpdateCanvas();
            totalTime += Time.deltaTime;

        }
        void UpdateCanvas()
        {
            liveText.text = "vidas: " + player.lives;
            shildText.text = "shields: " + player.shield;
            weaponText.text = "arma: " + player.BulletPref.name;
            pointsText.text = "puntaje: " + puntos.ToString("f0");
            timeText.text = "time: " + totalTime.ToString("f0");
        }
    /* private void CreateEnemy()
     {
         time += Time.deltaTime;
         if (time > spawnTime)
         {
             Instantiate(enemyPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0), Quaternion.identity);
             time = 0.0f;
         }
     }
    */
    void CreateShieldUpgrade()
    {
        if (Time.time >= nextUpgradeSpawnTime)
        {
            // Set random spawn position, within screen bounds
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            Vector3 spawnPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0);

            // Instantiate the shield upgrade at the random position
            Instantiate(shieldUpgradePrefab, spawnPosition, Quaternion.identity);

            // Set the next time to spawn a shield upgrade
            nextUpgradeSpawnTime = Time.time + upgradeSpawnTime;
        }
    }
    void CreateEnemy()
    
         {
            if (Time.time >= nextSpawnTime)
            {
            // Choose a random enemy prefab from the array
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Random spawn position
                Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0);
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Spawn at the random position

               nextSpawnTime = Time.time + spawnTime; // Set the next spawn time
            }
    }

    public void AddScore(int value)
        {
            puntos += value;
        }

    }


