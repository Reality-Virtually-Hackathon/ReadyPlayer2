using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class MovieScript : MonoBehaviour {
	public string pathToScript;
	public List<Quote> quotes = new List<Quote>();

	// Use this for initialization
	void Start () {
		parseScript(pathToScript);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void parseScript(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);
		while(!inp_stm.EndOfStream)
		{
			string inp_ln = inp_stm.ReadLine( );
			var strArray = inp_ln.Split(':');
			Debug.Log(inp_ln);
			quotes.Add(new Quote(strArray[0], strArray[1]));
		}

		inp_stm.Close( ); 
	}
}