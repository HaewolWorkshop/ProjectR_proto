using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 targetDistances;

    private Vector3 velocity;
    private float lastYPos;    

    private void Update()
    {
        var target = -player.transform.InverseTransformDirection(player.rigidbody.velocity);

        target.x *= targetDistances.x;
        target.y *= targetDistances.y;
        target.z *= targetDistances.z;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref velocity, smoothTime * Time.deltaTime);

        if (!player.isGrounded)
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
