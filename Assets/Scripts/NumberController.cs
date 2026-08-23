using System.Threading;
using TMPro;
using UnityEngine;

public class NumberController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI flashNum;
	[SerializeField] TextMeshProUGUI flashMark;
	[SerializeField] TextMeshProUGUI joker;

	[SerializeField] int flashSpeed = 140;

	public int answerScore = 0;
	int flashScore = 0;
	int flashNow = 0;
	string stNum;
	string stMark;

	bool colorRed = true;	// 一回ごとにトランプの色を切り替えるためのフラグ

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
				joker.gameObject.SetActive(true);
			}
			else if (timer >flashSpeed)
			{
				flashScore = Random.Range(1, 14);   // 数字決め　目標値を上回っていた場合やトランプに合わせて変更する
				if (flashNow + flashScore > answerScore) flashScore = answerScore - flashNow;
				stNum = flashScore.ToString();
				if (flashScore == 1) stNum = "A";
				if (flashScore == 10) stNum = "10";
				if (flashScore == 11) stNum = "J";
				if (flashScore == 12) stNum = "Q";
				if (flashScore == 13) stNum = "K";

				if (colorRed)
				{ // トランプの種類決め
					if (Random.Range(1, 2) == 1)   
					{ flashNum.color = Color.red; flashMark.color = Color.red; stMark = "♥"; }
					else { flashNum.color = Color.red; flashMark.color = Color.red; stMark = "♦"; }
					colorRed = false;
				}
				else { 					
					if (Random.Range(1, 2) == 1)
					{ flashNum.color = Color.black; flashMark.color = Color.black; stMark = "♠"; }
					else { flashNum.color = Color.black; flashMark.color = Color.black; stMark = "♣"; }
					colorRed = true;
				}

				flashNow += flashScore;

				flashNum.GetComponent<TextMeshProUGUI>().text = stNum.ToString();
				flashMark.GetComponent<TextMeshProUGUI>().text = stMark.ToString();

				flashNum.gameObject.SetActive(true);
				flashMark.gameObject.SetActive(true);
				joker.gameObject.SetActive(false);

				timer = 0;
			}
		}
		else
		{
			flashNum.gameObject.SetActive(false);
			flashMark.gameObject.SetActive(false);
			joker.gameObject.SetActive(true);
		}
	}
}
