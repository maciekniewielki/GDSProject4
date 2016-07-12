using UnityEngine;
using System.Collections;

public class ActionsList : MonoBehaviour {
	public TreeAction strzal;

	void Awake () 
	{
		//celny begin
		TreeAction celnyGol=new TreeAction(0.55f, null, true, "Bramkarz nie wybronil, gooool!");
		TreeAction celnyBroniChwyta=new TreeAction(0.3f, null, true, "Strzal celny, ale bramkarz lapie pilke");
		TreeAction celnyBroniPiastkujeDoRywala=new TreeAction(0.15f, null, true, "Strzal celny, ale bramkarz wypiastkowal do swojego");
		TreeAction celnyBroniPiastkujeDoNas=new TreeAction(0.85f, null, true, "Strzal celny, ale bramkarz wypiastkowal do nas");
		TreeAction celnyBroniOdbijaDoRywala=new TreeAction(0.4f, null, true, "Strzal celny, ale bramkarz odbil do swojego");
		TreeAction celnyBroniOdbijaDoNas=new TreeAction(0.6f, null, true, "Strzal celny, ale bramkarz odbil do nas");
		TreeAction celnyBroniZaLinieRoznyPrawy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Prawy rozny");
		TreeAction celnyBroniZaLinieRoznyLewy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Lewy rozny");
		TreeAction celnyTrafiaRywalaZaLiniePrawyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Prawy rozny");
		TreeAction celnyTrafiaRywalaZaLinieLewyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Lewy rozny");
		TreeAction celnyTrafiaRywalaOpanowuje=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia rywala, ktory ja opanowuje");
		TreeAction celnyTrafiaRywalaDoNas=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia rywala i leci do nas");
		TreeAction celnyTrafiaRywalaOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, "Strzal celny, ale pilka trafia rywala i leci na jedno z otaczajacych pol");
		TreeAction celnyTrafiaNaszegoZaLinie=new TreeAction(0.6f, null, true, "Strzal celny, ale pilka trafia naszego i leci za linie");
		TreeAction celnyTrafiaNaszegoDoRywala=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia naszego i leci do rywala");
		TreeAction celnyTrafiaNaszegoOpanowuje=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia naszego, ktory ja opanowuje");
		TreeAction celnyTrafiaNaszegoOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, "Strzal celny, ale pilka trafia naszego i leci na jedno z otaczajacych pol");

		TreeAction celnyBroniPiastkuje=new TreeAction(0.1f, new TreeAction[]{celnyBroniPiastkujeDoNas, celnyBroniPiastkujeDoRywala});
		TreeAction celnyBroniOdbija=new TreeAction(0.25f, new TreeAction[]{celnyBroniOdbijaDoNas, celnyBroniOdbijaDoRywala});
		TreeAction celnyBroniZaLinie=new TreeAction(0.35f, new TreeAction[]{celnyBroniZaLinieRoznyLewy, celnyBroniZaLinieRoznyPrawy});
		TreeAction celnyTrafiaRywalaZaLinie=new TreeAction(0.6f, new TreeAction[]{celnyTrafiaRywalaZaLinieLewyRozny, celnyTrafiaRywalaZaLiniePrawyRozny});

		TreeAction celnyBroni=new TreeAction(0.25f, new TreeAction[]{celnyBroniChwyta, celnyBroniPiastkuje, celnyBroniOdbija, celnyBroniZaLinie});
		TreeAction celnyTrafiaRywala=new TreeAction(0.15f, new TreeAction[]{celnyTrafiaRywalaZaLinie, celnyTrafiaRywalaOpanowuje, celnyTrafiaRywalaDoNas, celnyTrafiaRywalaOdbicieDoSektorowObok});
		TreeAction celnyTrafiaNaszego=new TreeAction(0.05f, new TreeAction[]{celnyTrafiaNaszegoZaLinie, celnyTrafiaNaszegoDoRywala, celnyTrafiaNaszegoOpanowuje, celnyTrafiaNaszegoOdbicieDoSektorowObok});

		TreeAction celny=new TreeAction(0.5f, new TreeAction[]{celnyGol, celnyBroni, celnyTrafiaRywala, celnyTrafiaNaszego});
		//celny end

		//niecelny begin
		TreeAction nieCelnyZaLinie=new TreeAction(0.85f, null, true, "Strzal byl niecelny, pilka poleciala za linie");
		TreeAction nieCelnySlupekDoBramki=new TreeAction(0.3f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do bramki");
		TreeAction nieCelnySlupekOdbijaDoNas=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do nas");
		TreeAction nieCelnySlupekOdbijaDoRywala=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do rywali");
		TreeAction nieCelnySlupekOdbijaZaLinie=new TreeAction(0.5f, null, true, "Strzal byl niecelny, pilka odbila sie od slupka i poleciala za linie");
		TreeAction nieCelnyPoprzeczkaDoBramki=new TreeAction(0.2f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do bramki");
		TreeAction nieCelnyPoprzeczkaOdbijaDoNas=new TreeAction(0.1f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do nas");
		TreeAction nieCelnyPoprzeczkaOdbijaDoRywala=new TreeAction(0.1f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do rywali");
		TreeAction nieCelnyPoprzeczkaOdbijaZaLinie=new TreeAction(0.8f, null, true, "Strzal byl niecelny, pilka odbila sie od poprzeczki i poleciala za linie");

		TreeAction nieCelnySlupekOdbija=new TreeAction(0.7f, new TreeAction[]{nieCelnySlupekOdbijaDoNas, nieCelnySlupekOdbijaDoRywala, nieCelnySlupekOdbijaZaLinie});
		TreeAction nieCelnyPoprzeczkaOdbija=new TreeAction(0.8f, new TreeAction[]{nieCelnyPoprzeczkaOdbijaDoNas, nieCelnyPoprzeczkaOdbijaDoRywala, nieCelnyPoprzeczkaOdbijaZaLinie});

		TreeAction nieCelnySlupek=new TreeAction(0.1f, new TreeAction[]{nieCelnySlupekOdbija, nieCelnySlupekDoBramki});
		TreeAction nieCelnyPoprzeczka=new TreeAction(0.05f, new TreeAction[]{nieCelnyPoprzeczkaOdbija, nieCelnyPoprzeczkaDoBramki});

		TreeAction nieCelny=new TreeAction(0.5f, new TreeAction[]{nieCelnyZaLinie, nieCelnySlupek, nieCelnyPoprzeczka});
		//niecelny end


		strzal=new TreeAction(2f, new TreeAction[]{nieCelny, celny});
	}

}
