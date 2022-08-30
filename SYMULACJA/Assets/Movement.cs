using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody RB;

    public float moveSpeed = 0.1f;

    public float smooth = 5.0f;
    float tiltAngle = 360.0f;
    public float kierunek=0f;
    public float czas=0f;
    public float czas_ruch=0f;

    public float czas1=0f;
    public bool kol=false;

    public bool target=false;
    public GameObject jedz;

    public float czas_glod=0;
    public int glod=100;
    public int zdrowie=100;
    public int pragnienie=100;
    public float czas_pra=0;

    int i=0;
    int a=0;
    int l=0;
    int s=0;
    int p=0;
    int o=0;


    private Rigidbody myRB;

    public float smierc=0f;

    private GameObject _myGameObject;
    private GameObject _myGameObject2;
    private Transform _myGameObject1;

    public GameObject matka;

    public float reset=0;

    public int rand1;
    public int kier;

    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        this._myGameObject = null;
        this._myGameObject2 = null;
        myRB=GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag=="ROS" && _myGameObject==null){
            if(glod<80){
                l=0;
                _myGameObject2 = other.gameObject;
            _myGameObject = other.gameObject;
            _myGameObject1 = other.transform;
            //jedz.transform.position = other.transform.position;
            transform.LookAt(_myGameObject1, Vector3.up);
            target=true;
            }
            else if(glod>80){
            rand1=Random.Range(1,2);
            if(rand1==1){
                kier=90;
            }
            else if(rand1==0){
                kier=(-90);
            }
            float tiltAroundZ1 = kier * 360;    
            Quaternion target1 = Quaternion.Euler(0, tiltAroundZ1, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target1,  Time.deltaTime * smooth);
        }
        }
        if(other.tag=="woda"){
            o=0;
            p=0;
            if(pragnienie<80 && _myGameObject==null){
                _myGameObject2 = other.gameObject;
            _myGameObject = other.gameObject;
            _myGameObject1 = other.transform;
            //jedz.transform.position = other.transform.position;
            transform.LookAt(_myGameObject1, Vector3.up);
            target=true;
            }
            else if(pragnienie>80){
            rand1=Random.Range(1,2);
            if(rand1==1){
                kier=90;
            }
            else if(rand1==0){
                kier=(-90);
            }
            float tiltAroundZ1 = kier * 360;    
            Quaternion target1 = Quaternion.Euler(0, tiltAroundZ1, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target1,  Time.deltaTime * smooth);
        }

            
        }
        
    }
    public void OnTriggerExit(Collider other){
        if(other.tag=="ROS"){
            target=false;
        }
        a=0;
        rand1=0;
    }
    

    public void OnCollisionEnter(Collision other){
        if(other.transform.tag=="ROS"){
            other.transform.gameObject.tag = "ZJE";
            for(;l<=1;l++){
                glod=glod+10;
                if(glod>100){
                    glod=100;
                }
            }
            target=false;
            _myGameObject=null;
        }
        else if(other.transform.tag=="ROSL"){
            for(;a<=1;a++){
                zdrowie=zdrowie-10;
            }
        }
        else if(other.transform.tag=="woda"){
            for(;p<=1;p++){
                pragnienie=pragnienie+10;
            }
            if(pragnienie>100){
                pragnienie=100;
            }
            _myGameObject=null;
            _myGameObject1=null;
            _myGameObject2=null;
            for(;o<=0;o++){
                transform.Rotate(0,180,0);
            }
        }
        else if(other.transform.tag=="Sciana"){
            kol=true;
        }
    }
    void OnCollisionExit(Collision other){
        kol=false;
        i=0;
    }



    void Update()
    {
        if(kol==true){
            for(;i<=2;i++){
                transform.Rotate(0,180,0);
            }
        }
        if(_myGameObject==null){
            target=false;
        }
        if(zdrowie>0){
        czas_glod+=Time.deltaTime;
        if(czas_glod>2){
            czas_glod=0;
            if(glod>0){
                glod--;
            }
            if(pragnienie>0){
                pragnienie--;
            }
            if(glod==0 || pragnienie==0)
            {
                zdrowie--;
            }
            else if(glod>=80 || pragnienie>=80){
                zdrowie++;
                if(zdrowie>100){
                    zdrowie=100;
                }
            }
        }
        if(target==false){
            czas += Time.deltaTime;
                l=0;
                Poruszanie();
            
        }
        else if(target==true){
            transform.position = Vector3.MoveTowards(transform.position, _myGameObject2.transform.position, 2f * Time.deltaTime);
        }
        }
        else if(zdrowie<=0){
            smierc+=Time.deltaTime;
            myRB.constraints = RigidbodyConstraints.None;
            for(;s<=1;s++){
                transform.Rotate(0,0,-90);
            }
            transform.gameObject.tag = "padlina";
            if(smierc>=150){
                Destroy(matka);
            }
        }

    }
    public void Poruszanie(){
        if(czas<=0.02){
            Randomowanie();
        }
        else if(czas<=0.5){
            Obrot();
        }
        else if(czas<=czas_ruch){
            Ruch();
        }
        else if(czas<=czas_ruch+1f){
            czas=0;
        }
        else if(czas>12f){
            czas=0;
        }
    }
    public void Randomowanie(){
        czas_ruch=Random.Range(5f, 11f);
        kierunek=Random.Range(-359.0f,359.0f);
    }
    public void Obrot(){
        float tiltAroundZ = kierunek * tiltAngle;    
        Quaternion target = Quaternion.Euler(0, tiltAroundZ, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
    public void Ruch(){
            RB.MovePosition(transform.position + (transform.forward) * moveSpeed); 
    }
    
}