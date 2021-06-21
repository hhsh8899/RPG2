using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDir : MonoBehaviour
{
    public GameObject effect_click_prefab;
    private bool isMoving=false;//Êó±ê×ó¼üÊÇ·ñÌ§Æð
    public Vector3 target=Vector3.zero;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    void Start()
    {
        target = transform.position;
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {//&& EventSystem.current.IsPointerOverGameObject()==false&& UICamera.hoveredObject == null

        
        if (playerAttack.isLocking==false&& Input.GetMouseButtonDown(0) && !UICamera.isOverUI && EventSystem.current.IsPointerOverGameObject() == false && playerAttack.state != NewPlayerState.Death)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isColider = Physics.Raycast(ray,out hitInfo);
            if (isColider && hitInfo.collider.tag == Tags.ground)
            {
                isMoving = true;
                ShowClickEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isColider = Physics.Raycast(ray, out hitInfo);
            if (isColider && hitInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }
           
         }
        else
        {
            if (playerMove.isMoving)
            {
                LookAtTarget(target);
            }
        }
    }
    void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.2f, hitPoint.z);
        GameObject.Instantiate(effect_click_prefab, hitPoint, Quaternion.identity);
    }

    void LookAtTarget(Vector3 hitPoint)
    {
        target = hitPoint;
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);
    }
}
