using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiHandler : MonoBehaviour {

	public void UpdateMoney(float money) {
		var moneyUI = transform.Find("Money").GetComponentInChildren<Text> ();
		moneyUI.text = "$" + money;
	}
}
