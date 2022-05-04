using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.NorfairUpper {

    class Crocomire : SMRegion {

        public override string Name => "Norfair Upper Crocomire";
        public override string Area => "Norfair Upper";

        public Crocomire(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 52, 0x8F8BA4, LocationType.Visible, "Energy Tank, Crocomire", Logic switch {
                    Hard => new Requirement(items => CanAccessCrocomire(items)),
                    _ => items => CanAccessCrocomire(items) && (items.HasEnergyReserves(1) || items.SpaceJump || items.Grapple),
                }),
                new Location(this, 54, 0x8F8BC0, LocationType.Visible, "Missile (above Crocomire)", Logic switch {
                    Hard => new Requirement(items => (
                            items.CanFly(Config) || items.Grapple ||
                            items.HiJump && (items.SpeedBooster || items.CanSpringBallJump() || items.Varia && items.Ice)
                        ) && items.CanHellRun()),
                    _ => items => items.CanFly(Config) || items.Grapple || items.HiJump && items.SpeedBooster,
                }),
                new Location(this, 57, 0x8F8C04, LocationType.Visible, "Power Bomb (Crocomire)", Logic switch {
                    Hard => new Requirement(items => CanAccessCrocomire(items)),
                    _ => items => CanAccessCrocomire(items) && (items.CanFly(Config) || items.HiJump || items.Grapple),
                }),
                new Location(this, 58, 0x8F8C14, LocationType.Visible, "Missile (below Crocomire)", Logic switch {
                    _ => new Requirement(items => CanAccessCrocomire(items) && items.Morph)
                }),
                new Location(this, 59, 0x8F8C2A, LocationType.Visible, "Missile (Grappling Beam)", Logic switch {
                    Hard => new Requirement(items => CanAccessCrocomire(items) && (items.SpeedBooster || items.Morph && (items.CanFly(Config) || items.Grapple))),
                    _ => items => CanAccessCrocomire(items) && items.Morph && (items.CanFly(Config) || items.SpeedBooster && items.CanUsePowerBombs()),
                }),
                new Location(this, 60, 0x8F8C36, LocationType.Chozo, "Grappling Beam", Logic switch {
                    Hard => new Requirement(items => CanAccessCrocomire(items) && (
                        items.SpaceJump || items.Morph || items.Grapple || items.HiJump && items.SpeedBooster
                    )),
                    _ => items => CanAccessCrocomire(items) && items.Morph && (items.CanFly(Config) || items.SpeedBooster && items.CanUsePowerBombs()),
                }),
            };
        }

        bool CanAccessCrocomire(Progression items) {
            return Config.Keysanity ? items.CardNorfairBoss : items.Super;
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Hard => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) && (
                        /* Ice Beam -> Croc Speedway */
                        (Config.Keysanity ? items.CardNorfairL1 : items.Super) && items.CanUsePowerBombs() &&
                            items.SpeedBooster && (items.HasEnergyReserves(3) || items.Varia) ||
                        /* Frog Speedway */
                        items.SpeedBooster && (items.HasEnergyReserves(2) || items.Varia) &&
                            (items.Missile || items.Super || items.Wave /* Blue Gate */) ||
                        /* Cathedral -> through the floor or Vulcano */
                        items.CanHellRun() && items.CanOpenRedDoors() && (Config.Keysanity ? items.CardNorfairL2 : items.Super) &&
                            (items.CanFly(Config) || items.HiJump || items.SpeedBooster || items.CanSpringBallJump() || items.Varia && items.Ice) &&
                            (items.CanPassBombPassages() || items.Varia && items.Morph) &&
                            (items.Missile || items.Super || items.Wave /* Blue Gate */)
                    ) ||
                    /* Reverse Lava Dive */
                    items.Varia && items.CanAccessNorfairLowerPortal() && items.ScrewAttack && items.SpaceJump && items.Super && 
                        items.HasEnergyReserves(2) && (items.CardNorfairL2 || items.Morph),
                _ => (
                        (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                        items.CanAccessNorfairUpperPortal()
                    ) && items.Varia && (
                        /* Ice Beam -> Croc Speedway */
                        (Config.Keysanity ? items.CardNorfairL1 : items.Super) && items.CanUsePowerBombs() && items.SpeedBooster ||
                        /* Frog Speedway */
                        items.SpeedBooster && items.Wave ||
                        /* Cathedral -> through the floor or Vulcano */
                        items.CanOpenRedDoors() && (Config.Keysanity ? items.CardNorfairL2 : items.Super) &&
                            (items.CanFly(Config) || items.HiJump || items.SpeedBooster) &&
                            (items.CanPassBombPassages() || items.Gravity && items.Morph) && items.Wave
                    ) ||
                    /* Reverse Lava Dive */
                    items.Varia && items.CanAccessNorfairLowerPortal() && items.ScrewAttack && items.SpaceJump && items.Super &&
                        items.Gravity && items.Wave && (items.CardNorfairL2 || items.Morph),
            };
        }

    }

}
