function clientCmdSetBombCounter(%score)
{
	BombCounter.setText("Bombs Remaining:" SPC %score);
}
function clientCmdShowVictory()
{
	MessageBoxOk("You Win!",
	"Congratulations you saved the world!!",	
	"quit();");
}
function clientCmdShowdefeat()
{
	MessageBoxOk("You Lose, Bombs went off!",
	"Better luck next time",
	"quit();");
}
