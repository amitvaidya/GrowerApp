using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Components.Component.Control
{
    public class TabContentButton : Grid
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(TabContentButton), null, BindingMode.Default);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(TabContentButton), null, BindingMode.Default);

        public object CommandParameter
        {
            get
            {
                var obj = (object)GetValue(CommandParameterProperty);
                return obj;

            }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty SelectionCommandParameterProperty = BindableProperty.Create("SelectionCommandParameter", typeof(object), typeof(TabContentButton), null, BindingMode.Default);

        public object SelectionCommandParameter
        {
            get
            {
                var obj = (object)GetValue(SelectionCommandParameterProperty);
                return obj;

            }
            set { SetValue(SelectionCommandParameterProperty, value); }
        }

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.AnchorX = 0.48;
                    this.AnchorY = 0.48;
                    await this.ScaleTo(0.8, 50, Easing.Linear);
                    await Task.Delay(100);
                    await this.ScaleTo(1, 50, Easing.Linear);
                    if (Command != null)
                    {
                        Command.Execute(CommandParameter);
                    }
                });
            }
        }

        private ICommand SelectionCommand
        {
            get
            {
                return new Command(() =>
                {
                    var row = GetRow(this);
                    var column = GetColumn(this);
                    SetRow((SelectionCommandParameter as BoxView), row);
                    SetColumn((SelectionCommandParameter as BoxView), column);
                });
            }
        }

        public TabContentButton()
        {
            Initialize();
        }


        public void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = SelectionCommand,
                CommandParameter = SelectionCommandParameter
            });
            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = TransitionCommand
            });
        }

    }
}
