using UnityEngine;
using System.Collections;

namespace EndlessRunner
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerInput swipeInput;

        [Header("Jump")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float jumpDuration = 0.4f;

        [Header("Slide")]
        [SerializeField] private float slideDuration = 0.6f;
        [SerializeField] private CapsuleCollider normalCollider;
        [SerializeField] private BoxCollider slideCollider;

        [Header("LaneChange")]
        [SerializeField] private float laneOffset = 2.2f;
        [SerializeField] private float laneChangeSpeed = 10f;

        [Header("Animation")]
        [SerializeField] private Animator animator;

        private Lane currentLane = Lane.Middle;
        private float targetZ;

        private bool isJumping;
        private bool isSliding;
        private float groundY;

        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int LeanLeftHash = Animator.StringToHash("LeanLeft");
        private static readonly int LeanRightHash = Animator.StringToHash("LeanRight");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int SlideHash = Animator.StringToHash("Slide");

        private void OnEnable()
        {
            swipeInput.OnUp += Jump;
            swipeInput.OnDown += Slide;
            swipeInput.OnLeft += MoveLeft;
            swipeInput.OnRight += MoveRight;
        }

        private void OnDisable()
        {
            swipeInput.OnUp -= Jump;
            swipeInput.OnDown -= Slide;
            swipeInput.OnLeft -= MoveLeft;
            swipeInput.OnRight -= MoveRight;
        }

        private void Start()
        {
            targetZ = transform.position.z;
            groundY = transform.position.y;
            slideCollider.enabled = false;
            animator.SetTrigger(IdleHash);
        }

        private void Update()
        {
            Vector3 pos = transform.position;
            pos.z = Mathf.MoveTowards(pos.z, targetZ, laneChangeSpeed * Time.deltaTime);
            transform.position = pos;
        }

        public void Jump()
        {
            if (isJumping || isSliding) return;
            StartCoroutine(JumpRoutine());
        }

        public void Slide()
        {
            if (isJumping || isSliding) return;
            StartCoroutine(SlideRoutine());
        }

        public void MoveLeft()
        {
            if (currentLane == Lane.Left) return;

            currentLane = currentLane == Lane.Middle ? Lane.Left : Lane.Middle;
            targetZ = currentLane == Lane.Left ? laneOffset : 0f;

            animator.SetTrigger(LeanRightHash);
        }

        public void MoveRight()
        {
            if (currentLane == Lane.Right) return;

            currentLane = currentLane == Lane.Middle ? Lane.Right : Lane.Middle;
            targetZ = currentLane == Lane.Right ? -laneOffset : 0f;

            animator.SetTrigger(LeanLeftHash);
        }

        private IEnumerator JumpRoutine()
        {
            isJumping = true;
            animator.SetBool(JumpHash, true);

            float t = 0f;
            while (t < jumpDuration)
            {
                float y = Mathf.Sin((t / jumpDuration) * Mathf.PI) * jumpHeight;
                transform.position = new Vector3(transform.position.x, groundY + y, transform.position.z);
                t += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            animator.SetBool(JumpHash, false);
            isJumping = false;
        }

        private IEnumerator SlideRoutine()
        {
            isSliding = true;
            animator.SetBool(SlideHash, true);

            normalCollider.enabled = false;
            slideCollider.enabled = true;

            yield return new WaitForSeconds(slideDuration);

            slideCollider.enabled = false;
            normalCollider.enabled = true;

            animator.SetBool(SlideHash, false);
            isSliding = false;
        }
    }
}
