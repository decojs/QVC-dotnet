using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleCommand<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}