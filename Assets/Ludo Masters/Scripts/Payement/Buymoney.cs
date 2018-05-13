using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paymentwall;
using UnityEngine.UI;
public class Buymoney : MonoBehaviour {

	public Text value;
	public void paypalPay(Text pay){

		StartCoroutine (paypal (pay.text));

	}
	public void paytmPay(Text pay){

		StartCoroutine (paytm (pay.text));

	}
	IEnumerator paypal(string pay){
		WWWForm form = new WWWForm ();
		form.AddField ("amountpay", pay);

		var headers = form.headers;
		form.headers["content-type"] = "application/x-www-form-urlencoded";

		WWW www = new WWW (BaseURL.base_url + "paypal", form);
		yield return www;


		if (www.error != null) {
			Debug.Log (www.error);

		} else {

			print (www.text);
			Application.OpenURL (www.text);
		}
	}

	IEnumerator paytm(string pay){
		WWWForm form = new WWWForm ();
		form.AddField ("amounttm", pay);

		var headers = form.headers;
		form.headers["content-type"] = "application/x-www-form-urlencoded";

		WWW www = new WWW (BaseURL.base_url + "generate", form);
		yield return www;


		if (www.error != null) {
			Debug.Log (www.error);

		} else {

			print (www.text);
			Application.OpenURL (www.text);
		}
	}
	public void updateText(Text value){
		this.value.text = value.text;
	}
}
