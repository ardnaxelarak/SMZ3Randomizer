﻿using System.Collections.Generic;
using static Randomizer.SMZ3.SMLogic;

namespace Randomizer.SMZ3.Regions.SuperMetroid.Maridia {

    class Outer : SMRegion {

        public override string Name => "Maridia Outer";
        public override string Area => "Maridia";

        public Outer(World world, Config config) : base(world, config) {
            Locations = new List<Location> {
                new Location(this, 136, 0x8FC437, LocationType.Visible, "Missile (green Maridia shinespark)", Logic switch {
                    Hard => new Requirement(items => items.Gravity && items.SpeedBooster),
                    _ => items => items.SpeedBooster,
                }),
                new Location(this, 137, 0x8FC43D, LocationType.Visible, "Super Missile (green Maridia)"),
                new Location(this, 138, 0x8FC47D, LocationType.Visible, "Energy Tank, Mama turtle", Logic switch {
                    Hard => new Requirement(items => items.CanOpenRedDoors() && (
                        items.CanFly(Config) || items.SpeedBooster || items.Grapple ||
                        items.CanSpringBallJump() && (items.Gravity || items.HiJump)
                    )),
                    _ => items => items.CanOpenRedDoors() && (items.CanFly(Config) || items.SpeedBooster || items.Grapple),
                }),
                new Location(this, 139, 0x8FC483, LocationType.Hidden, "Missile (green Maridia tatori)", Logic switch {
                    _ => new Requirement(items => items.CanOpenRedDoors())
                }),
            };
        }

        public override bool CanEnter(Progression items) {
            return Logic switch {
                Hard =>
                    World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() &&
                        (items.Gravity || items.HiJump && (items.CanSpringBallJump() || items.Ice)) ||
                    items.CanAccessMaridiaPortal(World) && items.CardMaridiaL1 && items.CardMaridiaL2 && (
                        items.CanPassBombPassages() ||
                        items.Gravity && items.ScrewAttack ||
                        items.Super && (items.Gravity || items.HiJump && (items.CanSpringBallJump() || items.Ice))
                    ),
                _ => items.Gravity && (
                        World.CanEnter("Norfair Upper West", items) && items.CanUsePowerBombs() ||
                        items.CanAccessMaridiaPortal(World) && items.CardMaridiaL1 && items.CardMaridiaL2 && (items.CanPassBombPassages() || items.ScrewAttack)
                    ),
            };
        }

    }

}
