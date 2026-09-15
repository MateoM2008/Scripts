using UnityEngine;

public class CameraFlow : MonoBehaviour
{

    public Transform target;
    public float smootSpeed=5f;
    public Vector3 offset= new Vector3(0,1,-10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate(){
        if(target != null){
            Vector3 desiredPosition =target.position + offset;

            Vector3 smoothePosition = Vector3.Lerp(transform.position, desiredPosition, smootSpeed * Time.deltaTime);

            transform.position = smoothePosition;
        }
    }
}
