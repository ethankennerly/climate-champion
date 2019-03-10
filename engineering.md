# Features

- Delta Time System
- Text Timer View
- Traveler
    - Traveler Scheduler
        - Calculates rotation, speed to arrive at destination after duration.
- Distance Emitter
    - Type
    - Rate Over Distance
- Timed Emitter
    - Type
    - Rate Over Time
- Emitter Path
    - Traveler
    - Timed Emitter Package
    - Path
        - Destination Object
        - Emission
- Prefab Toggle Opener
    - Open button
    - Spawned instance
    - List of prefabs
    - Selected index
- Prefab Toggle Group
    - Prefab Toggle Opener
    - Toggles
- Meter
    - Set Fill Position In World
        - Interpolates position between begin position and end position by fill amount.
    - Subscribes to emission enabled on traveler.
        - Schedules traveler to arrive at fill position after a duration.

# TODO

- Pool carbon emission

# Postmortem

- Overengineered Distance Emitter.
    - Timed Emitter simpler than Distance Emitter.
    - Decoupling unnecessarily added complexity.
    - Also, since time converts into distance, could have used timed emitter instead of distance emitter.
