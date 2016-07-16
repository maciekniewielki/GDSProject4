using UnityEngine;
using System.Collections;
//dupa
public class ActionsList : MonoBehaviour 
{

	public TreeAction shoot;

	void Awake () 
	{
//shoot begin
		//celny begin
		TreeAction celnyGol=new TreeAction(0.55f, null, true, "Bramkarz nie wybronil, gooool!", Goal);
		TreeAction celnyBroniChwyta=new TreeAction(0.3f, null, true, "Strzal celny, ale bramkarz lapie pilke", EnemyBall);
		TreeAction celnyBroniPiastkujeDoRywala=new TreeAction(0.15f, null, true, "Strzal celny, ale bramkarz wypiastkowal do swojego", EnemyBall);
		TreeAction celnyBroniPiastkujeDoNas=new TreeAction(0.85f, null, true, "Strzal celny, ale bramkarz wypiastkowal do nas", OurBall);
		TreeAction celnyBroniOdbijaDoRywala=new TreeAction(0.4f, null, true, "Strzal celny, ale bramkarz odbil do swojego", EnemyBall);
		TreeAction celnyBroniOdbijaDoNas=new TreeAction(0.6f, null, true, "Strzal celny, ale bramkarz odbil do nas", OurBall);
		TreeAction celnyBroniZaLinieRoznyPrawy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Prawy rozny");
		TreeAction celnyBroniZaLinieRoznyLewy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Lewy rozny");
		TreeAction celnyTrafiaRywalaZaLiniePrawyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Prawy rozny");
		TreeAction celnyTrafiaRywalaZaLinieLewyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Lewy rozny");
		TreeAction celnyTrafiaRywalaOpanowuje=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia rywala, ktory ja opanowuje", EnemyBall);
		TreeAction celnyTrafiaRywalaDoNas=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia rywala i leci do nas", OurBall);
		TreeAction celnyTrafiaRywalaOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, "Strzal celny, ale pilka trafia rywala i leci na jedno z otaczajacych pol", RandomAdjacementField);
		TreeAction celnyTrafiaNaszegoZaLinie=new TreeAction(0.6f, null, true, "Strzal celny, ale pilka trafia naszego i leci za linie", EnemyBall);
		TreeAction celnyTrafiaNaszegoDoRywala=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia naszego i leci do rywala", EnemyBall);
		TreeAction celnyTrafiaNaszegoOpanowuje=new TreeAction(0.15f, null, true, "Strzal celny, ale pilka trafia naszego, ktory ja opanowuje", OurBall);
		TreeAction celnyTrafiaNaszegoOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, "Strzal celny, ale pilka trafia naszego i leci na jedno z otaczajacych pol", RandomAdjacementField);

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
		TreeAction nieCelnyZaLinie=new TreeAction(0.85f, null, true, "Strzal byl niecelny, pilka poleciala za linie", EnemyBall);
		TreeAction nieCelnySlupekDoBramki=new TreeAction(0.3f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do bramki", Goal);
		TreeAction nieCelnySlupekOdbijaDoNas=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do nas", OurBall);
		TreeAction nieCelnySlupekOdbijaDoRywala=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do rywali", EnemyBall);
		TreeAction nieCelnySlupekOdbijaZaLinie=new TreeAction(0.5f, null, true, "Strzal byl niecelny, pilka odbila sie od slupka i poleciala za linie", EnemyBall);
		TreeAction nieCelnyPoprzeczkaDoBramki=new TreeAction(0.2f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do bramki", Goal);
		TreeAction nieCelnyPoprzeczkaOdbijaDoNas=new TreeAction(0.1f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do nas", OurBall);
		TreeAction nieCelnyPoprzeczkaOdbijaDoRywala=new TreeAction(0.1f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do rywali", EnemyBall);
		TreeAction nieCelnyPoprzeczkaOdbijaZaLinie=new TreeAction(0.8f, null, true, "Strzal byl niecelny, pilka odbila sie od poprzeczki i poleciala za linie", EnemyBall);

		TreeAction nieCelnySlupekOdbija=new TreeAction(0.7f, new TreeAction[]{nieCelnySlupekOdbijaDoNas, nieCelnySlupekOdbijaDoRywala, nieCelnySlupekOdbijaZaLinie});
		TreeAction nieCelnyPoprzeczkaOdbija=new TreeAction(0.8f, new TreeAction[]{nieCelnyPoprzeczkaOdbijaDoNas, nieCelnyPoprzeczkaOdbijaDoRywala, nieCelnyPoprzeczkaOdbijaZaLinie});

		TreeAction nieCelnySlupek=new TreeAction(0.1f, new TreeAction[]{nieCelnySlupekOdbija, nieCelnySlupekDoBramki});
		TreeAction nieCelnyPoprzeczka=new TreeAction(0.05f, new TreeAction[]{nieCelnyPoprzeczkaOdbija, nieCelnyPoprzeczkaDoBramki});

		TreeAction nieCelny=new TreeAction(0.5f, new TreeAction[]{nieCelnyZaLinie, nieCelnySlupek, nieCelnyPoprzeczka});
		//niecelny end

		shoot=new TreeAction(2f, new TreeAction[]{nieCelny, celny});
//shoot end
	}

	public void EnemyBall()
	{
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.ChangeBallPossession(Side.ENEMY);
		Miss();
	}

	public void OurBall()
	{
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.ChangeBallPossession(Side.PLAYER);
		Miss();
	}

	public void Goal()
	{
		EnemyBall();
		GameManager.instance.Goal(true, Side.PLAYER);
	}

	public void RandomAdjacementField()
	{
		Vector2[] targets=new Vector2[]{new Vector2(1,1), Vector2.zero, new Vector2(1,-1)};
		Vector2 target=targets[Random.Range(0,3)];
		GameManager.instance.SetBallPosition(target);
		GameManager.instance.noFightNextTurn=false;
	}

	public void Miss()
	{
		GameManager.instance.Miss(true, Side.PLAYER);
	}

}
