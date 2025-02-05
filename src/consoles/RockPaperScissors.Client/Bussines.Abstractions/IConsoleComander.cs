namespace RockPaperScissors.Client.Bussines.Abstractions;
public interface IConsoleComander {
    Task SetCommand(string command);
    void ShowCommands ();
}
