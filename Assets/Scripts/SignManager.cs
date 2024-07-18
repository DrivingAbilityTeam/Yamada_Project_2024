
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour
{
	//public GameObject score_object = null;
	
	[SerializeField]
	private SignDataBase signDataBase;
	//�@�A�C�e�����Ǘ�
	private Dictionary<Sign, int> numOfsign = new Dictionary<Sign, int>();
	

	

	
	// Use this for initialization
	void Start()
	{

		for (int i = 0; i < signDataBase.GetSignLists().Count; i++)
		{
			//�@�A�C�e������K���ɐݒ�
			numOfsign.Add(signDataBase.GetSignLists()[i], i);
			//�@�m�F�̈׃f�[�^�o��
			//Debug.Log(signDataBase.GetSignLists()[i].GetSignName() + ": " + signDataBase.GetSignLists()[i].GetInformation());
		}

		
	}

	void Update()
	{

		

	}

	//�@���O�ŃA�C�e�����擾
	public Sign GetSign(string searchName)
	{
		return signDataBase.GetSignLists().Find(signName => signName.GetSignName() == searchName);
	}


	public Sign GetSigncode(int Signcode)
	{
		return signDataBase.GetSignLists().Find(signcode => signcode.GetSigncode() == Signcode);
	}

}