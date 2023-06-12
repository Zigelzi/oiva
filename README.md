# Oiva - Clean the scooters

Oiva is game about finding electrical scooters parked in all odd places and bringing them back to the correct place.

## Player experience (MDA)

Player feels like they're constantly finding scooters in surprising and exhilarating places in a cheerful and playful world. Player feels that they're playing as friendly local person who just wants some order in the world.

Primary aesthetics are discovery and challenge.

### Core assumptions

1. Searching and finding scooters create experience of discovery
2. Searching scooters create experience of challenge
3. Players like varying areas and puzzles

## Core gameplay

**Player goal:** Discover all the scooters and deliver them to the parking spot

**Core gameplay loop:**

1. Level starts with X number of scooters
2. Player searches for scooter
3. Player finds the scooter
4. Player parks the scooter
5. Repeat

### Level progression

How mechanics are introduced to the player.

1. You can pick up scooters and you need to deliver them to parking spot
2. There can be multiple scooters that you need to deliver
3. Scooters can be hidden to hideouts
4. Hideouts can look different
5. Scooters can be hidden out of sight

## TODO

### Gameplay

- [x] Player can't exit the play area
- [x] Player can pick up a scooter
- [x] Player can move with the scooter
- [x] Player can deliver the scooter
- [x] Camera follows the player
- [x] Player needs to deliver multiple scooters to finish the level
- [ ] Player can get visual reward when delivering scooter
- [ ] Scooters aren't positioned correctly when carried

### Challenge

- [x] Player consumes energy when moving
- [ ] Player consumes additional energy when carrying scooter

### Discovery & surprise

- [x] Player can get hint of hideout
- [ ] Scooters fly out from the hideout
- [ ] Player can zoom out to view the big picture
- [ ] Player can zoom in to look for the details
- [ ] Misc items fly from hideout when searched
- [ ] Powerups fly from hideout when searched
- [ ] Player gets buff after delivering scooter
  - [ ] Speed boost
  - [ ] Additional carry capacity
  - [ ] Hint
  - [ ] Restore energy
- [ ] Player can interact with misc objects
- [ ] Player can see indicator displaying proximity to scooters

### Scenes

- [x] Player can advance to next level when successfully parking all scooters
- [ ] Success scene is displyed after a short delay
- [ ] Create level introducing the energy mechanic
- [ ] Create level introducing hideout mechanic properly

### Bugs

- [x] Player slides sometimes when moving
- [x] All scooters are placed into the same position
- [x] Player can carry multiple scooter at once
- [x] Player slides in the beginning of second level in built version
- [ ]
