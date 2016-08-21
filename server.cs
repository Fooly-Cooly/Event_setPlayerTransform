//-----------------------------------------------------------------------------
// Event_setPlayerTransform - An event used to make teleporters.
//-----------------------------------------------------------------------------
// Rotondo
//	??/??/???? - Original Release
// Fooly Cooly
//	07/08/2014 - Rewrote (Added Timer to prevent infinit teleport) & optimized 
//	06/05/2016 - Changed getTransform to getPosition and commented
//-----------------------------------------------------------------------------

function fxDTSBrick::setPlayerTransform(%obj, %dir, %oVel, %cl)
{
	//--Reset Timer-------------------------
	%pl = %cl.player;
	if(getTimeRemaining(%pl.transSchedule))
	{
		cancel(%pl.transSchedule);
		%pl.transSchedule = schedule(1000, 0, '');
		return;
	}

	//--Set Player Direction---------------------
	switch (%dir)
	{	
		case 0 :%rot = getwords(%pl.getTransform(), 3, 6);
		case 1 :%rot = "1 0 0 0";
		case 2 :%rot = "0 0 1 1.57079";
		case 3 :%rot = "0 0 1 3.14159";
		case 4 :%rot = "0 0 1 -1.57079";
	}
	
	//--Scale Destination Z Coordinate
	%pos = %obj.getPosition();
	%z = 0.1 * %obj.getdatablock().bricksizez; 
	%pos = vectorAdd(%pos, "0 0" SPC %z);

	//--Apply New Player Transform---------------
	%trans = %pos SPC %rot;
	%pl.setTransform(%trans);
	%pl.transSchedule = schedule(1000, 0, '');
	
	//--Kill Momentum----------------------------
	if(%oVel == 0)
		%pl.setVelocity("0 0 0");
}

registerOutputEvent("fxDTSBrick", "setPlayerTransform","list Player 0 North 1 East 2 South 3 West 4\tbool", 1);