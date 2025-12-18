using UnityEngine;

namespace EndlessRunner
{
    public abstract class BlockBase : MonoBehaviour
    {
        [Header("Block Data")]
        [SerializeField] private LevelBlocksType blockType;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [Header("Movement")]
        [SerializeField] private float destroyDistance = 50f;

        protected float currentSpeed;
        protected Transform playerTransform;
        protected LevelManager levelManager;

        public Transform StartPoint => startPoint;
        public Transform EndPoint => endPoint;

        public virtual void Initialize(LevelManager manager, Transform player)
        {
            levelManager = manager;
            playerTransform = player;
            levelManager.OnSpeedChanged += OnSpeedChanged;
        }

        protected virtual void OnEnable()
        {
            if (levelManager != null)
                currentSpeed = levelManager.CurrentSpeed;
        }

        protected virtual void OnDisable()
        {
            if (levelManager != null)
                levelManager.OnSpeedChanged -= OnSpeedChanged;
        }

        protected virtual void OnSpeedChanged(float speed)
        {
            currentSpeed = speed;
        }

        protected virtual void Update()
        {
            if (GameConstants.currentGameState != GameState.Resume) return;

            MoveBlock();
            CheckForRecycle();
        }

        protected virtual void MoveBlock()
        {
            // Blocks move towards player
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        public float dist;
        protected virtual void CheckForRecycle()
        {
            if (playerTransform == null) return;

            dist = (playerTransform.localPosition.x - transform.localPosition.x);
            if (dist > destroyDistance)
            {
                gameObject.SetActive(false);
                levelManager.SpawnNextBlock();
            }
        }
    }
}
