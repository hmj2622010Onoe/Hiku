using System.Threading;
using TMPro;
using UnityEngine;

public class NumberController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI flashNum;
	[SerializeField] TextMeshProUGUI flashMark;
	[SerializeField] TextMeshProUGUI jokerText;
	[SerializeField] GameObject jokerCard;

	[SerializeField] AudioClip cardSE1;
	[SerializeField] AudioClip cardSE2;
	[SerializeField] AudioClip cardSE3;
	[SerializeField] AudioClip cardJokerSE;
	
	public int flashSpeed = 140;
	public int answerScore = 0;
	public bool cardSelect = false;

	int flashScore = 0;
	int flashNow = 0;
	string stNum;
	string stMark;

	bool colorRed = true;   // 一回ごとにトランプの色を切り替えるためのフラグ
	bool jokerSebool = true;

	int timer = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		if (answerScore > 0)
		{
			if (flashNow == answerScore&&timer>flashSpeed)
			{
				flashNum.gameObject.SetActive(false);
				flashMark.gameObject.SetActive(false);
				jokerText.gameObject.SetActive(true);
				jokerCard.SetActive(true);
				if (jokerSebool)
				{
					AudioSource.PlayClipAtPoint(cardJokerSE, transform.position);
					cardSelect = true; ;
					jokerSebool = false;
				}
			}
			else if (timer >flashSpeed)
			{
				flashScore = Random.Range(1, 14);   // 数字決め　目標値を上回っていた場合やトランプに合わせて変更する
				if(flashSpeed>150) flashScore = Random.Range(1, 11);

				if (flashNow + flashScore > answerScore) flashScore = answerScore - flashNow;
				stNum = flashScore.ToString();
				if (flashScore == 1) stNum = "A";
				if (flashScore == 10) stNum = "10";
				if (flashScore == 11) stNum = "J";
				if (flashScore == 12) stNum = "Q";
				if (flashScore == 13) stNum = "K";

				if (colorRed)
				{ // トランプの種類決め
					if (Random.Range(1, 3) == 1)   
					{ flashNum.color = Color.red; flashMark.color = Color.red; stMark = "♥"; }
					else { flashNum.color = Color.red; flashMark.color = Color.red; stMark = "♦"; }
					colorRed = false;
				}
				else 
				{ 					
					if (Random.Range(1, 3) == 1)
					{ flashNum.color = Color.black; flashMark.color = Color.black; stMark = "♠"; }
					else { flashNum.color = Color.black; flashMark.color = Color.black; stMark = "♣"; }
					colorRed = true;
				}

				flashNow += flashScore;

				if (Random.Range(1, 4) == 1) AudioSource.PlayClipAtPoint(cardSE1, transform.position);
				else if (Random.Range(1, 3) == 1) AudioSource.PlayClipAtPoint(cardSE2, transform.position);
				else AudioSource.PlayClipAtPoint(cardSE3, transform.position);

				flashNum.GetComponent<TextMeshProUGUI>().text = stNum.ToString();
				flashMark.GetComponent<TextMeshProUGUI>().text = stMark.ToString();

				flashNum.gameObject.SetActive(true);
				flashMark.gameObject.SetActive(true);
				jokerText.gameObject.SetActive(false);
				jokerCard.SetActive(false);

				timer = 0;
			}
		}
		else
		{
			flashNum.gameObject.SetActive(false);
			flashMark.gameObject.SetActive(false);
			jokerText.gameObject.SetActive(true);
			jokerCard.SetActive(true);
		}
	}
}
