
using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{

    private Transform camara;
    private Vector3 previosCamaraPosition;
    [SerializeField] float parallaxMultiplayer;
    private float spriteWidth, startPosition;


    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main.transform;
        previosCamaraPosition = camara.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (camara.position.x - previosCamaraPosition.x)* parallaxMultiplayer;
        float moveAmount = camara.transform.position.x * (1 - parallaxMultiplayer);

        transform.Translate(new Vector3(deltaX, 0, 0));
        previosCamaraPosition = camara.position;

        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }else if (moveAmount < startPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            startPosition -= spriteWidth;
        }
    }
}
