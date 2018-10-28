using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{

	public Transform Body;
	public Transform MissileHandlers;
	public GameObject MissilePrefab;
	public float Speed = 40f;
	private List<GameObject> Missiles = new List<GameObject>();
	// Use this for initialization
	void Start()
	{
		foreach (Transform missileHandler in MissileHandlers)
		{
			var missile = Instantiate(MissilePrefab, missileHandler.position, missileHandler.rotation);
			missile.transform.parent = missileHandler;
			missile.transform.localScale = new Vector3(55f, 55f, 55f);
			Missiles.Add(missile);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			var direction = Input.GetKey(KeyCode.A) ? -1 : 1;
			Body.Rotate(Vector3.up * 15f * Time.deltaTime * direction);
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			LaunchMissiles();
		}
	}

	private void LaunchMissiles()
	{
		int index = 0;
		foreach (var missile in Missiles)
		{
			StartCoroutine(RealLaunch(missile, index));
			index++;
		}
	}

	IEnumerator RealLaunch(GameObject missile, int index)
	{
		yield return new WaitForSeconds(0.5f * index);
		var rigidBody = missile.AddComponent<Rigidbody>();
		rigidBody.velocity = missile.transform.forward * Speed;
		missile.transform.parent = null;
	}
}
