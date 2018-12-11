using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveEnemyToPoint : MonoBehaviour {

    public AIEnemy e;
    public Transform enemyPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void moveEnemyToPoint()
    {
        e.DisableAi();
        e.enemyMoveToPoint(enemyPosition.position);
    }
}
