# Top-down 2-D shooter feature list

### Player

    Player moves around the screen using 'wasd'
    Aims bullets with mouse
    Shoots on click
        - Can hold the mouse to continuously shoot

### Enemies

    Spawn from 4 points around the edge of an arena
        - North, South, East, West
    Enemies will move towards the player and kill the player when they touch

### Game is done in rounds

    Each round becomes increasingly difficult
        Difficulty is increased through more enemies on screen at a time
    The player starts with 3 lives which are lost whenever they die
        After a death the round is reset 

### High Scores

    The score is totalled and displayed as the game goes on
        - Each enemy is worth a number of points
        - When the player runs out of lives the game ends and their high score is stored somewhere (e.g. txt, CSV, JSON)
        - Name for the score is asked for at the game over screen

## EXTRA

### Animation

    Animated sprites
    Particle effects
    Lighting
    Shadows maybe 

### Can collect power-ups from killing enemies

    Power-ups
        - Speed up
        - Increased fire rate
        - Triple shot
        - Power-ups can stack

### At the end of 5 rounds there is a boss

    3 boss types
        - Boss is randomly chosen
    Boss types will have different fighting styles

### Different kinds of enemies

    Types of enemy
    - Faster moving enemies with less health
    - Slower moving enemies but with more health
    - Ranged enemies who will try to stay at a distance
    - Regular enemies who will simply walk to the player at a 'normal' speed with a 'normal' amount of health

### Two players

    Done by having two player entities on screen with no friendly fire
    - One player mode and two-player mode
    - Options are selected on the main menu

### Sound Effects

    Game should play a sound for
        Bullets fired
        Enemies killed
        Power-Ups collected
