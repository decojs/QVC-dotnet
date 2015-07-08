using System.ComponentModel.DataAnnotations;
using Qvc.Executables;

namespace Tests.Executables
{
    class CommandA : ICommand { }

    class CommandB : ICommand { }

    class CommandC : ICommand { }

    class QueryA : IQuery
    {
        [Required]
        public string Name { get; set; }
    }

    class QueryB : IQuery { }

    class QueryC : IQuery { }

    class CommandFullTest : ICommand { }

    class QueryFullTest : IQuery { }

    class TestCommand : ICommand { }
}
