﻿namespace Nebula.Roles;

static public class Roles
{
    public class SideCommonRoles
    {
        public Side side;
        public Role templateRole;

        public AllSideRoles.Secret? Secret;

        public SideCommonRoles(Side side, Role templateRole)
        {
            if (side == Side.Crewmate || side == Side.Impostor) Secret = new AllSideRoles.Secret(templateRole);

            SideRoles.Add(side, this);
        }

        public class SideCommonRolesLoader
        {
            public SideCommonRolesLoader()
            {
                new SideCommonRoles(Side.Crewmate, Roles.Crewmate);
                new SideCommonRoles(Side.Impostor, Roles.Impostor);
            }
        }

    }

    public static Dictionary<Side, SideCommonRoles> SideRoles = new Dictionary<Side, SideCommonRoles>();

    public static List<ExtraAssignable> AllExtraAssignable = new List<ExtraAssignable>();

    public static MetaRoles.VOID VOID = new MetaRoles.VOID();

    public static CrewmateRoles.Crewmate Crewmate = new CrewmateRoles.Crewmate();
    public static CrewmateRoles.CrewmateWithoutTasks CrewmateWithoutTasks = new CrewmateRoles.CrewmateWithoutTasks();
    public static CrewmateRoles.Agent Agent = new CrewmateRoles.Agent();
    public static CrewmateRoles.Alien Alien = new CrewmateRoles.Alien();
    public static CrewmateRoles.Bait Bait = new CrewmateRoles.Bait();
    public static CrewmateRoles.Busker Busker = new CrewmateRoles.Busker();
    public static CrewmateRoles.Comet Comet = new CrewmateRoles.Comet();
    public static CrewmateRoles.DamnedCrew DamnedCrew = new CrewmateRoles.DamnedCrew();
    public static CrewmateRoles.Doctor Doctor = new CrewmateRoles.Doctor();
    public static CrewmateRoles.Guardian Guardian = new CrewmateRoles.Guardian();
    public static CrewmateRoles.Mayor Mayor = new CrewmateRoles.Mayor();
    public static CrewmateRoles.Necromancer Necromancer = new CrewmateRoles.Necromancer();
    public static ComplexRoles.Guesser NiceGuesser = new ComplexRoles.Guesser("NiceGuesser", "niceGuesser", false);
    public static ComplexRoles.Tracker NiceTracker = new ComplexRoles.Tracker("NiceTracker", "niceTracker", false);
    public static ComplexRoles.Trapper NiceTrapper = new ComplexRoles.Trapper("NiceTrapper", "niceTrapper", false);
    public static CrewmateRoles.Oracle Oracle = new CrewmateRoles.Oracle();
    public static CrewmateRoles.Provocateur Provocateur = new CrewmateRoles.Provocateur();
    public static CrewmateRoles.Psychic Psychic = new CrewmateRoles.Psychic();
    public static CrewmateRoles.Navvy Navvy = new CrewmateRoles.Navvy();
    public static CrewmateRoles.Seer Seer = new CrewmateRoles.Seer();
    public static CrewmateRoles.Sheriff Sheriff = new CrewmateRoles.Sheriff();
    public static CrewmateRoles.Splicer Splicer = new CrewmateRoles.Splicer();
    public static CrewmateRoles.Spy Spy = new CrewmateRoles.Spy();

    public static CrewmateRoles.Madmate Madmate = new CrewmateRoles.Madmate();

    public static ImpostorRoles.Impostor Impostor = new ImpostorRoles.Impostor();
    public static ImpostorRoles.BountyHunter BountyHunter = new ImpostorRoles.BountyHunter();
    public static ImpostorRoles.Camouflager Camouflager = new ImpostorRoles.Camouflager();
    public static ImpostorRoles.Cleaner Cleaner = new ImpostorRoles.Cleaner();
    public static ImpostorRoles.Damned Damned = new ImpostorRoles.Damned();
    public static ImpostorRoles.Disturber Disturber = new ImpostorRoles.Disturber();
    public static ImpostorRoles.Eraser Eraser = new ImpostorRoles.Eraser();
    public static ImpostorRoles.EvilAce EvilAce = new ImpostorRoles.EvilAce();
    public static ComplexRoles.Guesser EvilGuesser = new ComplexRoles.Guesser("EvilGuesser", "evilGuesser", true);
    public static ComplexRoles.Tracker EvilTracker = new ComplexRoles.Tracker("EvilTracker", "evilTracker", true);
    public static ComplexRoles.Trapper EvilTrapper = new ComplexRoles.Trapper("EvilTrapper", "evilTrapper", true);
    public static ImpostorRoles.Executioner Executioner = new ImpostorRoles.Executioner();
    public static ImpostorRoles.Jailer Jailer = new ImpostorRoles.Jailer();
    public static ImpostorRoles.Marionette Marionette = new ImpostorRoles.Marionette();
    public static ImpostorRoles.Morphing Morphing = new ImpostorRoles.Morphing();
    public static ImpostorRoles.Painter Painter = new ImpostorRoles.Painter();
    public static ImpostorRoles.Raider Raider = new ImpostorRoles.Raider();
    public static ImpostorRoles.Reaper Reaper = new ImpostorRoles.Reaper();
    public static ImpostorRoles.Sniper Sniper = new ImpostorRoles.Sniper();

    public static NeutralRoles.Arsonist Arsonist = new NeutralRoles.Arsonist();
    public static NeutralRoles.Avenger Avenger = new NeutralRoles.Avenger();
    public static NeutralRoles.ChainShifter ChainShifter = new NeutralRoles.ChainShifter();
    public static NeutralRoles.Empiric Empiric = new NeutralRoles.Empiric();
    public static NeutralRoles.Jackal Jackal = new NeutralRoles.Jackal();
    public static NeutralRoles.Sidekick Sidekick = new NeutralRoles.Sidekick();
    public static NeutralRoles.Jester Jester = new NeutralRoles.Jester();
    public static NeutralRoles.Opportunist Opportunist = new NeutralRoles.Opportunist();
    //public static NeutralRoles.SantaClaus SantaClaus = new NeutralRoles.SantaClaus();
    //public static NeutralRoles.BlackSanta BlackSanta = new NeutralRoles.BlackSanta();
    public static NeutralRoles.Vulture Vulture = new NeutralRoles.Vulture();

    public static MinigameRoles.Player Player = new MinigameRoles.Player();

    public static MinigameRoles.Escapees.Biela Biela = new MinigameRoles.Escapees.Biela();
    public static MinigameRoles.Escapees.Halley Halley = new MinigameRoles.Escapees.Halley();

    public static MinigameRoles.Hunters.Hadar Hadar = new MinigameRoles.Hunters.Hadar();
    public static MinigameRoles.Hunters.Polis Polis = new MinigameRoles.Hunters.Polis();

    public static RitualRoles.RitualCrewmate RitualCrewmate = new RitualRoles.RitualCrewmate();
    public static RitualRoles.RitualKiller RitualKiller = new RitualRoles.RitualKiller();

    public static HnSCrewmateRoles.Crewmate HnSCrewmate = new HnSCrewmateRoles.Crewmate();
    public static HnSImpostorRoles.Impostor HnSImpostor = new HnSImpostorRoles.Impostor();

    public static ComplexRoles.FCrewmate F_Crewmate = new ComplexRoles.FCrewmate();
    public static ComplexRoles.FGuesser F_Guesser = new ComplexRoles.FGuesser();
    public static ComplexRoles.FTracker F_Tracker = new ComplexRoles.FTracker();
    public static ComplexRoles.FTrapper F_Trapper = new ComplexRoles.FTrapper();

    public static ComplexRoles.SecondaryGuesser SecondaryGuesser = new ComplexRoles.SecondaryGuesser();
    public static NeutralRoles.SecondarySidekick SecondarySidekick = new NeutralRoles.SecondarySidekick();
    public static CrewmateRoles.SecondaryMadmate SecondaryMadmate = new CrewmateRoles.SecondaryMadmate();
    public static ExtraRoles.DiamondPossessor DiamondPossessor = new ExtraRoles.DiamondPossessor();
    public static ExtraRoles.Bloody Bloody = new ExtraRoles.Bloody();
    public static ExtraRoles.Confused Confused = new ExtraRoles.Confused();
    public static ExtraRoles.Drunk Drunk = new ExtraRoles.Drunk();
    public static ExtraRoles.Lover Lover = new ExtraRoles.Lover();
    public static ExtraRoles.Trilemma Trilemma = new ExtraRoles.Trilemma();
    public static ExtraRoles.AvengerTarget AvengerTarget = new ExtraRoles.AvengerTarget();
   // public static ExtraRoles.TeamSanta TeamSanta = new ExtraRoles.TeamSanta();
    public static MetaRoles.MetaRole MetaRole = new MetaRoles.MetaRole();
    public static RitualRoles.PerkHolder PerkHolder = new RitualRoles.PerkHolder();

    public static GhostRoles.Poltergeist Poltergeist = new GhostRoles.Poltergeist();

    //全てのロールはこの中に含まれている必要があります
    public static List<Role> AllRoles = new List<Role>()
        {
            VOID,
            Impostor,BountyHunter,Camouflager,Cleaner,Damned,Disturber,Eraser,EvilAce,EvilGuesser,EvilTracker,EvilTrapper,Executioner,Jailer,Marionette,Morphing,Painter,Raider,Reaper,Sniper,
            /*SantaClaus,BlackSanta,*/Arsonist,Avenger,ChainShifter,Empiric,Jackal,Jester,Opportunist,Sidekick,Vulture,
            F_Crewmate,
            F_Guesser,F_Tracker,F_Trapper,
            Crewmate,CrewmateWithoutTasks,Agent,Alien,Bait,Busker,Comet,DamnedCrew,Doctor,Guardian,Mayor,Navvy,Necromancer,NiceGuesser,NiceTracker,NiceTrapper,Oracle,Provocateur,Psychic,Seer,Sheriff,Splicer,Spy,
            Madmate,
            Player,
            Halley,Biela,
            Polis,Hadar,
            RitualCrewmate,RitualKiller,
            HnSCrewmate,
            HnSImpostor
        };

    public static List<ExtraRole> AllExtraRoles = new List<ExtraRole>()
        {
            SecondaryGuesser,SecondarySidekick,SecondaryMadmate,
            DiamondPossessor,/*TeamSanta,*/
            Bloody,Confused,Drunk,Lover,Trilemma,
            MetaRole,AvengerTarget,
            PerkHolder
        };

    public static List<GhostRole> AllGhostRoles = new List<GhostRole>()
        {
            Poltergeist
        };

    private static SideCommonRoles.SideCommonRolesLoader loader = new SideCommonRoles.SideCommonRolesLoader();

    public static void ResetWinTrigger()
    {
        foreach (Role role in AllRoles)
        {
            if (role is Template.HasWinTrigger)
            {
                ((Template.HasWinTrigger)role).WinTrigger = false;
            }
        }

        foreach (ExtraRole role in AllExtraRoles)
        {
            if (role is Template.HasWinTrigger)
            {
                ((Template.HasWinTrigger)role).WinTrigger = false;
            }
        }
    }

    public static void StaticInitialize()
    {
        foreach (Role role in AllRoles)
        {
            role.StaticInitialize();
        }

        foreach (ExtraRole role in AllExtraRoles)
        {
            role.StaticInitialize();
        }

        foreach (Role role in AllRoles)
        {
            role.OnRoleRelationSetting();
        }
    }
}
