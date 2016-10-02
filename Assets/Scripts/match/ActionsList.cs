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
	public TreeAction longShoot;

	void Awake () 
	{
		LongShootInit();
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
		TreeAction udany=new TreeAction(0.4f, new TreeAction[]{udanyDoGracza, udanyDoPartneraStrzal}, false, "");
		//celny end

		//niecelny begin
		TreeAction nieudany=new TreeAction(0.4f, null, true, "Przeciwnik przejmuje pilke", EnemyBall);
		//niecelny end
		cornerCPU=new TreeAction(2f, new TreeAction[]{nieudany, udany}, false, "");
		//niecelny end
		//cornerCPUend
	}

    void PenaltyInit()
    {
        //penalty begin
        //celny begin
		TreeAction celnyBramkarzNieBroni = new TreeAction(0.75f, null, true, "Pewnie wykonana jedenastka!", CelnyKarny);
        TreeAction celnyBramkarzChwyta = new TreeAction(0.1f, null, true, "Bramkarz broni! Źle wykonany karny!", EnemyBall);
        TreeAction celnyBramkarzWybija = new TreeAction(0.15f, null, true, "Nie ma gola! Bramkarz wyczuł strzelającego! Rzut rożny.", OurBall);

        TreeAction celny = new TreeAction(0.4f, new TreeAction[] { celnyBramkarzNieBroni, celnyBramkarzChwyta, celnyBramkarzWybija }, false, "");
        //celny end

        //niecelny begin
        TreeAction niecelny = new TreeAction(0.4f, null, true, "Pudło! Zmarnowany karny!", EnemyBall);
        //niecelny end
        Penalty = new TreeAction(2f, new TreeAction[] { niecelny, celny }, false, "");
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

        TreeAction udanyPierwszySektor = new TreeAction(0.8f, new TreeAction[] { udanyPierwszySektorDoGracza, udanyPierwszySektorDoPartnera }, false, "");
        TreeAction udanyDrugiSektor = new TreeAction(0.2f, new TreeAction[] { udanyDrugiSektorDoGracza, udanyDrugiSektorDoPartnera }, false, "");
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyPierwszySektor, udanyDrugiSektor }, false, "");
        //udany end

        //nieudany begin
        TreeAction nieudany = new TreeAction(0.4f, null, true, "Źle wykonany aut, rywal przy piłce", EnemyBall);
        //nieudany end
        ThrowInCPU = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
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
        ThrowIn = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
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

        TreeAction udanyCelnyOdbijaZaLinie = new TreeAction(0.6f, new TreeAction[] { udanyCelnyOdbijaZaLinieLewyRozny, udanyCelnyOdbijaZaLiniePrawyRozny }, false, "");
        TreeAction udanyCelnyOdbija = new TreeAction(0.3f, new TreeAction[] { udanyCelnyOdbijaDoPartnera, udanyCelnyOdbijaDoRywala, udanyCelnyOdbijaZaLinie }, false, "");
        TreeAction udanyCelny = new TreeAction(0.6f, new TreeAction[] { udanyCelnyGol, udanyCelnyOdbija }, false, "");
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyCelny, udanyDoPartnera }, false, "");
        //udany end

        //nieudany begin
        TreeAction nieudanyBramkarzLapie = new TreeAction(0.8f, null, true, "Nieudany rzut wolny, piłka w rękach bramkarza", EnemyBall);
        TreeAction nieudanyDoRywala = new TreeAction(0.2f, null, true, "Nieudany rzut wolny, piłka trafia do rywali", EnemyBall);

        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyBramkarzLapie, nieudanyDoRywala }, false, "");
        //nieudany end
        FreeKick = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
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
        TreeAction udanyCelnyOdbijaDruzyna = new TreeAction(0.15f, new TreeAction[] { udanyCelnyOdbijaDruzynaGracz, udanyCelnyOdbijaDruzynaPartner }, false, "");
        TreeAction udanyCelnyOdbijaZaLinie = new TreeAction(0.6f, new TreeAction[] { udanyCelnyOdbijaZaLinieLewyRozny, udanyCelnyOdbijaZaLiniePrawyRozny }, false, "");
        TreeAction udanyCelnyOdbija = new TreeAction(0.3f, new TreeAction[] { udanyCelnyOdbijaDruzyna, udanyCelnyOdbijaDoRywala, udanyCelnyOdbijaZaLinie }, false, "");
        TreeAction udanyCelny = new TreeAction(0.6f, new TreeAction[] { udanyCelnyGol, udanyCelnyOdbija }, false, "");
		TreeAction udanyDoPartneraGracz = new TreeAction(0.0f, new TreeAction[] { udanyDoPartneraGraczGlowa, udanyDoPartneraGraczNoga }, false, "", null, checkTypes.PLAYER_ON_PENALTY, 0.7f);
        TreeAction udanyDoPartnera = new TreeAction(0.4f, new TreeAction[] { udanyDoPartneraGracz, udanyDoPartneraStrzal }, false, "");
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyCelny, udanyDoPartnera }, false, "");
        //udany end

        //nieudany begin
        TreeAction nieudanyBramkarzLapie = new TreeAction(0.8f, null, true, "Nieudany rzut wolny, piłka w rękach bramkarza", EnemyBall);
        TreeAction nieudanyDoRywala = new TreeAction(0.2f, null, true, "Nieudany rzut wolny, piłka trafia do rywali", EnemyBall);

        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyBramkarzLapie, nieudanyDoRywala }, false, "");
        //nieudany end
        FreeKickCPU = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
        //FreeKickCPU end
    }

    void DribblingInit()
    {
        //dribbling begin
        //udany begin
		TreeAction udanyRywalWybija = new TreeAction(0.0f, null, true, "Świetny drybling! Rywal ratuje się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction udanyPrzejscie = new TreeAction(0.8f, null, true, "Świetny drybling! Zawodnik przesuwa się wgłąb boiska", UdanyDrybling); //0.8f
		TreeAction udanyFaulZwyklyBezKontuzji = new TreeAction(0.95f, null, true, "Świetny drybling przerwany faulem. Nic strasznego. Wolny!", NormalnyFaul);
		TreeAction udanyFaulZwyklyKontuzja = new TreeAction(0.05f, null, true, "Świetny drybling przerwany faulem. Wygląda na kontuzję", FoulContusion);
		TreeAction udanyFaulNaZoltaBezKontuzji = new TreeAction(0.85f, null, true, "Świetny drybling przerwany faulem. Będzie żółta kartka", FoulYellow);
		TreeAction udanyFaulNaZoltaKontuzja = new TreeAction(0.15f, null, true, "Świetny drybling przerwany faulem. Jest żółta kartka i kontuzja", FoulContusionYellow);
		TreeAction udanyFaulNaCzerwonaBezKontuzji = new TreeAction(0.7f, null, true, "Świetny drybling przerwany faulem. Będzie czerwona kartka!", FoulRed);
		TreeAction udanyFaulNaCzerwonaKontuzja = new TreeAction(0.3f, null, true, "Świetny drybling przerwany faulem. Jest czerwona kartka i kontuzja", FoulContusionRed);

        TreeAction udanyFaulZwykly = new TreeAction(0.75f, new TreeAction[] { udanyFaulZwyklyBezKontuzji, udanyFaulZwyklyKontuzja }, false, "");
        TreeAction udanyFaulNaZolta = new TreeAction(0.2f, new TreeAction[] { udanyFaulNaZoltaBezKontuzji, udanyFaulNaZoltaKontuzja }, false, "");
        TreeAction udanyFaulNaCzerwona = new TreeAction(0.05f, new TreeAction[] { udanyFaulNaCzerwonaBezKontuzji, udanyFaulNaCzerwonaKontuzja }, false, "");
		TreeAction udanyFaul = new TreeAction(0.2f, new TreeAction[] { udanyFaulZwykly, udanyFaulNaZolta, udanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.15f); //0.2f
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyRywalWybija, udanyPrzejscie, udanyFaul }, false, "");
        //udany end

        //nieudany begin
		TreeAction nieudanyGraczWybija = new TreeAction(0.0f, null, true, "Kiepski drybling, trzeba ratować się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction nieudanyStrata = new TreeAction(0.75f, null, true, "Kiepski drybling i strata piłki", EnemyBall);
        TreeAction nieudanyKopaninaRywal = new TreeAction(0.5f, null, true, "Kiepski drybling, piłka po walce trafia do rywali", EnemyBall);
        TreeAction nieudanyKopaninaPartner = new TreeAction(0.5f, null, true, "Kiepski drybling, piłka szczęśliwie trafia po odbiciu do partnera", OurBall);

		TreeAction nieudanyKopanina = new TreeAction(0.25f, new TreeAction[] { nieudanyKopaninaRywal, nieudanyKopaninaPartner }, false, "", null, checkTypes.BALL_ON_SIDES, 0.2f);
        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyGraczWybija, nieudanyStrata, nieudanyKopanina }, false, "");
        //nieudany end
        Dribbling = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
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

        TreeAction udanyFaulZwykly = new TreeAction(0.75f, new TreeAction[] { udanyFaulZwyklyBezKontuzji, udanyFaulZwyklyKontuzja }, false, "");
        TreeAction udanyFaulNaZolta = new TreeAction(0.2f, new TreeAction[] { udanyFaulNaZoltaBezKontuzji, udanyFaulNaZoltaKontuzja }, false, "");
        TreeAction udanyFaulNaCzerwona = new TreeAction(0.05f, new TreeAction[] { udanyFaulNaCzerwonaBezKontuzji, udanyFaulNaCzerwonaKontuzja }, false, "");
		TreeAction udanyFaul = new TreeAction(0.2f, new TreeAction[] { udanyFaulZwykly, udanyFaulNaZolta, udanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.15f);
        TreeAction udany = new TreeAction(0.6f, new TreeAction[] { udanyRywalWybija, udanyOdbior, udanyFaul }, false, "");
        //udany end

        //nieudany begin
		TreeAction nieudanyGraczWybija = new TreeAction(0.0f, null, true, "Kiepski odbiór, trzeba ratować się wybiciem na aut", Out, checkTypes.BALL_ON_SIDES, 0.05f);
        TreeAction nieudanyMija = new TreeAction(0.75f, null, true, "Kiepski odbiór, akcja rywali trwa", EnemyBall);
		TreeAction nieudanyFaulZwykly=new TreeAction(0.75f, null, true, "Kiepski odbiór, faulujemy rywala. Bez konsekwencji", PlayerFoul);
		TreeAction nieudanyFaulNaZolta=new TreeAction(0.2f, null, true, "Kiepski odbiór, faulujemy rywala. Żółta kartka", PlayerYellow);
		TreeAction nieudanyFaulNaCzerwona = new TreeAction(0.05f, null, true, "Kiepski odbiór, faulujemy rywala. Czerwona kartka", PlayerRed);

		TreeAction nieudanyFaul = new TreeAction(0.25f, new TreeAction[] { nieudanyFaulZwykly, nieudanyFaulNaZolta, nieudanyFaulNaCzerwona }, false, "", null, checkTypes.BALL_ON_SIDES, 0.2f);
        TreeAction nieudany = new TreeAction(0.4f, new TreeAction[] { nieudanyGraczWybija, nieudanyMija, nieudanyFaul }, false, "");
        //nieudany end
        Tackle = new TreeAction(2f, new TreeAction[] { nieudany, udany }, false, "");
        //Tackle end
    }

    void ShootInit()
    {
        //shoot begin
        //celny begin
        TreeAction celnyGol = new TreeAction(0.55f, null, true, new string[]
        {
            "The keeper couldn't reach it! GOAL!",
            "GOOOOOOOAAAAAAL!",
            "GOAL!",
            "Hits the back of the net!",
            "Finds it's way to the net!",
            "What a goal! Lewandowski couldn't have done it better himself!",
            "Scores!",
            "He scores! That surely ain't no material for a stormtrooper!",
            "Perfect shot! Adds his name to the board!",
            "That's a goal!",
            "No chance for the keeper! That's a goal!",
            "Curled it right at the back of the net!",
            "And that's one more goal to his account!",
            "GOAL! He surely knows how to nail it!",
            "Goal! He didn't have any mercy for his opponents there!"
        }, CelnyGol);
        TreeAction celnyBroniChwyta = new TreeAction(0.3f, null, true, new string[]
        {
            "Here's the shot! Caught by the keeper!",
            "The shot caught with ease by the keeper.",
            "No goals from that shot...",
            "They'll need more than that to beat this keeper.",
            "Shot saved by the keeper!",
            "Steady hands from the goalkeeper.",
            "Saved! Ball in the keepers arms!",
            "Oh, what a great save!"
        }, EnemyBall);
		TreeAction celnyBroniPiastkujeDoRywala=new TreeAction(0.15f, null, true, new string[]
        {
            "Keeper blocks the shot! The ball goes to the opponents.",
            "Saved by the goalkeeper! Collected by the defender.",
            "Wasted chance! The defender collects it thanks to the keeper.",
            "Saved but still in play! But no follow-up shot...",
            "The keeper had to use his fists to clear that!",
            "Keeper stops it! Counterattack is possible!",
            "Great reflex from the keeper! Defender secures the line!",
            "No goal! The defender insured the goalkeeper."
        }, EnemyBall);
		TreeAction celnyBroniPiastkujeDoNas=new TreeAction(0.85f, null, true, new string[]
        {
            "Keeper saved it but there's still a chance!",
            "Saved but still in play! Chance to repeat the shot!",
            "The keeper didn't catch the ball! Chance to score!",
            "Keeper saves the situation but he's still in danger!",
            "One shot saved but can he take another?!",
            "He saved it! Will he win again?!",
            "Ball blocked by the keeper but the defence didn't get it!",
            "Keeper somehow blocks it! Can they finish it?!"
        }, OurBall);
		TreeAction celnyBroniOdbijaDoRywala=new TreeAction(0.4f, null, true, new string[]
        {
            "Shot punched away by the keeper! The enemy strikes back!",
            "He saves it! The defender gets to the ball first!",
            "Brilliant save! Ball bounces to his teammates.",
            "Shot deflected by the keeper! Defenders got it covered.",
            "Great shot and even greater save! Defence in possession."
        }, EnemyBall);
		TreeAction celnyBroniOdbijaDoNas=new TreeAction(0.6f, null, true, new string[]
        {
            "Shot deflected but there's a chance for a rebound!",
            "Keeper saves but loses the ball! Chance still alive!",
            "Saved! Can he stop another?!",
            "Ball deflected! Attackers still have a chance!",
            "The goalkeeper manages to save! Will he be able to repeat it?"
        }, OurBall);
		TreeAction celnyBroniZaLinieRoznyPrawy=new TreeAction(0.5f, null, true, new string[]
        {   
            "Shot saved! Ball goes out for a corner!",
            "No goal! Only a corner from the right side...",
            "Keeper deflects it to his right! Corner kick!",
            "Score doesn't change! Only a corner kick for the attackers.",
            "Saves it! Ball out of play and the referee grants them a corner."
        }, RightCorner);
		TreeAction celnyBroniZaLinieRoznyLewy=new TreeAction(0.5f, null, true, new string[]
        {
            "No goal for now! The keeper punches it for a corner.",
            "Save! Ball goes out for a corner!",
            "The keeper made it! He deflected the ball and gave them only a corner.",
            "Great shot and even better save! Corner kick from the left side.",
            "Left corner kick after that attack."
        }, LeftCorner);
		TreeAction celnyTrafiaRywalaZaLiniePrawyRozny=new TreeAction(0.5f, null, true, new string[]
        {
            "Shot blocked by the defender! Ball goes out of for a corner!",
            "Shot blocked by the opponents! Corner from the right side.",
            "Nice shot! The defender does his job. Corner kick!",
            "The ball hits the defender and goes out of play for a corner.",
            "Shot deflected by the defence! The game will continue from the right corner."
        }, RightCorner);
		TreeAction celnyTrafiaRywalaZaLinieLewyRozny=new TreeAction(0.5f, null, true, new string[]
        {
            "The defender was standing in the way! Corner from the left side.",
            "Great dedication from the defender! Ball deflected for a corner!",
            "No goal! Defender in the way - ball goes out for a corner.",
            "No goal! The defender was at his post. Corner after a block!",
            "Example of sacrifice from the defence! Game starts from the left corner."
        }, LeftCorner);
		TreeAction celnyTrafiaRywalaOpanowuje=new TreeAction(0.15f, null, true, new string[]
        {
            "Shot looked good but the defender intercepted it.",
            "He shoots! The defender got in the way! Counter-attack!",
            "The shot hit the defender!",
            "The ball hits the opponent! Opposite team looking to attack.",
            "Defender in the way. The ball changes possession."
        }, EnemyBall);
		TreeAction celnyTrafiaRywalaDoNas=new TreeAction(0.15f, null, true, new string[]
        {
            "Shot deflected by the defender! Another chance!",
            "The ball comes off the defender and lands at our feet!",
            "Shot blocked by the opposite team! Can they do it right this time?!",
            "Blocked in the nick of time! Will they save it again?",
            "First shot blocked! Time to take another!"
        }, OurBall);
		TreeAction celnyTrafiaRywalaOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, new string[]
        {
            "Shot blocked! Ball deflected to the side.",
            "Powerful shot goes off the opponents!",
            "The ball hit the defender and went down to the side.",
            "The defender knew where to stand! Ball goes back...",
            "That could've gone in if it hadn't been for the defender. Cleared it."
        }, RandomAdjacementField);
		TreeAction celnyTrafiaNaszegoZaLinie=new TreeAction(0.6f, null, true, new string[]
        {
            "Shot hit a teammate and went out of play.",
            "Team member in the way! No goal this time. Ball for the keeper.",
            "How unlucky! Teammate was in the way. Ball goes out.",
            "Shot directly into a teammate. Ball for the opponents.",
            "Ball for the rivals as the shot was unluckily blocked by a team member."
        }, EnemyBall);
		TreeAction celnyTrafiaNaszegoDoRywala=new TreeAction(0.15f, null, true, new string[]
        {
            "Shot hit a teammate! The defenders have the advantage.",
            "Counterattack possible as the ball was deflected from a teammate.",
            "Shot blocked by the team member. Defenders take possession.",
            "Unlucky! The shot hit your team member. Opponents start the action.",
            "The ball didn't find the net. It found the back of your teammate..."
        }, EnemyBall);
		TreeAction celnyTrafiaNaszegoOpanowuje=new TreeAction(0.15f, null, true, new string[]
        {    
            "Shoots for a goal but instead hits the teammate! Maybe he'll finish it!",
            "Team member in the line of the shot. Manages to keep in control.",
            "Ball goes into the box! Comes off a teammate! Will he finish?!",
            "Shot blocked by a team member! Teammate comes with support!",
            "Ball hits a teammate. Fortunately, he can continue the action!"
        }, OurBall);
		TreeAction celnyTrafiaNaszegoOdbicieDoSektorowObok=new TreeAction(0.1f, null, true, new string[]
        {
            "Powerful shot straight into the back of a teammate! Ball goes to the side.",
            "Team member hit by the shot! Who will gain the possession?",
            "Shot fatally deflected off a teammate. Will there be another chance?",
            "Ball went straight into a team member. Deflected and out of possession.",
            "No goal this time. The only thing you shot was your teammate..."
        }, RandomAdjacementField);

		TreeAction celnyBroniPiastkuje=new TreeAction(0.1f, new TreeAction[]{celnyBroniPiastkujeDoNas, celnyBroniPiastkujeDoRywala}, false, "");
		TreeAction celnyBroniOdbija=new TreeAction(0.25f, new TreeAction[]{celnyBroniOdbijaDoNas, celnyBroniOdbijaDoRywala}, false, "");
		TreeAction celnyBroniZaLinie=new TreeAction(0.35f, new TreeAction[]{celnyBroniZaLinieRoznyLewy, celnyBroniZaLinieRoznyPrawy}, false, "");
		TreeAction celnyTrafiaRywalaZaLinie=new TreeAction(0.6f, new TreeAction[]{celnyTrafiaRywalaZaLinieLewyRozny, celnyTrafiaRywalaZaLiniePrawyRozny}, false, "");

		TreeAction celnyBroni=new TreeAction(0.25f, new TreeAction[]{celnyBroniChwyta, celnyBroniPiastkuje, celnyBroniOdbija, celnyBroniZaLinie}, false, "");
		TreeAction celnyTrafiaRywala=new TreeAction(0.15f, new TreeAction[]{celnyTrafiaRywalaZaLinie, celnyTrafiaRywalaOpanowuje, celnyTrafiaRywalaDoNas, celnyTrafiaRywalaOdbicieDoSektorowObok}, false, "");
		TreeAction celnyTrafiaNaszego=new TreeAction(0.05f, new TreeAction[]{celnyTrafiaNaszegoZaLinie, celnyTrafiaNaszegoDoRywala, celnyTrafiaNaszegoOpanowuje, celnyTrafiaNaszegoOdbicieDoSektorowObok}, false, "");

		TreeAction celny=new TreeAction(0.5f, new TreeAction[]{celnyGol, celnyBroni, celnyTrafiaRywala, celnyTrafiaNaszego}, false, "");
		//celny end

		//niecelny begin
		TreeAction nieCelnyZaLinie=new TreeAction(0.85f, null, true, new string[]
        {
            "Missed! The shot's gone wide.",
            "Terrible shot! Way off the target.",
            "The shot was dangerous, but for the supporters, not the keeper...",
            "The keeper didn't have to move. Missed it.",
            "Not this time. The shot's gone wide."
        }, EnemyBall);
		TreeAction nieCelnySlupekDoBramki=new TreeAction(0.3f, null, true, new string[]
        {
            "It's in! It luckily came in off the post!",
            "Goal! The ball found its way from the post!",
            "GOAL! It came in off the post!",
            "Score! The keeper had no chance! Came in off the post!",
            "In off the post! Talking about lucky shots!"
        }, PlayerGoal);
		TreeAction nieCelnySlupekOdbijaDoNas=new TreeAction(0.25f, null, true, new string[]
        {
            "Hit the post and gets back into play!",
            "Hit the post but he's still able to finish it!",
            "The post took the hit but here comes another shot!",
            "The post didn't allow for the goal but here comes another try!",
            "First shot ended on the post! The ball's again at his feet..."
        }, OurBall);
		TreeAction nieCelnySlupekOdbijaDoRywala=new TreeAction(0.25f, null, true, new string[]
        {
            "Hit the post! No goal! Opponents with the ball!",
            "Off the post and to the opponent...",
            "No goal! The shot was deflected by the post! Enemy team with the ball.",
            "From the post and to the opposite team.",
            "Hit the bar! No! It was the post! Anyway, ball given away."
        }, EnemyBall);
		TreeAction nieCelnySlupekOdbijaZaLinie=new TreeAction(0.5f, null, true, new string[]
        {
            "Shot came wide off the post. Ball for the keeper.",
            "No goal! Only the post! Ball for the keeper.",
            "Hit the post and wemt over the line. No goal this time.",
            "Oooh! So close! Off the post and out of play.",
            "That was a close one! Ball went out after skimming the post."
        }, EnemyBall);
		TreeAction nieCelnyPoprzeczkaDoBramki=new TreeAction(0.2f, null, true, new string[]
        {
            "Hit the crossbar and... It's in! Unbelievable!",
            "It's in! It came in from the bar!",
            "Oooooh yes! A lovely goal from the bar!",
            "It's in! It's in! It is in! Lovely goal from the bar!",
            "GOAL! Straight from the crossbar and into the net!"
        }, PlayerGoal);
		TreeAction nieCelnyPoprzeczkaOdbijaDoNas=new TreeAction(0.1f, null, true, new string[]
        {
            "Hit the crossbar and goes back into play! Gets another chance!",
            "Hits the bar! There's still a chance to finish it!",
            "Off the crossbar and back into play!",
            "Oh that almost got in! Deflected by the bar! Still in possession!",
            "Comes back off the crossbar! Will he finish it?!"
        }, OurBall);
		TreeAction nieCelnyPoprzeczkaOdbijaDoRywala=new TreeAction(0.1f, null, true, new string[]
        {
            "Hits the crossbar! Possession lost.",
            "Almost got in from the bar! Almost... Possession lost.",
            "Shot deflected by the crossbar! The opponents take over the ball.",
            "Oh my! That nearly went in! Too bad it didn't. Rivals at the ball...",
            "Hits the bar and goes back into play! The defenders take it into possession.",
        }, EnemyBall);
		TreeAction nieCelnyPoprzeczkaOdbijaZaLinie=new TreeAction(0.8f, null, true, new string[]
        {
            "Went out of play after skimming the crossbar! That was a close one...",
            "Off the crossbar and out of play!",
            "No goal despite hitting the crossbar! Out of play.",
            "Hits the bar and went out of play! How unlucky!",
            "Oh I thought that was going in! Off the bar and out of play."
        }, EnemyBall);

		TreeAction nieCelnySlupekOdbija=new TreeAction(0.7f, new TreeAction[]{nieCelnySlupekOdbijaDoNas, nieCelnySlupekOdbijaDoRywala, nieCelnySlupekOdbijaZaLinie}, false, "");
		TreeAction nieCelnyPoprzeczkaOdbija=new TreeAction(0.8f, new TreeAction[]{nieCelnyPoprzeczkaOdbijaDoNas, nieCelnyPoprzeczkaOdbijaDoRywala, nieCelnyPoprzeczkaOdbijaZaLinie}, false, "");

		TreeAction nieCelnySlupek=new TreeAction(0.1f, new TreeAction[]{nieCelnySlupekOdbija, nieCelnySlupekDoBramki}, false, "");
		TreeAction nieCelnyPoprzeczka=new TreeAction(0.05f, new TreeAction[]{nieCelnyPoprzeczkaOdbija, nieCelnyPoprzeczkaDoBramki}, false, "");

		TreeAction nieCelny=new TreeAction(0.5f, new TreeAction[]{nieCelnyZaLinie, nieCelnySlupek, nieCelnyPoprzeczka}, false, "");
		//niecelny end

		shoot=new TreeAction(2f, new TreeAction[]{nieCelny, celny}, false, "");
		//shoot end
	}

	void LongShootInit()
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

		TreeAction celnyBroniPiastkuje=new TreeAction(0.1f, new TreeAction[]{celnyBroniPiastkujeDoNas, celnyBroniPiastkujeDoRywala}, false, "");
		TreeAction celnyBroniOdbija=new TreeAction(0.25f, new TreeAction[]{celnyBroniOdbijaDoNas, celnyBroniOdbijaDoRywala}, false, "");
		TreeAction celnyBroniZaLinie=new TreeAction(0.35f, new TreeAction[]{celnyBroniZaLinieRoznyLewy, celnyBroniZaLinieRoznyPrawy}, false, "");
		TreeAction celnyTrafiaRywalaZaLinie=new TreeAction(0.6f, new TreeAction[]{celnyTrafiaRywalaZaLinieLewyRozny, celnyTrafiaRywalaZaLiniePrawyRozny}, false, "");

		TreeAction celnyBroni=new TreeAction(0.25f, new TreeAction[]{celnyBroniChwyta, celnyBroniPiastkuje, celnyBroniOdbija, celnyBroniZaLinie}, false, "");
		TreeAction celnyTrafiaRywala=new TreeAction(0.15f, new TreeAction[]{celnyTrafiaRywalaZaLinie, celnyTrafiaRywalaOpanowuje, celnyTrafiaRywalaDoNas, celnyTrafiaRywalaOdbicieDoSektorowObok}, false, "");
		TreeAction celnyTrafiaNaszego=new TreeAction(0.05f, new TreeAction[]{celnyTrafiaNaszegoZaLinie, celnyTrafiaNaszegoDoRywala, celnyTrafiaNaszegoOpanowuje, celnyTrafiaNaszegoOdbicieDoSektorowObok}, false, "");

		TreeAction celny=new TreeAction(0.5f, new TreeAction[]{celnyGol, celnyBroni, celnyTrafiaRywala, celnyTrafiaNaszego}, false, "");
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

		TreeAction nieCelnySlupekOdbija=new TreeAction(0.7f, new TreeAction[]{nieCelnySlupekOdbijaDoNas, nieCelnySlupekOdbijaDoRywala, nieCelnySlupekOdbijaZaLinie}, false, "");
		TreeAction nieCelnyPoprzeczkaOdbija=new TreeAction(0.8f, new TreeAction[]{nieCelnyPoprzeczkaOdbijaDoNas, nieCelnyPoprzeczkaOdbijaDoRywala, nieCelnyPoprzeczkaOdbijaZaLinie}, false, "");

		TreeAction nieCelnySlupek=new TreeAction(0.1f, new TreeAction[]{nieCelnySlupekOdbija, nieCelnySlupekDoBramki}, false, "");
		TreeAction nieCelnyPoprzeczka=new TreeAction(0.05f, new TreeAction[]{nieCelnyPoprzeczkaOdbija, nieCelnyPoprzeczkaDoBramki}, false, "");

		TreeAction nieCelny=new TreeAction(0.5f, new TreeAction[]{nieCelnyZaLinie, nieCelnySlupek, nieCelnyPoprzeczka}, false, "");
		//niecelny end

		longShoot=new TreeAction(2f, new TreeAction[]{nieCelny, celny}, false, "");
		//shoot end
	}


    #region resultFunctions
    public void EnemyBall()
    {
        GameManager.instance.noFightNextTurn = true;
        GameManager.instance.ChangeBallPossession(Side.ENEMY);
    }

    public void OurBall()
    {
        GameManager.instance.noFightNextTurn = true;
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
        GameManager.instance.playAnimation("free_kick_successful");
        EnemyBall();
        GameManager.instance.Goal(true, Side.PLAYER);
    }

    public void RandomAdjacementField()
    {
        Vector2[] targets = new Vector2[] { new Vector2(1, 1), Vector2.zero, new Vector2(1, -1) };
        Vector2 target = targets[Random.Range(0, 3)];
        GameManager.instance.SetBallPosition(target);
        GameManager.instance.noFightNextTurn = false;
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
        bool isPlayerPerforming = GameManager.instance.player.position == Vector2.up || GameManager.instance.player.position == Vector2.down ? true : false;
        GameManager.instance.nextAction = new RestartAction(RestartActionType.CORNER, Side.PLAYER, isPlayerPerforming, new Vector2(1, 1));
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
        bool isPlayerPerforming = GameManager.instance.player.position == Vector2.up || GameManager.instance.player.position == Vector2.down ? true : false;
        GameManager.instance.nextAction = new RestartAction(RestartActionType.CORNER, Side.PLAYER, isPlayerPerforming, new Vector2(1, -1));
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
        if (!CalculationsManager.IsPositionOnTheEdge(GameManager.instance.ballPosition))
            return;
        bool isPlayerPerforming = GameManager.instance.ballPosition == GameManager.instance.player.position;
        GameManager.instance.nextAction = new RestartAction(RestartActionType.OUT, Side.PLAYER, isPlayerPerforming, GameManager.instance.ballPosition);
        GameManager.instance.PrepareForRestartMove();

    }

    public void Head()
    {
        if (!CalculationsManager.IsPlayerStandingOnBall())
            return;
        GameManager.instance.nextAction = new RestartAction(RestartActionType.HEAD, Side.PLAYER, true, GameManager.instance.ballPosition);
        GameManager.instance.PrepareForRestartMove();
    }

    public void FreeKickAction()
    {
        GameManager.instance.stats.AddSetPiece(GameManager.instance.possession, RestartActionType.FREEKICK);
        GameManager.instance.logs.FlushTheBuffer();
        RestartActionType moveType = RestartActionType.FREEKICK;
        string move = "Freekick";
        if (CalculationsManager.IsBallOnPenaltyArea() && CalculationsManager.GetResultByPercent(0.5f))
        {
            moveType = RestartActionType.PENALTY;
            move = "Penalty";
        }
        if (CalculationsManager.IsPlayerStandingOnBall() && GameManager.instance.player.contusion == null && !GameManager.instance.player.IsEnergyDepleted())
            GameManager.instance.nextAction = new RestartAction(moveType, Side.PLAYER, true, GameManager.instance.ballPosition);
        else
            GameManager.instance.nextAction = new RestartAction(moveType, CalculationsManager.OtherSide(GameManager.instance.possession), false, GameManager.instance.ballPosition);
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

    public void CelnyGol()
    {
        GameManager.instance.playAnimation("celny_gol");
        PlayerGoal();
    }

    public void CelnyKarny()
    {
        GameManager.instance.playAnimation("penalties_succesful");
        PlayerGoal();
    }

    public void UdanyDrybling()
    {
        GameManager.instance.playAnimation("dribbling_succesful");
        DribbleSuccessful();
    }

    public void NormalnyFaul()
    {
        GameManager.instance.playAnimation("foul_normal");
        Foul();
    }
    #endregion
}

