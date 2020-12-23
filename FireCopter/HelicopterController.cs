using System.Collections;
using System.Collections.Generic;
using EpicToonFX;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public float helicopterMovementSpeed;
    public float rotationSpeedOfHelicopter;
    public float helicopterMovementAnimationRotation;
    public bool isStopped;
    public FloatingJoystick virtualJoystick;
    public float waterFill;
    public Transform childHelicopter;
    public GameObject[] waterDripping;
    public GameObject[] waterShader;
    private float _waterFillSpeed = 2f;
    private float _xRotofHeli = 0f;
    private float _zRotofHeli = 0f;

    
    void Start() {
        DrippingWaterParticle();
    }

    void Update(){
        PlayerController();
        WaterCarrier();
        DrippingWaterParticle();
        ShaderOnBalloon();
    }

    //balon içindeki su shaderi ayarlayan
    private void ShaderOnBalloon(){
        if (waterFill > 75){
            waterShader[0].SetActive(true);//water shader %100 dolu
            waterShader[1].SetActive(false);//water shader %50 dolu
            waterShader[2].SetActive(false);//water shader %25 dolu
        }else if (waterFill > 50){
            waterShader[0].SetActive(false);
            waterShader[1].SetActive(true);
            waterShader[2].SetActive(false);
        }else if (waterFill >25){
            waterShader[0].SetActive(false);
            waterShader[1].SetActive(false);
            waterShader[2].SetActive(true);
        }else if (waterFill <= 0){
            waterShader[0].SetActive(false);
            waterShader[1].SetActive(false);
            waterShader[2].SetActive(false);
        }
    }
    
    private void WaterCarrier(){
        if (waterFill >= 100f){
            waterFill = 100f;
        }
        if (waterFill <= 0f){
            waterFill = 0f;
        }

        waterFill -= _waterFillSpeed * Time.deltaTime;
    }

    private void DrippingWaterParticle(){
        if (-virtualJoystick.Vertical < 0 && waterFill > 0){
            waterDripping[0].SetActive(true);//ileri giderken çalışacak particle
            waterDripping[1].SetActive(false);//dururken giderken çalışacak particle
            waterDripping[2].SetActive(false);//geri giderken çalışacak particle
        }else if (-virtualJoystick.Vertical > 0 && waterFill > 0){
            waterDripping[0].SetActive(false);
            waterDripping[1].SetActive(false);
            waterDripping[2].SetActive(true);
        }else if(waterFill > 0){
            waterDripping[0].SetActive(false);
            waterDripping[1].SetActive(true);
            waterDripping[2].SetActive(false);
        }else if (waterFill <= 0){
            waterDripping[0].SetActive(false);
            waterDripping[1].SetActive(false);
            waterDripping[2].SetActive(false);
        }
    }

    //oyuncunun kullandığı sistemleri kontrol eder.
    private void PlayerController(){
        if (virtualJoystick.Vertical > 0){
            MovementControllerOfHelicopter(-virtualJoystick.Vertical);
            _xRotofHeli = PosRotXandZCalculate(_xRotofHeli,15);
        }else if (virtualJoystick.Vertical < 0){
            MovementControllerOfHelicopter(-virtualJoystick.Vertical);
            _xRotofHeli = NegativeRotXandZCalculate(_xRotofHeli,15);
        }else{
            _xRotofHeli = ResetterForRotXandZ(_xRotofHeli);
            isStopped = true;
        }
        if (virtualJoystick.Horizontal > 0){
            _zRotofHeli = PosRotXandZCalculate(_zRotofHeli,5);
        }else if (virtualJoystick.Horizontal < 0){
            _zRotofHeli = NegativeRotXandZCalculate(_zRotofHeli,5);
        }else{
            _zRotofHeli = ResetterForRotXandZ(_zRotofHeli);  
        }
        RotationControllerOfHelicopter(virtualJoystick.Horizontal);
        ChildRotationHelicopter(_zRotofHeli, _xRotofHeli);

    }

    //helicopterin hareketini control eden method
    private void MovementControllerOfHelicopter(float direction){
        transform.Translate(new Vector3((direction * Time.deltaTime * helicopterMovementSpeed),0,0));
    }

    //helicopterin rotationını control eden method
    private void RotationControllerOfHelicopter(float yRot){
        transform.Rotate(new Vector3(0,(yRot * rotationSpeedOfHelicopter * Time.deltaTime),0), Space.World);
    }

    //child gameobject of helicopter
    private void ChildRotationHelicopter(float xRot, float zRot){
        childHelicopter.transform.localEulerAngles = new Vector3(xRot, transform.localRotation.y ,zRot);
    }
    
    //rotation x ve z hesaplayan calculator
    private float PosRotXandZCalculate(float temp, float clamp){
        if (temp >= clamp){
            temp = clamp;
        }else{
            temp += helicopterMovementAnimationRotation;
        }
        return temp;
    }
    
    private float NegativeRotXandZCalculate(float temp, float clamp){
        if (temp <= -clamp){
            temp = -clamp;
        }else{
            temp -= helicopterMovementAnimationRotation;
        }
        return temp;
    }

    private float ResetterForRotXandZ(float temp){
        if (temp > 0){
            temp -= 2 * helicopterMovementAnimationRotation;
            if (temp <= 0){
                temp = 0;
            }
        }else if (temp < 0){
            temp += 2 * helicopterMovementAnimationRotation;
            if (temp >= 0){
                temp = 0;
            }
        }else{
            temp = 0;
        }
        return temp;
    }
    //end of calculator
    
    //Helicopter Spawner
    public void HelicopterSpawner(){
        Transform tempTransform = GameObject.FindWithTag("Spawn").transform;
        transform.position = new Vector3(tempTransform.position.x, transform.position.y, tempTransform.position.z);
    }
}
