using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireSpreadController : MonoBehaviour{
    public float distanceForFireSpread;
    public bool isFireStarted;
    public float[] limitsOfScaler;
    public ParticleSystem[] fireParticle;
    public GameObject burntTree;
    public MeshRenderer aliveTreeMeshRenderer;
    public GameControl gameControl;
    public House[] houses;
    private float _completeBurnCooldown = 20f;
    private float _cooldownForFire = 5f;
    [Range(0,2)]
    private int _particleQueueNumber = 1;
    private bool _isExtinguished = false;
    private bool _isBurnt = false;

    void Start(){
        if (limitsOfScaler.Length != 2){
            Debug.LogError("Missing Limit Of Scaler array member");
        }else{
            TreeScaler(limitsOfScaler[0], limitsOfScaler[1]);
        }
        gameControl = FindObjectOfType<GameControl>();
        houses = FindObjectsOfType<House>();
    }

    private void Update(){
        if (_isBurnt || _isExtinguished){
            isFireStarted = false;
        }
        if (isFireStarted){
            if (!_isExtinguished && !_isBurnt){
                fireParticle[0].gameObject.SetActive(true);
            }
            _cooldownForFire -= Time.deltaTime;
            _completeBurnCooldown -= Time.deltaTime;
        }

        if (_completeBurnCooldown <= 0 && !_isExtinguished && !_isBurnt){
            burntTree.SetActive(true);
            aliveTreeMeshRenderer.enabled = false;
            fireParticle[4].gameObject.SetActive(true);
            fireParticle[0].gameObject.SetActive(false);
            fireParticle[1].gameObject.SetActive(false);
            fireParticle[2].gameObject.SetActive(false);
            gameControl.BurntTreeChecker();
            isFireStarted = false;
            gameControl.EndGameCheck();
            _isBurnt = true;
        }
    }

    void FixedUpdate(){
        if (_cooldownForFire <= 0f && isFireStarted && !_isExtinguished && !_isBurnt){
            SpreadFireChecker();
            _cooldownForFire = 5f;
            if (_particleQueueNumber < 3){
                fireParticle[_particleQueueNumber].gameObject.SetActive(true);
                _particleQueueNumber += 1;
            }
        }
    }
    
    private void SpreadFireChecker(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.right, out hit, distanceForFireSpread)){
            //print("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.name);
            HitTransform(hit.transform);
        }else if (Physics.Raycast(transform.position, (-Vector3.right + Vector3.down), out hit, distanceForFireSpread)){
            HitTransform(hit.transform);
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, distanceForFireSpread)){
            //print("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.name);
            HitTransform(hit.transform);
        }else if (Physics.Raycast(transform.position, (Vector3.right + Vector3.down), out hit, distanceForFireSpread)){
            HitTransform(hit.transform);
        }
        if (Physics.Raycast(transform.position, -Vector3.forward, out hit, distanceForFireSpread)){
            //print("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.name);
            HitTransform(hit.transform);
        }else if (Physics.Raycast(transform.position, (-Vector3.forward + Vector3.down), out hit, distanceForFireSpread)){
            HitTransform(hit.transform);
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, distanceForFireSpread)){
            //print("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.name);
            HitTransform(hit.transform);
        }else if (Physics.Raycast(transform.position, (Vector3.forward + Vector3.down), out hit, distanceForFireSpread)){
            HitTransform(hit.transform);
        }
    }
    
    //HitTransform Controller
    private void HitTransform(Transform hitTransform){
        if (hitTransform.GetComponent<FireSpreadController>()){
            hitTransform.GetComponent<FireSpreadController>().isFireStarted = true;
            foreach (var x in houses){
                x.SphereChecker();
            }
        }

        if (hitTransform.CompareTag("House")){
            gameControl.HouseIsBurning();
        }
    }
    
    
    //ağaçların boyunun değişmesini sağlıyoruz.
    private void TreeScaler(float limitBelow, float limitHigh){
        float randomScaler = Random.Range(limitBelow, limitHigh);

        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale = new Vector3(
            (localScale.x / randomScaler), 
            (localScale.y / randomScaler), 
            (localScale.z / randomScaler));
        transform1.localScale = localScale;
    }
    
    //ağaç yandıktan sonra yangın söndürülmesi
    public void IsFireExtinquishing(){
        if (!_isExtinguished && isFireStarted && _completeBurnCooldown > 0){
            isFireStarted = false;
            gameControl.EndGameCheck();
            foreach (var x in fireParticle){
                x.gameObject.SetActive(false);
            }
            fireParticle[3].gameObject.SetActive(true);
            _isExtinguished = true;

        }
    }
}