
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン切り替えに使用するライブラリ

[CreateAssetMenu(fileName = "SignDataBase", menuName = "CreateSignDataBase")]
public class SignDataBase : ScriptableObject
{

	[SerializeField]
	private List<Sign> signLists = new List<Sign>();

	

	//　アイテムリストを返す
	public List<Sign> GetSignLists()
	{
		return signLists;
	}
}
