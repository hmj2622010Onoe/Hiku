using UnityEngine;
using UnityEngine.InputSystem;

public class CardContllorer : MonoBehaviour
{
	[SerializeField] int startCard;
	float cardLocation = 0;
	int timer = 0;
	int cardNumber = 5;
	int cardNow = 3;

	bool plus = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer--;
		transform.rotation = Quaternion.Euler(0, 0, startCard * 35 + cardLocation * 35);
		if(startCard + cardLocation>=-1.1&& -0.9>=startCard + cardLocation) 
		{
			transform.position = new Vector3(-3,-4.5f, 0);
		}
		else transform.position = new Vector3(-4,-5.5f, 0);
		//transform.Rotate(new Vector3(0, 0, startCard * 35 + cardLocation * 35));

		if(startCard>cardNumber/2|| startCard < -cardNumber / 2) 
		{
			gameObject.SetActive(false);
		}
		else gameObject.SetActive(true);

		if (timer <= 0)
		{
			if (timer == 0)
			{
				if (plus == true) cardNow++;
				else cardNow--;
			}
			if (Keyboard.current.dKey.wasPressedThisFrame)
			{
				if (cardNow < cardNumber-1)
				{
					plus = true;
					timer = 100;
				}
			}
			if (Keyboard.current.sKey.wasPressedThisFrame)
			{
				if (cardNow > 0)
				{
					plus = false;
					timer = 100;
				}
			}
		}
		else
		{
			if (plus == false) cardLocation -= 0.01f;
			if (plus == true) cardLocation += 0.01f;
		}
	}
}
