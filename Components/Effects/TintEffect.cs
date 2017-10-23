using Xamarin.Forms;

namespace Components.Effects
{
    public class TintEffect : RoutingEffect
    {

        public Color TintColor { get; set; }

        public TintEffect() : base("GrowerApp.ImageEffects")
        {

        }
    }
}
