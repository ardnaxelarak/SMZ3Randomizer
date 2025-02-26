﻿using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Brinstar {

    class Red : SMRegion {

        public override string Name => "Brinstar Red";
        public override string Area => "Brinstar";

        public Red(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 38, 0x8F8876, LocationType.Chozo, "X-Ray Scope", Logic switch {
                    Hard => new Requirement(items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (
                        items.Grapple || items.SpaceJump ||
                        (items.CanIbj(Config) || items.HiJump && items.SpeedBooster || items.CanSpringBallJump()) &&
                            (items.Varia && items.HasEnergyReserves(3) || items.HasEnergyReserves(5)))),
                    _ => items => items.CanUsePowerBombs() && items.CanOpenRedDoors() && (items.Grapple || items.SpaceJump)
                }),
                new Location(this, 39, 0x8F88CA, LocationType.Visible, "Power Bomb (red Brinstar sidehopper room)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 40, 0x8F890E, LocationType.Chozo, "Power Bomb (red Brinstar spike room)", Logic switch {
                    Hard => new Requirement(items => items.Super),
                    _ => items => (items.CanUsePowerBombs() || items.Ice) && items.Super
                }),
                new Location(this, 41, 0x8F8914, LocationType.Visible, "Missile (red Brinstar spike room)", Logic switch {
                    _ => new Requirement(items => items.CanUsePowerBombs() && items.Super)
                }),
                new Location(this, 42, 0x8F896E, LocationType.Chozo, "Spazer", Logic switch {
                    _ => new Requirement(items => items.CanPassBombPassages() && items.Super)
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Hard =>
                    (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                    items.CanAccessNorfairUpperPortal() && (items.Ice || items.CanSpringBallJump() || items.HiJump || items.CanFly(Config)),
                _ =>
                    (items.CanDestroyBombWalls() || items.SpeedBooster) && items.Super && items.Morph ||
                    items.CanAccessNorfairUpperPortal() && (items.Ice || items.HiJump || items.SpaceJump)
            };
        }

    }

}
