using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Crateria {

    class West : SMRegion {

        public override string Name => "Crateria West";
        public override string Area => "Crateria";

        public West(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 8, 0x8F8432, LocationType.Visible, "Energy Tank, Terminator"),
                new Location(this, 5, 0x8F8264, LocationType.Visible, "Energy Tank, Gauntlet", Logic switch {
                    Hard => new Requirement(items => CanEnterAndLeaveGauntlet(items)),
                    _ => items => CanEnterAndLeaveGauntlet(items) && items.HasEnergyReserves(1),
                }),
                new Location(this, 9, 0x8F8464, LocationType.Visible, "Missile (Crateria gauntlet right)", Logic switch {
                    Hard => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages()),
                    _ => items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2),
                }),
                new Location(this, 10, 0x8F846A, LocationType.Visible, "Missile (Crateria gauntlet left)", Logic switch {
                    Hard => new Requirement(items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages()),
                    _ => items => CanEnterAndLeaveGauntlet(items) && items.CanPassBombPassages() && items.HasEnergyReserves(2),
                })
            };
        }

        public override bool CanEnter(Progression items) {
            return items.CanDestroyBombWalls() || items.SpeedBooster;
        }

        private bool CanEnterAndLeaveGauntlet(Progression items) {
            return Logic switch {
                Hard =>
                    items.CardCrateriaL1 && (
                        items.Morph && (items.Bombs || items.TwoPowerBombs) ||
                        items.ScrewAttack ||
                        items.SpeedBooster && items.CanUsePowerBombs() && items.HasEnergyReserves(2)
                    ),
                _ =>
                    items.CardCrateriaL1 && items.Morph && (items.CanFly(Config) || items.SpeedBooster) && (
                        items.CanIbj(Config) ||
                        items.CanUsePowerBombs() && items.TwoPowerBombs ||
                        items.ScrewAttack
                    ),
            };
        }

    }

}
