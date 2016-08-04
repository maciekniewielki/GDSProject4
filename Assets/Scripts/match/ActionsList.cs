using UnityEngine;
using System.Collections;
//dupa
public class ActionsList: MonoBehaviour
{
	public TreeAction shoot;
	public TreeAction cornerCPU;
    public TreeAction ThrowInCPU;
    public TreeAction ThrowIn;
    public TreeAction FreeKick;
    public TreeAction FreeKickCPU;
    public TreeAction Dribbling;
    public TreeAction Tackle;
    public TreeAction Penalty;

	void Awake () 
	{
		ShootInit();
		CornerCPUInit();
        ThrowInCPUInit();
        ThrowInInit();
        FreeKickInit();
        FreeKickCPUInit();
        DribblingInit();
        TackleInit();
        PenaltyInit();
	}

	void CornerCPUInit()
	{
		//cornerCPUbegin
		//celny begin
		TreeAction udanyDoGraczaGlowa=new TreeAction(0.6f, null, true, "Gracz glowkuje po dosrodkowaniu", Head);
		TreeAction udanyDoGraczaNoga=new TreeAction(0.4f, null, true, "Gracz strzela noga po dosrodkowaniu", PlayerFinishingShot);
		TreeAction udanyDoPartneraStrzal=new TreeAction(1f, null, true, "Partner strzela noga po dosrodkowaniu", ComputerShoot, checkTypes.PLAYER_ON_PENALTY, 0.6f);

		TreeAction udanyDoGracza=new TreeAction(0.0f, new TreeAction[]{udanyDoGraczaGlowa, udanyDoGraczaNoga}, false, "", null, checkTypes.PLAYER_ON_PENALTY, 0.4f);
		TreeAction udany=new TreeAction(0.4f, new TreeAction[]{udanyDoGracza, udanyDoPartneraStrzal});
		//celny end

		//niecelny begin
		TreeAction nieudany=new TreeAction(0.4f, null, true, "Przeciwnik przejmuje pilke", EnemyBall);
		//niecelny end
		cornerCPU=new TreeAction(2f, new TreeAction[]{nieudany, udany});
		//niecelny end
		//cornerCPUend
	}

    void PenaltyInit()
    {
        //penalty begin
        //celny begin
		TreeAction celnyBramkarzNieBroni = new TreeAction(0.75f, null, true, "Pewnie wykonana jedenastka!", PlayerGoal);
        TreeAction celnyBramkarzChwyta = new TreeAction(0.1f, null, true, "Bramkarz broni! Źle wykonany karny!", EnemyBall);
        TreeAction celnyBramkarzWybija = new TreeAction(0.15f, null, true, "Nie ma gola! Bramkarz wyczuł strzelającego! Rzut rożny.", OurBall);

        TreeAction celny = new TreeAction(0.4f, new TreeAction[] { celnyBramkarzNieBroni, celnyBramkarzChwyta, celnyBramkarzWybija });
        //celny end

        //niecelny begin
        TreeAction niecelny = new TreeAction(0.4f, null, true, "Pudło! Zmarnowany karny!", EnemyBall);
        //niecelny end
        Penalty = new TreeAction(2f, new TreeAction[] { niecelny, celny });
        //penalty end
    }

    void ThrowInCPUInit()
    {
        //ThrowInCPU begin
        // udany begin
		TreeAction udanyPierwszySektorDoGracza=new TreeAction(0.8f, null, true, "Gracz otrzymuje piłkę po bliskiej wrzutce z autu", OurBall);
        TreeAction udanyPierwszySektorDoPartnera=new TreeAction(0.2f, null, true, "Partnerzy otrzymują piłkę po bliskiej wrzutce z autu", OurBall);
		TreeAction udanyDrugiSektorDoGracza=new TreeAction(0.8f, null, true, "Gracz otrzymuje piłkę po długiej wrzutce z autu", OurBall);
        TreeAction udanyDrugiSektorDoPartnera=new TreeAction(0.2f, null, true, "Partnerzy otrzymują piłkę po długiej wrzutce z autu", OurBall);

        TreeAction udanyPierwszySektor = new TreeAction(0.8f, new TreeAction[] { udanyPierwszySektorDoGracza, udanyPierwszySektorDoPartnera });
        TreeAction udanyDrugiSektor = new TreeAction(0.2f, new TreeAction[] { udanyDrugiSektorDoGracza, udanyDrugiSektorDoPartnera });
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyPierwszySektor, udanyDrugiSektor });
        //udany end

        //nieudany begin
        TreeAction nieudany = new TreeAction(0.4f, null, true, "Źle wykonany aut, rywal przy piłce", EnemyBall);
        //nieudany end
        ThrowInCPU = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //ThrowInCPUend
    }

    void ThrowInInit()
    {
        //ThrowInPlayer begin
        //udany begin
        TreeAction udany=new TreeAction(0.6f, null, true, "Dobra wrzutka z autu, partnerzy przy piłce", OurBall);
        //udany end

        //nieudany begin
        TreeAction nieudany = new TreeAction(0.4f, null, true, "Zła wrzutka z autu, piłka trafia do rywali", EnemyBall);
        //nieudany end
        ThrowIn = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //ThrowInPlayer end
    }

    void FreeKickInit()
    {
        //FreeKick Player begin
        //udany begin
		TreeAction udanyCelnyGol = new TreeAction(0.7f, null, true, "Piękny gol zdobyty po strzale z rzutu wolnego!", GoalFreeKickPlayer);
        TreeAction udanyCelnyOdbijaDoPartnera = new TreeAction(0.15f, null, true, "Bramkarz broni, ale jeszcze nie koniec akcji...", ComputerShoot);
        TreeAction udanyCelnyOdbijaDoRywala = new TreeAction(0.25f, null, true, "Bramkarz broni! Piłka trafia do obrońców", EnemyBall);
        TreeAction udanyCelnyOdbijaZaLiniePrawyRozny = new TreeAction(0.5f, null, true, "Dobry rzut wolny, ale bramkarz wybil za linie. Prawy rozny", RightCorner);
        TreeAction udanyCelnyOdbijaZaLinieLewyRozny = new TreeAction(0.5f, null, true, "Dobry rzut wolny, ale bramkarz wybil za linie. Lewy rozny", LeftCorner);
        TreeAction udanyDoPartnera = new TreeAction(0.4f, null, true, "Dobre podanie z rzutu wolnego, partner przy piłce!", OurBall);

        TreeAction udanyCelnyOdbijaZaLinie = new TreeAction(0.6f, new TreeAction[] { udanyCelnyOdbijaZaLinieLewyRozny, udanyCelnyOdbijaZaLiniePrawyRozny });
        TreeAction udanyCelnyOdbija = new TreeAction(0.3f, new TreeAction[] { udanyCelnyOdbijaDoPartnera, udanyCelnyOdbijaDoRywala, udanyCelnyOdbijaZaLinie });
        TreeAction udanyCelny = new TreeAction(0.6f, new TreeAction[] { udanyCelnyGol, udanyCelnyOdbija });
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyCelny, udanyDoPartnera });
        //udany end

        //nieudany begin
        TreeAction nieudanyBramkarzLapie = new TreeAction(0.8f, null, true, "Nieudany rzut wolny, piłka w rękach bramkarza", EnemyBall);
        TreeAction nieudanyDoRywala = new TreeAction(0.2f, null, true, "Nieudany rzut wolny, piłka trafia do rywali", EnemyBall);

        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyBramkarzLapie, nieudanyDoRywala });
        //nieudany end
        FreeKick = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //FreeKick Player end
    }

    void FreeKickCPUInit()
    {
        //FreeKickCPU begin
        //udany begin
        TreeAction udanyCelnyGol = new TreeAction(0.7f, null, true, "Piękny gol zdobyty po strzale z rzutu wolnego!", Goal);
		TreeAction udanyCelnyOdbijaDruzynaGraczGlowa = new TreeAction(0.35f, null, true, "Bramkarz odbija wolny, ale jest szansa na dobitkę głową...", Head);
        TreeAction udanyCelnyOdbijaDruzynaGraczNoga = new TreeAction(0.65f, null, true, "Bramkarz odbija wolny, ale jest szansa na dobitkę nogą...", PlayerFinishingShot);
		TreeAction udanyCelnyOdbijaDruzynaPartner = new TreeAction(1f, null, true, "Bramkarz odbija wolny, ale partnerzy dobijają!", ComputerShoot, checkTypes.PLAYER_ON_PENALTY, 0.3f);
        TreeAction udanyCelnyOdbijaDoRywala = new TreeAction(0.25f, null, true, "Bramkarz odbija wolny, piłka trafia pod nogi rywali", EnemyBall);
        TreeAction udanyCelnyOdbijaZaLiniePrawyRozny = new TreeAction(0.5f, null, true, "Dobry rzut wolny, ale bramkarz wybil za linie. Prawy rozny", RightCorner);
        TreeAction udanyCelnyOdbijaZaLinieLewyRozny = new TreeAction(0.5f, null, true, "Dobry rzut wolny, ale bramkarz wybil za linie. Lewy rozny", LeftCorner);
        TreeAction udanyDoPartneraGraczGlowa = new TreeAction(0.65f, null, true, "Podanie z wolnego do partnerów, ci wrzucają piłkę na główkę...");
        TreeAction udanyDoPartneraGraczNoga = new TreeAction(0.35f, null, true, "Podanie z wolnego do partnerów, ci podają idealnie na nogę...", PlayerFinishingShot);
		TreeAction udanyDoPartneraStrzal = new TreeAction(1f, null, true, "Podanie z wolnego do partnera, ten oddaje strzał!", ComputerShoot, checkTypes.PLAYER_ON_PENALTY, 0.3f);

		TreeAction udanyCelnyOdbijaDruzynaGracz = new TreeAction(0.0f, new TreeAction[] { udanyCelnyOdbijaDruzynaGraczGlowa, udanyCelnyOdbijaDruzynaGraczNoga }, false, "", null, checkTypes.PLAYER_ON_PENALTY, 0.7f);
        TreeAction udanyCelnyOdbijaDruzyna = new TreeAction(0.15f, new TreeAction[] { udanyCelnyOdbijaDruzynaGracz, udanyCelnyOdbijaDruzynaPartner });
        TreeAction udanyCelnyOdbijaZaLinie = new TreeAction(0.6f, new TreeAction[] { udanyCelnyOdbijaZaLinieLewyRozny, udanyCelnyOdbijaZaLiniePrawyRozny });
        TreeAction udanyCelnyOdbija = new TreeAction(0.3f, new TreeAction[] { udanyCelnyOdbijaDruzyna, udanyCelnyOdbijaDoRywala, udanyCelnyOdbijaZaLinie });
        TreeAction udanyCelny = new TreeAction(0.6f, new TreeAction[] { udanyCelnyGol, udanyCelnyOdbija });
		TreeAction udanyDoPartneraGracz = new TreeAction(0.0f, new TreeAction[] { udanyDoPartneraGraczGlowa, udanyDoPartneraGraczNoga }, false, "", null, checkTypes.PLAYER_ON_PENALTY, 0.7f);
        TreeAction udanyDoPartnera = new TreeAction(0.4f, new TreeAction[] { udanyDoPartneraGracz, udanyDoPartneraStrzal });
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyCelny, udanyDoPartnera });
        //udany end

        //nieudany begin
        TreeAction nieudanyBramkarzLapie = new TreeAction(0.8f, null, true, "Nieudany rzut wolny, piłka w rękach bramkarza", EnemyBall);
        TreeAction nieudanyDoRywala = new TreeAction(0.2f, null, true, "Nieudany rzut wolny, piłka trafia do rywali", EnemyBall);

        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyBramkarzLapie, nieudanyDoRywala });
        //nieudany end
        FreeKickCPU = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //FreeKickCPU end
    }

    void DribblingInit()
    {
        //dribbling begin
        //udany begin
		TreeAction udanyRywalWybija = new TreeAction(0.0f, null, true, "Świetny drybling! Rywal ratuje się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction udanyPrzejscie = new TreeAction(0.05f, null, true, "Świetny drybling! Zawodnik przesuwa się wgłąb boiska", DribbleSuccessful); //0.8f
		TreeAction udanyFaulZwyklyBezKontuzji = new TreeAction(0.95f, null, true, "Świetny drybling przerwany faulem. Nic strasznego. Wolny!", Foul);
		TreeAction udanyFaulZwyklyKontuzja = new TreeAction(0.05f, null, true, "Świetny drybling przerwany faulem. Wygląda na kontuzję", FoulContusion);
		TreeAction udanyFaulNaZoltaBezKontuzji = new TreeAction(0.85f, null, true, "Świetny drybling przerwany faulem. Będzie żółta kartka", FoulYellow);
		TreeAction udanyFaulNaZoltaKontuzja = new TreeAction(0.15f, null, true, "Świetny drybling przerwany faulem. Jest żółta kartka i kontuzja", FoulContusionYellow);
		TreeAction udanyFaulNaCzerwonaBezKontuzji = new TreeAction(0.7f, null, true, "Świetny drybling przerwany faulem. Będzie czerwona kartka!", FoulRed);
		TreeAction udanyFaulNaCzerwonaKontuzja = new TreeAction(0.3f, null, true, "Świetny drybling przerwany faulem. Jest czerwona kartka i kontuzja", FoulContusionRed);

        TreeAction udanyFaulZwykly = new TreeAction(0.75f, new TreeAction[] { udanyFaulZwyklyBezKontuzji, udanyFaulZwyklyKontuzja });
        TreeAction udanyFaulNaZolta = new TreeAction(0.2f, new TreeAction[] { udanyFaulNaZoltaBezKontuzji, udanyFaulNaZoltaKontuzja });
        TreeAction udanyFaulNaCzerwona = new TreeAction(0.05f, new TreeAction[] { udanyFaulNaCzerwonaBezKontuzji, udanyFaulNaCzerwonaKontuzja });
		TreeAction udanyFaul = new TreeAction(0.95f, new TreeAction[] { udanyFaulZwykly, udanyFaulNaZolta, udanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.15f); //0/2f
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyRywalWybija, udanyPrzejscie, udanyFaul });
        //udany end

        //nieudany begin
		TreeAction nieudanyGraczWybija = new TreeAction(0.0f, null, true, "Kiepski drybling, trzeba ratować się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction nieudanyStrata = new TreeAction(0.75f, null, true, "Kiepski drybling i strata piłki", EnemyBall);
        TreeAction nieudanyKopaninaRywal = new TreeAction(0.5f, null, true, "Kiepski drybling, piłka po walce trafia do rywali", EnemyBall);
        TreeAction nieudanyKopaninaPartner = new TreeAction(0.5f, null, true, "Kiepski drybling, piłka szczęśliwie trafia po odbiciu do partnera", OurBall);

		TreeAction nieudanyKopanina = new TreeAction(0.25f, new TreeAction[] { nieudanyKopaninaRywal, nieudanyKopaninaPartner }, false, "", null, checkTypes.BALL_ON_SIDES, 0.2f);
        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyGraczWybija, nieudanyStrata, nieudanyKopanina });
        //nieudany end
        Dribbling = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //dribbling end
    }

    void TackleInit()
    {
        //tackle begin
        //udany begin
		TreeAction udanyRywalWybija = new TreeAction(0.0f, null, true, "Świetny odbiór! Rywal ratuje się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction udanyOdbior = new TreeAction(0.8f, null, true, "Świetny odbiór!");
		TreeAction udanyFaulZwyklyBezKontuzji = new TreeAction(0.95f, null, true, "Rywal nas fauluje w walce. Nic strasznego. Wolny!", Foul);
		TreeAction udanyFaulZwyklyKontuzja = new TreeAction(0.05f, null, true, "Rywal nas fauluje w walce. Wygląda na kontuzję", FoulContusion);
		TreeAction udanyFaulNaZoltaBezKontuzji = new TreeAction(0.85f, null, true, "Rywal nas fauluje w walce. Będzie żółta kartka", FoulYellow);
		TreeAction udanyFaulNaZoltaKontuzja = new TreeAction(0.15f, null, true, "Rywal nas fauluje w walce. Jest żółta kartka i kontuzja", FoulContusionYellow);
		TreeAction udanyFaulNaCzerwonaBezKontuzji = new TreeAction(0.7f, null, true, "Rywal nas fauluje w walce. Będzie czerwona kartka!", FoulRed);
		TreeAction udanyFaulNaCzerwonaKontuzja = new TreeAction(0.3f, null, true, "Rywal nas fauluje w walce. Jest czerwona kartka i kontuzja", FoulContusionRed);

        TreeAction udanyFaulZwykly = new TreeAction(0.75f, new TreeAction[] { udanyFaulZwyklyBezKontuzji, udanyFaulZwyklyKontuzja });
        TreeAction udanyFaulNaZolta = new TreeAction(0.2f, new TreeAction[] { udanyFaulNaZoltaBezKontuzji, udanyFaulNaZoltaKontuzja });
        TreeAction udanyFaulNaCzerwona = new TreeAction(0.05f, new TreeAction[] { udanyFaulNaCzerwonaBezKontuzji, udanyFaulNaCzerwonaKontuzja });
		TreeAction udanyFaul = new TreeAction(0.2f, new TreeAction[] { udanyFaulZwykly, udanyFaulNaZolta, udanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.15f);
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyRywalWybija, udanyOdbior, udanyFaul });
        //udany end

        //nieudany begin
		TreeAction nieudanyGraczWybija = new TreeAction(0.0f, null, true, "Kiepski odbiór, trzeba ratować się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction nieudanyMija = new TreeAction(0.75f, null, true, "Kiepski odbiór, akcja rywali trwa", EnemyBall);
		TreeAction nieudanyFaulZwykly=new TreeAction(0.75f, null, true, "Kiepski odbiór, faulujemy rywala. Bez konsekwencji", PlayerFoul);
		TreeAction nieudanyFaulNaZolta=new TreeAction(0.2f, null, true, "Kiepski odbiór, faulujemy rywala. Żółta kartka", PlayerYellow);
		TreeAction nieudanyFaulNaCzerwona = new TreeAction(0.05f, null, true, "Kiepski odbiór, faulujemy rywala. Czerwona kartka", PlayerRed);

		TreeAction nieudanyFaul = new TreeAction(0.25f, new TreeAction[] { nieudanyFaulZwykly, nieudanyFaulNaZolta, nieudanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.2f);
        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyGraczWybija, nieudanyMija, nieudanyFaul });
        //nieudany end
        Tackle = new TreeAction(2f, new TreeAction[] { nieudany, udany });
        //Tackle end
    }

    void ShootInit()
	{
		//shoot begin
		//celny begin
		TreeAction celnyGol=new TreeAction(0.55f, null, true, "Bramkarz nie wybronil, gooool!", PlayerGoal);
		TreeAction celnyBroniChwyta=new TreeAction(0.3f, null, true, "Strzal celny, ale bramkarz lapie pilke", EnemyBall);
		TreeAction celnyBroniPiastkujeDoRywala=new TreeAction(0.15f, null, true, "Strzal celny, ale bramkarz wypiastkowal do swojego", EnemyBall);
		TreeAction celnyBroniPiastkujeDoNas=new TreeAction(0.85f, null, true, "Strzal celny, ale bramkarz wypiastkowal do nas", OurBall);
		TreeAction celnyBroniOdbijaDoRywala=new TreeAction(0.4f, null, true, "Strzal celny, ale bramkarz odbil do swojego", EnemyBall);
		TreeAction celnyBroniOdbijaDoNas=new TreeAction(0.6f, null, true, "Strzal celny, ale bramkarz odbil do nas", OurBall);
		TreeAction celnyBroniZaLinieRoznyPrawy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Prawy rozny", RightCorner);
		TreeAction celnyBroniZaLinieRoznyLewy=new TreeAction(0.5f, null, true, "Strzal celny, ale bramkarz wybil za linie. Lewy rozny", LeftCorner);
		TreeAction celnyTrafiaRywalaZaLiniePrawyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Prawy rozny", RightCorner);
		TreeAction celnyTrafiaRywalaZaLinieLewyRozny=new TreeAction(0.5f, null, true, "Strzal celny, ale pilka trafia rywala i leci za linie. Lewy rozny", LeftCorner);
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
		TreeAction nieCelnySlupekDoBramki=new TreeAction(0.3f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do bramki", PlayerGoal);
		TreeAction nieCelnySlupekOdbijaDoNas=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do nas", OurBall);
		TreeAction nieCelnySlupekOdbijaDoRywala=new TreeAction(0.25f, null, true, "Strzal byl niecelny, ale pilka odbila sie od slupka do rywali", EnemyBall);
		TreeAction nieCelnySlupekOdbijaZaLinie=new TreeAction(0.5f, null, true, "Strzal byl niecelny, pilka odbila sie od slupka i poleciala za linie", EnemyBall);
		TreeAction nieCelnyPoprzeczkaDoBramki=new TreeAction(0.2f, null, true, "Strzal byl niecelny, ale pilka odbila sie od poprzeczki do bramki", PlayerGoal);
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
	}

	public void OurBall()
	{
		GameManager.instance.noFightNextTurn=true;
		GameManager.instance.ChangeBallPossession(Side.PLAYER);
	}

	public void PlayerGoal()
	{
		EnemyBall();
		GameManager.instance.Goal(true, Side.PLAYER);
	}

	public void Goal()
	{
		EnemyBall();
		GameManager.instance.Goal(false, Side.PLAYER);
	}

	public void GoalFreeKickPlayer()
	{
		GameManager.instance.EndActionTree();
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

	public void LeftCorner()
	{
		ActualLeftCorner();
	}

	public void ActualLeftCorner()
	{
		GameManager.instance.stats.AddSetPiece(Side.PLAYER, RestartActionType.CORNER);
		GameManager.instance.SetSelectedMove("Corner");
		GameManager.instance.ChangeBallPossession(Side.PLAYER);
		bool isPlayerPerforming=GameManager.instance.player.position==Vector2.up||GameManager.instance.player.position==Vector2.down?true: false;
		GameManager.instance.nextAction=new RestartAction(RestartActionType.CORNER, Side.PLAYER, isPlayerPerforming, new Vector2(1,1));
		GameManager.instance.PrepareForRestartMove();
	}

	public void RightCorner()
	{
		ActualRightCorner();
	}

	public void ActualRightCorner()
	{
		GameManager.instance.stats.AddSetPiece(Side.PLAYER, RestartActionType.CORNER);
		GameManager.instance.SetSelectedMove("Corner");
		GameManager.instance.ChangeBallPossession(Side.PLAYER);
		bool isPlayerPerforming=GameManager.instance.player.position==Vector2.up||GameManager.instance.player.position==Vector2.down?true: false;
		GameManager.instance.nextAction=new RestartAction(RestartActionType.CORNER, Side.PLAYER, isPlayerPerforming, new Vector2(1,-1));
		GameManager.instance.PrepareForRestartMove();
	}

	public void PlayerFinishingShot()
	{
		GameManager.instance.MakeMove("Shoot", Vector2.right);
	}

	public void ComputerShoot()
	{
		GameManager.instance.ComputerShoot();
	}

	public void Out()
	{
		GameManager.instance.stats.AddSetPiece(Side.PLAYER, RestartActionType.OUT);
		if(!CalculationsManager.IsPositionOnTheEdge(GameManager.instance.ballPosition))
			return;
		bool isPlayerPerforming=GameManager.instance.ballPosition==GameManager.instance.player.position;
		GameManager.instance.nextAction=new RestartAction(RestartActionType.OUT, Side.PLAYER, isPlayerPerforming, GameManager.instance.ballPosition);
		GameManager.instance.PrepareForRestartMove();
			
	}

	public void Head()
	{
		if(!CalculationsManager.IsPlayerStandingOnBall())
			return;
		GameManager.instance.nextAction=new RestartAction(RestartActionType.HEAD, Side.PLAYER, true, GameManager.instance.ballPosition);
		GameManager.instance.PrepareForRestartMove();
	}

	public void FreeKickAction()
	{
		GameManager.instance.stats.AddSetPiece(GameManager.instance.possession, RestartActionType.FREEKICK);
		GameManager.instance.logs.FlushTheBuffer();
		RestartActionType moveType=RestartActionType.FREEKICK;
		string move="Freekick";
		if(CalculationsManager.IsBallOnPenaltyArea()&&CalculationsManager.GetResultByPercent(0.5f))
		{
			moveType=RestartActionType.PENALTY;
			move="Penalty";
		}
		if(CalculationsManager.IsPlayerStandingOnBall()&&GameManager.instance.player.contusion==null&&!GameManager.instance.player.IsEnergyDepleted())
			GameManager.instance.nextAction=new RestartAction(moveType, Side.PLAYER, true, GameManager.instance.ballPosition);
		else
			GameManager.instance.nextAction=new RestartAction(moveType, CalculationsManager.OtherSide(GameManager.instance.possession), false, GameManager.instance.ballPosition);
		GameManager.instance.SetSelectedMove(move);
		GameManager.instance.PrepareForRestartMove();
	}

	public void DribbleSuccessful()
	{
		GameManager.instance.player.MoveYourself(GameManager.instance.player.dribblingTarget);
		GameManager.instance.SetBallPosition(GameManager.instance.player.dribblingTarget);
	}

	public void Foul()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		FreeKickAction();
	}

	public void FoulContusion()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		GameManager.instance.player.GetContusion(CalculationsManager.GetRandomContusion());
		FreeKickAction();
	}

	public void FoulContusionYellow()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		GameManager.instance.player.GetContusion(CalculationsManager.GetRandomContusion());
		GameManager.instance.Card(false, Side.ENEMY, GameManager.instance.ballPosition, "yellow");
		FreeKickAction();
	}

	public void FoulContusionRed()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		GameManager.instance.player.GetContusion(CalculationsManager.GetRandomContusion());
		GameManager.instance.Card(false, Side.ENEMY, GameManager.instance.ballPosition, "red");
		FreeKickAction();
	}

	public void FoulYellow()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		GameManager.instance.Card(false, Side.ENEMY, GameManager.instance.ballPosition, "yellow");
		FreeKickAction();
	}

	public void FoulRed()
	{
		GameManager.instance.stats.AddFoul(Side.ENEMY);
		GameManager.instance.Card(false, Side.ENEMY, GameManager.instance.ballPosition, "red");
		FreeKickAction();
	}

	public void PlayerYellow()
	{
		GameManager.instance.stats.AddFoul(Side.PLAYER);
		GameManager.instance.player.DoFoul();
		GameManager.instance.Card(true, Side.PLAYER, Vector2.zero, "yellow");
		FreeKickAction();
	}

	public void PlayerRed()
	{
		GameManager.instance.stats.AddFoul(Side.PLAYER);
		GameManager.instance.player.DoFoul();
		GameManager.instance.Card(true, Side.PLAYER, Vector2.zero, "red");
		FreeKickAction();
	}

	public void PlayerFoul()
	{
		GameManager.instance.player.DoFoul();
		GameManager.instance.stats.AddFoul(Side.PLAYER);
		FreeKickAction();
	}

}
