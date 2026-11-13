# Notes - (your project's name)
11/13:
State Machines


## The problem, in my words

The problem is that when the goblin attacks you, you have to attack it back or flee; 
you shouldn't just be able to go to a different place. 
Today, when I type attack goblin, it just says "I don't know how to attack." 
So, even if it attacked you, you wouldn't be able to attack it back. 
You have to add a state machine to be able to track "in a fight" 
to be able to have the loop that is "playing" as well as be able to attack in a state.

## Sources

The loop in Program.cs, then Engine/CommandParser.cs and Engine/Commands.cs.  
Rules/CombatRules.cs has the numbers a fight needs:  
ArmorClass and AttackBonus, for players and creatures both.
Game Programming Patterns, by Robert Nystrom, the chapter on State:  
gameprogrammingpatterns.com/state.htmlLinks to an external site. 
Written for game programmers, and free.  It opens with a bug you'll recognize.
Microsoft's page on records, especially the part on inheritance:  
Records - C# referenceLinks to an external site.
Good search terms, once those run out:  finite state machine, 
state pattern, state transition diagram, game loop.

## Questions to Research
Answer these in your notes, in your own words.  They're in roughly the order you'll need them.

Understanding the idea
What is a finite state machine?  What are its parts?  
Draw one for something that isn't a game:  
a traffic light, a vending machine, a door with a lock.
Nystrom opens with a pile of bool flags that keep growing.  Why do they go wrong?  
Our loop has one bool already.  What will happen to it as the game grows?
What's the difference between a state machine, the idea, 
and the State pattern, one way to write one?
Reading our code
Draw our game as a state diagram:  every state, and every arrow between them,
labeled with what makes it happen.  Start with what the game does today, then add Combat.
Which of today's commands should work in a fight?  Which shouldn't?  
For the ones that shouldn't, what should the game say?
Designing it
What does a state know?  What does it decide?
The big one.  Who decides when the state changes?  
The loop, or the state itself? Sketch it both ways.  
What does the loop look like when it doesn't need to know which state it's in?
What does Combat need to remember?  Who you're fighting, surely.  What else?  
Where you go back to when it's over?
A fight needs dice.  Where does the Random come from?  
Think back to Chapter 6, and the one Random at the top of Program.cs.
How do you get into a fight?  How do you get out?  Is there more than one way, each way?
What replaces the playing flag?
For Made It Mine.  A hit should hurt.  Where does the creature's damage live?  
There are two places it could go: in the Combat state, 
which remembers how hurt the creature is for as long as the fight lasts, 
or in the creature itself, stored back in the GameData.  
HealthPoints has no way to go down today.  
What happens to the damage, each way, when you flee and come back?

## Experiments

Do these before you design anything, and paste exactly what happens into your notes.

State Machine:
- Exploration
- Combat

11/13 class:
== Kyroki ==  level 1, 0 XP
HP 6/6    Gold 10
STR 13 (+1)   DEX 13 (+1)   CON 17 (+3)
INT 11 (+0)   WIS 13 (+1)   CHA  9 (+0)

Commands:  look, look <item>, go <place>, quit

You are in Clearing.
A patch of sunlight at the edge of the forest.  Someone camped here, not long ago.
You see the following items:
- Statue
- Lantern
- Locket
- Health Potion
- Chest
  From here you can go to:
- Forest

After Typing Commands:
/Users/haileyduan/Documents/GitHub/TheWorld_2026/bin/Debug/net10.0/TheWorld_2026
== Kyroki ==  level 1, 0 XP
HP 10/10    Gold 11
STR 15 (+2)   DEX 17 (+3)   CON  8 (-1)
INT  8 (-1)   WIS 13 (+1)   CHA 11 (+0)

Commands:  look, look <item>, go <place>, quit

You are in Clearing.
A patch of sunlight at the edge of the forest.  Someone camped here, not long ago.
You see the following items:
- Statue
- Lantern
- Locket
- Health Potion
- Chest
  From here you can go to:
- Forest

> go forest
You are in Forest.
A dark forest.  It has a lot of trees.
You see the following creatures:
- Goblin
- Goblin
  You see the following items:
- Tree
- Tree
  From here you can go to:
- Cave
- Clearing

> look
You are in Forest.
A dark forest.  It has a lot of trees.
You see the following creatures:
- Goblin
- Goblin
  You see the following items:
- Tree
- Tree
  From here you can go to:
- Cave
- Clearing

Next I tried doing the command "attack":
I expected that it would be able to attack. However it says the following:
> attack goblin
I don't know how to "attack".


## Decisions

A root type, GameState, with Exploration and Combat under it, 
and a way for the game to end that replaces the playing flag.
The game starts in Exploration, and everything that works today still works there.
The loop asks the current state what to do.  It doesn't check which state it's in.
attack <creature>, in an area where that creature is, starts a fight.
In Combat, attack rolls a d20 plus your AttackBonus against the creature's ArmorClass, and says whether you hit.  flee goes back to Exploration.  go gets refused, with a message that makes sense.
Your notes file, with the answers to the questions above, your state diagram, 
your sources, and your decisions.

## Finished means three things
The code is on your branch, pushed, and green, and it does everything in What you're building.
The notes have the questions answered in your words, your state diagram, your sources, 
and every decision you made with what it cost.
The presentation is given.  See The Presentation page:  
fifteen minutes, and one part of it is adding a new state, live, in front of us.

## Questions for the others

Commands as Types decides what a command is, and you decide which commands are available.  
You're both changing the loop in Program.cs.
Talk to each other in the first class, and agree on what the loop hands to a state.
- The loop hands the commands to a state. For example, the command "look" would be available 
- if the state is in the initial state, which is "Exploration" since it wouldn't make sense
- and thus the command wouldn't be available if you were in a different state like Combat
- because then there would nothing to look at. 

Items That Do Things might want use to work in a fight.  A health potion would.
- Yes, a health potion would want to be used in a fight because if the state was "Combat",
- it would be useful if you could use the item, "health potion", if you
- wanted to heal yourself.

Saving and Loading might want to save which state the game is in.  
Can you save in the middle of a fight?
- Yes, you should be able to save in the middle of a fight because
- you would want to save your data so that if you left the game
- you would still be able to have your past data and be able to pick
- off from where you left off of. Furthermore, you would also want to be able to
- save which state you are in because if you were in a fight and 
- were in the state "Combat", you would want to be able to save so that
- you could come and pick up where you left off of in that state, so you
- would not have to restart from the "Exploration" state.


