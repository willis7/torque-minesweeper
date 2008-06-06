//Used to transmit static data from server to client
datablock ItemData( SuperBomb )
{   
    category = "Bombs";
    shapeFile = "~/data/shapes/superbomb/superbomb.dts";
};

function ItemData::create( %data )
{
	echo( "ItemData::create for SuperBomb called --------------------------" );

	%obj = new Item()
	{
		dataBlock = %data;
		rotate = true; // All Super Bomb power-ups will rotate.
		static = true; // Super Bombs should stay put so they don't slide away.
	};

	return %obj;
}

function SuperBomb::onCollision( %this, %obj, %col )
{
    //This tests all collisions worked correctly
    echo( "SuperBomb::onCollision called ----------------------------------" );

        //Checks collision with player
	if(%col.getClassName() $= "Player")
	{
	%client = %col.client;
	%obj.delete();
        //Gets the number of bombs in the simgroup
	%scoreCount = Bombs.getCount();
        //Update GUI for the user
	commandToClient(%client, 'SetBombCounter', %scoreCount);
	if(%scoreCount > 0)
	return;
	// otherwise display victory screen
	commandToClient(%client, 'ShowVictory');
	}
}
