
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�V�[���؂�ւ��Ɏg�p���郉�C�u����

[CreateAssetMenu(fileName = "SignDataBase", menuName = "CreateSignDataBase")]
public class SignDataBase : ScriptableObject
{

	[SerializeField]
	private List<Sign> signLists = new List<Sign>();

	

	//�@�A�C�e�����X�g��Ԃ�
	public List<Sign> GetSignLists()
	{
		return signLists;
	}
}
