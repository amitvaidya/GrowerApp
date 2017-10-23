using System.Linq;
using Android.Widget;
using Components.Effects;
using GrowerApp.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("GrowerApp")]
[assembly: ExportEffect(typeof(ImageEffects), "ImageEffects")]
namespace GrowerApp.Droid.Effects
{
    public class ImageEffects : PlatformEffect
    {
        protected override void OnAttached()
        {
            var imageView = Control as ImageView;
            var tintEffect = (TintEffect)Element.Effects.FirstOrDefault(row => row is TintEffect);
            if (tintEffect != null)
            {
                imageView?.SetColorFilter(tintEffect.TintColor.ToAndroid());
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
