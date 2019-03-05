using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour {

    public float power;
    public int selected;
    public int sorte;
    public int weizen;
    private Animator anim;
    public LayerMask target;
    private Rigidbody2D rigi;
    public GameObject Flashlight;
    public GameObject Sun;
    public GameObject hacke;
    public GameObject hake;
    public GameObject saat_w;
    public GameObject sense;
    public float day;
    public float night;
    public float time;
    public float[] growthspeeds;
    public int[] saatgut;
    public GameObject Sprite;

    //0 = Möhre
    //1 = Weizen

    private void Start()
    {
        selected = 1;
        sorte = 1;
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
        rigi.velocity = new Vector2(Input.GetAxis("Horizontal") * 250, Input.GetAxis("Vertical") * 250);
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
        anim.SetFloat("Y", Input.GetAxis("Vertical"));
        anim.SetFloat("X", Input.GetAxis("Horizontal"));
        time += Time.deltaTime;
        if(time < day)
        {
            Flashlight.SetActive(false);
            Sun.SetActive(true);
        }
        else if(time > day && time < night)
        {
            Flashlight.SetActive(true);
            Sun.SetActive(false);
        }
        else if (time > night)
        {
            time = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selected < 4)
            {
                selected += 1;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selected > 1)
            {
                selected -= 1;
            }
        }
        hacke.transform.localPosition = new Vector3(selected* 64 - 64, hacke.transform.localPosition.y, hacke.transform.localPosition.z);
        hake.transform.localPosition = new Vector3(selected * 64 - 128, hake.transform.localPosition.y, hake.transform.localPosition.z);
        saat_w.transform.localPosition = new Vector3(selected * 64 - 192, saat_w.transform.localPosition.y, saat_w.transform.localPosition.z);
        sense.transform.localPosition = new Vector3(selected * 64 - 256, sense.transform.localPosition.y, sense.transform.localPosition.z);
        RaycastHit2D worktrigger = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector3.up, 1, target);
        if (worktrigger.collider != null)
        {
            if (Input.GetButton("Fire1"))
            {
                worktrigger.collider.SendMessage("Work", power, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void W_gesammelt(int value)
    {
        weizen += value;
        PlayerPrefs.SetInt("Weizen", weizen);
    }
}
