using UnityEngine;
using System.Collections;

public class TreeActionTest : MonoBehaviour {

	TreeAction ruch;
	bool toggled;
	// Use this for initialization
	void Start () 
	{
		TreeAction celnyGol=new TreeAction(0.7f, null, true, "Weee... Goool");
		TreeAction celnyOdbitaDoNasDoGraczaGlowa= new TreeAction(0.35f, null, true, "Glowka gracza po odbiciu bramkarza!");
		TreeAction celnyOdbitaDoNasDoGraczaNoga= new TreeAction(0.65f, null, true, "Strzal gracza po odbiciu bramkarza!");
		TreeAction celnyOdbitaDoNasDoPartnera= new TreeAction(0.3f, null, true, "Strzal partnera po odbiciu bramkarza!");
		TreeAction celnyOdbitaDoRywali= new TreeAction(0.25f, null, true, "Rywale dostali piłkę po odbiciu bramkarza!");
		TreeAction celnyOdbitaPozaLinie= new TreeAction(0.6f, null, true, "Rożny po odbiciu bramkarza!");
		TreeAction podanieDoNasDoGraczaGlowa= new TreeAction(0.65f, null, true, "Glowka gracza po dosrodkowaniu!");
		TreeAction podanieDoNasDoGraczaNoga= new TreeAction(0.35f, null, true, "Strzal gracza po dosrodkowaniu!");
		TreeAction podanieDoNasDoPartnera= new TreeAction(0.3f, null, true, "Strzal partnera po dosrodkowaniu!");

		TreeAction celnyOdbitaDoNasDoGracza= new TreeAction(0.7f, new TreeAction[]{celnyOdbitaDoNasDoGraczaGlowa, celnyOdbitaDoNasDoGraczaNoga});
		TreeAction celnyOdbitaDoNas= new TreeAction(0.15f, new TreeAction[]{celnyOdbitaDoNasDoGracza, celnyOdbitaDoNasDoPartnera});
		TreeAction celnyOdbita= new TreeAction(0.3f, new TreeAction[]{celnyOdbitaDoNas, celnyOdbitaDoRywali, celnyOdbitaPozaLinie});
		TreeAction celny= new TreeAction(0.6f, new TreeAction[]{celnyOdbita, celnyGol});

		TreeAction podanieDoNasDoGracza= new TreeAction(0.7f, new TreeAction[]{podanieDoNasDoGraczaGlowa, podanieDoNasDoGraczaNoga});
		TreeAction podanieDoNas= new TreeAction(0.4f, new TreeAction[]{podanieDoNasDoGracza, podanieDoNasDoPartnera});

		ruch= new TreeAction(2f, new TreeAction[]{podanieDoNas, celny});


		toggled=false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(toggled)
			ruch.MakeAction();
		if(Input.GetKeyDown("t"))
			toggled=!toggled;

		if(Input.GetKeyDown("space"))
			ruch.MakeAction();
	}
}
