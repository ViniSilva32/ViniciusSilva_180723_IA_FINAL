using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class Drive : MonoBehaviour {

	float speed = 20.0F;                //velocidade total
    float rotationSpeed = 120.0F;       //velocidade de rotação
    public GameObject bulletPrefab;     //disparos do player
    public Transform bulletSpawn;       //local de onde os tiros saem
    public Slider healthBar;            //barra de vida

    float health = 100.0f;              //vida total
    public Transform respawn;

    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;              //botões para fazer o personagem se mover para frente e para trás
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;       //botões para rotacionar o player
        translation *= Time.deltaTime;                                      //limitador de velocidade 
        rotation *= Time.deltaTime;                                         //limitador de velocidade de rotação
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);     //faz com que a barra de vida siga o player
        healthBar.value = (int)health;
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);

        if (Input.GetKeyDown("space"))      //se pressionar ã tecla espaço o player ativa
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);
        }

        if(health <= 0)                     //verifica a quantidade de vida do player
        {
            health = 100;
            transform.position = respawn.position;
        }
    }
    void OnCollisionEnter(Collision col)        //se colidir com um gameobject com a tag bullet, perde 10 de vida
    {
        if (col.gameObject.tag == "bullet")
        {
            health -= 10;
        }
    }
    public void Damege(float damage)        //ao explodir da dano no player
    {
        health -= damage;
    }
    public void Heal(float heal)            //ao explodir cura o player
    {
        health += heal;
    }
}
