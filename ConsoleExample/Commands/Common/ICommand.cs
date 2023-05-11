namespace ConsoleExample.Commands.Common
{
    public interface ICommand
    {
        string CommandName { get; }
        void Execute();
    }
}