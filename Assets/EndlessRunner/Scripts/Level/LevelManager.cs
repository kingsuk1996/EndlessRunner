using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Block Setup")]
        [SerializeField] private List<BlockBase> initialBlocks;
        [SerializeField] private List<BlockBase> repeatBlocks;
        [SerializeField] private float offset;

        [Header("Speed Settings")]
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] private int speedIncreaseInterval = 10;
        [SerializeField] private float speedIncreaseAmount = 0.5f;
        [SerializeField] private int maxSpeed = 20;

        [SerializeField] private Transform playerTransform;

        private BlockBase lastBlockInScene;
        private readonly List<BlockBase> pooledBlocks = new();
        private int poolIndex;
        private float currentSpeed;

        public Action<float> OnSpeedChanged;

        public float CurrentSpeed => currentSpeed;

        private void Awake()
        {
            currentSpeed = baseSpeed;

            SpawnInitialBlocks();
            CreateBlockPool();
            StartCoroutine(SpeedIncreaseRoutine());

            OnSpeedChanged?.Invoke(currentSpeed);
        }

        private void SpawnInitialBlocks()
        {
            foreach (var block in initialBlocks)
            {
                var instance = Instantiate(block, transform);
                instance.Initialize(this, playerTransform);

                Vector3 offset = instance.transform.position - instance.StartPoint.position;
                if (lastBlockInScene != null)
                    instance.transform.position = lastBlockInScene.EndPoint.position + offset;

                instance.gameObject.SetActive(true);
                lastBlockInScene = instance;
            }
        }

        private void CreateBlockPool()
        {
            foreach (var block in repeatBlocks)
            {
                var instance = Instantiate(block, transform);
                instance.Initialize(this, playerTransform);
                instance.gameObject.SetActive(false); 
                pooledBlocks.Add(instance);
            }
        }

        public void SpawnNextBlock()
        {
            if (pooledBlocks.Count == 0) return;

            var block = pooledBlocks[poolIndex];
            poolIndex = (poolIndex + 1) % pooledBlocks.Count;

            Vector3 newPos = lastBlockInScene.EndPoint.position;

            newPos.x += offset;

            block.transform.position = newPos;
            block.gameObject.SetActive(true);

            lastBlockInScene = block;
        }

        private IEnumerator SpeedIncreaseRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(speedIncreaseInterval);
                currentSpeed = Mathf.Min(currentSpeed + speedIncreaseAmount, maxSpeed);
                OnSpeedChanged?.Invoke(currentSpeed);
            }
        }
    }
}
