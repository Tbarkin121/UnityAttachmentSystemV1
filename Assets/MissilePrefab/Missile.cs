using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public int damageRolloff = 4;
    public float blastRadius = 0.24f;
    public float bodyWidth = 0.02f;
    public float bodyHeight = 0.08f;
    public Rigidbody2D rb;
    private float first_stage_counter = 1f;
    private float second_stage_counter = 1f;
    private bool stage_one_done = false;
    private bool stage_two_start = false;
    private bool missile_fuel_empty = false;
    private bool armed = false;
    GameObject bodyCollider;
    GameObject blastCollider;
    public ParticleSystem smokePrefab;
    // Start is called before the first frame update
    void Start()
    {
        createBlastCollider(blastRadius);
        createBodyCollider(bodyWidth, bodyHeight);
        // rb.AddForce(transform.right*10);
        Destroy(gameObject,5f);
        first_stage_counter = Random.Range(0.5f, first_stage_counter);
    }
    private void createBlastCollider(float _radius)
    {
        
        blastCollider = new GameObject("Blast Collider");
        blastCollider.transform.SetParent(transform);
        blastCollider.transform.localPosition = Vector3.zero;
        blastCollider.transform.rotation = Quaternion.identity;
        // blastCollider.tag = "VehiclePart";
        CircleCollider2D _collider = blastCollider.AddComponent<CircleCollider2D>();
        _collider.radius = _radius;
        _collider.isTrigger = true;
        CollisionManager _colliderManager = blastCollider.AddComponent<CollisionManager>();
        _colliderManager.parent = gameObject;
        _colliderManager.colliderType = ColliderType.BlastRadius;
        
    }
    private void createBodyCollider(float width, float height)
    {

        bodyCollider = new GameObject("Body Collider");
        bodyCollider.transform.SetParent(transform);
        bodyCollider.transform.localPosition = Vector3.zero;
        bodyCollider.transform.rotation = Quaternion.identity;
        // bodyCollider.tag = "VehiclePart";
        BoxCollider2D _collider = bodyCollider.AddComponent<BoxCollider2D>();
        _collider.size = new Vector2(width, height);
        _collider.isTrigger = true;
        CollisionManager _colliderManager = bodyCollider.AddComponent<CollisionManager>();
        _colliderManager.parent = gameObject;
        _colliderManager.colliderType = ColliderType.Body;
    }
    public void BodyCollisionReport(Collider2D _hitInfo)
    {
        List<Collider2D> _blastHits = blastCollider.GetComponent<CollisionManager>().hits;
        // Debug.Log("Body Collision Report!");
        // Debug.Log(_hitInfo.name);
        if(armed)
        {
            armed=false;
            Debug.Log("Blast Radius Count : " + _blastHits.Count);
            int i = 0;
            foreach(Collider2D x in _blastHits)
            {
                HealthMonitor _healthMonitor = new HealthMonitor();
                if(x != null)
                {
                    _healthMonitor = x.GetComponent<HealthMonitor>();
                }
                
                if( _healthMonitor != null)
                {
                    // Debug.Log("transform.position : " + i + " " + transform.position);
                    // Debug.Log("Collider2d.position : " + i + " " + x.GetComponent<Transform>().position);
                    i++;
                    float _dist = Vector3.Distance(transform.position, x.GetComponent<Transform>().position);
                    float modifier = Mathf.Clamp(_dist/(damageRolloff*blastCollider.GetComponent<CircleCollider2D>().radius),0 ,1);
                    int modifiedDamage = (int)((float)damage * (1f - modifier) );
                    _healthMonitor.TakeDamage(modifiedDamage);
                    Debug.Log("Damage Given :" + modifiedDamage + ". Distance :" + _dist);
                    
                }
            }
            Instantiate(smokePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }     
    }
    void Update()
    {
        if(!stage_one_done)
        {
            StartCoroutine(StageOne());
        }
        if(stage_one_done & !stage_two_start)
        {
            rb.velocity = rb.velocity*0.1f;
            stage_two_start = true;
            armed = true;
            StartCoroutine(StageTwo());
        }
        
    }
    public IEnumerator StageOne()
    {
        float counter = 0f;
        while(counter<first_stage_counter)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        stage_one_done = true;
        
    }
    public IEnumerator StageTwo()
    {
        float counter = 0f;
        while(counter<second_stage_counter)
        {
            counter += Time.deltaTime;
            rb.AddForce(transform.up*speed);
            yield return null;
        }
        missile_fuel_empty = true;
        
    }

    
    
}



