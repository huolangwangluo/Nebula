﻿namespace Nebula.Roles.ImpostorRoles;

public class Raider : Role
{
    private bool equipAxeFlag;

    /* オプション */
    private Module.CustomOption throwCoolDownOption;
    private Module.CustomOption axeSizeOption;
    public Module.CustomOption axeSpeedOption;
    private Module.CustomOption canKillImpostorsOption;

    private CustomMessage? message;

    private SpriteRenderer? guide;

    public override void LoadOptionData()
    {
        throwCoolDownOption = CreateOption(Color.white, "throwCoolDown", 20f, 5f, 60f, 2.5f);
        throwCoolDownOption.suffix = "second";
        axeSizeOption = CreateOption(Color.white, "axeSize", 1f, 0.5f, 4f, 0.25f);
        axeSizeOption.suffix = "cross";
        axeSpeedOption = CreateOption(Color.white, "axeSpeed", 1f, 0.5f, 2.5f, 0.25f);
        axeSpeedOption.suffix = "cross";
        canKillImpostorsOption = CreateOption(Color.white, "canKillImpostors", false);
    }


    /* ボタン */
    static private CustomButton axeButton;
    static private CustomButton killButton;

    public CustomObject? lastAxe = null;
    public CustomObject? thrownAxe = null;

    public override void ButtonInitialize(HudManager __instance)
    {
        if (axeButton != null)
        {
            axeButton.Destroy();
        }
        axeButton = new CustomButton(
            () =>
            {
                if (equipAxeFlag)
                {
                    RPCEventInvoker.RaiderSettleAxe();
                }
                else
                {
                    lastAxe = RPCEventInvoker.ObjectInstantiate(Objects.ObjectTypes.RaidAxe.Axe, PlayerControl.LocalPlayer.transform.position);

                    __instance.StartCoroutine(GetGuideEnumrator().WrapToIl2Cpp());
                }
                equipAxeFlag = !equipAxeFlag;

                axeButton.SetLabel(equipAxeFlag ? "button.label.unequip" : "button.label.equip");
            },
            () => { return !PlayerControl.LocalPlayer.Data.IsDead; },
            () => { return PlayerControl.LocalPlayer.CanMove && killButton.Timer <= 0f; },
            () =>
            {
                axeButton.Timer = axeButton.MaxTimer;
                axeButton.SetLabel("button.label.equip");
            },
            axeButtonSprite.GetSprite(),
            Expansion.GridArrangeExpansion.GridArrangeParameter.None,
            __instance,
            Module.NebulaInputManager.abilityInput.keyCode,
            "button.label.equip"
        );
        axeButton.Timer = axeButton.MaxTimer = 0f;

        if (killButton != null)
        {
            killButton.Destroy();
        }
        killButton = new CustomButton(
            () =>
            {
                Objects.SoundPlayer.PlaySound(Module.AudioAsset.RaiderThrow);

                RPCEventInvoker.RaiderThrow(lastAxe.GameObject.transform.position, lastAxe.GameObject.transform.eulerAngles.z);
                thrownAxe = lastAxe;
                lastAxe = null;

                killButton.Timer = killButton.MaxTimer;
                equipAxeFlag = false;
                axeButton.SetLabel("button.label.equip");
            },
            () => { return !PlayerControl.LocalPlayer.Data.IsDead; },
            () => { return PlayerControl.LocalPlayer.CanMove && equipAxeFlag && lastAxe.Renderer.color.g > 0.5f; },
            () => { killButton.Timer = killButton.MaxTimer; },
            __instance.KillButton.graphic.sprite,
            Expansion.GridArrangeExpansion.GridArrangeParameter.AlternativeKillButtonContent,
            __instance,
            Module.NebulaInputManager.modKillInput.keyCode,
             "button.label.throw"
        ).SetTimer(CustomOptionHolder.InitialKillCoolDownOption.getFloat());
        killButton.MaxTimer = throwCoolDownOption.getFloat();
        killButton.FireOnClicked = true;
        killButton.SetButtonCoolDownOption(true);

    }
    public override void EditCoolDown(CoolDownType type, float count)
    {
        killButton.Timer -= count;
        killButton.actionButton.ShowButtonText("+" + count + "s");
    }

    public byte deadBodyId;

    public override void OnMeetingStart()
    {
        RPCEventInvoker.RaiderSettleAxe();
        equipAxeFlag = false;
    }

    /* 画像 */
    private SpriteLoader axeGuideSprite = new SpriteLoader("Nebula.Resources.RaiderAxeGuide.png", 100f);
    private SpriteLoader axeButtonSprite = new SpriteLoader("Nebula.Resources.AxeButton.png", 115f);

    public override HelpSprite[] helpSprite => new HelpSprite[]
    {
            new HelpSprite(axeButtonSprite,"role.raider.help.equip",0.3f)
    };

    public override Tuple<string, Action>[] helpButton => new Tuple<string, Action>[]
    {
        new Tuple<string, Action>("role.raider.help.axeSize",()=>{new Objects.EffectCircle(PlayerControl.LocalPlayer.gameObject.transform.position, Palette.White, axeSizeOption.getFloat()*0.4f,16f,false,Palette.ImpostorRed);})
    };

    private IEnumerator GetMessageUpdater()
    {
        while (true)
        {
            bool flag = false;
            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p == PlayerControl.LocalPlayer) continue;
                if (p.GetModData().isInvisiblePlayer)
                {
                    if (p.transform.position.Distance(PlayerControl.LocalPlayer.transform.position) < 5.0f)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (flag) yield return null;
            else break;
        }
    }

    private IEnumerator GetGuideEnumrator()
    {
        if (guide == null)
        {
            GameObject obj = new GameObject();
            obj.name = "RaiderGuide";
            obj.transform.SetParent(PlayerControl.LocalPlayer.transform);
            guide = obj.AddComponent<SpriteRenderer>();
            guide.sprite = axeGuideSprite.GetSprite();
        }
        else
        {
            guide.gameObject.SetActive(true);
        }

        float counter = 0f;
        while (counter < 1f)
        {
            counter += Time.deltaTime;

            float angle = Game.GameData.data.myData.getGlobalData().MouseAngle;
            guide.transform.eulerAngles = new Vector3(0f, 0f, angle * 180f / Mathf.PI);
            guide.transform.localPosition = new Vector3(
                Mathf.Cos(angle) * (counter * 0.6f + 1.8f),
                Mathf.Sin(angle) * (counter * 0.6f + 1.8f),
                -1f);
            guide.color = new Color(1f, 1f, 1f, 0.9f - (counter * 0.9f));

            yield return null;

            if (!equipAxeFlag) break;
        }

        if (equipAxeFlag) HudManager.Instance.StartCoroutine(GetGuideEnumrator().WrapToIl2Cpp());
        else guide.gameObject.SetActive(false);

        yield break;
    }

    public override void MyPlayerControlUpdate()
    {
        if (equipAxeFlag && lastAxe != null)
        {
            if (lastAxe.Data[0] == (int)Objects.ObjectTypes.RaidAxe.AxeState.Static)
            {
                Vector2 axeVec = (Vector2)lastAxe.GameObject.transform.position - (Vector2)PlayerControl.LocalPlayer.GetTruePosition();
                if (PhysicsHelpers.AnyNonTriggersBetween(PlayerControl.LocalPlayer.GetTruePosition(), axeVec.normalized, axeVec.magnitude, Constants.ShipAndObjectsMask))
                {
                    lastAxe.Renderer.color = Color.red;
                }
                else
                {
                    lastAxe.Renderer.color = Color.white;
                }
            }
        }

        if (thrownAxe != null)
        {
            if (!MeetingHud.Instance)
            {
                if (thrownAxe.Data[0] == (int)Objects.ObjectTypes.RaidAxe.AxeState.Thrown)
                {
                    Vector3 pos = thrownAxe.GameObject.transform.position;
                    float d = 0.4f * axeSizeOption.getFloat();
                    foreach (var p in PlayerControl.AllPlayerControls)
                    {
                        if (p.Data.IsDead) continue;
                        if (p == PlayerControl.LocalPlayer) continue;
                        if (!canKillImpostorsOption.getBool() && p.Data.Role.Role == RoleTypes.Impostor) continue;

                        if (p.transform.position.Distance(pos) < d)
                        {
                            Vector2 vec = ((Vector2)p.transform.position) - (Vector2)pos;
                            if (!PhysicsHelpers.AnyNonTriggersBetween(p.transform.position, vec.normalized, vec.magnitude, Constants.ShipAndAllObjectsMask))
                            {
                                var res = Helpers.checkMuderAttemptAndKill(PlayerControl.LocalPlayer, p, Game.PlayerData.PlayerStatus.Beaten, false, false);
                            }
                        }
                    }
                }
            }
        }


        if (message == null || !message.isActive)
        {
            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p == PlayerControl.LocalPlayer) continue;
                if (p.GetModData().isInvisiblePlayer)
                {
                    if (p.transform.position.Distance(PlayerControl.LocalPlayer.transform.position) < 5.0f)
                    {
                        message = new CustomMessage(new Vector3(0, -1.5f, 0), false, Language.Language.GetString("role.sniper.nearMessage"), GetMessageUpdater(), 1.0f, Palette.ImpostorRed);
                        break;
                    }
                }
            }
        }

        if (equipAxeFlag)
        {
            RPCEventInvoker.UpdatePlayerControl();
        }
    }

    public override void OnDied()
    {
        if (equipAxeFlag)
        {
            RPCEventInvoker.RaiderSettleAxe();
            equipAxeFlag = false;
        }
    }

    public override void Initialize(PlayerControl __instance)
    {
        equipAxeFlag = false;

        message = null;

        thrownAxe = null;
        lastAxe = null;

        guide = null;
    }


    public override void CleanUp()
    {
        if (equipAxeFlag)
        {
            RPCEventInvoker.RaiderSettleAxe();
            equipAxeFlag = false;
        }

        if (axeButton != null)
        {
            axeButton.Destroy();
            axeButton = null;
        }

        if (killButton != null)
        {
            killButton.Destroy();
            killButton = null;
        }

        if (guide != null)
        {
            GameObject.Destroy(guide);
            guide = null;
        }

        thrownAxe = null;
        lastAxe = null;

    }

    public Raider()
        : base("Raider", "raider", Palette.ImpostorRed, RoleCategory.Impostor, Side.Impostor, Side.Impostor,
             Impostor.impostorSideSet, Impostor.impostorSideSet,
             Impostor.impostorEndSet,
             true, VentPermission.CanUseUnlimittedVent, true, true, true)
    {
        axeButton = null;
        killButton = null;
        guide = null;

        //通常のキルボタンは使用しない
        HideKillButtonEvenImpostor = true;
    }
}
