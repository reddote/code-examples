using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShipController : MonoBehaviour{
    [Header("Object Class")] public RectTransform paddleUI;
    public Slider gps;

    [Header("Ship Upgrade Things")] public GameObject[] allPaddles;
    public int levelOfPaddle;
    public int numberOfPeople;


    [Header("Numericals")] public float shipSpeed;
    public float shipSmoothStopSpeed;
    public int paddleHealth;
    [Header("Paddle hareketi kabul edeceği min ve max değerler 0-360 arasında")]
    [Range(1,180)]
    public float paddleMin;
    [Range(181,360)]
    public float paddleMax;
    private float _shipBackwardSpeed;


    [Header("BOUNCY VARIABLES")]
    //bouncy variables
    public float waterLevel;

    public float floatHeight;
    public float bounceDamp;
    public Vector3 bouncyCenterOffset;

    //private bouncy variables
    private float _shakeShipCoolDown = 5f;
    private float _forceFactor;
    private Vector3 _actionPoint;
    private Vector3 _uplift;

    private Rigidbody _shipRigidB;
    //end of bouncy variables

    private void Start(){
        _shipRigidB = GetComponent<Rigidbody>();
        _shipBackwardSpeed = shipSmoothStopSpeed;
        gps.maxValue = MapSpawner.instance.secondIslandSpawnPoint.position.z;
        ShipPaddleVisualUpdater();
    }

    private void Update(){
        Bouncer();
        if (SaveGame.control.isGameStarted && !SaveGame.control.isGameEnded){
            ShipPusher();
        }
    }

    private void FixedUpdate(){
        if (SaveGame.control.isGameStarted && !SaveGame.control.isGameEnded){
            ShipStopper();
        }     
    }

    //Ship Controller
    private void ShipPusher(){
        if (Input.GetMouseButtonDown(0) && PaddleRotateChecker()){
            ShipMovement(true);
            _shipBackwardSpeed = shipSmoothStopSpeed;
        } else if (Input.GetMouseButtonDown(0) && !PaddleRotateChecker()){
            paddleHealth -= 1;
        } else{
            _shipBackwardSpeed -= Time.deltaTime / 5f;
            if (_shipBackwardSpeed <= 0.1f){
                _shipBackwardSpeed = 0.1f;
            }
        }

        gps.value = transform.position.z;
    }

    public void ShipPaddleVisualUpdater(){
        for (int i = 0; i <= levelOfPaddle; i++){
            allPaddles[i].SetActive(true);
        }
        shipSpeed = shipSpeed + (levelOfPaddle * 10f);
    }

    //ship slow down time by time
    private void ShipStopper(){
        if (!Input.GetMouseButtonDown(0)){
            _shipRigidB.velocity = _shipRigidB.velocity * (_shipBackwardSpeed);
        }
    }
    
    //paddle UI rotation kontrol ediyor.
    private bool PaddleRotateChecker(){
        if (paddleUI.localEulerAngles.z >= paddleMin && paddleUI.localEulerAngles.z <= paddleMax){
            return true;
        } else{
            return false;
        }
    }

    //Ship Move Controller
    private void ShipMovement(bool isClickedRight){
        if (isClickedRight){
            _shipRigidB.AddForce(new Vector3(0, 0, shipSpeed), ForceMode.Impulse);
        }
    }

    //ship bouncer
    private void Bouncer(){
        bouncyCenterOffset.y = Random.Range(0.02f, 0.2f);
        
        if (_shakeShipCoolDown >= 0){
            bouncyCenterOffset.x = Random.Range(0.001f, 0.005f);
            _shakeShipCoolDown -= Time.deltaTime;
        } else if (_shakeShipCoolDown <= 0){
            bouncyCenterOffset.x = Random.Range(-0.001f, -0.005f);
            _shakeShipCoolDown -= Time.deltaTime;
            if (_shakeShipCoolDown <= -5f){
                _shakeShipCoolDown = 5f;
            }
        }

        //bouncyCenterOffset.z = Random.Range(0.01f, 0.05f);
        _actionPoint = transform.position + transform.TransformDirection(bouncyCenterOffset);
        _forceFactor = 1f - ((_actionPoint.y - waterLevel) / floatHeight);
        if (_forceFactor > 0f){
            _uplift = -Physics.gravity * (_forceFactor - _shipRigidB.velocity.y * bounceDamp);
            _shipRigidB.AddForceAtPosition(_uplift, _actionPoint);
        }
    }
    
    //Collider Things
    public void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Island")){
            SaveGame.control.UnboardPeopleOnShip(numberOfPeople);
        }
    }
}