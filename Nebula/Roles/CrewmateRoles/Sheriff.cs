﻿namespace Nebula.Roles.CrewmateRoles;

public class Sheriff : Role
{
    static public Color RoleColor = new Color(240f / 255f, 191f / 255f, 0f);

    private CustomButton killButton;

    private Module.CustomOption killCooldownOption;
    private Module.CustomOption canKillMadmateOption;
    private Module.CustomOption canKillSpyOption;
    private Module.CustomOption canKillNecromancerOption;
    private Module.CustomOption canKillSheriffOption;
    private Module.CustomOption canKillOpportunistOption;
    private Module.CustomOption canKillChainShifterOption;
    private Module.CustomOption numberOfShotsOption;
    
    private SpriteLoader killButtonSprite = new SpriteLoader("Nebula.Resources.SheriffKillButton.png", 100f);


    int shots;

    public override void MyPlayerControlUpdate()
    {
        Game.MyPlayerData data = Game.GameData.data.myData;
        data.currentTarget = Patches.PlayerControlPatch.SetMyTarget();
        Patches.PlayerControlPatch.SetPlayerOutline(data.currentTarget, Color.yellow);
    }

    //キルできる相手かどうか調べる
    private bool CanKill(PlayerControl target)
    {
        //Madmateなら確定で自殺する
        if (PlayerControl.LocalPlayer.IsMadmate()) return false;

        var p = target.GetModData();

        //個別に設定したい非クルー陣営
        if (p.role == Roles.Opportunist && canKillOpportunistOption.getBool()) return true;
        if (p.role == Roles.ChainShifter && canKillChainShifterOption.getBool()) return true;

        //非クルーおよび個別に設定するクルー陣営
        if (p.role.category != RoleCategory.Crewmate) return true;
        if (canKillMadmateOption.getBool() && (p.role == Roles.Madmate || p.HasExtraRole(Roles.SecondaryMadmate))) return true;
        if (p.role == Roles.Spy && canKillSpyOption.getBool()) return true;
        if (p.role == Roles.Necromancer && canKillNecromancerOption.getBool()) return true;
        if (p.role == Roles.Sheriff && canKillSheriffOption.getBool()) return true;

        return false;
    }

    public override void ButtonInitialize(HudManager __instance)
    {
        shots = (int)numberOfShotsOption.getFloat();

        if (killButton != null)
        {
            killButton.Destroy();
        }
        killButton = new CustomButton(
            () =>
            {
                PlayerControl target = Game.GameData.data.myData.currentTarget;
                if (!CanKill(target)) target = PlayerControl.LocalPlayer;

                var res = Helpers.checkMuderAttemptAndKill(PlayerControl.LocalPlayer, target, (target == PlayerControl.LocalPlayer) ? Game.PlayerData.PlayerStatus.Misfire : Game.PlayerData.PlayerStatus.Dead, false, true);
                if (res != Helpers.MurderAttemptResult.SuppressKill)
                    killButton.Timer = killButton.MaxTimer;
                Game.GameData.data.myData.currentTarget = null;

                shots--;
                killButton.UsesText.text = shots.ToString();
            },
            () => { return !PlayerControl.LocalPlayer.Data.IsDead && shots > 0; },
            () => { return Game.GameData.data.myData.currentTarget && PlayerControl.LocalPlayer.CanMove; },
            () => { killButton.Timer = killButton.MaxTimer; },
            killButtonSprite.GetSprite(),
            Expansion.GridArrangeExpansion.GridArrangeParameter.AlternativeKillButtonContent,
            __instance,
            Module.NebulaInputManager.modKillInput.keyCode,
            "button.label.kill",ImageNames.AdminMapButton
        ).SetTimer(CustomOptionHolder.InitialKillCoolDownOption.getFloat());
        killButton.MaxTimer = killCooldownOption.getFloat();
        killButton.UsesText.text = shots.ToString();
        killButton.LabelText.outlineColor = Palette.CrewmateBlue;
    }
    public override void CleanUp()
    {
        if (killButton != null)
        {
            killButton.Destroy();
            killButton = null;
        }
    }

    public override void OnRoleRelationSetting()
    {
        RelatedRoles.Add(Roles.Jackal);
    }

    public override void LoadOptionData()
    {
        killCooldownOption = CreateOption(Color.white, "killCoolDown", 30f, 10f, 60f, 2.5f);
        killCooldownOption.suffix = "second";

        canKillMadmateOption = CreateOption(Color.white, "canKillMadmate", true);

        canKillSpyOption = CreateOption(Color.white, "canKillSpy", false);

        canKillNecromancerOption = CreateOption(Color.white, "canKillNecromancer", false);

        canKillSheriffOption = CreateOption(Color.white, "canKillSheriff", false);

        canKillOpportunistOption = CreateOption(Color.white, "canKillOpportunist", false);

        canKillChainShifterOption = CreateOption(Color.white, "canKillChainShifter", true);

        numberOfShotsOption = CreateOption(Color.white, "numberOfShots", 3, 1, 15, 1);
    }

    public Sheriff()
        : base("Sheriff", "sheriff", RoleColor, RoleCategory.Crewmate, Side.Crewmate, Side.Crewmate,
             Crewmate.crewmateSideSet, Crewmate.crewmateSideSet, Crewmate.crewmateEndSet,
             false, VentPermission.CanNotUse, false, false, false)
    {
        killButton = null;
    }
}