![alt text](https://github.com/iriskotsinas/TDDD23_2D_game/blob/master/freeyourmind.png)
# Introduction
This is a 2D platformer game made for the course TDDD23 that challenges your creativity and gives you a freedom to play the game however you want to play it. It starts with a player waking up next to a magic(AI) book and not remembering anything. He, together with this book, goes on an adventure to find out more. The book is made using DoodleNet which is a pretrained model that uses "Googles Quick Draw!" dataset to train the model.

**There are three ways to play this game:**
1. Draw your way through it. Draw items and spawn
2. Say the objects name (if you give microphone-permission to the game)
3. Write the objects name

Since we say *"A picture is worth a thousand words"*, we made that drawing objects makes them more powerful. For weapons, they deal more damage, for vehicles they are faster and so on.

# Quick Start
Clone the repository
```bash 
git clone https://github.com/iriskotsinas/TDDD23_2D_game.git
```
Start the API that controls the books prediction
```bash 
cd TDDD23_2D_game/API
node server2.js
```
Then start the Unity Project and build the game/play in the editor.

# Prerequisites
Unity 2020.03.18.f1

Node
# API
API is built with node and express and uses Tensorflow for node.
# Game & Controls
**Q:** Open Canvas to draw

**E:** Pickup items

**R:** Open textbox to write

**WASD + Space:** Movement + Jump
