    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using TMPro;



    public class enemy_grid : MonoBehaviour
    {
        public enemy[] prefabs; 

        public int rows = 5;

        public int columns = 11;

        public AnimationCurve speed;

        public projectile missilePrefab;

        public float missileAttackRate = 1.0f;

    TextMeshProUGUI scoreText;

        public int score = 0;
        public int enemyKilled { get; private set; }

        public int amountAlive;
        public int totalEnemy => rows * columns;
        public float percentKilled => (float)enemyKilled / (float)totalEnemy;

        private Vector3 _direction = Vector2.right;
        // Start is called before the first frame update
        void Start()
        {

        scoreText = FindObjectOfType<TextMeshProUGUI>();

        for (int row = 0; row < rows; row++)
            {
                float width = 2.0f * (columns - 1);
                float height = 2.0f * (rows - 1);
                Vector3 centering = new Vector2(-width / 2, -height / 2);
                Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2), 0);
                for (int col = 0; col < columns; col++)
                {
                    enemy enemy = Instantiate(prefabs[row], transform);
                    enemy.killed += EnemyKilled;
                    Vector3 position = rowPosition;
                    position.x += col * 2;
                    enemy.transform.localPosition = position;
                }
            
            }

            InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);

        }
    



        // Update is called once per frame
        void Update()
        {
        amountAlive = totalEnemy - enemyKilled;
        scoreText.text = "amount alive: " + amountAlive;

            transform.position += _direction * speed.Evaluate(percentKilled) * Time.deltaTime;

            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        


            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    continue;
                }
                if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x) - 1)
                {
                    AdvanceRow();
                }
                else if (_direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1))
                {
                    AdvanceRow();
                }
            
            
            }
        }
        private void AdvanceRow()
        {
            _direction.x *= -1;

            Vector3 position = transform.position;
            position.y -= 1;
            transform.position = position;
        }

        private void EnemyKilled()
        {
            enemyKilled++;
    
        }

        private void MissileAttack()
        {
            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (Random.value <(1 / (float)amountAlive)) 
                {
                    Instantiate(missilePrefab, enemy.position, Quaternion.identity);
                    break;
                }

            }

        }
    }
