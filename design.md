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
    1. [ ] Vehicle moves horizontally on a schedule, returning to it's start location.
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
    1. While toggle group is open, highlights which object on map is being replaced.
        1. Background callout of object being replaced.
        1. Label at object being replaced.
    1. Show icon of object to replace with.
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
1. [ ] How to play better?
    1. [ ] How can I survive longer than 2030s?
    1. [ ] How to make a difference?
    1. [ ] I give up.
    1. [ ] What to do to have an effect?
    1. [ ] Starts too stressful. Maybe start off pre-industrial?
    1. [ ] Bit chaotic.
    1. [ ] What is most optimal?
        1. [ ] Why is there a duplicate government?
            1. Three islands, with land bridge.
            1. Remove downward CO2 traveler.
            1. Car commute on land.
            1. Airplane travel up right screen.
            1. Couple travels from left off screen to center of first island.
            1. Bed of next couple on right side.
            1. [ ] Couple tweens to next place.
            1. Next couple travel to next island.
                1. [ ] If four children, travel from first to next island.
            1. First couple on first island.
            1. [ ] Stations tween from the couple.
        1. Start meter fill in middle.
        1. Tween emission to meter.
            1. Tween emission to fill of meter.
        1. [ ] Couple travels from home to plot.
            1. [ ] Hammer animates and radial meter appears.
            1. [ ] When meter is full, couple brings product home.
            1. [ ] Couple visits each plot:
                1. [ ] Commute garage: money
                1. [ ] Power plant: lightning
                1. [ ] Shop: box
                1. [ ] Theater: smile
                1. [ ] Vacation port: sun
                1. [ ] Government embassy: red cross
                1. [ ] Bed: heart
        1. [ ] While meter appears, can switch option.
        1. [ ] Airplane flies more frequently, shorter distance.
1. [ ] Switch children. No effect. What does that change?
    1. [ ] Is two children and four children do something more?
    1. [ ] Children duplicated???
    1. [ ] Why are things duplicated at the end of the year?
    1. [ ] Why is there another cow?
        1. [ ] Bed changes meters on 0, 1, or 2, home plots.
        1. [ ] Next couple has different character images.
        1. [ ] Hammer meter appears on all locations to be built.
        1. [ ] Timer.
1. [ ] I want to change and look at the rate of the bar change.
    1. [ ] Animate previous meter location fade.
1. [ ] What is falling off the CO2 bar? What is the effect?
    1. [ ] Nothing falls when CO2 meter empty.
    1. [ ] Animate falling from fill amount.
1. [ ] Select. Want selection box to go away.
    1. [ ] Selecting does not dismiss the options.
1. [ ] Tap away. Want next selection to appear. Have to touch twice.
    1. [ ] Tap selected. No response.
1. [ ] What are "services?"
    1. [ ] Is cheaper worse or better?
    1. [ ] Don't have money?
    1. [ ] Rename options to omit quantity:
        - Shop A Lot
        - Shop Little
        - Entertain Frequently
        - Entertain Rarely
1. [ ] I hear 2 sounds: pop and squish. I want feedback sound, good or bad.
    1. [ ] Hear only pop portion of sound.
1. Does grass to barren correlate to CO2 bar?
    1. Same color on CO2 fill as barren.
    1. Same color on CO2 background as grass.
1. Align places to an isometric grid.
1. Reproduction.
    1. Have Three Or More Children: Doubles every place in 30 years.
        1. Resets to default.
        1. After 80 years, toggler per capita disappears.
            1. Extract reproduction toggle to toggles per capita.
            1. [ ] Animate disappearance.
                1. [ ] Sprite of person at each place.
                1. [ ] Swap sprite for age of person.
                1. [ ] Animate person visiting each place.
            1. [ ] After last capita disappears, end game.
        1. Position Offsets.
        1. [ ] Find empty place for new spawns.
    1. No Children.
    1. Have Two Children.
        1. Represents repopulation once, because two children will replace two parents.
    1. Have Four Children.
1. Calibrate emission metrics to citations.
    1. Calibrate CO2 per capita to raise 4 C.
1. Win by surviving until 2099.
1. Ocean and plants absorb CO2.
    1. Animate CO2 emitted from meter down to ocean and plants.
        1. Emission subtracts from Carbon Dioxide in Troposphere.
1. Government
    1. United States
    1. India
1. Callout behind item.
1. Decorate as a town.
1. [ ] Celebrate reducing CO2.
    1. With change in CO2 meter, deal shuffled tiles.
        1. Green grass to yellow sand.
        1. [ ] Green plants to empty.
        1. [ ] Green trees to barren trees.
1. 8-bit font.
1. [ ] End screen: carbon footprint in net tons/year/capita.
1. [ ] Investments
    1. [ ] Stocks
    1. [ ] Tech Stocks
    1. [ ] Industrial Stocks
1. [ ] Occupation
    1. [ ] Goods $200K
    1. [ ] Gooods $20K
    1. [ ] Services $200K
    1. [ ] Services $20K
    1. [ ] Occupation in the town.
1. [ ] Levels.
    1. [ ] Introduce places at levels.
1. [ ] Lightbulbs
1. [ ] Water Use
1. [ ] Electronics
1. [ ] Power plant type affects goods/services CO2 emissions.
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
