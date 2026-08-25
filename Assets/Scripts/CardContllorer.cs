using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardContllorer : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI startText;
	[SerializeField] TextMeshProUGUI cardScoreText;
	[SerializeField] GameObject cardScoreUI;

	[SerializeField] TextMeshProUGUI youWinLoseText;
	[SerializeField] GameObject endScreen;

	[SerializeField] AudioClip selectSE;
	[SerializeField] AudioClip RLCardSE;
	[SerializeField] AudioClip winSE;
	[SerializeField] AudioClip loseSE;
	[SerializeField] AudioClip startSE;

	[SerializeField] GameObject startBGM;
	[SerializeField] GameObject gameBGM;

	[SerializeField] TextMeshProUGUI title;
	[SerializeField] TextMeshProUGUI subTitle;

	[SerializeField] int cardStart;
	
	[SerializeField] NumberController NumberController;

	float cardLocation = 0;	// カードの場所(角度)
	int timer = 0;
	int cardNumber = 5;	// カードの数
	int cardNow = 3;    // 現在のカードの番号
	int cSpacing = 40; // カードの間隔
	float selectXY = 0; // カードの選択時に動く座標
	
	int cardScore = 0; // このカードのスコア
	int cardMedian = 60;	// カード数値の中央の値
	int cardRange = 40;	// カード数値の幅

	bool start = false;
	bool select = false;
	bool plus = true;
	bool seEnd = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
	}

    // Update is called once per frame
    void Update()
    {
		if (start)
		{
			if (select == false)
			{
				if (NumberController.answerScore == 0 && Random.Range(1, 15) == 4) NumberController.answerScore = cardScore;    // まだ答えが決まっていない場合に設定する
				timer--;

				transform.rotation = Quaternion.Euler(0, 0, cardStart * cSpacing + cardLocation * cSpacing);    // 角度を反映
				if (cardStart + cardLocation >= -1.2 && -0.8 >= cardStart + cardLocation)   // カードが正面に来たとき
				{
					cardScoreText.GetComponent<TextMeshProUGUI>().text = cardScore.ToString();
					transform.position = new Vector3(-7, -5, 0);
					if (Keyboard.current.sKey.wasReleasedThisFrame&& NumberController.cardSelect==true)	// Sキーが押された＆フラッシュ暗算が終わっていたら
					{
						AudioSource.PlayClipAtPoint(selectSE, transform.position);
						cardScoreText.gameObject.SetActive(false);
						cardScoreUI.SetActive(false);
						select = true;
						NumberController.cardSelect = false;
					}
				}
				else transform.position = new Vector3(-9, -7f, 0);

				if (cardStart > cardNumber / 2 || cardStart < -cardNumber / 2)  // カードの数を超えた場合は非表示にする(今回のプロジェクトでは未使用)
				{
					gameObject.SetActive(false);
				}
				else gameObject.SetActive(true);

				if (timer <= 0) // カードの角度を変える処理達
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
							if (cardStart == 0) AudioSource.PlayClipAtPoint(RLCardSE, transform.position);
							// ↑特定のスプライトからのみ効果音を鳴らすためのif文
							plus = true;
							timer = 20;
						}
					}
					if (Keyboard.current.aKey.wasPressedThisFrame)
					{
						if (cardNow > 0)
						{
							if (cardStart == 0) AudioSource.PlayClipAtPoint(RLCardSE, transform.position);
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
				transform.position = new Vector3(-7 + selectXY, -5 + selectXY, 0);
				selectXY += 0.5f;
				timer++;
				if (timer > 20)
				{
					endScreen.SetActive(true);
					youWinLoseText.gameObject.SetActive(true);
					if (NumberController.answerScore == cardScore)
					{
						if(cardStart==0&&seEnd==false)AudioSource.PlayClipAtPoint(winSE, transform.position); 
						// ↑特定のスプライトからのみ効果音を一回だけ鳴らすためのif文
						seEnd= true;
						youWinLoseText.color = Color.yellow;
						youWinLoseText.GetComponent<TextMeshProUGUI>().text = "You Win !".ToString();
						gameBGM.GetComponent<AudioSource>().Stop();
					}
					else
					{
						if (cardStart == 0 && seEnd == false) AudioSource.PlayClipAtPoint(loseSE, transform.position);
						seEnd= true;
						youWinLoseText.color = Color.blue;
						youWinLoseText.GetComponent<TextMeshProUGUI>().text = "You Lose..".ToString();
						gameBGM.GetComponent<AudioSource>().Stop();
					}
				}
			}
		}
		else
		{
			if (Keyboard.current.spaceKey.wasReleasedThisFrame)
			{
				cardScore = Random.Range(cardMedian-cardRange/2, cardMedian + cardRange / 2);
				startText.gameObject.SetActive(false);
				cardScoreText.gameObject.SetActive(true);
				title.gameObject.SetActive(false);
				subTitle.gameObject.SetActive(false);
				startBGM.GetComponent<AudioSource>().Stop();
				gameBGM.GetComponent<AudioSource>().Play();
				if (cardStart == 0) AudioSource.PlayClipAtPoint(startSE, transform.position);
				start = true;
			}
			if (Keyboard.current.nKey.wasReleasedThisFrame)	// 通常モード
			{
				title.color = new Color32(0x00, 0xff, 0x34, 0xff);
				subTitle.color = new Color32(0x00, 0xff, 0x34, 0xff);
				cardMedian = 60;   
				cardRange = 40;
				NumberController.flashSpeed = 140;
			}
			if (Keyboard.current.eKey.wasReleasedThisFrame)	// イージーモード
			{
				title.color = Color.yellow;
				subTitle.color = Color.yellow;
				cardMedian = 40;  
				cardRange = 20;
				NumberController.flashSpeed = 160;
			}
			if (Keyboard.current.hKey.wasReleasedThisFrame)	// ハードモード
			{
				title.color = Color.red;
				subTitle.color = Color.red;
				cardMedian = 100;
				cardRange = 50;
				NumberController.flashSpeed = 100;
			}
			if (Keyboard.current.vKey.wasReleasedThisFrame)	// ベリーハードモード
			{
				title.color = Color.white;
				subTitle.color = Color.white;
				cardMedian = 300;
				cardRange = 200;
				NumberController.flashSpeed = 120;
			}
		}
	}
}
