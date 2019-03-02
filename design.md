# TODO
Climate Champion simulate and animate choices. Choose. Review:
- Reproduce
    - Duplicate households
- Travel (vacation / commute)
    - Fly
    - Drive
    - Train
    - Bike
- Power
    - Coal
    - Nuclear
- Eat
- Home
- Shop
- Military

# Test plan

- [ ] TODO
---

1. Start screen.
    1. Climate Champion.
    1. How long can you survive?
    1. Start button.
    1. [ ] Best year.
1. Vehicle moves forward.
    1. Each unit of distance traveled by vehicle, emit CO2.
    1. [ ] Vehicle moves horizontally on a schedule, returning to it's start location:
1. HUD.
    1. Year 2000.
    1. After tap start, each second or two, year increments.
    1. CO2 meter.
1. Emit CO2:
    1. Gray cloud rises up.
    1. Hear sound.
    1. After rising up, CO2 meter increases.
        1. If CO2 meter reaches max:
            1. Game ends.
                1. Prompt: How long can you survive?
                1. Current year.
                1. Restart button.
                1. Hides gameplay canvas, such as:
                    1. toggle group and
                    1. game instructions.
1. Clicking replacable item, shows toggle group.
    1. Click is mouse down and up in the click area.
    1. Toggle group has options for this replacable.
1. Toggle group. Touch one.
    1. Highlights selected option, such as vehicle.
    1. Highlights object selected.
    1. Other options lose highlight.
    1. Touched option is activated.
    1. Selecting one replaces the others on map.
    1. Switching does not reset timer to emit.
        - Otherwise, a player's optimal strategy is to repeatedly toggle.
    1. Clicking outside toggle area closes group.
    1. [ ] Label at group.
    1. [ ] While toggle group is open, highlights which object on map is being replaced.
    1. [ ] Last toggle group option is help button.
        1. [ ] Clicking help shows modal with text.
1. Vacation.
    1. Airplane
    1. Car
    1. Train
    1. Bicycle: No CO2.
    1. [ ] Vehicles emerge from base of building.
    1. [ ] Vehicles do not collide.
1. Power Plant generates CO2 over power produced.
    1. Coal
    1. Nuclear
1. Commute:
    1. Car
    1. Train
    1. Bicycle: No CO2.
1. Farm generates CO2 over food produced.
    1. Omnivore
    1. Herbivore: Half CO2.
1. Shop.
    1. Produces goods. Generates CO2 over good produced.
    1. Little.
    1. A lot.
1. Services.
    1. Produces services. Generates CO2 over services produced.
    1. Little.
    1. A lot.
1. Reproduction.
    1. Have Three Or More Children: Doubles every station in 30 years.
        1. Resets to default.
        1. After 80 years, toggler per capita disappears.
            1. Extract reproduction toggle to toggles per capita.
            1. [ ] Animate disappearance.
            1. [ ] After last capita disappears, end game.
        1. Position Offsets.
        1. [ ] Find empty place for new spawns.
    1. No Children.
    1. Have Two Children.
        1. Represents repopulation once, because two children will replace two parents.
    1. Have Four Children.
1. Calibrate emission metrics to citations.
1. [ ] Taxes
    1. [ ] United States
    1. [ ] India
1. [ ] Investments
1. [ ] Ocean and plants absorb CO2.
1. [ ] Lightbulbs
1. [ ] Water Use
1. [ ] Electronics
1. [ ] House consumes.
    1. [ ] Food.
    1. [ ] Power.
    1. [ ] Goods.
    1. [ ] Services.
    1. [ ] Money to pay for the above.
1. [ ] Gradually animate weather effect of CO2 concentration.
1. [ ] Isometric grid of a town.
1. [ ] Work produces money.
    1. [ ] Work also consumes resources like a house.
1. [ ] Drag.
    1. [ ] Map scrolls.
1. [ ] As demand increases, another supplier spawns.
1. [ ] House power drain.
    1. [ ] Little.
    1. [ ] Lot.
1. [ ] Speed up and slow down time.
    1. [ ] Read from x1/8 to x8.
    1. [ ] Button to slow down speed up.

# Reference gameplay

- [Me Playing SimCity Classic](https://www.youtube.com/watch?v=OKI2SI9mp8I)
- [SimCity History - SimCity Classic #2](https://www.youtube.com/watch?v=z8TS_0cDFBw)

# Brainstorm

- Sidescroller shows scenarios.
- Rotate around planet.
- Traveler travels path. Player toggles switch of tracks (like Train of Thought). Each vehicle has an emission rate and speed.
