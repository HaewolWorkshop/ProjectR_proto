using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxDownDist;
    [SerializeField] private float maxUpDist;
    [SerializeField] private Vector3 targetDistances;

    private Vector3 velocity;
    private float lastYPos;

    private void Awake()
    {
        lastYPos = transform.position.y;
    }

    private void Update()
    {
        var target = -player.transform.InverseTransformDirection(player.rigidbody.velocity);

        target.x *= targetDistances.x;
        target.y *= targetDistances.y;
        target.z *= targetDistances.z;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref velocity, smoothTime * Time.deltaTime);

        var dist = transform.parent.position.y - transform.position.y;
        var isOut = Mathf.Abs(dist) > (dist < 0 ? maxDownDist : maxUpDist);

        if (!player.isGrounded && !isOut)
        {
            var position = transform.position;
            position.y = lastYPos;
            transform.position = position;
        }
        else
        {
            lastYPos = transform.position.y;
        }
    }
}
