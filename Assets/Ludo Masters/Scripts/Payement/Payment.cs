using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paymentwall;
using UnityEngine.UI;

public class Payment : MonoBehaviour {

	string base_url = "http://139.59.6.58:3000/";
	public Text  payment;
	public Text name;
	public Text email;
	public Text coinCount;
	public Button pleaseWaitButton;
	public Text pleaseWaitText;
	public void payPal(){
		pleaseWaitButton.interactable = false;
		 StartCoroutine (requestAdd());
	}
	IEnumerator requestAdd()
	{
		pleaseWaitText.text = "Please wait...";
		float pay;
		float amount;
		if (float.TryParse (payment.text, out pay) && float.TryParse (coinCount.text, out amount)) {
			if (amount >= pay) {
				if ((email.text != null)) {

					WWWForm form = new WWWForm ();
					form.AddField ("name", name.text);
					form.AddField ("email", email.text);
					form.AddField ("reqAmount", pay + "");
					form.AddField ("payType", "paypal");
					form.AddField ("amount", coinCount.text);

					var headers = form.headers;
					form.headers["content-type"] = "application/x-www-form-urlencoded";

					WWW www = new WWW (base_url + "userAdd", form);
					yield return www;


					if (www.error != null) {
						pleaseWaitText.text = www.error;
						pleaseWaitButton.interactable = true;
						Debug.Log (www.error);

					} else {

						print (www.text);
						pleaseWaitText.text = "Success";
						pleaseWaitButton.interactable = true;
					}
				}
			}

			/*
			user user;
			string json = www.downloadHandler.text;
			user = JsonUtility.FromJson<user>(json.ToString());
			if (user.token != null) {

				WWWForm formReg = new WWWForm ();
				form.AddField ("userID", userID);
				form.AddField ("userNickName", name);
				UnityWebRequest wwwReg = UnityWebRequest.Post (baseUrl + "/v1/register", formReg);

				yield return wwwReg.Send ();

				if (wwwReg.isNetworkError) {
					Debug.Log (wwwReg.error);
				} else {
					string jsonReg = wwwReg.downloadHandler.text;
					user regUser = JsonUtility.FromJson<user>(jsonReg.ToString());
					token = regUser.token;
					print ("Successfully registered");
				}
			} else {
				token = user.token;
				print ("Successfully loged");
			}
				*/
		} else {
			pleaseWaitText.text = "Data invalid...";

		}
		pleaseWaitButton.interactable = true;
		yield break;
	}

	public void creditPay(){
		int pay;
		if (int.TryParse (payment.text, out pay)) {
			PWBrick brick = new PWBrick (pay + 0.0f, "USD", "Your Awesome Game", "Small pack");
			PWBase.SetAppKey ("t_b33d46eda162a2537ee9849040b02b"); // your Project Public key - available in your Paymentwall merchant area
			PWBase.SetSecretKey ("t_c99c2918ee0538144fd81d7dbc40ec"); 
			brick.ShowPaymentForm ();
		}
	
	}
}
