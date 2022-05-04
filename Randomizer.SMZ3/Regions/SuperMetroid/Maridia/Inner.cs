using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Inner : SMRegion, IReward {

        public override string Name => "Maridia Inner";
        public override string Area => "Maridia";

        public RewardType Reward { get; set; } = RewardType.None;

        public Inner(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 140, 0x8FC4AF, LocationType.Visible, "Super Missile (yellow Maridia)", Logic switch {
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() && CanReachAqueduct(items)
                         && (items.Gravity || items.Ice || items.HiJump && items.SpringBall))
                }),
                new Location(this, 141, 0x8FC4B5, LocationType.Visible, "Missile (yellow Maridia super missile)", Logic switch {
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() && CanReachAqueduct(items)
                         && (items.Gravity || items.Ice || items.HiJump && items.SpringBall))
                }),
                new Location(this, 142, 0x8FC533, LocationType.Visible, "Missile (yellow Maridia false wall)", Logic switch {
                    _ => new Requirement(items => items.CardMaridiaL1 && items.CanPassBombPassages() && CanReachAqueduct(items)
                         && (items.Gravity || items.Ice || items.HiJump && items.SpringBall))
                }),
                new Location(this, 143, 0x8FC559, LocationType.Chozo, "Plasma Beam", Logic switch {
                    Hard => new Requirement(items => CanDefeatDraygon(items) &&
                        (items.Charge && items.HasEnergyReserves(3) || items.ScrewAttack || items.Plasma || items.SpeedBooster) &&
                        (items.HiJump || items.CanSpringBallJump() || items.CanFly(Config) || items.SpeedBooster)),
                    _ => items => CanDefeatDraygon(items) && (items.ScrewAttack || items.Plasma) && (items.HiJump || items.CanFly(Config)),
                }),
                new Location(this, 144, 0x8FC5DD, LocationType.Visible, "Missile (left Maridia sand pit room)", Logic switch {
                    Hard => new Requirement(items => CanReachAqueduct(items) && items.Super &&
                        (items.Gravity || items.HiJump && (items.SpaceJump || items.CanSpringBallJump()))),
                    _ => items => CanReachAqueduct(items) && items.Super && items.CanPassBombPassages(),
                }),
                new Location(this, 145, 0x8FC5E3, LocationType.Chozo, "Reserve Tank, Maridia", Logic switch {
                    Hard => new Requirement(items => CanReachAqueduct(items) && items.Super &&
                        (items.Gravity || items.HiJump && (items.SpaceJump || items.CanSpringBallJump()))),
                    _ => items => CanReachAqueduct(items) && items.Super && items.CanPassBombPassages(),
                }),
                new Location(this, 146, 0x8FC5EB, LocationType.Visible, "Missile (right Maridia sand pit room)", Logic switch {
                    Hard => items => CanReachAqueduct(items) && items.Super && (items.HiJump || items.Gravity),
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Super),
                }),
                new Location(this, 147, 0x8FC5F1, LocationType.Visible, "Power Bomb (right Maridia sand pit room)", Logic switch {
                    Hard => items => CanReachAqueduct(items) && items.Super && (items.Gravity || items.HiJump && items.CanSpringBallJump()),
                    _ => new Requirement(items => CanReachAqueduct(items) && items.Super),
                }),
                new Location(this, 148, 0x8FC603, LocationType.Visible, "Missile (pink Maridia)", Logic switch {
                    Hard => new Requirement(items => CanReachAqueduct(items) && items.Gravity),
                    _ => items => CanReachAqueduct(items) && items.SpeedBooster,
                }),
                new Location(this, 149, 0x8FC609, LocationType.Visible, "Super Missile (pink Maridia)", Logic switch {
                    Hard => new Requirement(items => CanReachAqueduct(items) && items.Gravity),
                    _ => items => CanReachAqueduct(items) && items.SpeedBooster,
                }),
                new Location(this, 150, 0x8FC6E5, LocationType.Chozo, "Spring Ball", Logic switch {
                    Hard => new Requirement(items => items.Super && items.Grapple && items.CanUsePowerBombs() && (
                        items.Gravity && (items.CanFly(Config) || items.HiJump) ||
                        items.Ice && items.HiJump && items.CanSpringBallJump() && items.SpaceJump)
                    ),
                    _ => items => items.Super && items.Grapple && items.CanUsePowerBombs() && (items.SpaceJump || items.HiJump),
                }),
                new Location(this, 151, 0x8FC74D, LocationType.Hidden, "Missile (Draygon)", Logic switch {
                    Hard => new Requirement(items => (
                            items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                            items.CanAccessMaridiaPortal(World)
                        ) && items.Gravity),
                    _ => items =>
                        items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                        items.CanAccessMaridiaPortal(World),
                }),
                new Location(this, 152, 0x8FC755, LocationType.Visible, "Energy Tank, Botwoon", Logic switch {
                    _ => new Requirement(items =>
                        items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                        items.CanAccessMaridiaPortal(World) && items.CardMaridiaL2)
                }),
                new Location(this, 154, 0x8FC7A7, LocationType.Chozo, "Space Jump", Logic switch {
                    _ => new Requirement(items => CanDefeatDraygon(items))
                })
            };
        }

        bool CanReachAqueduct(Progression items) {
            return Logic switch { 
               Hard => items.CardMaridiaL1 && (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple)
                        || items.CardMaridiaL2 && items.CanAccessMaridiaPortal(World),
               _ => items.CardMaridiaL1 && (items.CanFly(Config) || items.SpeedBooster || items.Grapple) 
                        || items.CardMaridiaL2 && items.CanAccessMaridiaPortal(World),
            };
        }

        bool CanDefeatDraygon(Progression items) {
            return Logic switch {
                Hard => (
                    items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                    items.CanAccessMaridiaPortal(World)
                ) && items.CardMaridiaBoss && items.Gravity,
                _ => (
                    items.CardMaridiaL1 && items.CardMaridiaL2 && CanDefeatBotwoon(items) ||
                    items.CanAccessMaridiaPortal(World)
                ) && items.CardMaridiaBoss && items.Gravity && (items.SpeedBooster && items.HiJump || items.CanFly(Config)),
            };
        }

        bool CanDefeatBotwoon(Progression items) {
            return Logic switch {
                Hard => items.Ice || items.SpeedBooster && items.Gravity || items.CanAccessMaridiaPortal(World),
                _ => items.SpeedBooster || items.CanAccessMaridiaPortal(World),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Hard =>
                    items.Super && World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                        (items.Gravity || items.HiJump && (items.Ice || items.CanSpringBallJump()) && items.Grapple) ||
                    items.CanAccessMaridiaPortal(World),
                _ => items.Gravity && (
                    World.CanEnter("Norfair Upper West", items) && items.Super && items.CanUsePowerBombs() &&
                        (items.CanFly(Config) || items.SpeedBooster || items.Grapple) ||
                    items.CanAccessMaridiaPortal(World)
                ),
            };
        }

        public bool CanComplete(Progression items) {
            return GetLocation("Space Jump").Available(items);
        }

    }

}
