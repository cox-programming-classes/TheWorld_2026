The problem in my words:

items can be looked at right now
thats about it. 
but if you play a game, any game, you have to do things with items 
and interact with them in a more complicated way than just 
hearing what they are

SO: 
We need to code ways to interact with items that can be mutual 
across different items in different classes without just labeling
them as items categorized by what you can do while 'using' them. 


**What is an interface in C#?**  
In C# an interface is a way to hold the list that tells you what methods, properties, events, and indexers a type must implement--which can be detailed, defined, and instructed within the type itself.

**What does it promise, and what does it leave to the type that implements it?**
It promises that the type has a way to implement that method or property but it leaves it to the type to give the actual instructions of how.

**What's the difference between saying a lantern is an item and saying a lantern can be carried?  Which of those is inheritance for, and which is an interface for?**
The inheritance tells one that the lantern is an item--> it says the type. 
The interface tells one that a lantern can be carried. Eg. for a lantern: inheritance = item; interface = ICarryAble

**How many types can a C# class or record inherit from? How many interfaces can it implement?  Find the reason the language chose that, and write it down.**
there is only a singular base class that a class can inherit but it can implement multiple interfaces within the class mentioned and the base class. if a class was able to inherit more than one class, it would have two different sets of instructions for the one method inherited. 

**Sort every item in StockItems.cs into a table:  carryable or not, usable or not.  Which ones were hard to place, and why?  The Brass Key, the Chest, and the Wanted Poster are there on purpose.**
When sorting items from StockItems into categories of usable and carryable, the brass key can only be used ON something else so the 'usable' was questionable and will need to be specifies
chest was questionable because using it means opening it up to a bunch more items. the carryable question is thinking about physical logistic and carrying weight--maybe this could depend on character game data stats
wanted poster--you can read it and maybe should be able to take it down?

**Where in our game would the difference show up?  Which commands need to know whether an item can be carried?  Which need to know whether it can be used?**
This affects how the player commands 'take' or carry and 'use'--which needs to know whether an item can be used. 

**Draw it with child classes first.  On paper, make CarryableItem and UsableItem, both children of Item.  Where does the Lantern go?  Now add a third kind, something you can read, like the Wanted Poster.  How many classes do you need?  Then draw it again with interfaces, and count again.**
if you were to do this with child classes, you would need a greater amount of classes due to the singular liimit of class inheritance but with interfaces there is a way to do this with less classes because they can share ICarryable and IUsable and more without worrying about dual sets of instructions. -- inheritances onky tell you which items have to be able to implement smth, not what is implemented

**What does ICarryable need to promise?  Does it need any members at all?  What about IUsable:  what does using something need to know about the world, and what does it hand back?**
ICarryable and IUsable promises that the item can implement it but it does not define or state exactly how/what the item implements

**How does take find out whether the thing you named can be taken?  Look up the is keyword in the pattern-matching docs.**
take finds out if you can take it by looking at whether the item has a method for how to carry it and if it is ICarryable

**Taking an item changes the area it was in.  In Chapter 8, how did a stored area change?**
A stored area changed by making a copy of the area so you could edit it and there would have to be a new ID instead of attempting to edit the original area

**Where does an item go when you pick it up?  The Player isn't in the GameData yet.  Decide where carried things live, and defend it.**
carried things should live in an inventory because it makes sense for a player to be able to carry multiple things at once and not constantly putting one down. This would also come into play with the inventory needed to fight in the combat state machine. 

**ItemBuilder.Build() always makes a plain Item.  How does a Lantern get built, and filed in the GameData?  Before you decide, read how WorldBuilder files the stock items.  Then make StockItems.Lantern hand back a Lantern, and check whether the lantern in the clearing is one.  Write down what you find, and why.**
Item builder just copies the name and the description but it does not copy the ID. Lantern gets built in its own record from a name and description but it has its own methods. the lantern would get filed in the gamedata using Update gamedata and replace something already stored. 

**Using the lantern should light it.  But a Lantern is a record, and it can't change.  In Chapter 8, how did a stored thing change?**