using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleCommand<in TCommand> : IHandleExecutable where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}