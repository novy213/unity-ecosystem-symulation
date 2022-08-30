using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roslina : MonoBehaviour
{
    public GameObject lodyga1;
    public GameObject lodyga2;
    public GameObject lodyga3;
    public GameObject lodyga4;
    public GameObject lodyga5;
    public GameObject lodyga6;
    public GameObject lodyga7;
    public GameObject lodyga8;
    public GameObject lodyga9;
    public GameObject lodyga10;
    public float czas=0;

   void Update(){
       czas+=Time.deltaTime;
       if(this.tag == "ZJE"){

           lodyga1.SetActive(false);
           lodyga2.SetActive(false);
           lodyga3.SetActive(false);
           lodyga4.SetActive(false);
           lodyga5.SetActive(false);
           lodyga6.SetActive(false);
           lodyga7.SetActive(false);
           lodyga8.SetActive(false);
           lodyga9.SetActive(false);
           lodyga10.SetActive(false);
           this.GetComponent<MeshRenderer>().enabled = false;
       }
       else if(this.tag=="ROS"){
           czas=0;
           lodyga1.SetActive(true);
           lodyga2.SetActive(true);
           lodyga3.SetActive(true);
           lodyga4.SetActive(true);
           lodyga5.SetActive(true);
           lodyga6.SetActive(true);
           lodyga7.SetActive(true);
           lodyga8.SetActive(true);
           lodyga9.SetActive(true);
           lodyga10.SetActive(true);
           this.GetComponent<MeshRenderer>().enabled = true;
       }
        if(czas>=120){
            this.transform.tag="ROS";
        }
   }
}
