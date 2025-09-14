using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Move
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);

        // Rotate
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        Vector3 lookAt = new Vector3(0, mouseX, 0);
        transform.Rotate(lookAt);

        // Kotiteht‰v‰: Toteuta pelaajalle hyppyominaisuus. Vaikuta Rigidbody linearvelocity y:n arvoon. 
        // Tee hahmolle mahdollisuus tuplahyppyyn, mutta ei enemp‰‰. Toka hyppy on hieman heikompi kuin ensimm‰inen.

    }
}
