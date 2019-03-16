using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroControl : MonoBehaviour {

    static Animator anim;
    AnimatorClipInfo[] animInfo;
    private float default_speed = 5.0F;
    private float rotationSpeed = 200.0F;
    private float translation;
    private float rotation;

    private bool flag_transX = false;
    private bool flag_transZ = false;
    private bool flag_rotate = false;

    private float speed_run = 10F;
    private float speed_walkback = 3F;
    private float speed_strafe = 8F;
    private float speed_crouchwalk = 3F;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        animInfo = anim.GetCurrentAnimatorClipInfo(0);

        // mouse view rotation
        rotateActY(rotationSpeed, "Mouse X");

        // walk back and run (up,down or w,s)
        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isBackWalking", false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBackWalking", true);
        }
        else if (Input.GetAxis("Vertical") == 0) {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBackWalking", false);
        }

        //Strafe left and right (left,right or a,d)
        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("isStrafeLeft", false);
            anim.SetBool("isStrafeRight", true); 
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isStrafeLeft", true);
            anim.SetBool("isStrafeRight", false); 
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("isStrafeLeft", false);
            anim.SetBool("isStrafeRight", false);
        }

        //Jump (Space bar)
        if (Input.GetButtonDown("Jump")) {
            anim.SetTrigger("isJumping");
        }

        //Slide (Left Shift)
        if (Input.GetButtonDown("Slide"))
        {
            anim.SetTrigger("isSlide");
        }


        //Crouch (c,left ctrl)
        if ((Input.GetButtonDown("Crouch")) && (anim.GetBool("isCrouching")==false))
            anim.SetBool("isCrouching",true);
        else if ((Input.GetButtonDown("Crouch")) && (anim.GetBool("isCrouching")))
            anim.SetBool("isCrouching", false);

        AnimationSelectAction(animInfo[0].clip.name);

    }

    void transActZ() {
        if (flag_transZ) default_speed = 0;
        translation = Input.GetAxis("Vertical") * default_speed;
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
    }
    void transActZ(float speed)
    {
        if (flag_transZ) speed = 0;
        translation = Input.GetAxis("Vertical") * speed;
        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
    }
    void transActZ(float z,bool flag)
    {
        if(flag)
            transform.Translate(0, 0, z);
    }
    void transActX(){
        if (flag_transX) default_speed = 0;
        translation = Input.GetAxis("Horizontal") * default_speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);
    }
    void transActX(float speed)
    {
        if (flag_transX) speed = 0;
        translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);
    }
    void rotateActY() {
        float temp = rotationSpeed;
        if (flag_rotate) rotationSpeed = 0;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        rotationSpeed = temp;
    }

    void rotateActY(float speed,string InputName)
    {
        if (flag_rotate) speed = 0;
        rotation = Input.GetAxis(InputName) * speed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    /// ANIMATION NAME SELECTION FUNCTION ///
    void AnimationSelectAction(string anim_name) {
        switch (anim_name) {
            case("Back Jump"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = true;
                transActX(speed_walkback);
                transActZ(speed_walkback);
                break;
            case ("Crouch Walk Back"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = false;
                transActX(speed_crouchwalk);
                transActZ(speed_crouchwalk);
                break;
            case ("Crouched Sneaking Left"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX(speed_crouchwalk);
                transActZ(speed_crouchwalk);
                break;
            case ("Crouched Sneaking Right"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX(speed_crouchwalk);
                transActZ(speed_crouchwalk);
                break;
            case ("Crouched To Standing"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Crouched Walking"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = false;
                transActX(speed_crouchwalk);
                transActZ(speed_crouchwalk);
                break;
            case ("Crouching Idle"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Idle"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Jump"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = true;
                transActX(speed_run);
                transActZ(speed_run);
                break;
            case ("Jumping inpalce"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Left Strafe"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX(speed_strafe);
                transActZ(speed_strafe);
                break;
            case ("Left Strafe Walking"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Right Strafe"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX(speed_strafe);
                transActZ(speed_strafe);
                break;
            case ("Right Strafe Walking"):
                flag_transX = false;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Run To Stop"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = true;
                transActX();
                transActZ();
                break;
            case ("Running"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = false;
                transActX(speed_run);
                transActZ(speed_run);
                break;
            case ("Running Slide"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = true;
                transActX(speed_run);
                transActZ(speed_run);
                break;
            case ("Standing To Crouched"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Unarmed Jump"):
                flag_transX = true;
                flag_transZ = true;
                flag_rotate = false;
                transActX();
                transActZ();
                break;
            case ("Walking Backwards"):
                flag_transX = true;
                flag_transZ = false;
                flag_rotate = false;
                transActX(speed_walkback);
                transActZ(speed_walkback);
                break;
            default:
                Debug.Log("Animation is not in the selection list: "+ animInfo[0].clip.name);
                break;
        }
    }


    //// EVENTs DIFINED FUNCTION ////

    void startJump() {
        flag_transX = true;
        flag_rotate = true;
    }
    void endJump(){
        flag_transX = false;
        flag_rotate = false;
    }
}
