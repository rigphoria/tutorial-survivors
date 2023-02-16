using Godot;
using System;

// to use godot's node system we have to make a class that
// inherits from the type we want to use. in this case
// CharacterBody2D is what we want to use because it has
// built-in functions to help with, well, characters
public partial class Player : CharacterBody2D{
	//exports are basically a way to expose a variable inside
	//of the engine. when we click on our player class we'll be
	//able to see moveSpeed in the inspector and set it to whatever
	//value we decide without having to change this code and recompile
	[Export()] public float moveSpeed = 5000.0f;
	
	//Vector2 is just X and Y variables wrapped up together. it can
	//be used for any pair of number values. here we're just using
	//it for movement values because in 2d X and Y are the two 
	//movement axes
	private Vector2 movement;

	//_Ready() is a built-in method for all godot scripts. 
	//public variables and methods will be available to other
	//scripts. there's also private ones that can only be used
	//inside of the current class
	//override is a special keyword that tells the engine we're 
	//going to implement our own logic inside of the method instead
	//of the default stuff it wants to do
	//void is the return type. methods can return a value if they
	//so choose. because this method gets called automatically we
	//don't want a return type. void means there's no return type
	public override void _Ready(){
		movement = new Vector2();
	}
	
	//_Process(double delta) is another built-in method for godot
	//scripts. it gets called frequently; up to multiple times per
	//second. anything we want to do at least every frame can go here
	//which is why it's great for movement code. 
	//delta is the amount of time that has lapsed since the last time
	//_Process() got called
	public override void _Process(double delta){
		//we're zeroing out the movement speed in case the player stops
		//pressing any movement input actions. without this the player
		//character never stops
		movement = Vector2.Zero;
		
		//we're polling the engine for input events. actions are
		//user-defined events that are mapped to a key. it's easier to
		//tell the engine to do action "jump" than it is to poll directly
		//for spacebar or whatever key
		//we're adding or subtracting 1 to the movement axes. 1 isn't
		//the direct movement speed, we're just indicating the direction
		//of movement for the moment
		if (Input.IsActionPressed("up")) movement.Y -= 1;
		if (Input.IsActionPressed("down")) movement.Y += 1;
		if (Input.IsActionPressed("left")) movement.X -= 1;
		if (Input.IsActionPressed("right")) movement.X += 1;

		//Velocity is a special variable inside of CharacterBody2D that
		//will get used for movement.
		//multiplying movement, any kind of movement, by delta is a way
		//of making games feel less terrible when the framerates are low.
		//then we're multiplying that all by moveSpeed, which is our
		//chosen value for how fast the character should move
		Velocity = movement * (float)delta * moveSpeed;

		//MoveAndSlide() is another special method inside of CharacterBody2D.
		//it uses Velocity to move the character in that direction
		MoveAndSlide();
	}
}
