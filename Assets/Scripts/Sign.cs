using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Sign", menuName = "CreateSign")]
public class Sign : ScriptableObject
{

	public enum KindOfItem
	{
		right_and_left,
		center
	}

	public KindOfItem kind_of_sign;

	


	[SerializeField]//�����Ă��邩���Ȃ���
	private int signcode;

	[SerializeField]//��p�X�N���v�g
	private GameObject signprefab;

	//�@�A�C�e���̖��O
	[SerializeField]
	private string signName;

	[SerializeField]
	private string information;

	[SerializeField]
	private bool script;

	[SerializeField]
	private bool prefab;



	public string GetKind()
	{
		return kind_of_sign.ToString();
	}

	public bool Script()
	{
		return script;
	}

	public bool Prefab()
	{
		return prefab;
	}




	public string GetSignName()
	{
		return signName;
	}


	public string GetInformation()
	{
		return information;
	}

	public int GetSigncode()
	{
		return signcode;
	}

	public GameObject GetSign()
    {
		return signprefab;
    }
}
