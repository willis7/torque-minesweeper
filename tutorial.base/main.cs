//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load up common script base
loadDir("common");

//-----------------------------------------------------------------------------

// Load up default console values.
exec("./client/defaults.cs");
exec("./server/defaults.cs");

// Preferences (overide defaults)
exec("./prefs.cs");


//-----------------------------------------------------------------------------
// Package overrides to initialize the game
package ttb {

function onStart()
{
   // Initialize the client and the server
   Parent::onStart();
   initServer();
   initClient();
   $Editor::newMissionOverride = "tutorial.base/data/missions/flat.mis";
}

function onExit()
{
   // Save off our current preferences for next time
   echo("Exporting prefs");
   export("$Pref::*", "./prefs.cs", False);
   Parent::onExit();
}

}; // Client package
activatePackage(ttb);


//-----------------------------------------------------------------------------

function initServer()
{
   echo("\n--------- Initializing TTB: Server ---------");

   // The common module provides the basic server functionality
   initBaseServer();

   // Load up game server support scripts
   exec("./server/game.cs");
}


//-----------------------------------------------------------------------------

function initClient()
{
   echo("\n--------- Initializing TTB: Client ---------");

   // The common module provides basic client functionality
   initBaseClient();

   // InitCanvas starts up the graphics system.
   // The canvas needs to be constructed before the gui scripts are
   // run because many of the controls assume the canvas exists at
   // load time.
   initCanvas("Torque Tutorial Base");

   // Load client-side Audio Profiles/Descriptions
   exec("./client/audioProfiles.cs");

   // Load up the shell and game GUIs
   exec("./client/ui/PlayGui.gui");
   exec("./client/ui/mainMenuGui.gui");
   exec("./client/ui/optionsDlg.gui");
   exec("./client/ui/loadingGui.gui");

   // Client scripts
   exec("./client/optionsDlg.cs");
   exec("./client/missionDownload.cs");
   exec("./client/serverConnection.cs");
   exec("./client/loadingGui.cs");
   exec("./client/playGui.cs");

   // Default player key bindings
   exec("./client/default.bind.cs");

   exec("./client/clientGame.cs");

   // Copy saved script prefs into C++ code.
   setShadowDetailLevel( $pref::shadows );
   setDefaultFov( $pref::Player::defaultFov );
   setZoomSpeed( $pref::Player::zoomSpeed );

   // Start up the main menu...
   Canvas.setContent(MainMenuGui);
   Canvas.setCursor("DefaultCursor");
}


//-----------------------------------------------------------------------------
// LOAD MY MISSION

function loadMyMission()
{
   // make sure we are not connected to a server already
   disconnect();
   
   // Create the server and load the mission
   createServer("SinglePlayer", expandFilename("./data/missions/flat.mis"));

   // Make a local connection
   %conn = new GameConnection(ServerConnection);
   RootGroup.add(ServerConnection);
   %conn.setConnectArgs("Player");
   %conn.setJoinPassword("None");
   %conn.connectLocal();
}
