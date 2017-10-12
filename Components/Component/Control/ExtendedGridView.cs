using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Components.Component.Control
{
    public class ExtendedGridView : Grid
    {
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<ExtendedGridView, object>(p => p.CommandParameter, null);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<ExtendedGridView, ICommand>(p => p.Command, null);
        private int _maxColumns = 2;
        private int _maxrows = 1;

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(object), typeof(ExtendedGridView), null, BindingMode.Default, null, (bindable, oldvalue, newvalue) => { OnItemsSourceProperyChanged(bindable, oldvalue, newvalue); }, null, null, null);

        private static async void OnItemsSourceProperyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue != null)
            {
                var parent = bindable as ExtendedGridView;
                if (parent != null) await parent.BuildTiles();
            }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<ExtendedGridView, DataTemplate>(p => p.ItemTemplate, null);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public object ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public ExtendedGridView()
        {
            for (var i = 0; i < MaxColumns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set { _maxColumns = value; }
        }

        public int MaxRows
        {
            get { return _maxrows; }
            set { _maxrows = value; }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public async Task BuildTiles()
        {
            // Wipe out the previous row definitions if they're there.
            if (RowDefinitions.Any())
            {
                RowDefinitions.Clear();
            }
            var decType = ItemsSource.GetType().GetGenericTypeDefinition();
            var enumerable = ItemsSource as IList;
            var numberOfRows = Math.Ceiling(enumerable.Count / (float)MaxColumns);                        

            if (MaxRows > 1)
            {
                for (var i = 0; i < numberOfRows; i++)
                {
                    RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
                }
            }
            else
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (var index = 0; index < enumerable.Count; index++)
            {
                var column = index % MaxColumns;
                if (index >= MaxColumns && MaxRows <= 1)
                    break;
                var row = MaxRows > 1 ? (int)Math.Floor(index / (float)MaxColumns) : 1;

                var tile = await BuildTile(enumerable[index]);
                var dummyHeight = tile.Height;

                Children.Add(tile, column, row);
            }
            InvalidateLayout();
        }

        protected override bool ShouldInvalidateOnChildAdded(View child)
        {
            return false;
        }

        private async Task<Xamarin.Forms.View> BuildTile(object item1)
        {
            return await Task.Run(() =>
            {
                Xamarin.Forms.View buildTile = (Xamarin.Forms.View)ItemTemplate.CreateContent();
                buildTile.BindingContext = item1;
                var tapGestureRecognizer = new TapGestureRecognizer
                {
                    Command = Command,
                    CommandParameter = item1
                };

                buildTile.GestureRecognizers.Add(tapGestureRecognizer);
                return buildTile;
            });
        }
    }
}
