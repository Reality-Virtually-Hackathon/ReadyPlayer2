using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif

public class MovieScript : MonoBehaviour {
	public TextAsset script;

	public static MovieScript main;
	public string pathToScript;
	public List<Quote> quotes = new List<Quote>();

	public TextMesh obiText;
	public TextMesh hanText;
	public TextMesh scoreText;

	public int quoteIndex = 0;

	// Use this for initialization
	void Start () {
		main = this;
		parseScript(script);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void nextQuote(){
        if (quoteIndex >= quotes.Count) quoteIndex = 0;
		if(quotes[quoteIndex].character == "HAN"){
			hanText.text = quotes[quoteIndex].text;
			obiText.text = "";
		}
		if(quotes[quoteIndex].text == "BEN"){
			obiText.text = quotes[quoteIndex].text;
			hanText.text = "";
		}
		//scoreText.text = scoreQuote();
		quoteIndex++;
		
	}
	private void parseScript(TextAsset text)
	{
		string[] strArray = text.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
		foreach(var line in strArray)
		{
			try{
				string[] quote = line.Split(':');
                Debug.Log(line);
			    quotes.Add(new Quote(quote[0], quote[1]));
			}
			catch{
				
			}
		}
			
	}
	public float scoreQuote(string input){
		float score =0;
		int numberOfWords =0;
			var words = input.Split(' ');
			foreach(string word in words){
				numberOfWords++;
				if(quotes[quoteIndex].text.Contains(word)) score++;
			}
			score = score/numberOfWords;
			return score;
        }
}