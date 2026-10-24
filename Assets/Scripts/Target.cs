using UnityEngine;
using UnityEngine.InputSystem;
public class Target : MonoBehaviour
{
    private Rigidbody TargetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 3;
    private float xRange = 4;
    private float ySpawnPos = -3;

    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       TargetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       
       TargetRb.AddForce(randomForce(), ForceMode.Impulse); 
       TargetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse   );

       transform.position = randomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin,ray.direction * 100f, Color.red, 2f);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.transform == transform){
                    Destroy(gameObject);

                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

                    gameManager.UpdateScore(pointValue);
                }
            }
        }
    }

    private Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    } 
    float RandomTorque(){

        return Random.Range(-maxTorque,maxTorque); 
    }
    Vector3 randomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos);
    }

 
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
            if(!gameObject.CompareTag("Bad"))
            {

                gameManager.GameOver();

            }
            
        }
    }
}
