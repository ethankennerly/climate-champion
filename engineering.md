# Features

- Delta Time System
- Text Timer View
- Traveler
- Distance Emitter
    - Type
    - Rate Over Distance
- Timed Emitter
    - Type
    - Rate Over Time
- Prefab Toggle Opener
    - Open button
    - Spawned instance
    - List of prefabs
    - Selected index
- Prefab Toggle Group
    - Prefab Toggle Opener
    - Toggles

# TODO

- Traveler Scheduler
    - Calculates rotation, speed to arrive at destination after duration.
- Traveler
- Meter
    - Set Fill Position In World
        - Interpolates position between begin position and end position by fill amount.
    - Subscribes to emission enabled on traveler.
        - Schedules traveler to arrive at fill position after a duration.
- Pool carbon emission

# Postmortem

- Overengineered Distance Emitter.
    - Timed Emitter simpler than Distance Emitter.
    - Decoupling unnecessarily added complexity.
    - Also, since time converts into distance, could have used timed emitter instead of distance emitter.
