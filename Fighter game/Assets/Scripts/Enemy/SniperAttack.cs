using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemyAttack
{
    public LineRenderer lr;
    public float timeTillLock;
    public float timeTillAttack;

    public Material chargeMaterial;
    public SpriteRenderer chargeMeter;

    public bool charging;
    public bool tracking;


    public Color startColor;
    public Color currentColor;
    public float colorChange;


    public int lineRange;
    private void OnEnable()
    {
        StartCoroutine("PrepareShot");
       

        chargeMeter.material = chargeMaterial;
        startColor = chargeMeter.material.GetColor("_OutlineColor");

        colorChange = timeTillAttack / 255f;

        currentColor = startColor;
    }

    public void Update()
    {
        if (!inAttack)
            RotateWeapon();

        if(charging)
            setCharge();

        if (tracking)
            SetLine();

        lr.SetPosition(0, transform.position);
    }

    public IEnumerator PrepareShot()
    {
        tracking = true;
        lr.enabled = true;
        yield return new WaitForSeconds(timeTillLock);
        tracking = false;
        inAttack = true;
        SetFinalLine();
        charging = true;
        chargeMeter.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeTillAttack);
        lr.enabled = false;
        charging = false;
        inAttack = true;
        chargeMeter.gameObject.SetActive(false);


        move.SwapStates("Moving");
        chargeMeter.gameObject.SetActive(false);
        chargeMeter.material.SetColor("_OutlineColor", startColor);
    }

    public Vector2 CalculateDirection()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        return direction;
    }
    public void SetLine()
    {
        lr.SetPosition(1, player.transform.position);
    }

    public void SetFinalLine()
    {
        Vector2 dir = CalculateDirection();

        var pos = (Vector2)transform.position + dir * lineRange;
        Debug.DrawLine(transform.position, pos, Color.black);
        Debug.DrawRay(transform.position, dir * lineRange, Color.white);
        
        lr.SetPosition(1, pos);
    }
    public override void CancelAttack()
    {
        StopCoroutine("PrepareShot");
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);
        lr.enabled = false;
        chargeMeter.material.SetColor("_OutlineColor", startColor);
        this.enabled = false;
    }

    public void setCharge()
    {

        Color color = currentColor;

        color.g -= colorChange * Time.fixedDeltaTime;
        currentColor = color;
        chargeMeter.material.SetColor("_OutlineColor", color);

    }
}
