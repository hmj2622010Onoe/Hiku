using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardContllorer : MonoBehaviour
{
	[SerializeField] GameObject cardScoreText;
	[SerializeField] GameObject cardScoreUI;
	[SerializeField] GameObject youWin;
	[SerializeField] GameObject youLose;
	
	[SerializeField] int cardStart;

	[SerializeField] NumberController NumberController;

	float cardLocation = 0;	// カードの場所(角度)
	int timer = 0;
	int cardNumber = 5;	// カードの数
	int cardNow = 3;    // 現在のカードの番号
	int cSpacing = 40; // カードの間隔
	float selectXY = 0; // カードの選択時に動く座標
	
	int cardScore = 0; // このカードのスコア

	bool select = false;
	bool plus = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
		cardScore = Random.Range(40,80);
	}

    // Update is called once per frame
    void Update()
    {
		if (select == false)
		{
			if(NumberController.answerScore==0&&Random.Range(1,15)==4) NumberController.answerScore = cardScore;	// まだ答えが決まっていない場合に設定する

			timer--;
			transform.rotation = Quaternion.Euler(0, 0, cardStart * cSpacing + cardLocation * cSpacing);	// 角度を反映
			if (cardStart + cardLocation >= -1.2 && -0.8 >= cardStart + cardLocation)   // カードが正面に来たとき
			{
				cardScoreText.GetComponent<TextMeshProUGUI>().text = cardScore.ToString();
				transform.position = new Vector3(-7, -5, 0);
				if (Keyboard.current.spaceKey.wasReleasedThisFrame)
				{
					cardScoreText.SetActive(false);
					cardScoreUI.SetActive(false);
					Debug.Log("カードを選択しました");
					select = true;
				}
			}
			else transform.position = new Vector3(-9, -7f, 0);
			//transform.Rotate(new Vector3(0, 0, startCard * 35 + cardLocation * 35));

			if (cardStart > cardNumber / 2 || cardStart < -cardNumber / 2)  // カードの数を超えた場合は非表示にする(今回のプロジェクトでは未使用)
			{
				gameObject.SetActive(false);
			}
			else gameObject.SetActive(true);

			if (timer <= 0)	// カードの角度を変える処理達
			{
				if (timer == 0)
				{
					if (plus == true) cardNow++;
					else cardNow--;
				}
				if (Keyboard.current.dKey.wasPressedThisFrame)
				{
					if (cardNow < cardNumber - 1)
					{
						plus = true;
						timer = 20;
					}
				}
				if (Keyboard.current.sKey.wasPressedThisFrame)
				{
					if (cardNow > 0)
					{
						plus = false;
						timer = 20;
					}
				}
			}
			else
			{   // カードの回転処理
				if (plus == false) cardLocation -= 0.05f;
				if (plus == true) cardLocation += 0.05f;
			}
		}
		else
		{   // カードを選択した後の処理
			transform.position = new Vector3(-7+selectXY, -5 + selectXY, 0);
			selectXY+=0.5f;
			timer++;
			if (timer > 50) 
			{
			if(NumberController.answerScore == cardScore) 
				{
					youWin.SetActive(true);
				}
			else
				{
					youLose.SetActive(true);
				}
			}
		}
	}
}
