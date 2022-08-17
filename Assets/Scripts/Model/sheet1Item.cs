using UnityEngine;
using System.Collections;
using LitJson;

public class Sheet1Item : DataItem  {
	public string id;
	public override string StringIdentity(){ return id; }
	public string name;
	public int age;
	public string season;
	public string word;
	public bool isalive;
	public string imgname;

    public override void Setup(JsonData data) {
		base.Setup(data);
		id = data["id"].ToString();
		name = data["name"].ToString();
		age = int.Parse(data["age"].ToString());
		season = data["season"].ToString();
		word = data["word"].ToString();
		isalive = data["isalive"].ToString() != "0";
		imgname = data["imgname"].ToString();

    }

	public Sheet1Item () {
	
	}
}
